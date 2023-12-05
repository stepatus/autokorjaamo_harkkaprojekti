using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

using Microsoft.Data.SqlClient;


//Mustajärvi Teemu ja Esa Yritys yhteinen projekti
namespace Harkkaprojekti
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string solun_arvo;
        string polku = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\k2101816\\Documents\\tietokanta.mdf;Integrated Security=True;Connect Timeout=30";
        public MainWindow()
        {
            InitializeComponent();
            paivitaDataGrid("SELECT * FROM ASIAKKAAT", "ASIAKKAAT", asiakas_lista);
            paivitaDataGrid("SELECT * FROM VARAOSAT", "VARAOSAT", varaosat_tiedot);
            paivitaDataGrid("SELECT * FROM AUTOT", "AUTOT", auton_tiedot);

            paivitaDataGrid("SELECT ASIAKKAAT.id as id, ASIAKKAAT.nimi AS nimi, ASIAKKAAT.puhelinnro AS puhelinnumero, VARAOSAT.nimi AS varaosan_nimi, VARAOSAT.hinta AS hinta, AUTOT.merkki AS merkki, AUTOT.malli AS malli, HUOLTOTILAUS.toimitettu AS KUITTI FROM VARAOSAT, ASIAKKAAT, AUTOT, HUOLTOTILAUS", "HUOLTOTILAUS", huolto_tiedot);
            päivitäCombobox();
        }

        //Asiakkaan lisäys omaan tauluun
        private void painike_lisää_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection kanta = new SqlConnection(polku);
            kanta.Open();

            string sql = "INSERT INTO ASIAKKAAT (nimi, puhelinnro) VALUES ('" + asiakas_nimi.Text + "','" + asiakas_puhelin.Text + "')";

            SqlCommand komento = new SqlCommand(sql, kanta);
            komento.ExecuteNonQuery();

            kanta.Close();

            paivitaDataGrid("SELECT * FROM ASIAKKAAT", "ASIAKKAAT", asiakas_lista);
            päivitäCombobox();
        }


        //Varaosan lisääminen omaan tauluun
        private void varaosan_lisäys_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection kanta = new SqlConnection(polku);
            kanta.Open();

            string sql = "INSERT INTO VARAOSAT (nimi, hinta) VALUES ('" + varaosa_nimi.Text + "','" + hinta_varaosalle.Text + "')";

            SqlCommand komento = new SqlCommand(sql, kanta);
            komento.ExecuteNonQuery();

            kanta.Close();

            paivitaDataGrid("SELECT * FROM VARAOSAT", "VARAOSAT", varaosat_tiedot);
            päivitäCombobox();
        }


        //Auton lisääminen omaan tauluun
        private void auton_lisäys_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection kanta = new SqlConnection(polku);
            kanta.Open();

            string sql = "INSERT INTO AUTOT (merkki, malli, vuosimalli) VALUES ('" + auto_merkki.Text + "','" + auto_malli.Text + "'," + auto_vuosimalli.Text + ")";

            SqlCommand komento = new SqlCommand(sql, kanta);
            komento.ExecuteNonQuery();

            kanta.Close();

            paivitaDataGrid("SELECT * FROM AUTOT", "AUTOT", auton_tiedot);
            päivitäCombobox();
        }


        //Taulujen päivittäminen
        private void paivitaDataGrid(string kysely, string taulu, DataGrid grid)
        {
            SqlConnection kanta = new SqlConnection(polku);
            kanta.Open();

            /*tehdään sql komento*/
            SqlCommand komento = kanta.CreateCommand();
            komento.CommandText = kysely; // kysely

            /*tehdään data adapteri ja taulu johon tiedot haetaan*/
            SqlDataAdapter adapteri = new SqlDataAdapter(komento);
            DataTable dt = new DataTable(taulu);
            adapteri.Fill(dt);

            /*sijoitetaan data-taulun tiedot DataGridiin*/
            grid.ItemsSource = dt.DefaultView;
            kanta.Close();
        }


        //Comboboxien päivittäminen aina tiedon lisäämisen jälkeen
        private void päivitäCombobox() 
        {
            SqlConnection kanta = new SqlConnection(polku);
            kanta.Open();

            SqlCommand komento = new SqlCommand("SELECT * from ASIAKKAAT", kanta);
            SqlDataReader lukija = komento.ExecuteReader();

            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(string));
            dt.Columns.Add("nimi", typeof(string));

            combo_asiakkaat.ItemsSource = dt.DefaultView;
            combo_asiakkaat.DisplayMemberPath = "nimi";
            combo_asiakkaat.SelectedValuePath = "ID";

            combo_asiakkaat2.ItemsSource = dt.DefaultView;
            combo_asiakkaat2.DisplayMemberPath = "nimi";
            combo_asiakkaat2.SelectedValuePath = "ID";

            while (lukija.Read())
            {
                int id = lukija.GetInt32(0);
                string tuote = lukija.GetString(1);
                dt.Rows.Add(id, tuote);
            }
            lukija.Close();


            //autot boxin täyttö
            SqlCommand komento2 = new SqlCommand("SELECT * from AUTOT", kanta);
            SqlDataReader lukija2 = komento2.ExecuteReader();

            dt = new DataTable();
            dt.Columns.Add("ID", typeof(string));
            dt.Columns.Add("merkki", typeof(string));

            combo_autot.ItemsSource = dt.DefaultView;
            combo_autot.DisplayMemberPath = "merkki";
            combo_autot.SelectedValuePath = "ID";

            combo_autot2.ItemsSource = dt.DefaultView;
            combo_autot2.DisplayMemberPath = "merkki";
            combo_autot2.SelectedValuePath = "ID";

            while (lukija2.Read())
            {
                int id = lukija2.GetInt32(0);
                string tuote = lukija2.GetString(1);
                dt.Rows.Add(id, tuote);
            }
            lukija2.Close();


            //varaosa boxin täyttö
            SqlCommand komento3 = new SqlCommand("SELECT * from VARAOSAT", kanta);
            SqlDataReader lukija3 = komento3.ExecuteReader();

            dt = new DataTable();
            dt.Columns.Add("ID", typeof(string));
            dt.Columns.Add("nimi", typeof(string));

            combo_varaosat.ItemsSource = dt.DefaultView;
            combo_varaosat.DisplayMemberPath = "nimi";
            combo_varaosat.SelectedValuePath = "ID";

            combo_varaosat2.ItemsSource = dt.DefaultView;
            combo_varaosat2.DisplayMemberPath = "nimi";
            combo_varaosat2.SelectedValuePath = "ID";

            while (lukija3.Read())
            {
                int id = lukija3.GetInt32(0);
                string tuote = lukija3.GetString(1);
                dt.Rows.Add(id, tuote);
            }
            lukija3.Close();
            kanta.Close();
        }


        //Ylimääräisten tietojen poistamninen tietokannasta
        private void painike_poista_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection kanta = new SqlConnection(polku);
            kanta.Open();

            //asiakastietojen poisto
            if (combo_asiakkaat2.SelectedValue != null) 
            {
                string id = combo_asiakkaat2.SelectedValue.ToString();
                SqlCommand komento = new SqlCommand("DELETE from ASIAKKAAT WHERE id = " + id + ";", kanta);
                komento.ExecuteNonQuery();
            }

            //autojen poisto
            if (combo_autot2.SelectedValue != null)
            {
                string id2 = combo_autot2.SelectedValue.ToString();
                SqlCommand komento2 = new SqlCommand("DELETE from AUTOT WHERE id = " + id2 + ";", kanta);
                komento2.ExecuteNonQuery();
            }

            //varaosatietojen poisto
            if (combo_varaosat2.SelectedValue != null) 
            {
                string id3 = combo_varaosat2.SelectedValue.ToString();
                SqlCommand komento3 = new SqlCommand("DELETE from VARAOSAT WHERE id = " + id3 + ";", kanta);
                komento3.ExecuteNonQuery();
            }
            kanta.Close();

            päivitäCombobox();
            paivitaDataGrid("SELECT * FROM ASIAKKAAT", "ASIAKKAAT", asiakas_lista);
            paivitaDataGrid("SELECT * FROM AUTOT", "AUTOT", auton_tiedot);
            paivitaDataGrid("SELECT * FROM VARAOSAT", "VARAOSAT", varaosat_tiedot);
        }


        //Huollon lisääminen huoltolistaan
        private void huolto_lisäys_Click(object sender, RoutedEventArgs e)
        {

            SqlConnection kanta = new SqlConnection(polku);
            kanta.Open();

            string asiakasID = combo_asiakkaat.SelectedValue.ToString();
            string autoID = combo_autot.SelectedValue.ToString();
            string varaosaID = combo_varaosat.SelectedValue.ToString();

            string sql = "INSERT INTO HUOLTOTILAUS (asiakas_id, auto_id, varosa_id) VALUES ('" + asiakasID + "', '" + autoID + "', '" + varaosaID + "')";

            SqlCommand komento = new SqlCommand(sql, kanta);
            komento.ExecuteNonQuery();
            kanta.Close();

            paivitaDataGrid("SELECT ASIAKKAAT.id as id, ASIAKKAAT.nimi AS nimi, ASIAKKAAT.puhelinnro AS puhelinnumero, VARAOSAT.nimi AS varaosan_nimi, VARAOSAT.hinta AS hinta, AUTOT.merkki AS merkki, AUTOT.malli AS malli, HUOLTOTILAUS.toimitettu AS KUITTI FROM VARAOSAT, ASIAKKAAT, AUTOT, HUOLTOTILAUS", "HUOLTOTILAUS", huolto_tiedot);
        }
    }
}
