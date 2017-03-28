using System.ComponentModel;
using Moq;
using NUnit.Framework;


namespace MVVMTestableDialog.UnitTests
{


  [TestFixture]
  public class ContactViewModelTests
  {

    [Test]
    public void ContactViewModel_Getters_Return_Model_Field_Values()
    {
      var contact = new Contact();
      var contactVm = new ContactViewModel(contact);

      Assert.AreEqual(contact.Name, contactVm.Name);
      Assert.AreEqual(contact.Email, contactVm.Email);
      Assert.AreEqual(contact.Age, contactVm.Age);
    }


    [Test]
    public void ContactViewModel_SetName_Raises_PropertyChanged_Event()
    {
      var contact = new Contact();
      var contactVm = new ContactViewModel(contact);
      var mockHandler = Mock.Of<PropertyChangedEventHandler>();

      contactVm.PropertyChanged += mockHandler;
      contactVm.Name = "John Doe";
      contactVm.Name = "John Doe";

      contactVm.OKCommand.Execute();

      Mock.Get(mockHandler).Verify(handler =>
        handler(contactVm, It.Is<PropertyChangedEventArgs>(e => e.PropertyName == "Name"))
        , Times.Once);

      Assert.AreEqual("John Doe", contactVm.Name);
      contactVm.PropertyChanged -= mockHandler;
    }


    [Test]
    public void ContactViewModel_SetEmail_Raises_PropertyChanged_Event()
    {
      var contact = new Contact();
      var contactVm = new ContactViewModel(contact);
      var mockHandler = Mock.Of<PropertyChangedEventHandler>();

      contactVm.PropertyChanged += mockHandler;
      contactVm.Email = "johndoe@gmail.com";
      contactVm.Email = "johndoe@gmail.com";

      contactVm.OKCommand.Execute();

      Mock.Get(mockHandler).Verify(handler =>
        handler(contactVm, It.Is<PropertyChangedEventArgs>(e => e.PropertyName == "Email"))
        , Times.Once);

      Assert.AreEqual("johndoe@gmail.com", contactVm.Email);
      contactVm.PropertyChanged -= mockHandler;
    }


    [Test]
    public void ContactViewModel_InvalidModel_ErrorsAreReturnedFromInvalidModel()
    {
      var contact = new Contact();
      var contactVm = new ContactViewModel(contact);

      Assert.AreEqual(contact["Name"], contactVm["Name"]);
      Assert.AreEqual(contact["Email"], contactVm["Email"]);
      Assert.AreEqual(contact["Age"], contactVm["Age"]);
    }


    [Test]
    public void ContactViewModel_CancelCommand_OriginalModelRestored()
    {
      var contact = new Contact("John Doe", "johndoe@gmail.com", 34);
      var contactVm = new ContactViewModel(contact);
      var mockHandler = Mock.Of<PropertyChangedEventHandler>();
 
      contactVm.PropertyChanged += mockHandler;
      contactVm.Name = "Abdur Rahman Khan";
      contactVm.Email = "abdurrahmankhan420@msn.com";
      contactVm.Age = 65;

      contactVm.CancelCommand.Execute();

      Mock.Get(mockHandler).Verify(handler =>
        handler(contactVm, It.Is<PropertyChangedEventArgs>(e => e.PropertyName == "Name"))
        , Times.Never);

      Mock.Get(mockHandler).Verify(handler =>
        handler(contactVm, It.Is<PropertyChangedEventArgs>(e => e.PropertyName == "Email"))
        , Times.Never);

      Mock.Get(mockHandler).Verify(handler =>
        handler(contactVm, It.Is<PropertyChangedEventArgs>(e => e.PropertyName == "Age"))
        , Times.Never);

      contactVm.PropertyChanged -= mockHandler;

      Assert.AreEqual("John Doe", contactVm.Name);
      Assert.AreEqual("johndoe@gmail.com", contactVm.Email);
      Assert.AreEqual(34, contactVm.Age);
    }


    [Test]
    public void ContactViewModel_OKCommand_ModelIsUpdated()
    {
      var contact = new Contact("John Doe", "johndoe@gmail.com", 34);
      var contactVm = new ContactViewModel(contact);
      var mockHandler = Mock.Of<PropertyChangedEventHandler>();

      contactVm.PropertyChanged += mockHandler;
      contactVm.Name = "Abdur Rahman Khan";
      contactVm.Email = "abdurrahmankhan420@msn.com";
      contactVm.Age = 65;

      contactVm.OKCommand.Execute();

      Mock.Get(mockHandler).Verify(handler =>
        handler(contactVm, It.Is<PropertyChangedEventArgs>(e => e.PropertyName == "Name"))
        , Times.Once);

      Mock.Get(mockHandler).Verify(handler =>
        handler(contactVm, It.Is<PropertyChangedEventArgs>(e => e.PropertyName == "Email"))
        , Times.Once);

      Mock.Get(mockHandler).Verify(handler =>
        handler(contactVm, It.Is<PropertyChangedEventArgs>(e => e.PropertyName == "Age"))
        , Times.Once);

      contactVm.PropertyChanged -= mockHandler;

      Assert.AreEqual("Abdur Rahman Khan", contactVm.Name);
      Assert.AreEqual("abdurrahmankhan420@msn.com", contactVm.Email);
      Assert.AreEqual(65, contactVm.Age);
    }

  }


}
