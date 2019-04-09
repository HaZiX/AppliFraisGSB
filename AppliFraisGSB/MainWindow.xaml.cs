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

        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void SetUser(string Role, MySqlDataReader read,int id)
        {
            if (textName.Text == read["mail"].ToString() && textPassword.Password == read["mdp"].ToString())
            {
                AppContextUtility.Role = Role;
                AppContextUtility.Id = id;
                AppContextUtility.Nom = read["nom"].ToString();
                AppContextUtility.Prenom = read["prenom"].ToString();
                HomeWindow main = new HomeWindow();
                App.Current.MainWindow = main;
                this.Close();
                main.Show();
            }
            
        }

        private void CheckUser()
        {
            string connectionString = "SERVER=localhost;DATABASE=appli_frais;UID=root;PASSWORD=;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM secretaire;", connection);
            connection.Open();
            MySqlDataReader read = cmd.ExecuteReader();
            while (read.Read())
            {
                this.SetUser("secretaire", read, (int)read["id_secretaire"]);
             }

            connection.Close();

            MySqlCommand cmd1 = new MySqlCommand("SELECT * FROM medecin;", connection);
            connection.Open();
            MySqlDataReader read1 = cmd1.ExecuteReader();
            while (read1.Read())
            {
                this.SetUser("medecin", read1, (int)read1["id_medecin"]);
            }

            connection.Close();

            MySqlCommand cmd2 = new MySqlCommand("SELECT * FROM visiteur;", connection);
            connection.Open();
            MySqlDataReader read2 = cmd2.ExecuteReader();
            while (read2.Read())
            {
                this.SetUser("visiteur", read2, (int)read2["id_visiteur"]);
            }

            connection.Close();

        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.CheckUser();
        }
    }
}
