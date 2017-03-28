using NUnit.Framework;


namespace MVVMTestableDialog.UnitTests
{


  [TestFixture]
  public class ContactTests
  {

    [Test]
    public void Contact_Constructor_Initialises_With_Empty_Fields()
    {
      var contact = new Contact();

      Assert.AreEqual(string.Empty, contact.Name);
      Assert.AreEqual(string.Empty, contact.Email);
      Assert.AreEqual(0, contact.Age);
    }


    [Test]
    public void Contact_Constructor_Initialises_Fields_With_Values_Passed()
    {
      var contact = new Contact("John Doe", "johndoe@msn.com", 23);

      Assert.AreEqual("John Doe", contact.Name);
      Assert.AreEqual("johndoe@msn.com", contact.Email);
      Assert.AreEqual(23, contact.Age);
    }


    [Test]
    public void Contact_Copy_Returns_Identical_Clone_Of_The_Object()
    {
      var contact = new Contact("John Doe", "johndoe@msn.com", 23);
      var copy = contact.DeepCopy();

      Assert.AreEqual(contact.Name, copy.Name);
      Assert.AreEqual(contact.Email, copy.Email);
      Assert.AreEqual(contact.Age, copy.Age);
      Assert.AreEqual(contact, copy);
      Assert.AreEqual(contact.GetHashCode(), copy.GetHashCode());
    }


    [Test]
    public void Contact_Name_Must_Not_Be_Empty()
    {
      var contact = new Contact(string.Empty, "johndoe@msn.com", 23);

      Assert.AreEqual(string.Empty, contact.Name);
      Assert.AreEqual("\"Name\" must not be blank", contact["Name"]);
    }


    [Test]
    public void Contact_Name_Must_Not_Be_Null()
    {
      var contact = new Contact(null, "johndoe@msn.com", 23);

      Assert.AreEqual(null, contact.Name);
      Assert.AreEqual("\"Name\" must not be blank", contact["Name"]);
    }


    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase("abc")]
    [TestCase("abc.com")]
    public void Contact_Email_Invalid_Values(string value)
    {
      var contact = new Contact("John Doe", value, 23);

      Assert.AreEqual(value, contact.Email);
      Assert.IsFalse(contact.IsValid);
      Assert.AreEqual("Email address is invalid", contact["Email"]);
    }


    [Test]
    [TestCase("abc@a")]
    [TestCase("abc@abc.com")]
    [TestCase("abc@abc.co.uk")]
    public void Contact_Email_Valid_Values(string value)
    {
      var contact = new Contact("John Doe", value, 23);

      Assert.AreEqual(value, contact.Email);
      Assert.IsTrue(contact.IsValid);
      Assert.AreEqual(string.Empty, contact["Email"]);
    }


    [Test]
    public void Contact_Age_Below_Minimum_Not_Allowed()
    {
      var contact = new Contact("John Doe", "johndoe@gmail.com", Contact.MinimumAge - 1);

      Assert.AreEqual(Contact.MinimumAge - 1, contact.Age);
      Assert.IsFalse(contact.IsValid);
      Assert.AreEqual("Age must be between " + Contact.MinimumAge + " and " + Contact.MaximumAge, contact["Age"]);
    }


    [Test]
    public void Contact_Age_Above_Maximum_Not_Allowed()
    {
      var contact = new Contact("John Doe", "johndoe@gmail.com", Contact.MaximumAge + 1);

      Assert.AreEqual(Contact.MaximumAge + 1, contact.Age);
      Assert.IsFalse(contact.IsValid);
      Assert.AreEqual("Age must be between " + Contact.MinimumAge + " and " + Contact.MaximumAge, contact["Age"]);
    }


    [Test]
    public void Contact_Constructor_Errors_Are_Populated()
    {
      var contact = new Contact();

      Assert.AreEqual(string.Empty, contact.Name);
      Assert.AreEqual(string.Empty, contact.Email);
      Assert.AreEqual(0, contact.Age);

      Assert.IsFalse(contact.IsValid);
      Assert.AreEqual("\"Name\" must not be blank", contact["Name"]);
      Assert.AreEqual("Email address is invalid", contact["Email"]);
      Assert.AreEqual("Age must be between " + Contact.MinimumAge + " and " + Contact.MaximumAge, contact["Age"]);
    }


  }

}
