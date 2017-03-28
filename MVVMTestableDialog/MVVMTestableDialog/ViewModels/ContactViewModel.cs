using System.ComponentModel;
using System.Diagnostics;


namespace MVVMTestableDialog
{
  public class ContactViewModel : DialogViewModel, IDataErrorInfo, IEditableObject
  {

    public ContactViewModel(Contact contact)
    {
      _originalObject = contact;
      BeginEdit();
    }


    public string Name
    {
      get
      {
        return _workingCopy.Name;
      }

      set
      {
        _workingCopy.Name = value;
        OKCommand.RaiseCanExecuteChanged();
      }
    }


    public string Email
    {
      get
      {
        return _workingCopy.Email;
      }
      set
      {
        _workingCopy.Email = value;
        OKCommand.RaiseCanExecuteChanged();
      }
    }


    public int Age
    {
      get
      {
        return _workingCopy.Age;
      }
      set
      {
        _workingCopy.Age = value;
        OKCommand.RaiseCanExecuteChanged();
      }
    }


    public string this[string propertyName]
    {
      get { return _workingCopy[propertyName]; }
    }


    public string Error
    {
      get { return string.Empty; }
    }


    protected override bool CanOK()
    {
      return _workingCopy.IsValid;
    }


    protected override void OnOK()
    {
      base.OnOK();
      EndEdit();
      RaisePropertyChanged("Name");
      RaisePropertyChanged("Email");
      RaisePropertyChanged("Age");
    }


    protected override void OnCancel()
    {
      base.OnCancel();
      CancelEdit();
    }


    public bool Editing
    {
      get { return _workingCopy != null && !_workingCopy.Equals(_originalObject); }
    }


    public void BeginEdit()
    {
      if (!Editing)
        _workingCopy = _originalObject.DeepCopy();
      else
        Debug.WriteLine("WARNING: Edit already in progress!!");
    }


    public void EndEdit()
    {
      if (Editing)
        _originalObject = _workingCopy.DeepCopy();
    }


    public void CancelEdit()
    {
      if (Editing)
        _workingCopy = _originalObject.DeepCopy();
    }



    private Contact _originalObject;
    private Contact _workingCopy;
  }

}
