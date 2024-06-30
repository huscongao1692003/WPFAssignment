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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPF.Service;
using MessageBox = System.Windows.Forms.MessageBox;

namespace SalesManagement
{
    /// <summary>
    /// Interaction logic for UserProfile.xaml
    /// </summary>
    public partial class UserProfile : Window
    {
        public bool isUpdate { get; set; }
        public Member member { get; set; }
        private readonly IMemberService _memberService = new MemberService();
        public UserProfile()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(isUpdate)
            {
                txtEmail.IsEnabled = false;
                txtID.Text = member.MemberId.ToString() ?? "";
                txtEmail.Text = member.Email ?? "";
                txtCountry.Text = member.Country ?? "";
                txtCompany.Text = member.CompanyName ?? "";
                txtCity.Text = member.City ?? "";
                txtPassword.Text = member.Password ?? "";
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var user = new Member
            {
                Email = txtEmail.Text,
                City = txtCity.Text,
                CompanyName = txtCompany.Text,
                Country = txtCountry.Text,
                Password = txtPassword.Text,
            };

            bool result;

            if(isUpdate)
            {
                user.MemberId = int.Parse(txtID.Text);
                result = _memberService.UpdateUser(user);
            }
            else
            {
                result = _memberService.RegisterUser(user);
            }
            
            if(result)
            {
                MessageBox.Show(isUpdate == true ? "Update Profile Successfully!" : "Create Member Successfully!", "Notification");
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show(isUpdate == true ? "Update Profile Failed!" : "Create Member Failed", "Notification");
                this.DialogResult = false;
                this.Close();
            }

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

       
    }
}
