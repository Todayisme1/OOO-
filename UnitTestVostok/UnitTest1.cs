using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System;
using System.Threading;
using OOO_Восток;
using OOO_Восток.View;

using System.Windows.Controls;
using System.Windows;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using System.Data.SqlClient;

namespace UnitTestVostok
{
    [Apartment(ApartmentState.STA)]
    [TestFixture]
    [TestClass]
    public class UnitTest1
    {
        //проверка ввода поля логина

        [TestMethod]
        public void CorrectLogin()
        {
            //Arrange
            var logIn = new MainWindow();
            var login = (TextBox)logIn.FindName("Login");
            login.Text = "Admin";
            //Act
            var loginCurrent = (TextBox)logIn.FindName("Login");
            //var edi
            //Assert
            Assert.IsTrue(loginCurrent.Text == login.Text);
        }

        //проверка ввода поля пароля 
        [TestMethod]
        public void CorrectPassword()
        {
            //Arrange
            var main = new MainWindow();
            var password = (PasswordBox)main.FindName("PasswordDot");
            password.Password = "Admin";
            //Act
            var passwordCurrent = (PasswordBox)main.FindName("PasswordDot");
           
            //Assert
            Assert.IsTrue(passwordCurrent.Password == password.Password);
        }

        //проверка подключения к БД
        [TestMethod]
        public void NowDB()
        {
            //Arrange
            var strConnect = "Server=localhost;Database=Vostok;Integrated Security=True;";
            //Act
            var connect = new SqlConnection(strConnect);
            connect.Open();
            //Assert
            Assert.IsTrue(connect.State == System.Data.ConnectionState.Open);
        }
       
        //При старте поля логин и пароль пустые

        [TestMethod]
        public void NullLogPassInStart()
        {
            //Arrange
            var main = new MainWindow();
            var login = (TextBox)main.FindName("Login");
            var password = (PasswordBox)main.FindName("PasswordDot");
            //Assert
            Assert.IsTrue(login.Text == "" && password.Password == "");
        }


        // Окно авторизации открывается и закрывается без ошибок

        [TestMethod]
        public void OpenCloseMainWindow()
        {
            //Arrange
            var main = new MainWindow();
            //Act
            main.Show();
            main.Close();
        }

    }


}
