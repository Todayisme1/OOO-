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

namespace OOO_Восток
{
    /// <summary>
    /// Логика взаимодействия для BlockWindow.xaml
    /// </summary>
    public partial class BlockWindow : Window
    {
        public BlockWindow()
        {
            InitializeComponent();
            tick();
        }

        private async void tick()
        {
            TimeSpan ts = new TimeSpan(0, 0, 10);

            while (ts > TimeSpan.Zero)
            {
                Message.Text = "Окно разблокируется через: " + ts.Seconds.ToString();
                await Task.Delay(1000);
                ts -= TimeSpan.FromSeconds(1);
            }
            Message.Text = "Нажмите ok";
            okButton.Visibility = Visibility.Visible;
        }


        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
