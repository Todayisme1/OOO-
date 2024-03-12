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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OOO_Восток
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int myTry = 0;
        public MainWindow()
        {
            Application.ResourceAssembly = typeof(MainWindow).Assembly;
            InitializeComponent();
        }

        public void Window_Loaded(object sender, RoutedEventArgs e)
        {

            //Подключение к БД
            Classes.Helper.DB = new Model.DBVostok();

        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void gost_Click(object sender, RoutedEventArgs e)
        {
            gotoCatalog();
        }

        private void gotoCatalog()
        {
            View.CatalogWindow catalogWindow = new View.CatalogWindow();
            this.Hide();
            catalogWindow.ShowDialog();
            this.Show();
        }

        public void login_Click(object sender, RoutedEventArgs e)
        {

            string login = Login.Text;
            string password = "";
            if (PasswordText.Visibility == Visibility.Visible)
            {
                password = PasswordText.Text;
            }
            if (PasswordDot.Visibility == Visibility.Visible)
            {
                password = PasswordDot.Password;
            }
            StringBuilder strMessage = new StringBuilder();
            if (login == "") strMessage.Append("Введите логин\n\n");
            if (password == "") strMessage.Append("Введите пароль\n\n");

            //Сообщение об ошибке
            if (strMessage.Length > 0)
            {
                MessageBox.Show(strMessage.ToString(), "Ошибка");
                return;
            }

            Helper.users = Helper.DB.Users.ToList().Where(u => u.Login == login && u.Password == password).FirstOrDefault();


            if (Helper.users != null)
            {
                if (myTry >= 1)
                {
                    if (captcha.CaptchaText.ToString() == CaptchaText.Text.ToString())
                    {
                        MessageBox.Show(Helper.users.UserName + " " + Helper.users.Role.ToString() + " " + Helper.users.Roles.RoleName);
                        gotoCatalog();
                    }
                    else if (myTry > 1)
                    {
                        BlockWindow blockWindow = new BlockWindow();
                        blockWindow.ShowDialog();

                        myTry++;
                    }
                    else
                    {
                        myTry++;
                        MessageBox.Show("Капча введена неверно");
                    }
                    captcha.CreateCaptcha(EasyCaptcha.Wpf.Captcha.LetterOption.Alphanumeric, 4);
                }
                else
                {
                    MessageBox.Show(Helper.users.UserName + " " + Helper.users.Roles.RoleName);
                    gotoCatalog();
                }
            }
            else
            {
                if (myTry > 1)
                {
                    if (captcha.CaptchaText.ToString() == CaptchaText.Text.ToString())
                    {
                        MessageBox.Show(Helper.users.UserName + " " + Helper.users.Role.ToString() + " " + Helper.users.Roles.RoleName);
                    }
                    else
                    {
                        myTry++;
                        BlockWindow blockWindow = new BlockWindow();
                        blockWindow.ShowDialog();
                    }
                    captcha.CreateCaptcha(EasyCaptcha.Wpf.Captcha.LetterOption.Alphanumeric, 4);
                }
                else
                {
                    MessageBox.Show("Пользователь не найден", "Ошибка");
                    myTry++;
                    captcha.Visibility = Visibility.Visible;
                    CaptchaText.Visibility = Visibility.Visible;
                    captcha.CreateCaptcha(EasyCaptcha.Wpf.Captcha.LetterOption.Alphanumeric, 4);
                }
            }

        }

        private void visiblePassword_Checked(object sender, RoutedEventArgs e)
        {
            PasswordText.Text = PasswordDot.Password.ToString();
            PasswordText.Visibility = Visibility.Visible;
            PasswordDot.Visibility = Visibility.Hidden;
        }

        private void visiblePassword_Unchecked(object sender, RoutedEventArgs e)
        {
            PasswordDot.Password = PasswordText.Text.ToString();
            PasswordDot.Visibility = Visibility.Visible;
            PasswordText.Visibility = Visibility.Hidden;
        }

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            captcha.Visibility = Visibility.Hidden;
            CaptchaText.Visibility = Visibility.Hidden;
        }

    }
}

