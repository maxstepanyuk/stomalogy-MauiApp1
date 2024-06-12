namespace MauiApp1;

public partial class UpdateDelete : ContentPage
{
	int id { get; set; }
    Appointment p;
    public UpdateDelete(string id_)
    {
        InitializeComponent();
        id = Convert.ToInt32(id_);
        using (BaseContext context = new BaseContext())
        {
            p = context.Apps.FirstOrDefault(a => a.Id == id);
            Description.Text=p.Description;
            Tooth.Text=p.Tooth.ToString();
            TimeOnly t = new();
            datePicker.Date = p.Date.ToDateTime(t);
            timePicker.Time = p.Time.ToTimeSpan();
            patient.Text = context.Pats.FirstOrDefault(a => a.Id == p.PatientId).Name;
            
            var Dents = context.Dents.ToList();
            foreach (var d in Dents) dentistPicker.Items.Add(d.Name);
            dentistPicker.SelectedItem = p.Dentist.Name;
            
            var Reass = context.Reasons.ToList();
            foreach (var d in Reass) reasonPicker.Items.Add(d.Name);
            reasonPicker.SelectedItem = p.Reason.Name;

            var Cabs = context.Cabs.ToList();
            foreach (var d in Cabs) cabinetPicker.Items.Add(d.Name);
            cabinetPicker.SelectedItem = p.Cabinet.Name;
        }
    }
    private void Back(object sender, System.EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new MainPage());
    }
    private void Update(object sender, System.EventArgs e)
    {
        using (BaseContext context = new BaseContext())
        {
            p.Description = Description.Text;
            if (int.TryParse(Tooth.Text,out int t))
                p.Tooth = t;
            else DisplayAlert("Попередження", "Введіть номер зуба коректно", "ОK");
            p.Time = TimeOnly.FromTimeSpan(timePicker.Time);
            p.Date = DateOnly.FromDateTime(datePicker.Date);
            
            p.Dentist = context.Dents.FirstOrDefault(p => p.Name == dentistPicker.SelectedItem.ToString());

            p.Reason = context.Reasons.FirstOrDefault(p => p.Name == reasonPicker.SelectedItem.ToString());

            p.Cabinet = context.Cabs.FirstOrDefault(p => p.Name == cabinetPicker.SelectedItem.ToString());

            context.Apps.Update(p);
            context.SaveChanges();
            DisplayAlert("Повідомлення", "запис оновлений", "ОK");
        }
    }
    private void Delete(object sender, System.EventArgs e)
    {
        using (BaseContext context = new BaseContext())
        {
            context.Apps.Remove(p);
            context.SaveChanges();
            DisplayAlert("Повідомлення", "зустріч видалена", "ОK");
        }
    }
}