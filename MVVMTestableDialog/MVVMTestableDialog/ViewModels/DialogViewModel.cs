using Prism.Commands;
using Prism.Mvvm;


namespace MVVMTestableDialog
{

  public enum DialogResult
  {
    Undefined,
    OK,
    Cancel
  }


  public class DialogViewModel : BindableBase
  {

    public DialogViewModel()
    {
      _dialogResult = DialogResult.Undefined;
    }


    public DialogResult DialogResult
    {
      get { return _dialogResult; }
      set { _dialogResult = value; }
    }


    protected virtual void OnOK()
    {
      _dialogResult = DialogResult.OK;
    }


    protected virtual void OnCancel()
    {
      _dialogResult = DialogResult.Cancel;
    }


    protected virtual bool CanCancel()
    {
      return true;
    }


    protected virtual bool CanOK()
    {
      return false;
    }


    public DelegateCommand OKCommand
    {
      get
      {
        if (_okCommand == null)
          _okCommand = new DelegateCommand(OnOK, CanOK);
        return _okCommand;
      }
    }


    public DelegateCommand CancelCommand
    {
      get
      {
        if (_cancelCommand == null)
          _cancelCommand = new DelegateCommand(OnCancel, CanCancel);
        return _cancelCommand;
      }
    }


    private DelegateCommand _okCommand;
    private DelegateCommand _cancelCommand;
    private DialogResult _dialogResult;
  }

}
