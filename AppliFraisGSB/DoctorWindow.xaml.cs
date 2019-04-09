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
        string connectionString = "SERVER=localhost;DATABASE=appli_frais;UID=root;PASSWORD=;";
        

        public DoctorWindow()
        {
                InitializeComponent();
                
                dgUsers.ItemsSource = this.GetListMedecin();
                this.SetListCabinet();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            HomeWindow main = new HomeWindow();
            App.Current.MainWindow = main;
            this.Close();
            main.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DialogDoctor.IsOpen = true;
            ButtonCreate.Visibility = Visibility.Visible;
            dgUsers.IsEnabled = false ;
            ButtonAdd.IsEnabled = false;
            ButtonBack.IsEnabled = false;
            dialogLabel.Content = "Ajouter un médecin";
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            DialogDoctor.IsOpen = false;
            ButtonCreate.Visibility = Visibility.Hidden;
            ButtonUpdate.Visibility = Visibility.Hidden;
            dgUsers.IsEnabled = true;
            ButtonAdd.IsEnabled = true;
            ButtonBack.IsEnabled = true;
            ResetForm();

        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            DialogDoctor.IsOpen = true;
            dialogLabel.Content = "Modifier un médecin";
            ButtonUpdate.Visibility = Visibility.Visible;
            dgUsers.IsEnabled = false;
            ButtonAdd.IsEnabled = false;
            ButtonBack.IsEnabled = false;
            Medecin selectedClient = (Medecin)dgUsers.SelectedItem;
            id.Text = selectedClient.id.ToString();
            prenom.Text = selectedClient.name;
            nom.Text = selectedClient.surname;
            mail.Text = selectedClient.mail;
            telephone.Text = selectedClient.phone;
            mdp.Password = selectedClient.password;
            date.Text = selectedClient.dateNaissance;
            idCabinet.Text = selectedClient.IdCabinet.ToString();
            test.IsChecked = false;
        }

        private List<Medecin> GetListMedecin()
        {
            List<Medecin> users = new List<Medecin>();
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM medecin;", connection);
            connection.Open();
            MySqlDataReader read = cmd.ExecuteReader();
            while (read.Read())
            {
                users.Add(new Medecin((int)read["id_medecin"], read["prenom"].ToString(), read["nom"].ToString(), read["mail"].ToString(), read["telephone"].ToString(), read["mdp"].ToString(), read["sexe"].ToString(), read["date_naissance"].ToString(), (int)read["id_cabinet"]));
            }

            connection.Close();
            return users;
        }

        private void CreateMedecin(object sender, RoutedEventArgs e)
        {
            string isFemme = "Homme";
            if(test.IsChecked == true)
            {
                isFemme = "Femme";
            }
            Medecin medecin = new Medecin(prenom.Text, nom.Text, mail.Text, telephone.Text, mdp.Password, isFemme,date.Text, Convert.ToInt32(idCabinet.Text));
            try
            {
                MySqlConnection connection = new MySqlConnection(connectionString);
                MySqlCommand cmd = new MySqlCommand("INSERT INTO medecin(prenom,nom,mail,telephone,mdp,sexe,date_naissance,id_cabinet) VALUES (@prenom, @nom, @mail, @telephone, @mdp, @sexe, @dateNaissance, @idCabinet);", connection);
                cmd.Parameters.AddWithValue("@prenom", medecin.name);
                cmd.Parameters.AddWithValue("@nom", medecin.surname);
                cmd.Parameters.AddWithValue("@mail", medecin.mail);
                cmd.Parameters.AddWithValue("@telephone", medecin.phone);
                cmd.Parameters.AddWithValue("@mdp", medecin.password);
                cmd.Parameters.AddWithValue("@sexe", medecin.sexe);
                cmd.Parameters.AddWithValue("@dateNaissance", medecin.dateNaissance);
                cmd.Parameters.AddWithValue("@idCabinet", medecin.IdCabinet);
                
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
                ResetForm();
                DialogDoctor.IsOpen = false;
                dgUsers.IsEnabled = true;
                ButtonAdd.IsEnabled = true;
                ButtonBack.IsEnabled = true;
                ButtonCreate.Visibility = Visibility.Hidden;
                ButtonUpdate.Visibility = Visibility.Hidden;
                dgUsers.ItemsSource = null;
                dgUsers.ItemsSource = this.GetListMedecin();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SetListCabinet()
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("SELECT id_cabinet FROM cabinet;", connection);
            connection.Open();
            MySqlDataReader read = cmd.ExecuteReader();
            while (read.Read())
            {
                idCabinet.Items.Add(read["id_cabinet"].ToString());
            }

            connection.Close();
        }

        private void UpdateMedecin(object sender, RoutedEventArgs e)
        {
            string isFemme = "Homme";
            if (test.IsChecked == true)
            {
                isFemme = "Femme";
            }
            string dateSQL = Format(date.Text);
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("UPDATE medecin set prenom = @prenom, nom = @nom, mail = @mail,telephone = @telephone, mdp = @mdp,sexe = @sexe,date_naissance = @dateNaissance,id_cabinet = @idCabinet WHERE id_medecin = @id", connection);
            cmd.Parameters.AddWithValue("@prenom", prenom.Text);
            cmd.Parameters.AddWithValue("@nom", nom.Text);
            cmd.Parameters.AddWithValue("@mail", mail.Text);
            cmd.Parameters.AddWithValue("@telephone", telephone.Text);
            cmd.Parameters.AddWithValue("@mdp", mdp.Password);
            cmd.Parameters.AddWithValue("@sexe", isFemme);
            cmd.Parameters.AddWithValue("@dateNaissance", dateSQL);
            cmd.Parameters.AddWithValue("@idCabinet", idCabinet.Text);
            cmd.Parameters.AddWithValue("@id", id.Text);


            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
            ResetForm();
            DialogDoctor.IsOpen = false;
            dgUsers.IsEnabled = true;
            ButtonAdd.IsEnabled = true;
            ButtonBack.IsEnabled = true;
            ButtonCreate.Visibility = Visibility.Hidden;
            ButtonUpdate.Visibility = Visibility.Hidden;
            dgUsers.ItemsSource = null;
            dgUsers.ItemsSource = this.GetListMedecin();

        }

        private string Format(string date)
        {
            string annee;
            string mois;
            string jour;
            string dateFormat;
            annee = date[6].ToString() + date[7].ToString() + date[8].ToString() + date[9].ToString();
            mois = date[3].ToString() + date[4].ToString();
            jour = date[0].ToString() + date[1].ToString();
            dateFormat =annee + "-" + mois + "-" + jour;
            return dateFormat;
        }

        private void ResetForm()
        {
            prenom.Text = "";
            nom.Text = "";
            mail.Text = "";
            telephone.Text = "";
            mdp.Password = "";
            date.Text = "";
            idCabinet.Text = "";
            test.IsChecked = false;
        }        
    }

}
