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
using MySql.Data.MySqlClient;

namespace AppliFraisGSB
{
    /// <summary>
    /// Logique d'interaction pour DoctorWindow.xaml
    /// </summary>
    public partial class DoctorWindow : Window
    {
        List<Medecin> users = new List<Medecin>();

        public DoctorWindow()
        {
                InitializeComponent();
                string connectionString = "SERVER=localhost;DATABASE=appli_frais;UID=root;PASSWORD=;";
                MySqlConnection connection = new MySqlConnection(connectionString);
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM medecin;", connection);
                connection.Open();
                MySqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    
                    users.Add(new Medecin((int)read["id_medecin"], read["prenom"].ToString(), read["nom"].ToString(), read["mail"].ToString(), read["telephone"].ToString(), read["mdp"].ToString(), read["sexe"].ToString()) );
                }

                connection.Close();

                dgUsers.ItemsSource = users;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            HomeWindow main = new HomeWindow();
            App.Current.MainWindow = main;
            this.Close();
            main.Show();
        }
    }

}
