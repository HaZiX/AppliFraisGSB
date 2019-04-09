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
    /// Logique d'interaction pour VisiteWindow.xaml
    /// </summary>
    public partial class VisiteWindow : Window
    {
        string connectionString = "SERVER=localhost;DATABASE=appli_frais;UID=root;PASSWORD=;";
        public VisiteWindow()
        {
            InitializeComponent();
            dgUsers.ItemsSource = this.GetListVisite();
            this.SetListCabinet();
            if (AppContextUtility.Role == "medecin")
            {
                ButtonAdd1.Visibility = Visibility.Hidden;
                dgUsers.IsReadOnly = true;
            }
        }

        private void Button_Back(object sender, RoutedEventArgs e)
        {
            HomeWindow main = new HomeWindow();
            App.Current.MainWindow = main;
            this.Close();
            main.Show();
        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            DialogVisit.IsOpen = true;
            ButtonCreate.Visibility = Visibility.Visible;
            dgUsers.IsEnabled = false;
            ButtonAdd1.IsEnabled = false;
            ButtonBack1.IsEnabled = false;
            dialogLabel.Content = "Ajouter une visite";
        }
        private void Button_Close(object sender, RoutedEventArgs e)
        {
            DialogVisit.IsOpen = false;
            ButtonCreate.Visibility = Visibility.Hidden;
            ButtonUpdate.Visibility = Visibility.Hidden;
            dgUsers.IsEnabled = true;
            ButtonAdd1.IsEnabled = true;
            ButtonBack1.IsEnabled = true;
            ResetForm();

        }

        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            Visite selectedClient = (Visite)dgUsers.SelectedItem;
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("Delete FROM visites WHERE id_visite = @id_visite",connection);
            cmd.Parameters.AddWithValue("@id_visite", selectedClient.id.ToString());
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();

        }



        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (AppContextUtility.Role != "medecin")
            {
                DialogVisit.IsOpen = true;
                dialogLabel.Content = "Modifier une visite";
                ButtonUpdate.Visibility = Visibility.Visible;
                dgUsers.IsEnabled = false;
                ButtonAdd1.IsEnabled = false;
                ButtonBack1.IsEnabled = false;
                Visite selectedClient = (Visite)dgUsers.SelectedItem;
                id.Text = selectedClient.id.ToString();
                heureDebutEntr.Text = selectedClient.heureDebut;
                heureDepartCab.Text = selectedClient.heureDepart;
                idVisiteur.Text = selectedClient.id_visiteur.ToString();
                idMedecin.Text = selectedClient.id_medecin.ToString();
                test.IsChecked = false;
            }
            
        }

        private List<Visite> GetListVisite()
        {
            List<Visite> visites = new List<Visite>();
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM visites;", connection);
            connection.Open();
            MySqlDataReader read = cmd.ExecuteReader();
            while (read.Read())
            {
                visites.Add(new Visite((int)read["id_visite"], (bool)read["estRDV"], read["heureDebutEntr"].ToString(), read["heureDepartCab"].ToString(), (int)read["id_medecin"], (int)read["id_visiteur"]));
            }
            connection.Close();
            return visites;
        }

        private void CreateVisite(object sender, RoutedEventArgs e)
        {
            Visite visite = new Visite((bool)test.IsChecked, heureDebutEntr.Text, heureDepartCab.Text, Convert.ToInt32(idMedecin.Text), Convert.ToInt32(idVisiteur.Text));
            try
            {
                MySqlConnection connection = new MySqlConnection(connectionString);
                MySqlCommand cmd = new MySqlCommand("INSERT INTO visites(estRdv,heureDebutEntr,heureDepartCab,id_medecin,id_visiteur) VALUES (@estRDV,@heureDebutEntr,@heureDepartCab,@id_medecin,@id_visiteur);", connection);
                cmd.Parameters.AddWithValue("@estRDV", visite.estRdv);
                cmd.Parameters.AddWithValue("@heureDebutEntr", visite.heureDebut);
                cmd.Parameters.AddWithValue("@heureDepartCab", visite.heureDepart);
                cmd.Parameters.AddWithValue("@id_medecin", visite.id_medecin);
                cmd.Parameters.AddWithValue("@id_visiteur", visite.id_visiteur);


                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
                ResetForm();
                DialogVisit.IsOpen = false;
                dgUsers.IsEnabled = true;
                ButtonAdd1.IsEnabled = true;
                ButtonBack1.IsEnabled = true;
                ButtonCreate.Visibility = Visibility.Hidden;
                ButtonUpdate.Visibility = Visibility.Hidden;
                dgUsers.ItemsSource = null;
                dgUsers.ItemsSource = this.GetListVisite();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SetListCabinet()
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("Select id_visiteur From visiteur", connection);
            connection.Open();
            MySqlDataReader read = cmd.ExecuteReader();
            while (read.Read())
            {
                idVisiteur.Items.Add(read["id_visiteur"].ToString());
            }

            connection.Close();

            MySqlConnection connection1 = new MySqlConnection(connectionString);
            MySqlCommand cmd1 = new MySqlCommand("Select id_medecin From medecin", connection1);
            connection1.Open();
            MySqlDataReader read1 = cmd1.ExecuteReader();

            while (read1.Read())
            {
                idMedecin.Items.Add(read1["id_medecin"].ToString());
            }
        }

        private void UpdateVisite(object sender, RoutedEventArgs e)
        {
            int isRDV = 0;
            if (test.IsChecked == true)
            {
                isRDV = 1;
            }
            string dateSQL = Format(heureDebutEntr.Text);
            string dateSQL1 = Format(heureDepartCab.Text);
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("UPDATE visites set estRDV = @estRDV, heureDebutEntr = @heureDebutEntr, heureDepartCab = @heureDepartCab,id_medecin = @id_medecin, id_visiteur = @id_visiteur WHERE id_visite = @id", connection);
            cmd.Parameters.AddWithValue("@estRDV", isRDV);
            cmd.Parameters.AddWithValue("@heureDebutEntr", dateSQL);
            cmd.Parameters.AddWithValue("@heureDepartCab", dateSQL1);
            cmd.Parameters.AddWithValue("@id_medecin", idMedecin.Text);
            cmd.Parameters.AddWithValue("@id_visiteur", idVisiteur.Text);
            cmd.Parameters.AddWithValue("@id", id.Text);


            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
            ResetForm();
            DialogVisit.IsOpen = false;
            dgUsers.IsEnabled = true;
            ButtonAdd1.IsEnabled = true;
            ButtonBack1.IsEnabled = true;
            ButtonCreate.Visibility = Visibility.Hidden;
            ButtonUpdate.Visibility = Visibility.Hidden;
            dgUsers.ItemsSource = null;
            dgUsers.ItemsSource = this.GetListVisite();

        }




        public string Format(string date)
        {
            string annee;
            string mois;
            string jour;
            string dateFormat;
            string heure;
            string minute;
            string seconde;
            annee = date[6].ToString() + date[7].ToString() + date[8].ToString() + date[9].ToString();
            mois = date[3].ToString() + date[4].ToString();
            jour = date[0].ToString() + date[1].ToString();
            heure = date[11].ToString() + date[12].ToString();
            minute = date[14].ToString() + date[15].ToString();
            seconde = date[17].ToString() + date[18].ToString();
            dateFormat = annee + "-" + mois + "-" + jour + " " + heure + ":" + minute + ":" + seconde;
            return dateFormat;
        }

        private void ResetForm()
        {
            heureDebutEntr.Text = "";
            heureDepartCab.Text = "";
            idMedecin.Text = "";
            idVisiteur.Text = "";
            test.IsChecked = false;
        }
    }
}
