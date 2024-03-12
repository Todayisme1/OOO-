using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using OOO_Восток;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using Assert = NUnit.Framework.Assert;
using OOO_Восток.View;

namespace UnitTestOOOVostok
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ExitAdmin()
        {
            // Создаем экземпляры окон
            var main = new OOO_Восток.MainWindow();
            var ord = new OOO_Восток.View.CatalogWindow();

            // Показываем главное окно
            main.Show();

            // Используем диспетчер, чтобы убедиться, что элементы UI обновлены перед выполнением действия
            Application.Current.Dispatcher.Invoke(() =>
            {
                // Находим кнопку в главном окне
                var button = (Button)main.FindName("gost");

                // Вызываем событие Click на кнопке
                button.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

                // Прячем главное окно
                main.Hide();

                // Показываем окно каталога
                ord.Show();
            });
        }
    }
}
