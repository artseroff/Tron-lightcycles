using Model.Instructions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using View.Instructions;

namespace ViewWpf.Instructions
{
  /// <summary>
  /// Wpf представление инструкций
  /// </summary>
  public class ViewInstructionWpf : ViewInstructionBase, IWpfKeyPressableView
  {
    /// <summary>
    /// Метка с текстом инструкции
    /// </summary>
    private TextBlock _textBlock;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parInstruction">Модель инструкции</param>
    public ViewInstructionWpf(Instruction parInstruction) : base(parInstruction)
    {
      Init();
      Draw();
    }

    /// <summary>
    /// Инициализация
    /// </summary>
    public void Init()
    {
      Window window = SingletonWindow.Instance.Window;
      Panel parentControl = new StackPanel();

      parentControl.VerticalAlignment = VerticalAlignment.Center;
      parentControl.HorizontalAlignment = HorizontalAlignment.Center;
      _textBlock = new TextBlock
      {
        TextWrapping = TextWrapping.Wrap,
        Foreground = new SolidColorBrush(Colors.White),
        FontSize = 16,
        Margin = new Thickness(15)
      };

      parentControl.Children.Add(_textBlock);
      window.Content = parentControl;
      
    }

    /// <summary>
    /// Нарисовать представление класса
    /// </summary>
    public override void Draw()
    {
      _textBlock.Text = TextInstruction;
    }
  }
}
