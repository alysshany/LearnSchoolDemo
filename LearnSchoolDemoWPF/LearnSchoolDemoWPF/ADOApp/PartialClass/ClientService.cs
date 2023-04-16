using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace LearnSchoolDemoWPF.ADOApp
{
    public partial class ClientService
    {
        public DateTime TimeLeft 
        {
            get
            {
                return new DateTime() + (StartTime - DateTime.Now);
            }
        }

        public string TimeLeftString
        {
            get
            {
                return $"{TimeLeft.Day-1} дней {TimeLeft.Hour} часов {TimeLeft.Minute} минут";
            }
        }

        public Brush ServiceClientColor
        {
            get
            {
                if (TimeLeft.Day - 1 == 0 && TimeLeft.Hour < 1)
                    return Brushes.Red;
                else
                    return Brushes.Transparent;
            }
        }
    }
}
