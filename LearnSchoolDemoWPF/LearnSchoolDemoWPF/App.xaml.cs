using LearnSchoolDemoWPF.ADOApp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace LearnSchoolDemoWPF
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static LearnSchoolDemoEntities Connection = new LearnSchoolDemoEntities();
        public static bool isAuth = false;
        public static DispatcherTimer dispatcherTimer = new DispatcherTimer();
    }
}
