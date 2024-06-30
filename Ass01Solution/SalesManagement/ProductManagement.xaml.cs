using BusinessObject.Models;
using Repository;
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
using static System.Runtime.InteropServices.JavaScript.JSType;
using MessageBox = System.Windows.Forms.MessageBox;

namespace SalesManagement
{
    /// <summary>
    /// Interaction logic for ProductManagement.xaml
    /// </summary>
    public partial class ProductManagement : Window
    {
        private readonly IProductService productService = new ProductService();
        BindingSource source;
        public ProductManagement()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            ProductDetail productDetail = new ProductDetail
            {
                isUpdate = false
            };
            bool? result = productDetail.ShowDialog();
            if (result == true)
            {
                LoadProductList();
            }
        }

        private void LoadProductList()
        {
            var products = productService.GetProducts();
            source = new BindingSource();
            source.DataSource = products;

            dgData.ItemsSource = null;
            dgData.ItemsSource = source;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            ProductDetail productDetail = new ProductDetail
            {
                isUpdate = true
            };
            bool? result = productDetail.ShowDialog();
            if (result == true)
            {
                LoadProductList();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(txtProductID.Text);
            var result = productService.Remove(id);
            if (result is false)
            {
                MessageBox.Show("Delete Failed!");
            }
            MessageBox.Show("Delete Successfully!");
            LoadProductList();
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
                btnUpdate.IsEnabled = true;
            }
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            LoadProductList();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadProductList();
            btnDelete.IsEnabled = false;
            btnUpdate.IsEnabled = false;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            int? id = null;
            decimal? unitPrice = null;
            int? unitsInStock = null;

            var name = txtSearchName.Text;

            if(!string.IsNullOrWhiteSpace(txtSearchID.Text)) 
            {
                if (int.TryParse(txtSearchID.Text, out int parseId) && parseId > 0)
                {
                    id = parseId;
                }
                else
                {
                    MessageBox.Show("Id must be positive number!");
                    return;
                }
            }

            if (!string.IsNullOrWhiteSpace(txtSearchPrice.Text))
            {
                if (decimal.TryParse(txtSearchPrice.Text, out decimal parsePrice) && parsePrice > 0)
                {
                    unitPrice = parsePrice;
                }
                else
                {
                    MessageBox.Show("Price must be positive number!");
                    return;
                }
            }

            if (!string.IsNullOrWhiteSpace(txtSearchQuantity.Text))
            {
                if (int.TryParse(txtSearchQuantity.Text, out int parsedStock) && parsedStock > 0)
                {
                    unitsInStock = parsedStock;
                }
                else
                {
                    MessageBox.Show("Quantity must be positive number!");
                    return;
                }
            }

            ProductFilter productFilter = new ProductFilter
            {
                ProductId = id,
                ProductName = name,
                UnitPrice = unitPrice,
                UnitsInStock = unitsInStock,
            };

            var products = productService.FindByFilter(productFilter);

            source = new BindingSource();
            source.DataSource = products;

            dgData.ItemsSource = null;
            dgData.ItemsSource = source;
        }
    }
}
