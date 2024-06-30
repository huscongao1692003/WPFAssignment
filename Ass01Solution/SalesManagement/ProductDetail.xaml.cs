using BusinessObject.Models;
using Microsoft.VisualBasic.ApplicationServices;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SalesManagement
{
    /// <summary>
    /// Interaction logic for ProductDetail.xaml
    /// </summary>
    public partial class ProductDetail : Window
    {
        public bool isUpdate { get; set; } = false;
        public Product product { get; set; }
        private readonly IProductService _productService = new ProductService();
        public ProductDetail()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (isUpdate)
            {
                txtID.Text = product.ProductId.ToString();
                txtName.Text = product.ProductName.ToString();
                txtUnitInStock.Text = product.UnitsInStock.ToString();
                txtUnitPrice.Text = product.UnitPrice.ToString();
                txtWeight.Text = product.Weight;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            int? unitStock;
            decimal? unitPrice;

            if (!string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Produce name is required!");
                return;
            }

            if (!string.IsNullOrWhiteSpace(txtWeight.Text))
            {
                MessageBox.Show("Weight is required!");
                return;
            }

            if (decimal.TryParse(txtUnitPrice.Text, out decimal parsePrice) && parsePrice > 0)
            {
                unitPrice = parsePrice;
            }
            else
            {
                MessageBox.Show("UnitPrice must be positive number!");
                return;
            }

            if (int.TryParse(txtUnitInStock.Text, out int parsedStock) && parsedStock > 0)
            {
                unitStock = parsedStock;
            }
            else
            {
                MessageBox.Show("UnitInStock must be positive number!");
                return;
            }

            var product1 = new Product
            {
                ProductName = txtName.Text,
                Weight = $"{txtWeight.Text}kg",
                UnitPrice = (decimal)unitPrice,
                UnitsInStock = (int)unitStock
            };

            bool result;

            if (isUpdate)
            {
                product1.ProductId = int.Parse(txtID.Text);
                result = _productService.Update(product1);
            }
            else
            {
                result = _productService.Create(product1);
            }

            if (result)
            {
                MessageBox.Show(isUpdate == true ? "Update Successfully!" : "Create Successfully!", "Notification");
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show(isUpdate == true ? "Update Failed!" : "Create Failed", "Notification");
                this.DialogResult = false;
                this.Close();
            }
        }

        private void txtName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
