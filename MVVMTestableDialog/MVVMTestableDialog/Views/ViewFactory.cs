namespace MVVMTestableDialog
{
  public class ViewFactory : IViewFactory
  {
    public void EditContact(ContactViewModel vm)
    {
      var contactView = new ContactView();
      contactView.DataContext = vm;
      contactView.ShowDialog();
    }
  }
}
