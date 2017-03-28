using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MVVMTestableDialog
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {

    protected override void OnStartup(StartupEventArgs e)
    {
      base.OnStartup(e);

      var contactsRepo = new TestContactRepository();
      var viewFactory = new ViewFactory();

      var contactListVm = new ContactListViewModel(contactsRepo, viewFactory);
      var contactListView = new ContactListView();

      contactListView.DataContext = contactListVm;
      contactListView.Show();
    }

  }
}
