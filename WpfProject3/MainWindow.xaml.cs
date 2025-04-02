using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;

namespace WpfProject3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Buttons 
        private void BtnStudent_Click(object sender, RoutedEventArgs e)
        {
            Grid1.Visibility = Visibility.Collapsed;
            Grid2.Visibility = Visibility.Visible;
        }

        private void BtnCompany_Click(object sender, RoutedEventArgs e)
        {
            Grid1.Visibility = Visibility.Collapsed;
            Grid4.Visibility = Visibility.Visible;
        }

        private void BtnTerug_Click(object sender, RoutedEventArgs e)
        {
            Grid2.Visibility = Visibility.Collapsed;
            Grid1.Visibility = Visibility.Visible;
        }

        private void BtnTerug1_Click(object sender, RoutedEventArgs e)
        {
            Grid3.Visibility = Visibility.Collapsed;
            Grid2.Visibility = Visibility.Visible;
        }

        private void BtnTerug2_Click(object sender, RoutedEventArgs e)
        {
            Grid4.Visibility = Visibility.Collapsed;
            Grid1.Visibility = Visibility.Visible;
        }

        private void BtnVerder_Click(object sender, RoutedEventArgs e)
        {
            string selectedCursus = string.Empty;

            if (RbnCursus1.IsChecked == true)
            {
                selectedCursus = RbnCursus1.Content.ToString();
            }
            else if (RbnCursus2.IsChecked == true)
            {
                selectedCursus = RbnCursus2.Content.ToString();
            }
            else if (RbnCursus3.IsChecked == true)
            {
                selectedCursus = RbnCursus3.Content.ToString();
            }
            else if (RbnCursus4.IsChecked == true)
            {
                selectedCursus = RbnCursus4.Content.ToString();
            }
            else if (RbnCursus5.IsChecked == true)
            {
                selectedCursus = RbnCursus5.Content.ToString();
            }
            else if (RbnCursus6.IsChecked == true)
            {
                selectedCursus = RbnCursus6.Content.ToString();
            }

            // Opslaan van de geselecteerde waarde in een sessie of variabele
            Application.Current.Properties["SelectedCursus"] = selectedCursus;

            // Toon de waarde in een popup venster
            MessageBox.Show("Geselecteerde cursus: " + selectedCursus, "Informatie", MessageBoxButton.OK, MessageBoxImage.Information);

            // Navigeren naar de volgende form of actie uitvoeren
            Grid3.Visibility = Visibility.Visible;
        }

        // Verzenden knop studenten
        private void BtnSubmit1_Click(object sender, RoutedEventArgs e)
        {
            // Controleer of alle TextBoxen zijn ingevuld
            if (string.IsNullOrWhiteSpace(TxtVoornaam.Text) ||
                string.IsNullOrWhiteSpace(TxtAchternaam.Text) ||
                string.IsNullOrWhiteSpace(TxtStraat.Text) ||
                string.IsNullOrWhiteSpace(TxtHuisnr.Text) ||
                string.IsNullOrWhiteSpace(TxtPostcode.Text) ||
                string.IsNullOrWhiteSpace(TxtPlaats.Text) ||
                string.IsNullOrWhiteSpace(TxtGsm.Text) ||
                string.IsNullOrWhiteSpace(TxtEmail.Text) ||
                string.IsNullOrWhiteSpace(TxtRijks.Text))
            {
                MessageBox.Show("Alle velden moeten ingevuld zijn.", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Controleer of de postcode een geldig getal van 4 cijfers is
            if (!int.TryParse(TxtPostcode.Text, out int postcode) || TxtPostcode.Text.Length != 4)
            {
                MessageBox.Show("Postcode moet een geldig getal van 4 cijfers zijn.", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Controleer of het e-mailadres geldig is
            if (!IsValidEmail(TxtEmail.Text))
            {
                MessageBox.Show("Voer een geldig e-mailadres in.", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Controleer of het rijksregisternummer uit 11 cijfers bestaat
            if (TxtRijks.Text.Length != 11 || !long.TryParse(TxtRijks.Text, out long rijksregisternummer))
            {
                MessageBox.Show("Rijksregisternummer moet uit 11 cijfers bestaan.", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Als alle validaties slagen, kun je de gegevens verwerken
            MessageBox.Show("Formulier succesvol verzonden!", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        // Verzenden knop Bedrijven
        private void BtnSubmit2_Click(object sender, RoutedEventArgs e)
        {
            // Controleer of alle TextBoxen zijn ingevuld
            if (string.IsNullOrWhiteSpace(TxtBedrijfsNaam.Text) ||
                string.IsNullOrWhiteSpace(TxtRechtsVorm.Text) ||
                string.IsNullOrWhiteSpace(TxtBedrijfStraat.Text) ||
                string.IsNullOrWhiteSpace(TxtBedrijfHuisnr.Text) ||
                string.IsNullOrWhiteSpace(TxtBedrijfPostcode.Text) ||
                string.IsNullOrWhiteSpace(TxtBedrijfPlaats.Text) ||
                string.IsNullOrWhiteSpace(TxtTelefoon.Text) ||
                string.IsNullOrWhiteSpace(TxtBedrijfEmail.Text) ||
                string.IsNullOrWhiteSpace(TxtKBO.Text))
            {
                MessageBox.Show("Alle velden moeten ingevuld zijn.", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Controleer of de postcode een geldig getal van 4 cijfers is
            if (!int.TryParse(TxtBedrijfPostcode.Text, out int postcode) || TxtBedrijfPostcode.Text.Length != 4)
            {
                MessageBox.Show("Postcode moet een geldig getal van 4 cijfers zijn.", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Controleer of het e-mailadres geldig is
            if (!IsValidEmail(TxtBedrijfEmail.Text))
            {
                MessageBox.Show("Voer een geldig e-mailadres in.", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Controleer of het KBO-nummer uit 10 cijfers bestaat en begint met 0 of 1
            if (TxtKBO.Text.Length != 10 || !long.TryParse(TxtKBO.Text, out long kboNummer) || (TxtKBO.Text[0] != '0' && TxtKBO.Text[0] != '1'))
            {
                MessageBox.Show("KBO-nummer moet uit 10 cijfers bestaan en beginnen met 0 of 1.", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Maak een nieuw Bedrijf object en vul het met de gegevens van de TextBoxen
            Bedrijf bedrijf = new Bedrijf
            {
                BedrijfsNaam = TxtBedrijfsNaam.Text,
                RechtsVorm = TxtRechtsVorm.Text,
                Straat = TxtBedrijfStraat.Text,
                Huisnr = TxtBedrijfHuisnr.Text,
                Toevoeging = TxtBedrijfToevoeging.Text,
                Postcode = TxtBedrijfPostcode.Text,
                Plaats = TxtBedrijfPlaats.Text,
                Telefoon = TxtTelefoon.Text,
                Email = TxtBedrijfEmail.Text,
                KBO = TxtKBO.Text
            };

            // Converteer het Bedrijf object naar JSON
            string json = JsonConvert.SerializeObject(bedrijf, Formatting.Indented);

            // Toon de JSON-string in een MessageBox om te testen
            MessageBox.Show(json, "Bedrijfsgegevens", MessageBoxButton.OK, MessageBoxImage.Information);

            // Maak de bestandsnaam dynamisch op basis van bedrijfsnaam
            string fileName = $"{bedrijf.BedrijfsNaam}.json";
            string filePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), fileName);

            // Schrijf de JSON naar een bestand
            File.WriteAllText(filePath, json);
            MessageBox.Show($"Formulier succesvol verzonden en gegevens opgeslagen in {fileName} !", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);

            // Maak het scherm leeg na het verzenden
            TxtBedrijfsNaam.Clear();
            TxtRechtsVorm.Clear();
            TxtBedrijfStraat.Clear();
            TxtBedrijfHuisnr.Clear();
            TxtBedrijfToevoeging.Clear();
            TxtBedrijfPostcode.Clear();
            TxtBedrijfPlaats.Clear();
            TxtTelefoon.Clear();
            TxtBedrijfEmail.Clear();
            TxtKBO.Clear();
        }

        public class Bedrijf
        {
            public string BedrijfsNaam { get; set; }
            public string RechtsVorm { get; set; }
            public string Straat { get; set; }
            public string Huisnr { get; set; }
            public string Toevoeging { get; set; }
            public string Postcode { get; set; }
            public string Plaats { get; set; }
            public string Telefoon { get; set; }
            public string Email { get; set; }
            public string KBO { get; set; }
        }

        // Knop wissen studenten
        private void BtnClear1_Click(object sender, RoutedEventArgs e)
        {
            // Maak alle TextBoxen leeg
            TxtVoornaam.Clear();
            TxtAchternaam.Clear();
            TxtStraat.Clear();
            TxtHuisnr.Clear();
            TxtToevoeging.Clear();
            TxtPostcode.Clear();
            TxtPlaats.Clear();
            TxtGsm.Clear();
            TxtEmail.Clear();
            TxtRijks.Clear();
        }

        // Knop wissen bedrijven
        private void BtnClear2_Click(object sender, RoutedEventArgs e)
        {
            // Maak alle TextBoxen leeg
            TxtBedrijfsNaam.Clear();
            TxtRechtsVorm.Clear();
            TxtBedrijfStraat.Clear();
            TxtBedrijfHuisnr.Clear();
            TxtBedrijfToevoeging.Clear();
            TxtBedrijfPostcode.Clear();
            TxtBedrijfPlaats.Clear();
            TxtTelefoon.Clear();
            TxtBedrijfEmail.Clear();
            TxtKBO.Clear();
        }
    }
}
