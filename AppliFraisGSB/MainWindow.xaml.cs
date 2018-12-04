using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Data;
using System.Windows.Input;
using System.Windows.Media;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AppliFraisGSB
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Personne secretaire;

        public MainWindow()
        {
            InitializeComponent();
            string connectionString = "SERVER=localhost;DATABASE=appli_frais;UID=root;PASSWORD=;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM secretaire;", connection);
            connection.Open();
            MySqlDataReader read = cmd.ExecuteReader();
            read.Read();
            secretaire = new Personne(read["prenom"].ToString(), read["nom"].ToString(), read["mail"].ToString(), read["telephone"].ToString(), read["mdp"].ToString(), read["sexe"].ToString());
            connection.Close();

        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (textName.Text == secretaire.mail && textPassword.Password == secretaire.password)
            {
                HomeWindow main = new HomeWindow();
                App.Current.MainWindow = main;
                this.Close();
                main.Show();
            }
        }
    }
}
