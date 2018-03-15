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

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            List<CityInfo> list = new List<CityInfo>();
            this.dataGrid1.ItemsSource = CityInfo.GetInfo(list);
        }
        //定义绑定DataGrid的实体和集合
        public class CityInfo
        {
            public string AddrName { get; set; }
            public string CityName { get; set; }
            public string TelNum { get; set; }
            public double TotalSum { get; set; }
            public static List<CityInfo> GetInfo(List<CityInfo>  list)
            {
                NFineBaseEntities db = new NFineBaseEntities();
                foreach (var item in db.Sys_User)
                {
                    CityInfo tmpentity = new CityInfo();
                    tmpentity.AddrName = item.F_RealName;
                    tmpentity.CityName = item.F_NickName;
                    tmpentity.TelNum = item.F_MobilePhone;
                    tmpentity.TotalSum = 3.66;
                    list.Add(tmpentity);
                }
                return list;
            }
        }
    }
}
