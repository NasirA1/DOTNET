using System.Windows;

namespace MVVMTestableDialog
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class ContactListView : Window
  {
    public ContactListView()
    {
      InitializeComponent();
    }


    private void ListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
      var vm = DataContext as ContactListViewModel;
      vm.EditSelectedItemCommand.Execute();
    }
  }
}
