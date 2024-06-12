using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace MauiApp1;

public partial class Statistic : ContentPage
{
    DateOnly Date1;
    DateOnly Date2;

    public Statistic()
	{
		InitializeComponent();
        datePicker1.DateSelected += DateSelected;
        datePicker2.DateSelected += DateSelected;
        UpdateData();
    }

    void DateSelected(object sender, DateChangedEventArgs e)
    {
        UpdateData();
    }

    private void UpdateData()
    {
        stack.Clear();
        Date1 = DateOnly.FromDateTime(datePicker1.Date);
        Date2 = DateOnly.FromDateTime(datePicker2.Date);
        using (BaseContext context = new BaseContext())
        {
            if (Date1 <= Date2)
            {
                var dentPatients = context.Apps.
                    Where(p => p.Date >= Date1).Where(p => p.Date <= Date2).
                    GroupBy(u => u.Dentist!.Name).
                    Select(g => new
                    {
                        g.Key,
                        Count = g.Count()
                    }).OrderByDescending(u => u.Count);
                int i = 1;
                foreach (var item in dentPatients)
                {
                    Label label = new Label() { Text = i + ". " + item.Key + " - " + item.Count };
                    stack.Add(label);
                    i++;
                }
            }
            else
            {
                DisplayAlert("Повідомлення", "Виберіть коректні дати", "ОK");
            }
        }
    }

    private void Back(object sender, System.EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new MainPage());
    }

}