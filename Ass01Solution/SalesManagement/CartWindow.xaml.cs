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
    /// Interaction logic for CartWindow.xaml
    /// </summary>
    public partial class CartWindow : Window
    {
        private readonly Shopping shopping;
        private readonly IOrderSerivce orderSerivce = new OrderService();
        private readonly IProductService productService = new ProductService();

        public CartWindow(Shopping shopping)
        {
            InitializeComponent();
            if (Session.carts == null)
            {
                Session.carts = new List<OrderDetail>();
            }
            this.shopping = shopping;
            updateCarts();
        }

        private void Button_Plus(object sender, RoutedEventArgs e)
        {
            Button? button = sender as Button;
            if (button != null)
            {
                var tag = button.Tag;
                if (!string.IsNullOrEmpty(tag.ToString()))
                {
                    int id = int.Parse(tag.ToString());
                    int index = Session.carts.FindIndex(cart => cart.ProductId == id);
                    if (index != -1)
                    {
                        Session.carts[index].Quantity += 1;
                        updateCarts();
                    }
                }
            }
        }

        private void Button_Minus(object sender, RoutedEventArgs e)
        {
            Button? button = sender as Button;
            if (button != null)
            {
                var tag = button.Tag;
                if (!string.IsNullOrEmpty(tag.ToString()))
                {
                    int id = int.Parse(tag.ToString());
                    int index = Session.carts.FindIndex(cart => cart.ProductId == id);
                    if (index != -1)
                    {
                        Session.carts[index].Quantity -= 1;
                        if (Session.carts[index].Quantity <= 0)
                        {
                            Session.carts.RemoveAt(index);
                        }
                        updateCarts();
                    }
                }
            }
        }

        private void Button_Remove(object sender, RoutedEventArgs e)
        {
            Button? button = sender as Button;
            if (button != null)
            {
                var tag = button.Tag;
                if (!string.IsNullOrEmpty(tag.ToString()))
                {
                    int id = int.Parse(tag.ToString());
                    int index = Session.carts.FindIndex(cart => cart.ProductId == id);
                    if (index != -1)
                    {
                        Session.carts.RemoveAt(index);
                        updateCarts();
                    }
                }
            }
        }

        private void Button_Checkout(object sender, RoutedEventArgs e)
        {
            DateTime? requiredDate = RequiredDate.SelectedDate == null ? null : RequiredDate.SelectedDate.Value.Date;
            DateTime? shippedDate = ShippedDate.SelectedDate == null ? null : ShippedDate.SelectedDate.Value.Date;

            List<OrderDetail> orderDetails = Session.carts.Select(cart =>
            {
                return cart;
            }).ToList();

            foreach (OrderDetail detail in orderDetails)
            {
                var product = productService.FindById(detail.ProductId);
                if(detail.Quantity > product.UnitsInStock)
                {
                    MessageBox.Show($"Not Enough in {product.ProductName} Stock");
                    return;
                }
            }

            Order order = new Order
            {
                OrderDate = DateTime.Now,
                OrderDetails = orderDetails,
                RequiredDate = requiredDate,
                ShippedDate = shippedDate,
                Freight = Displacement.Freight,
                MemberId = (int)Session.userId
            };
            
            var result = orderSerivce.Create(order);
            if (result)
            {
                foreach(OrderDetail detail in orderDetails)
                {
                    var product = productService.FindById(detail.ProductId);
                    product.UnitsInStock -= detail.Quantity;
                    productService.Update(product);
                }
            }
            
            Session.carts = new List<OrderDetail>();
            updateCarts();
           
            this.DialogResult = true;
            this.Close();
        }

        private void updateCarts()
        {
            listView.ItemsSource = Session.carts.ToList();
            TxtBoxTotalPrice.Text = Session.carts.Sum(cart => (cart.Quantity * cart.UnitPrice) - (cart.Quantity * cart.UnitPrice * (decimal)cart.Discount)).ToString();
            shopping.UpdateCartQuantity();
        }
    }
}
