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
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using MySql.Data.MySqlClient;

namespace AppliFraisGSB
{
    /// <summary>
    /// Logique d'interaction pour StatistiqueWindow.xaml
    /// </summary>
    public partial class StatistiqueWindow : Window
    {
        string connectionString = "SERVER=localhost;DATABASE=appli_frais;UID=root;PASSWORD=;";
        public StatistiqueWindow()
        {
            InitializeComponent();
            day.Content = getDayVisite();
            total.Content = getTotalVisite();
            SetListMedecin();
            SetListVisiteur();
            if(AppContextUtility.Role == "medecin" || AppContextUtility.Role == "visiteur")
            {
                ComboMedecin.Visibility = Visibility.Hidden;
                ComboVisiteur.Visibility = Visibility.Hidden;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            HomeWindow main = new HomeWindow();
            App.Current.MainWindow = main;
            this.Close();
            main.Show();
        }

        public string getTotalVisite()
        {
            string numberVisite = "";
            MySqlConnection connection = new MySqlConnection(connectionString);

            if(AppContextUtility.Role == "medecin")
            {
                MySqlCommand cmd = new MySqlCommand("SELECT count(*) as nbvisite FROM visites WHERE id_medecin = @id_medecin ;", connection);
                cmd.Parameters.AddWithValue("@id_medecin", AppContextUtility.Id);
                connection.Open();
                MySqlDataReader read = cmd.ExecuteReader();
                read.Read();

                numberVisite = read["nbvisite"].ToString();

                connection.Close();

            }
            if(AppContextUtility.Role == "visiteur")
            {
                MySqlCommand cmd = new MySqlCommand("SELECT count(*) as nbvisite FROM visites WHERE id_visiteur = @id_visiteur ;", connection);
                cmd.Parameters.AddWithValue("@id_visiteur", AppContextUtility.Id);
                connection.Open();
                MySqlDataReader read = cmd.ExecuteReader();
                read.Read();

                numberVisite = read["nbvisite"].ToString();

                connection.Close();
            }
            if(AppContextUtility.Role == "secretaire")
            {
                MySqlCommand cmd = new MySqlCommand("SELECT count(*) as nbvisite FROM visites ;", connection);
                connection.Open();
                MySqlDataReader read = cmd.ExecuteReader();
                read.Read();

                numberVisite = read["nbvisite"].ToString();

                connection.Close();
            }
            
            return "Nombre total de visites: "+numberVisite;
        }

        private void SetListMedecin()
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("SELECT nom,prenom FROM medecin;", connection);
            connection.Open();
            MySqlDataReader read = cmd.ExecuteReader();
            while (read.Read())
            {
                ComboMedecin.Items.Add(read["nom"].ToString()+" "+read["prenom"].ToString());
            }

            connection.Close();
        }

        private void SetListVisiteur()
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("SELECT nom,prenom FROM visiteur;", connection);
            connection.Open();
            MySqlDataReader read = cmd.ExecuteReader();
            while (read.Read())
            {
                ComboVisiteur.Items.Add(read["nom"].ToString() + " " + read["prenom"].ToString());
            }

            connection.Close();
        }





        public string getDayVisite()
        {
            string numberVisite = "";
            MySqlConnection connection = new MySqlConnection(connectionString);

            if (AppContextUtility.Role == "medecin")
            {
                MySqlCommand cmd = new MySqlCommand("SELECT count(*) as nbvisite FROM visites WHERE id_medecin = @id_medecin ;", connection);
                cmd.Parameters.AddWithValue("@id_medecin", AppContextUtility.Id);
                connection.Open();
                MySqlDataReader read = cmd.ExecuteReader();
                read.Read();

                numberVisite = read["nbvisite"].ToString();

                connection.Close();

            }
            if (AppContextUtility.Role == "visiteur")
            {
                MySqlCommand cmd = new MySqlCommand("SELECT count(*) as nbvisite FROM visites WHERE id_visiteur = @id_visiteur ;", connection);
                cmd.Parameters.AddWithValue("@id_visiteur", AppContextUtility.Id);
                connection.Open();
                MySqlDataReader read = cmd.ExecuteReader();
                read.Read();

                numberVisite = read["nbvisite"].ToString();

                connection.Close();
            }
            if (AppContextUtility.Role == "secretaire")
            {
                MySqlCommand cmd = new MySqlCommand("SELECT count(*) as nbvisite FROM visites WHERE DAY(heureDebutEntr) = DAY(NOW())", connection);
                connection.Open();
                MySqlDataReader read = cmd.ExecuteReader();
                read.Read();

                numberVisite = read["nbvisite"].ToString();

                connection.Close();
            }

            return "Visites totales d'aujourd'hui : "+numberVisite;
        }

        private void Medecin_Changed(object sender, SelectionChangedEventArgs e)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("SELECT count(*) as nbvisite FROM visites,medecin WHERE DAY(heureDebutEntr) = DAY(NOW()) AND visites.id_medecin = medecin.id_medecin AND nom = @nom ;", connection);
            cmd.Parameters.AddWithValue("@nom", parseNom((sender as ComboBox).SelectedItem as string));
            connection.Open();
            MySqlDataReader read = cmd.ExecuteReader();
            read.Read();

            LabelMedecin.Content = "Nombre de visites aujourd'hui : "+read["nbvisite"].ToString();

            connection.Close();
        }

        private void Visiteur_Changed(object sender, SelectionChangedEventArgs e)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("SELECT count(*) as nbvisite FROM visites,visiteur WHERE DAY(heureDebutEntr) = DAY(NOW()) AND visites.id_visiteur = visiteur.id_visiteur AND nom = @nom ;", connection);
            cmd.Parameters.AddWithValue("@nom", parseNom((sender as ComboBox).SelectedItem as string));
            connection.Open();
            MySqlDataReader read = cmd.ExecuteReader();
            read.Read();

            LabelVisiteur.Content = "Nombre de visites aujourd'hui : " + read["nbvisite"].ToString();

            connection.Close();
        }

        private string parseNom(string nom)
        {
            string realName= "";
            for(int i = 0; i < nom.Length; i++)
            {
                if(nom[i] == ' ')
                {
                    break;
                }
                realName += nom[i];
            }
            return realName;
        }

    }
}
