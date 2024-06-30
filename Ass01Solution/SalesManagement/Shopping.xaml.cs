using BusinessObject.Models;
using Repository;
using Repository.MemberRepo;
using Repository.OrderRepo;
using Repository.ProductRepo;
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
    /// Interaction logic for Shopping.xaml
    /// </summary>
    public partial class Shopping : Window
    {
        private readonly IProductService productService = new ProductService();

        public Shopping()
        {
            InitializeComponent();
            ListProduct.ItemsSource = productService.GetProducts();
        }

        private void Button_OpenMyOrder(object sender, RoutedEventArgs e)
        {
            MyOrderWindow myOrderWindow = new MyOrderWindow();
            myOrderWindow.ShowDialog();
        }

        private void Button_OpenOrder(object sender, RoutedEventArgs e)
        {
            CartWindow cart = new CartWindow(this);
            bool? isCheckout = cart.ShowDialog();
            if(isCheckout == true)
            {
                ListProduct.ItemsSource = productService.GetProducts();
            }

        }

        private void Button_Search(object sender, RoutedEventArgs e)
        {
            string? name = searchByName.Text;
            decimal? unitPrice = null;
            int? unitsInStock = null;

            if (!string.IsNullOrWhiteSpace(searchByUnitPrice.Text))
            {
                if (decimal.TryParse(searchByUnitPrice.Text, out decimal parsePrice) && parsePrice > 0)
                {
                    unitPrice = parsePrice;
                }
                else
                {
                    MessageBox.Show("Price must be positive number!");
                    return;
                }
            }

            if (!string.IsNullOrWhiteSpace(searchByUnitsInStock.Text))
            {
                if (int.TryParse(searchByUnitsInStock.Text, out int parsedStock) && parsedStock > 0)
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
                ProductName = name,
                UnitPrice = unitPrice,
                UnitsInStock = unitsInStock,
            };

            ListProduct.ItemsSource = productService.FindByFilter(productFilter);
        }

        private void Button_Load(object sender, RoutedEventArgs e)
        {
            ListProduct.ItemsSource = productService.GetProducts();
        }

        private void AddToCart(object sender, RoutedEventArgs e)
        {
            Button? button = (Button)sender;
            if (button != null)
            {
                var tag = button.Tag;
                if (!string.IsNullOrEmpty(tag.ToString()))
                {
                    int id = int.Parse(tag.ToString());
                    Product product = productService.FindById(id);
                    OrderDetail orderDetail = new OrderDetail();
                    orderDetail.ProductId = product.ProductId;
                    orderDetail.UnitPrice = product.UnitPrice;
                    orderDetail.Quantity = 1;
                    orderDetail.Discount = Displacement.Discount;
                    if (Session.carts == null)
                    {
                        Session.carts = new List<OrderDetail> { orderDetail };
                    }
                    else
                    {
                        int index = Session.carts.FindIndex(cart => cart.ProductId == orderDetail.ProductId);
                        if (index == -1)
                        {
                            Session.carts.Add(orderDetail);
                        }
                        else
                        {
                            Session.carts[index].Quantity++;
                        }
                    }
                }
            }
            UpdateCartQuantity();
        }

        public void UpdateCartQuantity()
        {
            CartCount.Text = Session.carts.Sum(product => product.Quantity).ToString();
        }
    }
}
