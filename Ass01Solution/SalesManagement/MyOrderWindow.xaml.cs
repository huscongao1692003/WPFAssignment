using Repository.MemberRepo;
using Repository.OrderRepo;
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
using System.Windows.Shapes;
using WPF.Service;

namespace SalesManagement
{
    /// <summary>
    /// Interaction logic for MyOrderWindow.xaml
    /// </summary>
    public partial class MyOrderWindow : Window
    {
        private readonly IOrderSerivce orderSerivce = new OrderService();

        public MyOrderWindow()
        {
            InitializeComponent();
            listView.ItemsSource = orderSerivce.FindById((int)Session.userId).OrderByDescending(order => order.OrderDate);
        }
    }
}
