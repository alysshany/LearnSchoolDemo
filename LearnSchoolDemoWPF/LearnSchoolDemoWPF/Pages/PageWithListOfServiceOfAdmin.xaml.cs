using LearnSchoolDemoWPF.ADOApp;
using LearnSchoolDemoWPF.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
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
    /// Логика взаимодействия для PageWithListOfServiceOfAdmin.xaml
    /// </summary>
    public partial class PageWithListOfServiceOfAdmin : Page
    {
        public List<Service> Services { get; set; }
        public List<Service> SortedServices { get; set; }

        public PageWithListOfServiceOfAdmin()
        {
            InitializeComponent();

            if (App.isAuth)
            {
                addingButton.Visibility = Visibility.Visible;
                viewUpButton.Visibility = Visibility.Visible;
                addUpComingButton.Visibility = Visibility.Visible;
                backPanel.Visibility = Visibility.Visible;
                codePanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                addingButton.Visibility = Visibility.Collapsed;
                viewUpButton.Visibility = Visibility.Collapsed;
                addUpComingButton.Visibility = Visibility.Collapsed;
                backPanel.Visibility = Visibility.Collapsed;
                codePanel.Visibility = Visibility.Visible;
            }
            Services = App.Connection.Service.ToList();
            ListOfServices.ItemsSource = Services;
            SortingBox.ItemsSource = SortingInfo.Sorting;
            SortingBox.SelectedIndex = 0;
            CountOfServicesUpdate();
        }

        private void DeletingService(object sender, RoutedEventArgs e)
        {
            if (ListOfServices.SelectedItems.Count == 1)
            {
                var service = ListOfServices.SelectedItem as Service;

                try
                {
                    App.Connection.Service.Remove(service);
                    App.Connection.SaveChanges();
                    MessageBox.Show("Удалено");
                    this.NavigationService.Navigate(new PageWithListOfServiceOfAdmin());
                }
                catch 
                {
                    MessageBox.Show("Нельзя удалить");
                }
            }
            else
            {
                MessageBox.Show("Выберите услугу для удаления");
            }

        }

        private void ButtonToAddNewService(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PageOfEditingService(new Service()));
        }

        private void EditingService(object sender, RoutedEventArgs e)
        {
            if (ListOfServices.SelectedItems.Count == 1)
            {
                var service = ListOfServices.SelectedItem as Service;
                
                this.NavigationService.Navigate(new PageOfEditingService(service));
            }
            else
            {
                MessageBox.Show("Выберите услугу для изменения");
            }
        }

        private void ButtonToViewUpComing(object sender, RoutedEventArgs e)
        {
            if (ListOfServices.SelectedItems.Count == 1)
            {
                var service = ListOfServices.SelectedItem as Service;

                this.NavigationService.Navigate(new PageOfUpComing(service));
            }
            else
            {
                MessageBox.Show("Выберите услугу");
            }
        }

        private void AddClient(object sender, RoutedEventArgs e)
        {
            if (ListOfServices.SelectedItems.Count == 1)
            {
                var service = ListOfServices.SelectedItem as Service;
                this.NavigationService.Navigate(new PageOfClientServiceAdding(service));
            }
            else
            {
                MessageBox.Show("Выберите услугу");
            }
        }

        private void ForCodeBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (ForCodeBox.Text == "0000")
                {
                    App.isAuth = true;
                    this.NavigationService.Navigate(new PageWithListOfServiceOfAdmin());
                    MessageBox.Show("Вы зашли за администратора");
                }
            }
        }

        /// <summary>
        /// Searching method
        /// </summary>

        private void SearchTextChanged(object sender, TextChangedEventArgs e)
        {
            if (Search.Text == "")
            {
                SortedServices = Services;
            }
            ListOfServices.ItemsSource = SortedServices.Where(x => string.Join(" ", x.Title, x.Description).ToLower().Contains(Search.Text.ToLower())).ToList();
            CountOfServicesUpdate();
        }

        /// <summary>
        /// Sorting method
        /// </summary>

        private void SortingBoxChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (SortingBox.SelectedIndex)
            {
                case 0:
                    SortedServices = Services;
                    break;
                case 1:
                    SortedServices = Services.OrderBy(x => x.CostWithDiscount).ToList();
                    break;
                case 2:
                    SortedServices = Services.OrderByDescending(x => x.CostWithDiscount).ToList();
                    break;
                case 3:
                    SortedServices = Services.Where(x => x.Discount >= 70 && x.Discount < 100).ToList();
                    break;
                case 4:
                    SortedServices = Services.Where(x => x.Discount >= 30 && x.Discount < 70).ToList();
                    break;
                case 5:
                    SortedServices = Services.Where(x => x.Discount >= 15 && x.Discount < 30).ToList();
                    break;
                case 6:
                    SortedServices = Services.Where(x => x.Discount >= 5 && x.Discount < 15).ToList();
                    break;
                case 7:
                    SortedServices = Services.Where(x => x.Discount >= 0 && x.Discount < 5).ToList();
                    break;
                default:
                    SortedServices = Services;
                    break;
            }
            ListOfServices.ItemsSource = SortedServices;
            CountOfServicesUpdate();
        }

        private void CountOfServicesUpdate()
        {
            CountOfServices.Text = $"{ListOfServices.Items.Count} из {Services.Count()}";
        }
    }
}