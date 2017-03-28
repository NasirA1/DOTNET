using System.ComponentModel;
using Moq;
using NUnit.Framework;



namespace MVVMTestableDialog.UnitTests
{

  [TestFixture]
  public class ContactListViewModelTests
  {

    [Test]
    public void ContactListViewModel_Contacts_Returns_ContactViewModels()
    {
      IContactRepository contactsRepo = new TestContactRepository();
      ContactListViewModel contactListVm = new ContactListViewModel(contactsRepo, null);
      Assert.AreEqual(6, contactListVm.Contacts.Count);
    }


    [Test]
    public void ContactListViewModel_SelectedItem_Selects_Given_Item_In_the_List()
    {
      IContactRepository contactsRepo = new TestContactRepository();
      ContactListViewModel contactListVm = new ContactListViewModel(contactsRepo, null);

      var mockHandler = Mock.Of<PropertyChangedEventHandler>();

      contactListVm.PropertyChanged += mockHandler;
      contactListVm.SelectedItem = contactListVm.Contacts[0];

      Mock.Get(mockHandler).Verify(handler =>
        handler(contactListVm, It.Is<PropertyChangedEventArgs>(e => e.PropertyName == "SelectedItem"))
        , Times.Once);

      Assert.AreEqual(contactListVm.Contacts[0], contactListVm.SelectedItem);
      contactListVm.PropertyChanged -= mockHandler;
    }


    [Test]
    public void ContactListViewModel_EditSelectedItemCommand_EditedItemIsSavedToList()
    {
      IContactRepository contactsRepo = new TestContactRepository();
      Mock<IViewFactory> mockViewFactory = new Mock<IViewFactory>();

      ContactListViewModel contactListVm = new ContactListViewModel(contactsRepo, mockViewFactory.Object);

      contactListVm.SelectedItem = contactListVm.Contacts[0];
      var editableObject = contactListVm.SelectedItem;

      //Mock OK command
      mockViewFactory.Setup(e => e.EditContact(editableObject)).Callback( () => {
        editableObject.BeginEdit();
        editableObject.Name = "Abdur Rahman Khan";
        editableObject.EndEdit();
      });
      
      contactListVm.EditSelectedItemCommand.Execute();

      //Verify that the view model has been updated
      mockViewFactory.Verify(t => t.EditContact(editableObject));
      Assert.AreEqual("Abdur Rahman Khan", contactListVm.SelectedItem.Name);
    }


    [Test]
    public void ContactListViewModel_EditSelectedItemCommand_Cancel_ListIsUnchanged()
    {
      IContactRepository contactsRepo = new TestContactRepository();
      Mock<IViewFactory> mockViewFactory = new Mock<IViewFactory>();

      ContactListViewModel contactListVm = new ContactListViewModel(contactsRepo, mockViewFactory.Object);

      contactListVm.SelectedItem = contactListVm.Contacts[0];
      var editableObject = contactListVm.SelectedItem;
      var originalValue = editableObject.Name;

      //Mock Cancel command
      mockViewFactory.Setup(e => e.EditContact(editableObject)).Callback(() => {
        editableObject.BeginEdit();
        editableObject.Name = "Abdur Rahman Khan";
        editableObject.CancelEdit();
      });

      contactListVm.EditSelectedItemCommand.Execute();

      //Verify that the view model is unchanged
      mockViewFactory.Verify(t => t.EditContact(editableObject));
      Assert.AreEqual(originalValue, contactListVm.SelectedItem.Name);
    }

  }

}
