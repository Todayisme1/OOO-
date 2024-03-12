using OOO_Восток.Classes;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
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
using System.Xml.Linq;

namespace OOO_Восток.View
{
    /// <summary>
    /// Логика взаимодействия для OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {

        List<Classes.ProductInOrder> listOrder = new List<ProductInOrder>();
        public OrderWindow()
        {
            InitializeComponent();
            //this.listOrder = Helper.productInOrders;
            //listBoxOrder.ItemsSource = listOrder;
            //List<Model.Requests> points = new List<Model.Requests>();
            //points = Helper.DB.Requests.ToList();
            //Points.ItemsSource = points;
            //Points.DisplayMemberPath = "Product";
            //Points.SelectedValuePath = "IDRequest";
            //Points.SelectedIndex = 0;
            //ShowInfo();
        }


        //Конструктор с параментром, передача выбранных товаров в заказ

        public OrderWindow(List<ProductInOrder> listOrder)
        {
            InitializeComponent();
            this.listOrder = listOrder;
            listBoxOrder.ItemsSource = listOrder;
            ShowInfo();
            List<Model.Requests> points = new List<Model.Requests>();
            points = Helper.DB.Requests.ToList();
           
        }
        private void ShowInfo()
        {
            double totalSum = 0;
            double discountSum = 0;
            if (listOrder.Count == 0) butOrder.Visibility = Visibility.Hidden;
            else butOrder.Visibility = Visibility.Visible;
            foreach (var item in listOrder)
            {
                totalSum += item.ProductExtended.Product.Price * item.CountProductInOrder;
                discountSum += item.ProductExtended.ProductCostWithDiscont * item.CountProductInOrder;
            }
            tbTotalSum.Text = "Сумма заказа: " + totalSum.ToString();
            tbDiscount.Text = "Cкидка: " + (totalSum - discountSum).ToString();
            tbDiscountSum.Text = "Итого: " + discountSum.ToString();
            listBoxOrder.ItemsSource = null;
            listBoxOrder.ItemsSource = this.listOrder;
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void deleteProduct_Click(object sender, RoutedEventArgs e)
        {
            //Список товаров
            ProductInOrder selectProduct = (sender as Button).DataContext as ProductInOrder;
            listOrder.Remove(selectProduct);
            ShowInfo();
        }

        private void tbCountInOrder_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb.Text != "")
            {

                ProductInOrder selectProduct = (sender as TextBox).DataContext as ProductInOrder;
                if (tb.Text != "0")
                {
                    try
                    {
                        int ind = listOrder.IndexOf(selectProduct);
                        listOrder[ind].CountProductInOrder = Convert.ToInt32(tb.Text.ToString());
                    }
                    catch
                    {
                        MessageBox.Show("Можно вводить только числа");
                    }
                }
                else
                {
                    listOrder.Remove(selectProduct);
                }
                ShowInfo();
            }
        }

        private void butOrder_Click(object sender, RoutedEventArgs e)
        {
            //if (listOrder.Count == 0)
            //{
            //    MessageBox.Show("Заказ пуст");
            //}
            //else
            //{
            //    //Объект заказа
            //    Model.Requests order = new Model.Requests();
            //    order.Data = DateTime.Now;
            //    //order.Manager = order.Data.AddDays(6); //Доработать
            //    order.Client = tbFullName.Text;
            //    order.OrderCode = new Random().Next(100, 1000);
            //    order.OrderStatus = 1;
            //    order.OrderPoint = (int)Points.SelectedValue;
            //    Helper.DB.Orders.Add(order);
            //    try
            //    {
            //        Helper.DB.SaveChanges();
            //        foreach (var item in listOrder)
            //        {
            //            OrderProduct orderProduct = new OrderProduct();
            //            orderProduct.OrderId = order.OrderId;
            //            orderProduct.ProductArticle = item.ProductExtended.Product.ProductArticle;
            //            orderProduct.ProductCountInOrder = item.CountProductInOrder;
            //            Helper.DB.OrderProducts.Add(orderProduct);
            //            Helper.DB.SaveChanges();
            //        }
            //        MessageBox.Show("Заказ оформлен");
            //        this.Close();
            //        listOrder.Clear();
            //    }
            //    catch
            //    {
            //        MessageBox.Show("Ошибка прдключения к серверу, невозможно оформить заказ");
            //        return;
            //    }

            //}
        }



    }
}
