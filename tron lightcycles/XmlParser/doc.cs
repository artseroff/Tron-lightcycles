﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Xml.Serialization;

// 
// This source code was auto-generated by xsd, Version=4.0.30319.33440.
// 


/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
public class doc
{

  private object[] itemsField;

  /// <remarks/>
  [System.Xml.Serialization.XmlElementAttribute("assembly", typeof(docAssembly), Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
  [System.Xml.Serialization.XmlElementAttribute("members", typeof(docMembers), Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
  public object[] Items
  {
    get
    {
      return this.itemsField;
    }
    set
    {
      this.itemsField = value;
    }
  }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public class docAssembly
{

  private string nameField;

  /// <remarks/>
  [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
  public string name
  {
    get
    {
      return this.nameField;
    }
    set
    {
      this.nameField = value;
    }
  }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public class docMembers
{

  private docMembersMember[] memberField;

  /// <remarks/>
  [System.Xml.Serialization.XmlElementAttribute("member", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
  public docMembersMember[] member
  {
    get
    {
      return this.memberField;
    }
    set
    {
      this.memberField = value;
    }
  }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public class docMembersMember
{

  private string summaryField;

  private string returnsField;

  private docMembersMemberParam[] paramField;

  private string nameField;

  /// <remarks/>
  [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
  public string summary
  {
    get
    {
      return this.summaryField;
    }
    set
    {
      this.summaryField = value;
    }
  }

  /// <remarks/>
  [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
  public string returns
  {
    get
    {
      return this.returnsField;
    }
    set
    {
      this.returnsField = value;
    }
  }

  /// <remarks/>
  [System.Xml.Serialization.XmlElementAttribute("param", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true)]
  public docMembersMemberParam[] param
  {
    get
    {
      return this.paramField;
    }
    set
    {
      this.paramField = value;
    }
  }

  /// <remarks/>
  [System.Xml.Serialization.XmlAttributeAttribute()]
  public string name
  {
    get
    {
      return this.nameField;
    }
    set
    {
      this.nameField = value;
    }
  }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public class docMembersMemberParam
{

  private string nameField;

  private string valueField;

  /// <remarks/>
  [System.Xml.Serialization.XmlAttributeAttribute()]
  public string name
  {
    get
    {
      return this.nameField;
    }
    set
    {
      this.nameField = value;
    }
  }

  /// <remarks/>
  [System.Xml.Serialization.XmlTextAttribute()]
  public string Value
  {
    get
    {
      return this.valueField;
    }
    set
    {
      this.valueField = value;
    }
  }
}
