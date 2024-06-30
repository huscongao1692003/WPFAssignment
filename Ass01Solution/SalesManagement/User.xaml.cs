using BusinessObject.Models;
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
    /// Interaction logic for User.xaml
    /// </summary>
    public partial class User : Window
    {
        private readonly IMemberService _memberService = new MemberService();

        public User()
        {
            InitializeComponent();
        }

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            var user = _memberService.GetMember((int)Session.userId); 
            UserProfile profile = new UserProfile
            {
                member = user,
                isUpdate = true
            };
            profile.ShowDialog();
        }

        private void btnShopping_Click(object sender, RoutedEventArgs e)
        {
            Shopping shopping = new Shopping();
            shopping.ShowDialog();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            Session.carts = new List<OrderDetail>();
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        
    }
}
