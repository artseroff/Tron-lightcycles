using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace ConsoleStarter
{
  /// <summary>
  /// Класс, который перегоняет хмл документацию
  /// в текст через разделитель
  /// </summary>
  public class XMLtoText
  {
    public static StringBuilder errors = new StringBuilder();

    public const char METHOD_COLUMN_DELIMETER = '&';

    /*public const char PARAM_DELIMETER = '%';

    public const char RETURN_DELIMETER = '~';

    public const char SUMMARY_DELIMETER = '`';*/
    public const char PARAM_DELIMETER = '%';

    public const char RETURN_DELIMETER = PARAM_DELIMETER;

    public const char SUMMARY_DELIMETER = PARAM_DELIMETER;

    public const char CLASS_COLUMN_DELIMETER = METHOD_COLUMN_DELIMETER;

    public class XmlClass
    {
      public string InfoEnumOrClass { get; set; }

      public string Name { get; set; }

      public string Namespace { get; set; }

      public string Summary { get; set; }

      public XmlClass(string infoEnumOrClass, string name, string @namespace, string summary)
      {
        InfoEnumOrClass = infoEnumOrClass;
        this.Name = name;
        Namespace = @namespace;
        Summary = summary;
      }

      public override string ToString()
      {
        Summary = Summary.Replace("\n            ", SUMMARY_DELIMETER.ToString());
        return $"{Namespace}{CLASS_COLUMN_DELIMETER}{InfoEnumOrClass} {Name}{CLASS_COLUMN_DELIMETER}{Summary}";
      }
    }

    public class XmlMethod
    {
      public string modifier { get; set; }
      public string methodName { get; set; }

      public string methodSummary { get; set; }

      public string methodClass { get; set; }

      public string returns { get; set; }

      public docMembersMemberParam[] parameters { get; set; }

      public XmlMethod(string modifier, string methodName, string methodSummary, string methodClass, string returns, docMembersMemberParam[] parameters)
      {
        this.modifier = modifier;
        this.methodName = methodName;
        this.methodSummary = methodSummary;
        this.methodClass = methodClass;
        this.returns = returns;
        this.parameters = parameters;
      }

      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();

        if (parameters != null)
        {
          //Убрать последнюю ')'
          int leftBracketInd = methodName.LastIndexOf('(') + 1;
          string tempBetweenBrackets = methodName[leftBracketInd..^1];
          methodName = methodName[0..leftBracketInd];
          string[] paramTypes = tempBetweenBrackets.Split(",");
          for (int i = 0; i < paramTypes.Length; i++)
          {
            
              paramTypes[i] = paramTypes[i].Replace("\n            ", RETURN_DELIMETER.ToString());
            //DeleteMenu
            //if ()
            {
              int s = 0;
            }
            // Список или дженерик
            if (paramTypes[i].Contains('{'))
            {
              //Убрать последнюю '}'
              int leftBracketFigureInd = paramTypes[i].LastIndexOf('{') + 1;
              int rightBracketFigureInd = paramTypes[i].LastIndexOf('}');
              string tempBetweenFigureBrackets = paramTypes[i][leftBracketFigureInd..rightBracketFigureInd];
              string tempBeforeFigureBrackets = paramTypes[i][0..(leftBracketFigureInd - 1)];
              tempBeforeFigureBrackets = GetTextFromLastCharsToEnd(tempBeforeFigureBrackets, '.');

              tempBetweenFigureBrackets = GetTextFromLastCharsToEnd(tempBetweenFigureBrackets, '.');

              string addOut = "";
              if (paramTypes[i].Contains("@"))
              {
                addOut = "out ";
              }
              paramTypes[i] = $"{addOut}{tempBeforeFigureBrackets}<{tempBetweenFigureBrackets}>";

              paramTypes[i] += $" {parameters[i].name}";

            }
            else
            {

              // Последняя точка
              int pointInd = paramTypes[i].LastIndexOf('.') + 1;
              // Делаем из System.String -> String
              string addOut = "";
              if (paramTypes[i].Contains("@"))
              {
                addOut = "out ";
              }
              paramTypes[i] = addOut + paramTypes[i][pointInd..paramTypes[i].Length];
              paramTypes[i] = paramTypes[i].Replace("@", "");
              paramTypes[i] += $" {parameters[i].name}";
            }

          }
          methodName += String.Join(", ", paramTypes) + ")";
        }
        else
        {
          methodName += "()";
        }
        methodSummary = methodSummary.Replace("\n            ", SUMMARY_DELIMETER.ToString());

        if (returns != null)
        {
          returns = returns.Replace("\n            ", RETURN_DELIMETER.ToString());
          methodSummary += ($"{RETURN_DELIMETER}Возврат:{RETURN_DELIMETER} {returns}");
        }

        string parametersString = "";
        if (parameters != null)
        {
          int counter = 0;
          foreach (var item in parameters)
          {

            if (counter == 0)
            {
              parametersString += $"{item.name} - {item.Value?.Replace("\n            ", PARAM_DELIMETER.ToString())}";
            }
            else
            {
              parametersString += $"{(PARAM_DELIMETER)}{item.name} - {item.Value?.Replace("\n            ", PARAM_DELIMETER.ToString())}";

            }
            counter++;
          }
        }
        stringBuilder.Append($"{modifier}{methodName}{METHOD_COLUMN_DELIMETER}{methodSummary}{METHOD_COLUMN_DELIMETER}{parametersString}");

        return stringBuilder.ToString();
      }
    }

    public static string GetTextFromLastCharsToEnd(string text, char parChar)
    {
      int charInd = text.LastIndexOf(parChar) + 1;
      // Делаем из System.String -> String
      return text[charInd..text.Length];
    }

    public static bool Overrides(MethodInfo baseMethod, Type type)
    {
      if (baseMethod == null)
        throw new ArgumentNullException("baseMethod");
      if (type == null)
        throw new ArgumentNullException("type");
      /*if (!type.IsSubclassOf(baseMethod.ReflectedType))
        throw new ArgumentException(string.Format("Type must be subtype of {0}", baseMethod.DeclaringType));*/
      while (type != baseMethod.ReflectedType)
      {
        MethodInfo[] methods = type.GetMethods(BindingFlags.Instance |
                                    BindingFlags.DeclaredOnly |
                                    BindingFlags.Public |
                                    BindingFlags.NonPublic);
        if (methods.Any(m => m.GetBaseDefinition() == baseMethod))
          return true;
        type = type.BaseType;
      }
      return false;
    }

    public static void Main()
    {


      XmlSerializer xmlSerializer = new XmlSerializer(typeof(doc));

      List<XmlMethod> xmlAttrs = new List<XmlMethod>();
      List<XmlClass> xmlClasses = new List<XmlClass>();

      doc doc;
      DirectoryInfo di = new DirectoryInfo(@"E:\вуз 4 курс 7 сем\кпо гит\Docs");
      FileInfo[] files = di.GetFiles("*.xml");
      foreach (var elFile in files)
      {
        using (FileStream fs = new FileStream(elFile.FullName, FileMode.Open))
        {
          doc = xmlSerializer.Deserialize(fs) as doc;
        }
        //в assembly название проекта
        //Поля
        docMembers docMembers = (docMembers)doc.Items[1];


        foreach (docMembersMember item in docMembers.member)
        {
          string methodName = item.name;
          string className = "";
          // М - метод и не конструктор
          if (methodName.StartsWith("M:") && !methodName.Contains("#ctor"))
          {

            if (methodName.EndsWith(')'))
            {
              docMembersMemberParam[] par = item.param;

              int leftBracketInd = methodName.LastIndexOf('(');
              string temp = methodName[0..leftBracketInd];

              // Последняя точка перед (
              int lastPointInd = temp.LastIndexOf('.');
              temp = methodName[0..lastPointInd];
              // Предпоследняя точка перед (
              int prevLastPointInd = temp.LastIndexOf('.');

              className = methodName[(prevLastPointInd + 1)..lastPointInd];
              methodName = methodName[(lastPointInd + 1)..methodName.Length];
            }
            else
            {
              // Последняя точка
              int pointInd = methodName.LastIndexOf('.');

              string temp = methodName[0..pointInd];
              // Предпоследняя точка
              int prevLastPointInd = temp.LastIndexOf('.');

              //methodName = methodName[(prevLastPointInd + 1)..methodName.Length];
              className = methodName[(prevLastPointInd + 1)..pointInd];
              methodName = methodName[(pointInd + 1)..methodName.Length];
            }


            className = $"{xmlClasses[^1].InfoEnumOrClass} {xmlClasses[^1].Namespace}.{xmlClasses[^1].Name}";

            // Определение модификатора
            string _namespace = xmlClasses[^1].Namespace;
            string classNameLocal = xmlClasses[^1].Name;
            int namespaceFirstIndexPoint = _namespace.Length;
            if (_namespace.IndexOf('.') != -1)
            {
              namespaceFirstIndexPoint = _namespace.IndexOf('.');
            }
            string methodNameWithoutBrackets = methodName;
            int posBracket = methodName.IndexOf('(');
            if (posBracket != -1)
            {

              methodNameWithoutBrackets = methodName[0..posBracket];
            }
            if (methodNameWithoutBrackets.Contains("#"))
            {
              int d = 0;
            }
            methodNameWithoutBrackets = GetTextFromLastCharsToEnd(methodNameWithoutBrackets, '#');
            Type type = Type.GetType($"{_namespace }.{classNameLocal}, {_namespace[0..namespaceFirstIndexPoint]}");
            string modifierString = "";
            
            if (type != null)
            {
              MethodInfo methodInfo = type.GetMethod(GetTextFromLastCharsToEnd(methodNameWithoutBrackets, '#'));
              // ищем протектед
              if (methodInfo == null)
              {
                methodInfo = type.GetMethod(GetTextFromLastCharsToEnd(methodNameWithoutBrackets, '#'), BindingFlags.Instance
                    | BindingFlags.Static
                    | BindingFlags.Public
                    | BindingFlags.NonPublic | BindingFlags.IgnoreReturn);
              }
              if (methodInfo == null)
              {
                methodInfo = type.GetMethod(GetTextFromLastCharsToEnd(methodNameWithoutBrackets, '#'), BindingFlags.Instance
                    | BindingFlags.DeclaredOnly | BindingFlags.IgnoreReturn);
              }
              if (methodInfo == null)
              {
                methodInfo = type.GetMethod(GetTextFromLastCharsToEnd(methodNameWithoutBrackets, '#'),
                    BindingFlags.NonPublic | BindingFlags.IgnoreReturn);
              }
              

              if (methodInfo == null)
              {
                methodInfo = new List<MethodInfo>(type.GetMethods(BindingFlags.Instance
                    | BindingFlags.Static
                    | BindingFlags.Public
                    | BindingFlags.NonPublic
                    | BindingFlags.IgnoreReturn | BindingFlags.DeclaredOnly)).Find(method => method.Name.Contains(methodNameWithoutBrackets));
                if (methodInfo == null)
                {
                  methodInfo = type.GetRuntimeMethods().ToList().Find(method => method.Name.Contains(methodNameWithoutBrackets));
                  modifierString = "НУЛЛ МЕТОД МОДИФИКАТОР ";
                  errors.Append($"НУЛЛ МЕТОД МОДИФИКАТОР{_namespace }.{classNameLocal}, method - {methodName}\n");

                  //continue;
                }
              }
              if (methodInfo != null)

              {
                if (methodInfo.IsFamily)
                {
                  modifierString = "protected ";
                }
                if (methodInfo.IsPrivate)
                {
                  modifierString = "private ";
                }
                if (methodInfo.IsPublic)
                {
                  modifierString = "public ";
                }
                if (methodInfo.IsStatic)
                {
                  modifierString += "static ";
                }
                if (methodInfo.IsAbstract)
                {
                  if (type.IsInterface)
                  {
                    int s = 0;
                  }
                  else
                  {

                    modifierString += "abstract ";
                  }
                }
                else
                if (methodInfo.IsVirtual)
                {
                  Regex regex = new Regex(@".*[.]I[A-Z].*");
                  if (type.IsInterface || regex.IsMatch(methodInfo.GetBaseDefinition().Name))
                  {
                    int s = 0;
                  }
                  else
                  {
                    if (type.Name.Contains("ConsoleControllerBase") && !type.Name.Contains("I"))
                    {
                      int s = 0;
                    }
                    List<Type> baseTypes = new List<Type>();
                    if (type.GetInterfaces() != null && type.GetInterfaces() != new Type[0])
                    {
                      baseTypes = new List<Type>(type.GetInterfaces());
                    }
                    if (type.BaseType != null)
                    {
                      baseTypes.Add(type.BaseType);
                    }
                    bool _override = false;
                    MethodInfo findMethodInfo = null;
                    foreach (var elType in baseTypes)
                    {

                      findMethodInfo = new List<MethodInfo>(elType.GetMethods(BindingFlags.Instance
                      | BindingFlags.Static
                      | BindingFlags.Public
                      | BindingFlags.NonPublic | BindingFlags.IgnoreReturn | BindingFlags.DeclaredOnly)).Find(method => method.Name.Contains(methodNameWithoutBrackets));


                     
                      
                      if (findMethodInfo != null )
                      {
                        _override = true;
                        break;

                      }
                    }
                    if (!_override && findMethodInfo != null && findMethodInfo.DeclaringType.IsInterface)
                    {
                      

                    }
                    else
                    {
                      if (!_override)
                      {
                        // Для интерфейсов проверка не подходит
                        _override = methodInfo.GetBaseDefinition().DeclaringType != methodInfo.DeclaringType;
                      }
                      if (_override)
                      {
                        modifierString += "override ";
                      }
                      else
                      {

                        modifierString += "virtual ";
                      }
                    }
                    
                  }

                }
 
                string returnType = GetTextFromLastCharsToEnd(methodInfo.ReturnType.Name, '.');
                if (returnType.Contains("List`1"))
                {
                  returnType = $"List<{methodInfo.ReturnType.GenericTypeArguments[0].Name}>";
                }
                modifierString += returnType.Replace("Void", "void") + " ";
              }

            }


            xmlAttrs.Add(new XmlMethod(modifierString, methodName, item.summary.Trim(), className, item.returns, item.param));

          }
          else
          // Тип, но не делегат
          if (methodName.StartsWith("T:") && !methodName.Contains(".d"))
          {

            // Последняя точка
            int pointInd = methodName.LastIndexOf('.');

            string temp = methodName[0..pointInd];
            // Предпоследняя точка
            int prevLastPointInd = temp.LastIndexOf('.');


            // Без T:
            string _namespace = methodName[2..pointInd];
            string classNameLocal = methodName[(pointInd + 1)..methodName.Length];

            // Определение того интерфейс или перечисление
            int namespaceFirstIndexPoint = _namespace.Length;
            if (_namespace.IndexOf('.') != -1)
            {
              namespaceFirstIndexPoint = _namespace.IndexOf('.');
            }

            string infoClassOrEnumOrInterface = "Класс";

            Type? type = Type.GetType($"{_namespace }.{classNameLocal}, {_namespace[0..namespaceFirstIndexPoint]}");

            if (type == null)
            {
              errors.Append($"НУЛЛ!!!{_namespace }.{classNameLocal}, {_namespace[0..namespaceFirstIndexPoint]}\n");
            }
            else
            {
              if (type.IsEnum)
              {
                infoClassOrEnumOrInterface = "Перечисление";
              }
              if (type.IsInterface)
              {
                infoClassOrEnumOrInterface = "Интерфейс";

              }
            }

            xmlClasses.Add(new XmlClass(infoClassOrEnumOrInterface, classNameLocal, _namespace, item.summary.Trim()));
          }



        }

      }
      StringBuilder stringBuilderMethods = new StringBuilder();
      string currentClass = "";
      foreach (var item in xmlAttrs)
      {
        if (currentClass != item.methodClass)
        {
          currentClass = item.methodClass;

          stringBuilderMethods.Append(currentClass).Append("\n");
        }
        stringBuilderMethods.Append(item).Append("\n");
      }
      //Console.Write(stringBuilderMethods.ToString());

      StringBuilder stringBuilderClasses = new StringBuilder();

      foreach (var item in xmlClasses)
      {
        stringBuilderClasses.Append(item).Append("\n");
      }
      //Console.Write(stringBuilderClasses);


      File.WriteAllText(@"E:\вуз 4 курс 7 сем\кпо гит\Methods.txt", stringBuilderMethods.ToString());
      File.WriteAllText(@"E:\вуз 4 курс 7 сем\кпо гит\Classes.txt", stringBuilderClasses.ToString());
      File.WriteAllText(@"E:\вуз 4 курс 7 сем\кпо гит\Errors.txt", errors.ToString());
    }
  }
}
