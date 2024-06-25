
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Layouts;
using System.Collections.ObjectModel;
using static System.Net.Mime.MediaTypeNames;

namespace MauiApp1;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        datePicker.DateSelected += PickerSelectedIndexChanged;
        UpdateData();
    }

    private void AddPatient(object sender, System.EventArgs e)
    {

        App.Current.MainPage = new NavigationPage(new AddPatient());
    }
    private void AddAppointment(object sender, System.EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new AddAppointment());
    }
    private void PickerSelectedIndexChanged(object sender, System.EventArgs e)
    {
        UpdateData();

    }
    private void UpdateData()
    {

        grid.Clear();
        using (BaseContext context = new BaseContext())
        {
            var dents = context.Dents.ToList();
            int numberDentist = 0;
            foreach (var item in dents)
            {
                Label label = new Label { Text = item.Name };
                label.BackgroundColor = Colors.LightBlue;
                label.HorizontalTextAlignment = TextAlignment.Center;
                label.VerticalTextAlignment = TextAlignment.Center;

                grid.Add(label, 0, numberDentist);

                DateOnly data = DateOnly.FromDateTime(datePicker.Date);

                /*
                var apps = context.Apps.Where(p => p.Dentist == item)
                    .Where(p => p.Date == data)
                    .OrderBy(p => p.Time)
                    .Join(context.Pats, // другий набір
                    u => u.PatientId, // властивість-селектор об'єкта з першого набору
                    c => c.Id, // властивість-селектор об'єкта другого набору
                    (u, c) => new // результат
                    {
                        Id = u.Id,
                        Time = u.Time,
                        Tooth = u.Tooth,
                        Description = u.Description,
                        Name = c.Name,
                        Phone = c.Phone 
                    });*/

                var apps = from u in context.Apps
                           where u.Date == data
                           where u.Dentist == item
                           join c in context.Pats on u.PatientId equals c.Id
                           join v in context.Cabs on u.CabinetId equals v.Id
                           join w in context.Reasons on u.ReasonId equals w.Id
                           select new
                           {
                               Id = u.Id,
                               Time = u.Time,
                               Cabinet = v.Name,
                               Tooth = u.Tooth,
                               Reason = w.Name,
                               Description = u.Description,
                               Name = c.Name,
                               Phone = c.Phone

                           };

                TapGestureRecognizer tapGesture = new TapGestureRecognizer
                {
                    NumberOfTapsRequired = 2
                };

                int numberApp = 1;
                foreach (var app in apps)
                {
                    String someText = app.Id + ", " + app.Time.ToString() + ", " + app.Cabinet.ToString() + ", " + app.Tooth.ToString() + ",\n" + app.Reason.ToString() + ",\n" + app.Description.ToString() + ",\n" + app.Name.ToString() + ",\n" + app.Phone.ToString();
                    label = new Label { Text = someText };
                    tapGesture.Tapped += UpdateDelete;
                    label.GestureRecognizers.Add(tapGesture);
                    label.BackgroundColor = Colors.AliceBlue;
                    label.HorizontalTextAlignment = TextAlignment.Center;
                    label.VerticalTextAlignment = TextAlignment.Center;
                    grid.Add(label, numberApp, numberDentist);
                    numberApp++;
                }
                numberDentist++;
            }
        }
    }

    private void UpdateDelete(object sender, System.EventArgs e)
    {
        string text = ((Label)sender).Text;
        string[] words = text.Split(new char[] { ',' });
        App.Current.MainPage = new NavigationPage(new UpdateDelete(words[0]));
        //Користувач натискає двічі на Label, що містить текст: "123, 10:30, Cabinet A, 2, Reason A, Description A, John Doe, 555-1234".
        //Текст Label розбивається на частини, і витягується перший елемент: "123".
        //Додаток переходить на нову сторінку UpdateDelete, передаючи Id зустрічі "123" у конструктор.
    }
    private void Analytics(object sender, System.EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new Analitycs());

    }
    private void Statistic(object sender, System.EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new Statistic());
    }
}
