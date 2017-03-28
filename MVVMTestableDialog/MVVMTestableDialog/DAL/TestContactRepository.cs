using System.Collections.Generic;


namespace MVVMTestableDialog
{
  public class TestContactRepository : IContactRepository
  {
    public List<Contact> GetContacts()
    {
      return new List<Contact>() {
        new Contact("John Doe", "johndoe@gmail.com", 23),
        new Contact("Sultan", "sultankhan@yahoo.co.uk", 34),
        new Contact("Fahima Jana", "fahima_2312@msn.com", 85),
        new Contact("Abdul Qadir", "qadir555@hotmail.com", 23),
        new Contact("Jack London", "jacklond@mail.com", 23),
        new Contact("Mullah Bor Jan", "mullahborjan@akhunds.com", 23)
      };
    }
  }
}
