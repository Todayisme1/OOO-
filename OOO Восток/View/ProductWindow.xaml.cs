using Microsoft.Win32;
using OOO_Восток.Classes;
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

namespace OOO_Восток.View
{
    /// <summary>
    /// Логика взаимодействия для ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        
        OpenFileDialog dlg = new OpenFileDialog();
        bool isPhoto = false;		//Наличие фото
        string filePath;			//Путь к фото из диалога
        //Путь к папке с фотографиями
        string pathPhoto = System.IO.Directory.GetCurrentDirectory() + @"\Images\";
        Model.Products product;	//Товар, с которым сейчас работают

        public ProductWindow()
        {
            InitializeComponent();
            //tbTitle.Text = "Добавление товара";
            tbArt.IsEnabled = true;	//Доступен артикль для заполнения
            tbDiscr.Clear();
        }
     

        /// Конструктор окна с параметром - при редактировании товара
        /// <param name="productSelected">Переданный товар</param>
        public ProductWindow(Classes.ProductExtended productSelected)
        {
           
            InitializeComponent();
            //tbTitle.Text = "Редактирование товара";
            //Отобразить фото
            imagePhoto.Source = new BitmapImage(new Uri(productSelected.ProductPathPhoto, UriKind.RelativeOrAbsolute));
            //Блокировать кнопку изменения фото
            butSelectPhoto.Visibility = Visibility.Collapsed;
            //Информация о продукте
            product = productSelected.Product;
            //tbArt.Text = product.ProductID.ToString;
            tbArt.IsEnabled = false;		//Блокировать артикль
            //Все остальные поля товара вывести в элементы интерфейса
            tbName.Text = product.ProductName;
            cbManuf.SelectedValue = product.Produser;
           
            cbCat.SelectedValue = product.Category;
            tbCost.Text = product.Price.ToString();
            
            tbDisc.Text = product.Sale.ToString();
           
            tbDiscr.Text = product.Comment;
        }

        /// Подготовительные действия
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Заполнение и настройка списков из БД
            cbManuf.ItemsSource = Helper.DB.Producers.ToList();
            cbManuf.DisplayMemberPath = "ProducerName";
            cbManuf.SelectedValuePath = "ProducerID";
            cbManuf.SelectedIndex = 0;
           
            cbCat.ItemsSource = Helper.DB.Categories.ToList();
            cbCat.DisplayMemberPath = "CategoryName";
            cbCat.SelectedValuePath = "CategoryID";
            cbCat.SelectedIndex = 0;
           
            //Настройка диалога
            dlg.InitialDirectory = @"C:\Users\user\Pictures\";
            dlg.Filter = "Image files (*.png)|*.png";
        }

        private void butSelectPhoto_Click(object sender, RoutedEventArgs e)
        {
            if (dlg.ShowDialog() == true)
            {
                filePath = dlg.FileName;
                //Отобразить фото в компоненте
                imagePhoto.Source = new BitmapImage(new Uri(filePath, UriKind.Absolute));
                isPhoto = true;		 //Есть фото
            }
        }

        /// Сохранить в БД
        private void butSaveInDB_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();	//Строка с сообщениями
            sb.Clear();
          
            //Проверка непустых значений для обязательных полей
            if (String.IsNullOrEmpty(tbName.Text))
            { sb.Append("Вы не ввели название." + Environment.NewLine); }
            if (String.IsNullOrEmpty(tbCost.Text))
            { sb.Append("Вы не ввели цену." + Environment.NewLine); }
            if (String.IsNullOrEmpty(tbDisc.Text))
            { sb.Append("Вы не ввели скидку." + Environment.NewLine); }
                      
            if (sb.Length > 0)			//Есть сообщения об ошибках
            {
                MessageBox.Show(sb.ToString());
            }
            else					//Ошибок нет
            {
                if (tbArt.IsEnabled)		//При добавлении
                {
                    product = new Model.Products();		//Создаем добавляемый объект
                   /* product.ProductArticle = tbArt.Text;*/		//Внимание на артикль и фото
                    //Есть фото
                    if (isPhoto)
                    {
                        product.ProductPhoto = product.ProductID + ".png";	 //Для записи в БД
                        string newPath = pathPhoto + product.ProductPhoto;	//Полное имя файла цели
                        System.IO.File.Copy(filePath, newPath, true); //Копирование из диалога в целевую
                    }
                }
                //Получаем значения для всех остальных полей при добавлении/редактировании
                product.ProductName = tbName.Text;
                product.Produser = (int)cbManuf.SelectedValue;
               
                product.Category = (int)cbCat.SelectedValue;
               
                product.Price = (int)Convert.ToDouble(tbCost.Text);
              
                product.Sale = Convert.ToInt32(tbDisc.Text);
               
                product.Comment = tbDiscr.Text;
                if (tbArt.IsEnabled)				//При добавлении
                {
                    Helper.DB.Products.Add(product);		//Добавить в кэш новую запись
                }
                try
                {
                    Helper.DB.SaveChanges();		//Обновить БД и при добавлении/редактировании
                    MessageBox.Show("БД успешно обновлена");
                }
                catch
                {
                    MessageBox.Show("При обновлении БД возникли проблемы");
                }
                this.Close();
            }
        }

    

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
