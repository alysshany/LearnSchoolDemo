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
        public Service Service { get; set; }
        public string imagePath;
        
        public PageOfEditingService(Service service)
        {
            InitializeComponent();
            Service = service;
            DataContext = Service;
            ListOfImages.ItemsSource = App.Connection.ServicePhoto.Where(z => z.Service.ID == Service.ID).ToList();
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
                OpenFileDialog dialog = new OpenFileDialog();
                if (dialog.ShowDialog() != null)
                {
                    Service.MainImageByte = System.IO.File.ReadAllBytes(dialog.FileName);
                    image.Source = new BitmapImage(new Uri(dialog.FileName));
                }

                MessageBox.Show("Успешно добавлено");
            }
            catch 
            {
                MessageBox.Show("Ошибка");
            }
        }

        private void ChoosingOtherImages(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                if (dialog.ShowDialog() != null)
                {
                   var imageByte = System.IO.File.ReadAllBytes(dialog.FileName);

                    Service.ServicePhoto.Add(new ServicePhoto { Service = Service, PhotoByte = imageByte});
                    ListOfImages.ItemsSource = null;
                    ListOfImages.ItemsSource = Service.ServicePhoto.ToList();

                    MessageBox.Show("Успешно добавлено");
                }
            }
            catch 
            {
                MessageBox.Show("Ошибка");
            }
        }

        private void SavingButton(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Service.ID == 0)
                {
                    App.Connection.Service.Add(Service);
                }
                
                App.Connection.SaveChanges();

                MessageBox.Show("Успешно сохранено");
            }
            catch
            {
                MessageBox.Show("Ошибка");
            }
        }

        private void DeletingImage(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ListOfImages.SelectedItems.Count == 1)
                {
                    var servicePhoto = ListOfImages.SelectedItem as ServicePhoto;

                    if (MessageBox.Show("Вы действительно хотите удалить изображение?", "", MessageBoxButton.YesNo,
                        MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        App.Connection.ServicePhoto.Remove(servicePhoto);
                        App.Connection.SaveChanges();
                        MessageBox.Show("Удалено");

                        ListOfImages.ItemsSource = null;
                        ListOfImages.ItemsSource = Service.ServicePhoto.ToList();
                    }
                }
                else
                {
                    MessageBox.Show("Выберите изображение");
                }
            }
            catch
            {
                MessageBox.Show("Ошибка");
            }
        }
    }
}