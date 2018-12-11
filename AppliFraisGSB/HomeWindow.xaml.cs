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

namespace AppliFraisGSB
{
    /// <summary>
    /// Logique d'interaction pour HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        public string Role { private get; set; }
        public HomeWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DoctorWindow main = new DoctorWindow();
            App.Current.MainWindow = main;
            this.Close();
            main.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CabinetWindow main = new CabinetWindow();
            App.Current.MainWindow = main;
            this.Close();
            main.Show();

        }
    }
}
