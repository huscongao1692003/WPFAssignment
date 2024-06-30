using Repository.QueryFilter;
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
    /// Interaction logic for OrderManagement.xaml
    /// </summary>
    public partial class OrderManagement : Window
    {
        private readonly IOrderSerivce orderSerivce = new OrderService();
        public OrderManagement()
        {
            InitializeComponent();
            dgData.ItemsSource = orderSerivce.GetOrders();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Search(object sender, RoutedEventArgs e)
        {
            DateTime? startDate = StartDate.SelectedDate == null ? null : StartDate.SelectedDate.Value.Date;
            DateTime? endDate = EndDate.SelectedDate == null ? null : EndDate.SelectedDate.Value.Date;
            OrderFilter orderFilter = new OrderFilter
            {
                StartDate = startDate,
                EndDate = endDate
            };
            dgData.ItemsSource = orderSerivce.FindAllByFilter(orderFilter); 
        }
    }
}
