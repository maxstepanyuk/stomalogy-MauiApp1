namespace MauiApp1;

public partial class AddPatient : ContentPage
{
	public AddPatient()
	{
		InitializeComponent();
	}
    private void Back(object sender, System.EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new MainPage());
    }
    private void Add(object sender, System.EventArgs e)
    {
        using (BaseContext context = new BaseContext())
        {
            if (NameP.Text != null && Phone.Text != null)
            {
                Patient pat = new() { Name = NameP.Text, Phone = Phone.Text };
                context.Pats.Add(pat);
                context.SaveChanges();
                DisplayAlert("Повідомлення", "Пацієнт доданий", "ОK");
            }
            else
            {
                DisplayAlert("Попередження", "Введіть дані", "ОK");
            }
        }
    }
}