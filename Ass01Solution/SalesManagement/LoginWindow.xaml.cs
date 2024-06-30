using BusinessObject.Models;
using Microsoft.Extensions.Configuration;
using Repository.MemberRepo;
using System;
using System.Collections.Generic;
using System.Configuration;
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
using WPF.Service;

namespace SalesManagement
{
    public partial class LoginWindow : Window
    {
        private readonly IConfiguration _configuration;
        private readonly IMemberService _memberService = new MemberService();
        public LoginWindow()
        {
            InitializeComponent();
            var builder = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json");
            _configuration = builder.Build();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var check = checkLogin(txtUser.Text, txtPass.Password);
            if(check is null)
            {
                MessageBox.Show("Login Failed", "Notification");
                txtUser.Focus();
            }
            else if(check.MemberId == 0)
            {
                Admin admin = new Admin();
                admin.Show();
                this.Close();
            }
            else
            {
                Session.userId = check.MemberId;
                User user = new User();
                user.Show();
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private Member? checkLogin(String email, String passwword)
        {
            string defaultEmail = _configuration["AccountAdmin:Email"];
            string defaultPassword = _configuration["AccountAdmin:Password"];

            if (email == defaultEmail && passwword == defaultPassword)
            {
                return new Member
                {
                    MemberId = 0
                };
            }
            else
            {
                var check = _memberService.LoginUser(email, passwword);
                return check;
            }
        }
    }
}
