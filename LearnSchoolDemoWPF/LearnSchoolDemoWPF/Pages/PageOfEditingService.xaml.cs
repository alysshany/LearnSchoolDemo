using LearnSchoolDemoWPF.ADOApp;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
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
using static System.Net.WebRequestMethods;

namespace LearnSchoolDemoWPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageOfEditingService.xaml
    /// </summary>
    public partial class PageOfEditingService : Page
    {
        public bool editing = false;
        public byte[] Images { get; set; }
        public Service Service { get; set; }
        public string imagePath;
        
        public PageOfEditingService(Service service)
        {
            InitializeComponent();
            Service = service;
            DataContext = Service;
        }

        private void BackButton(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PageWithListOfServiceOfAdmin());
        }

        /// <summary>
        /// Image saving method
        /// </summary>

        private void ChoosingImage(object sender, RoutedEventArgs e)
        {
            try
            {

                var btnSelect = (Button)sender;
                OpenFileDialog dialog = new OpenFileDialog();
                if (dialog.ShowDialog() != null)
                {
                    Images = System.IO.File.ReadAllBytes(dialog.FileName);
                }
                btnSelect.Background = Brushes.Gray;
            }
            catch { }
        }


        private void SavingButton(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Service.ID == 0)
                {
                    Service.MainImageByte = Images;
                    App.Connection.Service.Add(Service);
                }
                
                App.Connection.SaveChanges();

                MessageBox.Show("Успешно");
            }
            catch
            {
                MessageBox.Show("Ошибка");
            }
        }
    }
}