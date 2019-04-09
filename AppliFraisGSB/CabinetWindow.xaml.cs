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
    /// Logique d'interaction pour CabinetWindow.xaml
    /// </summary>
    public partial class CabinetWindow : Window
    {
        string connectionString = "SERVER=localhost;DATABASE=appli_frais;UID=root;PASSWORD=;";
        public CabinetWindow()
        {
            InitializeComponent();
            dgUsers.ItemsSource = this.GetListCabinet();

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
            DialogCabinet.IsOpen = true;
            ButtonCreate.Visibility = Visibility.Visible;
            dgUsers.IsEnabled = false;
            ButtonAdd1.IsEnabled = false;
            ButtonBack1.IsEnabled = false;
            dialogLabel.Content = "Ajouter un cabinet";
        }
        private void Button_Close(object sender, RoutedEventArgs e)
        {
            DialogCabinet.IsOpen = false;
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
            MySqlCommand cmd = new MySqlCommand("Delete FROM visites WHERE id_visite = @id_visite", connection);
            cmd.Parameters.AddWithValue("@id_visite", selectedClient.id.ToString());
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }



        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (AppContextUtility.Role != "medecin")
            {
                DialogCabinet.IsOpen = true;
                dialogLabel.Content = "Modifier un cabinet";
                ButtonUpdate.Visibility = Visibility.Visible;
                dgUsers.IsEnabled = false;
                ButtonAdd1.IsEnabled = false;
                ButtonBack1.IsEnabled = false;
                Cabinet selectedClient = (Cabinet)dgUsers.SelectedItem;
                id.Text = selectedClient.IdCabinet.ToString();
                adresse.Text = selectedClient.Adresse;
                coordonnees.Text = selectedClient.Coordonnees;
            }

        }

        private List<Cabinet> GetListCabinet()
        {
            List<Cabinet> cabinets = new List<Cabinet>();
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM cabinet;", connection);
            connection.Open();
            MySqlDataReader read = cmd.ExecuteReader();
            while (read.Read())
            {
                cabinets.Add(new Cabinet((int)read["id_cabinet"],read["adresse"].ToString(), read["coordonnees"].ToString()));
            }
            connection.Close();
            return cabinets;
        }

        private void CreateCabinet(object sender, RoutedEventArgs e)
        {
            Cabinet cabinet = new Cabinet(adresse.Text, coordonnees.Text);
            try
            {
                MySqlConnection connection = new MySqlConnection(connectionString);
                MySqlCommand cmd = new MySqlCommand("INSERT INTO cabinet(adresse, coordonnees) VALUES (@adresse, @coordonnees);", connection);
                cmd.Parameters.AddWithValue("@adresse", cabinet.Adresse);
                cmd.Parameters.AddWithValue("@coordonnees", cabinet.Coordonnees);


                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
                ResetForm();
                DialogCabinet.IsOpen = false;
                dgUsers.IsEnabled = true;
                ButtonAdd1.IsEnabled = true;
                ButtonBack1.IsEnabled = true;
                ButtonCreate.Visibility = Visibility.Hidden;
                ButtonUpdate.Visibility = Visibility.Hidden;
                dgUsers.ItemsSource = null;
                dgUsers.ItemsSource = this.GetListCabinet();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        

        private void UpdateCabinet(object sender, RoutedEventArgs e)
        {
            
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("UPDATE cabinet set adresse = @adresse, coordonnees = @coordonnees WHERE id_cabinet = @id", connection);
            cmd.Parameters.AddWithValue("@adresse", adresse.Text);
            cmd.Parameters.AddWithValue("@coordonnees", coordonnees.Text);
            cmd.Parameters.AddWithValue("@id", id.Text);


            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
            ResetForm();
            DialogCabinet.IsOpen = false;
            dgUsers.IsEnabled = true;
            ButtonAdd1.IsEnabled = true;
            ButtonBack1.IsEnabled = true;
            ButtonCreate.Visibility = Visibility.Hidden;
            ButtonUpdate.Visibility = Visibility.Hidden;
            dgUsers.ItemsSource = null;
            dgUsers.ItemsSource = this.GetListCabinet();

        }





        private void ResetForm()
        {
            adresse.Text = "";
            coordonnees.Text = "";
        }
    }

}
