using OOO_Восток.Classes;
using OOO_Восток.Model;
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

namespace OOO_Восток.View
{
    /// <summary>
    /// Логика взаимодействия для CatalogWindow.xaml
    /// </summary>
    public partial class CatalogWindow : Window      
    {

        List<ProductInOrder> order = new List<ProductInOrder>(); 

        public CatalogWindow()
        {
            
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Helper.users != null)
            {
                fullName.Text = Helper.users.UserName.ToString();
            }
            if (Helper.users == null)
            {
                //cmAddInOrder.Visibility = Visibility.Visible;
                //createOrder.Visibility = Visibility.Visible;
            }
            else if (Helper.users.Roles.RoleID == 2)
            {   cmAddInOrder.Visibility = Visibility.Visible;
                createOrder.Visibility = Visibility.Visible;
            }
            else
                {
                editOrder.Visibility = Visibility.Visible;
                if (Helper.users.Role == 1)
                    addProduct.Visibility = Visibility.Visible;
            }


           

            //Список категорий
            List<Categories> categories = new List<Categories>();
            categories = Classes.Helper.DB.Categories.ToList();
            CategoryFilter.DisplayMemberPath = "CategoryName";
            CategoryFilter.SelectedValuePath = "CategoryID";
            Categories category = new Categories();
            category.CategoryID = 0;
            category.CategoryName = "Все категории";
            categories.Insert(0, category);
            CategoryFilter.ItemsSource = categories;
            CategoryFilter.SelectedIndex = 0;


            List<Model.Products> listProduct = new List<Model.Products>();
            listProduct = Classes.Helper.DB.Products.ToList();
            //listBoxProduct.ItemsSource = listProduct;
            List<Classes.ProductExtended> productExtendeds = new List<Classes.ProductExtended>();
            foreach (Model.Products product in listProduct)
            {
                Classes.ProductExtended productExtended = new Classes.ProductExtended();
                productExtended.Product = product;
                productExtendeds.Add(productExtended);
            }
            listBoxProduct.ItemsSource = productExtendeds;

        }

        private void ShowProduct()
        {
            List<Model.Products> listProduct = new List<Model.Products>();
            listProduct = Classes.Helper.DB.Products.ToList();
            int totalCount = listProduct.Count;
            //listBoxProduct.ItemsSource = listProduct;
            List<Classes.ProductExtended> productExtendeds = new List<Classes.ProductExtended>();
            foreach (Model.Products product in listProduct)
            {
                Classes.ProductExtended productExtended = new Classes.ProductExtended();
                productExtended.Product = product;
                productExtendeds.Add(productExtended);
            }
            //Сортировка по возрастанию и убыванию
            if (rbSortAsc.IsChecked == true)
            {
                productExtendeds = productExtendeds.OrderBy(x => x.Product.Price).ToList();
            }
            else
            {
                productExtendeds = productExtendeds.OrderByDescending(x => x.Product.Price).ToList();
            }

            
        
            //productExtendeds = productExtendeds.Where(pr => pr.Product.ProductDiscountMax >= min && pr.Product.ProductDiscountMax <= max).ToList();
            //Фильтрация по категории

            if (CategoryFilter.SelectedValue != null)
            {
                int idCat = (int)CategoryFilter.SelectedValue;
                if (idCat > 0)
                {
                    productExtendeds = productExtendeds.Where(pr => pr.Product.Category == idCat).ToList();
                }
            }

            string search = tbSearch.Text;	//Введенная строка поиска
            if (search.Length > 0)		//Если она не пустая
            {
                search = search.ToLower();
                productExtendeds = productExtendeds.Where(pr => pr.Product.ProductName.ToLower().Contains(search)).ToList();
            }

            //Отображение количество найденных товаров
            int filterCount = productExtendeds.Count;
            CountProduct.Text = "Отображено " + filterCount + " товаров из " + totalCount;

            listBoxProduct.ItemsSource = productExtendeds;

        }

        private void rbSortAsc_Checked(object sender, RoutedEventArgs e)
        {
            ShowProduct();
        }

        private void rbSortDesc_Checked(object sender, RoutedEventArgs e)
        {
            ShowProduct();
        }

        private void CategoryFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowProduct();
        }

        private void DiscountFilter_Selected(object sender, SelectionChangedEventArgs e)
        {
            ShowProduct();
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            ShowProduct();
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void listBoxProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void createOrder_Click(object sender, RoutedEventArgs e)
        {
            View.OrderWindow orderWindow = new OrderWindow(order);
            this.Hide();
            orderWindow.ShowDialog();
            this.ShowDialog();
        }

        private void editOrder_Click(object sender, RoutedEventArgs e)
        {
            View.OrderWindow orderWindow = new OrderWindow(order);
            this.Hide();
            orderWindow.ShowDialog();
            this.ShowDialog();
        }

        private void addProduct_Click(object sender, RoutedEventArgs e)
        {
            View.ProductWindow productWindow = new ProductWindow();
            this.Hide();
            productWindow.ShowDialog();
            this.ShowDialog();
        }


        /// Для редактирования товара - двойной клик по товару
        private void listBoxProducts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (Helper.users.Role == 1) 		//Только для роли администратора 
            {
                //Выбранный товар в каталоге
                Classes.ProductExtended productSelected = listBoxProduct.SelectedItem as Classes.ProductExtended;
                //Вызов конструктора с параметром - выбранный товар для редактирования
                View.ProductWindow productWindow = new View.ProductWindow();
                this.Hide();
                productWindow.ShowDialog();
                this.ShowDialog();
            }
        }

        /// При активизации окна - переход на это окно - обновить каталог при отображении
        private void Window_Activated(object sender, EventArgs e)
        {
            ShowProduct();
        }
        //Добавление товара в заказ
        private void AddInOrder_Click(object sender, RoutedEventArgs e)
        {
            //createOrder.Visibility = Visibility.Visible;
            //Classes.ProductExtended productSelected = (Classes.ProductExtended)listBoxProduct.SelectedItem;
            //Classes.ProductInOrder productFind = Helper.productInOrders.Find(pr => pr.ProductExtended.Product.ProductID == productSelected.Product.ProductID);
            //if (productFind == null)
            //{
            //    Classes.ProductInOrder newProduct = new Classes.ProductInOrder();
            //    newProduct.ProductExtended = productSelected;
            //    newProduct.CountProductInOrder = 1;
            //    Helper.productInOrders.Add(newProduct);

            //}
            //else { productFind.CountProductInOrder++; } ;

            ///
            createOrder.Visibility = Visibility.Visible;
            int ind = listBoxProduct.SelectedIndex;
            Classes.ProductExtended productExtendedSelected = (Classes.ProductExtended)listBoxProduct.SelectedItem;

            //Поиск выбранного товара по артиклу в заказе
            Classes.ProductInOrder productInOrderFind = order.Find(pr => pr.ProductExtended.Product.ProductID == productExtendedSelected.Product.ProductID);
            //Товар найден увеличить количество
            if (productInOrderFind != null)
            {
                productInOrderFind.CountProductInOrder++;
            }//Товар не найден, добавить товар
            else
            {
                Classes.ProductInOrder productNew = new ProductInOrder(productExtendedSelected);
                order.Add(productNew);
            }

        }
    }



}

