using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace LearnSchoolDemoWPF.ADOApp
{
    public partial class Service
    {

        public Brush ServiceColor
        {
            get
            {
                if (Discount > 0)
                    return Brushes.LightGreen;
                else
                    return Brushes.White;
            }
        }

        public Visibility Visibility 
        {
            get 
            {
                if (App.isAuth)
                    return Visibility.Visible;
                else 
                    return Visibility.Collapsed;
            }
        }

        public Visibility HavingDiscount
        {
            get
            {
                if (Discount > 0)
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }
        }

        public decimal CostWithDiscount
        {
            get
            {
                if (Discount > 0)
                    return Cost - Cost * (decimal)Discount / 100;
                else
                    return Cost;
            }
        }

        public string DurationInMinutes
        {
            get
            {
                if (DurationInSeconds == 0)
                    return "";
                else
                    return $"{DurationInSeconds / 60} минут";
            }
        }

        public string DiscountShowing
        {
            get
            {
                if (Discount == null || Discount == 0)
                    return "";
                else
                    return $"* скидка {Discount}%";
            }
        }

        public string ColorDisMethod
        {
            get
            {
                if (Discount == 0 || Discount == null)
                    return "#ffffff";
                else
                    return "#D1FFD1";
            }
        }
    }
}
