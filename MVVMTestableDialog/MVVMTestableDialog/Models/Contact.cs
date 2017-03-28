using System.Collections.Generic;
using System.ComponentModel;
using Prism.Mvvm;


namespace MVVMTestableDialog
{
  public class Contact : BindableBase, IDataErrorInfo
  {
    public const int MinimumAge = 6;
    public const int MaximumAge = 200;


    public Contact(string name = "", string email = "", int age = 0)
    {
      Name = name;
      Email = email;
      Age = age;
    }


    public string Name
    {
      get
      {
        return _name;
      }

      set
      {
        if (string.IsNullOrWhiteSpace(value))
          _errors["Name"] = "\"Name\" must not be blank";
        else
          _errors.Remove("Name");

        if (value != null)
          value = value.Trim();

        SetProperty(ref _name, value);
      }
    }


    public string Email
    {
      get
      {
        return _email;
      }

      set
      {
        if (string.IsNullOrWhiteSpace(value) || !value.IsValidEmail())
          _errors["Email"] = "Email address is invalid";
        else
          _errors.Remove("Email");

        if (value != null)
          value = value.Trim();

        SetProperty(ref _email, value);
      }
    }


    public int Age
    {
      get
      {
        return _age;
      }
      set
      {
        if (value < MinimumAge || value > MaximumAge)
          _errors["Age"] = "Age must be between " + MinimumAge + " and " + MaximumAge;
        else
          _errors.Remove("Age");

        SetProperty(ref _age, value);
      }
    }


    public string Error
    {
      get { return null; }
    }


    public string this[string propertyName]
    {
      get
      {
        if (_errors.ContainsKey(propertyName))
          return _errors[propertyName];

        return string.Empty;
      }
    }


    public bool IsValid
    {
      get { return _errors.Count <= 0; }
    }


    public Contact DeepCopy()
    {
      var copy = new Contact(_name, _email, _age);
      return copy;
    }


    public override bool Equals(object obj)
    {
      if (obj == null) return false;
      if (GetType() != obj.GetType()) return false;

      var other = obj as Contact;

      return (Name == other.Name) 
        && (Email == other.Email) 
        && (Age == other.Age);
    }


    public override int GetHashCode()
    {
      unchecked
      {
        // Choose large primes to avoid hashing collisions
        const int HashingBase = (int)2166136261;
        const int HashingMultiplier = 16777619;

        int hash = HashingBase;
        hash = (hash * HashingMultiplier) ^ (!object.ReferenceEquals(null, Name) ? Name.GetHashCode() : 0);
        hash = (hash * HashingMultiplier) ^ (!object.ReferenceEquals(null, Email) ? Email.GetHashCode() : 0);
        hash = (hash * HashingMultiplier) ^ Age;
        return hash;
      }
    }



    private string _name;
    private string _email;
    private int _age;

    private readonly Dictionary<string, string> _errors = new Dictionary<string, string>();
  }

}
