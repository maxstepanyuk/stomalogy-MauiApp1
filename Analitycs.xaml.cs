using Microsoft.Maui.Graphics;

namespace MauiApp1;

public partial class Analitycs : ContentPage
{
    DateOnly Date1;
    DateOnly Date2;
    public Analitycs()
	{
		InitializeComponent();
        datePicker1.DateSelected += DateSelected;
        datePicker2.DateSelected += DateSelected;
        DentistP.SelectedIndexChanged += DateSelected;

        using (BaseContext context = new BaseContext())
        {
            var Dents = context.Dents.ToList();
            foreach (var d in Dents) DentistP.Items.Add(d.Name);
        }
    }
    private void DateSelected(object sender, System.EventArgs e) 
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
            //DisplayAlert("�����������", Date1.ToString(), "�K");
            //DisplayAlert("�����������", Date2.ToString(), "�K");
            if (Date1 <= Date2 && DentistP.SelectedItem != null)
            {
                for (DateOnly dateP = Date1; dateP <= Date2; dateP=dateP.AddDays(1))
                {
                    //DisplayAlert("�����������", dateP.ToString(), "�K");
                    var dentPatients = context.Apps.
                        Where(p => p.Date == dateP).
                        Where(p => p.Dentist!.Name == DentistP.SelectedItem.ToString());

                    Label label = new Label()
                    {
                        Text = dateP.ToString(),
                        TextColor = Colors.White,
                        FontSize =9,
                        WidthRequest = 50,
                        HeightRequest = dentPatients.Count()*50,
                        VerticalOptions = LayoutOptions.End,
                        BackgroundColor = Colors.Blue
                    };
                    stack.Add(label);
                }
            }
            else
            {
                DisplayAlert("�����������", "������� ������� ���� �� ��������", "�K");
            }
        }
    }
    private void Back(object sender, System.EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new MainPage());
    }
}