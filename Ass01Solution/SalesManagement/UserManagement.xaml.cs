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
    /// Interaction logic for UserManagement.xaml
    /// </summary>
    public partial class UserManagement : Window
    {

        private readonly IMemberService _memberService = new MemberService();
        BindingSource source;
        public int id { get; set; }
        public UserManagement()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            btnDelete.IsEnabled = false;
            btnUpdate.IsEnabled = false;
            LoadMemberList();


            //if (!isAdmin)
            //{
            //    btnCreate.IsEnabled = false;
            //    btnDelete.IsEnabled = false;
            //}

        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            UserProfile profile = new UserProfile
            {
                isUpdate = false
            };
            bool? result = profile.ShowDialog();
            if (result == true)
            {
                LoadMemberList();
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            UserProfile profile = new UserProfile
            {
                isUpdate = true
            };
            LoadMemberList();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(txtUserID.Text);
            var result = _memberService.DeleteUser(id);
            if (result is false)
            {
                MessageBox.Show("Delete Failed!");
            }
            MessageBox.Show("Delete Successfully!");
            LoadMemberList();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void dgData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgData.SelectedItems.Count > 0)
            {
                btnDelete.IsEnabled = true;
            }
        }

        public void LoadMemberList()
        {
            var members = _memberService.GetMembers();
            source = new BindingSource();
            source.DataSource = members;

            dgData.ItemsSource = null;
            dgData.ItemsSource = source;
        }
    }
}
