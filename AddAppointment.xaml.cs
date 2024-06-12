namespace MauiApp1;

public partial class AddAppointment : ContentPage
{
	public AddAppointment()
	{
        InitializeComponent();
        using (BaseContext context = new BaseContext())
        {
            List<Patient> Pats = context.Pats.ToList();
            foreach (var p in Pats) patientPicker.Items.Add(p.Name);
           
            var Dents = context.Dents.ToList();
            foreach (var p in Dents) dentistPicker.Items.Add(p.Name);

            var Cabs = context.Cabs.ToList();
            foreach (var p in Cabs) cabinetPicker.Items.Add(p.Name);

            var Reass = context.Reasons.ToList();
            foreach (var p in Reass) reasonPicker.Items.Add(p.Name);
        }
    }
    private void Back(object sender, System.EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new MainPage());
    }
    private void Add(object sender, System.EventArgs e)
    {
        using (BaseContext context = new BaseContext())
        {
            if (Tooth.Text != null && Description.Text != null  && dentistPicker.SelectedItem != null 
                && patientPicker.SelectedItem != null &&
                reasonPicker.SelectedItem != null  &&
                cabinetPicker.SelectedItem != null)
            {
                int.TryParse(Tooth.Text, out int tooth);
                string description = Description.Text;
                TimeOnly time = TimeOnly.FromTimeSpan(timePicker.Time);
                DateOnly date = DateOnly.FromDateTime(datePicker.Date);
                Dentist dent = context.Dents.FirstOrDefault(u => u.Name == dentistPicker.SelectedItem.ToString());
                Patient pat = context.Pats.FirstOrDefault(u => u.Name == patientPicker.SelectedItem.ToString());
                Reason reas = context.Reasons.FirstOrDefault(u => u.Name == reasonPicker.SelectedItem.ToString());
                Cabinet cab = context.Cabs.FirstOrDefault(u => u.Name == cabinetPicker.SelectedItem.ToString());

                Appointment ap = new()
                {
                    Tooth = tooth,
                    Description = description,
                    Time = time,
                    Date = date,
                    PatientId = pat.Id,
                    Dentist = dent,
                    Reason = reas,
                    Cabinet = cab
                };
                context.Apps.Add(ap);
                context.SaveChanges();
                DisplayAlert("Повідомлення", "Зустріч додана", "ОK");
            }
            else
            {
                DisplayAlert("Попередження", "Введіть дані", "ОK");
            }
        } 
    }
}