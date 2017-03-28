using System.Collections.ObjectModel;
using Prism.Commands;
using Prism.Mvvm;


namespace MVVMTestableDialog
{
  public class ContactListViewModel : BindableBase
  {
    public ContactListViewModel(IContactRepository contactsRepo, IViewFactory viewFactory)
    {
      _contactsRepo = contactsRepo;
      _viewFactory = viewFactory;
    }


    public ObservableCollection<ContactViewModel> Contacts
    {
      get
      {
        if(_contacts == null)
        {
          _contacts = new ObservableCollection<ContactViewModel>();
          var contacts = _contactsRepo.GetContacts();
          foreach (var contact in contacts)
            _contacts.Add(new ContactViewModel(contact));
        }
        return _contacts;
      }
    }


    public ContactViewModel SelectedItem
    {
      get
      {
        return _selectedItem;
      }
      set
      {
        SetProperty(ref _selectedItem, value);
      }
    }


    private void EditSelectedItem()
    {
      if (SelectedItem == null) return;

      _viewFactory.EditContact(SelectedItem);
    }


    public DelegateCommand EditSelectedItemCommand
    {
      get
      {
        if (_editSelectedItemCommand == null)
          _editSelectedItemCommand = new DelegateCommand(EditSelectedItem);
        return _editSelectedItemCommand;
      }
    }


    private IContactRepository _contactsRepo;
    private ObservableCollection<ContactViewModel> _contacts;
    private ContactViewModel _selectedItem;
    private IViewFactory _viewFactory;
    private DelegateCommand _editSelectedItemCommand;
  }

}
