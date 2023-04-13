using LearnSchoolDemoWPF.ADOApp;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LearnSchoolDemoWPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageOfClientServiceAdding.xaml
    /// </summary>
    public partial class PageOfClientServiceAdding : Page
    {
        public Service Service { get; set; }
        public string TimeForNow = " 12:00";

        public PageOfClientServiceAdding(Service service)
        {
            InitializeComponent();
            ClientsComboBox.ItemsSource = App.Connection.Client.ToList();
            Service = service;
            TxtTitle.Text = service.Title;
            TxtDuration.Text = service.DurationInSeconds.ToString();
        }

        private void BackButton(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PageWithListOfServiceOfAdmin());
        }

        private void SavingButton(object sender, RoutedEventArgs e)
        {
            string[] needTime = Time.Text.Split(':');
            DateTime expectedDateTime = Convert.ToDateTime(Date.SelectedDate).Add(new TimeSpan(Convert.ToInt32(needTime[0]), Convert.ToInt32(needTime[1]), 0));
            
            ClientService clientService = new ClientService()
            {
                ClientID = (ClientsComboBox.SelectedItem as Client).ID,
                ServiceID = (Service.ID),
                StartTime = expectedDateTime,
                Comment = " ",
            };

            App.Connection.ClientService.Add(clientService);
            App.Connection.SaveChanges();

            MessageBox.Show("Успешно");
        }
    }
}