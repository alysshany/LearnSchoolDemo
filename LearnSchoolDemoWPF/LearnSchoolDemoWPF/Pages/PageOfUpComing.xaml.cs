using LearnSchoolDemoWPF.ADOApp;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для PageOfUpComing.xaml
    /// </summary>
    public partial class PageOfUpComing : Page
    {
        public PageOfUpComing()
        {
            InitializeComponent();

            ListOfClientServices.ItemsSource = App.Connection.ClientService.ToList().Where(z =>
            (z.StartTime.Year == DateTime.Today.Year && z.StartTime.Month == DateTime.Today.Month && z.StartTime.Day == DateTime.Today.Day && z.StartTime.TimeOfDay >= DateTime.Now.TimeOfDay) ||
            (z.StartTime.Year == DateTime.Today.Year && z.StartTime.Month == DateTime.Today.Month && z.StartTime.Day == DateTime.Now.AddDays(1).Day)).ToList();

            App.dispatcherTimer.Tick += new EventHandler(DispatcherTimer_Tick); 
            App.dispatcherTimer.Interval = new TimeSpan(0, 0, 30);
            App.dispatcherTimer.Start();
            
        }

        private void BackButton(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PageWithListOfServiceOfAdmin());
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            ListOfClientServices.ItemsSource = null;
            ListOfClientServices.ItemsSource = App.Connection.ClientService.Where(z =>
            (z.StartTime.Year == DateTime.Today.Year && z.StartTime.Month == DateTime.Today.Month && z.StartTime.Day == DateTime.Today.Day && z.StartTime.Hour >= DateTime.Now.Hour && z.StartTime.Minute >= DateTime.Now.Minute)).ToList();
        }
    }
}