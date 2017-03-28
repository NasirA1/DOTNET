using System.Collections.Generic;


namespace MVVMTestableDialog
{
  public interface IContactRepository
  {
    List<Contact> GetContacts();
  }
}
