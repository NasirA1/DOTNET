using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace MVVMTestableDialog
{
  /// <summary>
  /// Interaction logic for ContactView.xaml
  /// </summary>
  public partial class ContactView : Window
  {
    private bool buttonWasClicked;


    public ContactView()
    {
      InitializeComponent();
      buttonWasClicked = false;
    }


    private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
      if (!buttonWasClicked)
      {
        var vm = DataContext as ContactViewModel;
        vm.CancelCommand.Execute();
      }
    }


    private void Button_Click(object sender, RoutedEventArgs e)
    {
      buttonWasClicked = true;
      Close();
    }
  }
}
