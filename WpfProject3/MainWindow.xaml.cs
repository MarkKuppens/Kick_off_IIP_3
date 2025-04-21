using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static WpfProject3.MainWindow;

namespace WpfProject3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string CourseFile = "courses.json";
        private readonly JsonSerializerOptions jsonOptions = new ()
        {
            WriteIndented = true,
            Converters = { new DateOnlyJsonConverter() }
        };

        private readonly Dictionary<string, int> courseAvailability = new ()
        {
         { "bandenmonteur", 10 },
         { "Boekhoudkundig Assistent", 8 },
         { "Brood- en banketbakker", 12 },
         { "Dakwerker", 0 },
         { "fietshersteller", 2 },
         { "Winkelverkoper", 9 }
        };
        private Course _tempSelectedCourse; 

        private string _tempSelectedCourseName;
        private CourseB _tempSelectedCourseNieuweCursus;

        // ObservableCollection to hold the courses
        public ObservableCollection<Course> Courses { get; set; }

        public Course Cursus { get; set; }

        private Course? _previousSelectedCourse;
        public class DateOnlyJsonConverter : JsonConverter<DateTime>
        {
            private readonly string _format = "yyyy-MM-dd";

            public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                return DateTime.ParseExact(reader.GetString(), _format, null);
            }

            public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
            {
                writer.WriteStringValue(value.ToString(_format));
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            if (File.Exists(CourseFile))
            {
                string json = File.ReadAllText(CourseFile);
                var loadedCourses = JsonSerializer.Deserialize<List<Course>>(json);
                Courses = new ObservableCollection<Course>(loadedCourses);
            }
            else
            {
                // Terugvallen op deze data (wanneer JSON niet bestaat)
                Courses =
             [
             new () { Name = "bandenmonteur", AvailablePlaces = 10 },
             new () { Name = "Boekhoudkundig Assistent", AvailablePlaces = 8 },
             new () { Name = "Brood- en banketbakker", AvailablePlaces = 12 },
             new () { Name = "Dakwerker", AvailablePlaces = 0 },
             new () { Name = "fietshersteller", AvailablePlaces = 1 },
             new () { Name = "Winkelverkoper", AvailablePlaces = 9 }
             ];
            }
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

        // Terugknoppen
        private void BtnTerug_Click(object sender, RoutedEventArgs e)
        {
            if (_tempSelectedCourse != null)
            {
                _tempSelectedCourse.AvailablePlaces++;
                _tempSelectedCourse = null;
                _previousSelectedCourse = null;
            }
            Grid1.Visibility = Visibility.Visible;
            Grid2.Visibility = Visibility.Hidden;
            ResetRadioButtons();
        }

        private void BtnTerug1_Click(object sender, RoutedEventArgs e)
        {
            if (_tempSelectedCourse != null)
            {
                _tempSelectedCourse.AvailablePlaces++;
                _tempSelectedCourse = null;
                _previousSelectedCourse = null;
            }
            Grid3.Visibility = Visibility.Collapsed;
            Grid2.Visibility = Visibility.Visible;
            ResetRadioButtons();
        }
        private void BtnTerug2_Click(object sender, RoutedEventArgs e)
        {
            Grid4.Visibility = Visibility.Collapsed;
            Grid1.Visibility = Visibility.Visible;
        }

        private void BtnTerug3_Click(object sender, RoutedEventArgs e)
        {
            Grid5.Visibility = Visibility.Collapsed;
            Grid4.Visibility = Visibility.Visible;

            // Deselecteer radiobuttons
            if (RbnCursus1B.IsChecked == true)
            {
                RbnCursus1B.IsChecked = false;
            }

            if (RbnCursus2B.IsChecked == true)
            {
                RbnCursus2B.IsChecked = false;
            }

            if (RbnCursus3B.IsChecked == true)
            {
                RbnCursus3B.IsChecked = false;
            }

            if (RbnCursus4B.IsChecked == true)
            {
                RbnCursus4B.IsChecked = false;
            }

            if (RbnCursus5B.IsChecked == true)
            {
                RbnCursus5B.IsChecked = false;
            }

            if (RbnCursus6B.IsChecked == true)
            {
                RbnCursus6B.IsChecked = false;
            }

            if (RbnCursus7B.IsChecked == true)
            {
                RbnCursus7B.IsChecked = false;
            }

            // Reset velden van de nieuwe cursus input
            TxtCursusNaam.Clear();
            CbxNiveau.SelectedIndex = -1;
            CbxLocatie.SelectedIndex = -1;
            TxtCursusBeschikbaar.Clear();
            TxtCursusStart.SelectedDate = null;
            TxtCursusEind.SelectedDate = null;

            // Reset tijdelijke variabelen
            _tempSelectedCourseName = null;
            _tempSelectedCourseNieuweCursus = null;
        }

        private void ResetRadioButtons()
        {
            RbnCursus1.IsChecked = false;
            RbnCursus2.IsChecked = false;
            RbnCursus3.IsChecked = false;
            RbnCursus4.IsChecked = false;
            RbnCursus5.IsChecked = false;
            RbnCursus6.IsChecked = false;
        }

        public class Course : INotifyPropertyChanged
        {
            private int availablePlaces;
            public string Name { get; set; }
            public int AvailablePlaces
            {
                get => availablePlaces;
                set
                {
                    if (availablePlaces != value)
                    {
                        availablePlaces = value;
                        OnPropertyChanged(nameof(AvailablePlaces));
                    }
                }
            }

            public event PropertyChangedEventHandler? PropertyChanged;
            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void BtnVerder_Click(object sender, RoutedEventArgs e)
        {
            if (_previousSelectedCourse == null)
            {
                MessageBox.Show("Selecteer eerst een cursus.", "Fout", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Course selectedCursus = _previousSelectedCourse;

            // Maak student aan
            _ = new

            // Maak student aan
            Student()
            {
                Voornaam = TxtVoornaam.Text,
                Achternaam = TxtAchternaam.Text,
                Straat = TxtStraat.Text,
                Huisnr = TxtHuisnr.Text,
                Toevoeging = TxtToevoeging.Text,
                Postcode = TxtPostcode.Text,
                Plaats = TxtPlaats.Text,
                Gsm = TxtGsm.Text,
                Email = TxtEmail.Text,
                Rijks = TxtRijks.Text,
                Cursus = selectedCursus.Name,
            };

            // Sla eventueel op in Application properties
            Application.Current.Properties["SelectedCursus"] = selectedCursus.Name;

            // Toon bevestiging met cursus en resterende plaatsen
            MessageBox.Show(
                $"Geselecteerde cursus: {selectedCursus.Name}\n" +
                $"Aantal resterende plaatsen: {selectedCursus.AvailablePlaces}",
                "Inschrijving voltooid",
                MessageBoxButton.OK,
                MessageBoxImage.Information);

            // Cursussen opnieuw opslaan na vermindering plaatsen
            File.WriteAllText(CourseFile, JsonSerializer.Serialize(Courses, jsonOptions));

            // Ga naar volgende stap (zoals je al deed)
            Grid3.Visibility = Visibility.Visible;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton radioButton && radioButton.DataContext is Course selectedCourse)
            {
                if (_previousSelectedCourse != selectedCourse)
                {
                    if (_previousSelectedCourse != null)
                    {
                        _previousSelectedCourse.AvailablePlaces++;
                    }

                    if (selectedCourse.AvailablePlaces > 0)
                    {
                        selectedCourse.AvailablePlaces--;
                        _previousSelectedCourse = selectedCourse;
                        _tempSelectedCourse = selectedCourse; // tijdelijke bewaar voor BtnSubmit1
                    }
                    else
                    {
                        MessageBox.Show("Geen beschikbare plaatsen meer voor deze cursus.");
                    }
                }

                BtnVerder.IsEnabled = true;
            }
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

            // Haal de geselecteerde cursus op uit de applicatie-instellingen
            string selectedCursus = Application.Current.Properties.Contains("SelectedCursus")
                ? Application.Current.Properties["SelectedCursus"].ToString()
                : null; // Null als er geen cursus is geselecteerd

            // Controleer of er een cursus is geselecteerd
            bool isCursusGeselecteerd = !string.IsNullOrEmpty(selectedCursus);

            // Indien geen cursus geselecteerd, toon melding
            if (!isCursusGeselecteerd)
            {
                selectedCursus = "Er is geen cursus geselecteerd, Kies een cursus a.u.b.";
                MessageBox.Show(selectedCursus, "Informatie", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            // De knop uitschakelen als er geen cursus is
            BtnVerder.IsEnabled = isCursusGeselecteerd;

            // Controleer of de postcode een geldig getal van 4 cijfers is
            if (!int.TryParse(TxtPostcode.Text, out _) || TxtPostcode.Text.Length != 4)
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
            if (TxtRijks.Text.Length != 11 || !long.TryParse(TxtRijks.Text, out _))
            {
                MessageBox.Show("Rijksregisternummer moet uit 11 cijfers bestaan.", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Aanmaken van een instantie van de Student klasse
            Student student = new ()
            {
                Voornaam = TxtVoornaam.Text,
                Achternaam = TxtAchternaam.Text,
                Straat = TxtStraat.Text,
                Huisnr = TxtHuisnr.Text,
                Toevoeging = TxtToevoeging.Text,
                Postcode = TxtPostcode.Text,
                Plaats = TxtPlaats.Text,
                Gsm = TxtGsm.Text,
                Email = TxtEmail.Text,
                Rijks = TxtRijks.Text,
                Cursus = selectedCursus
            };

            // Converteren van Student naar JSON
            string json = JsonSerializer.Serialize(student, jsonOptions);

            // Dynamisch aanmaken van de bestandsnaam van voor -en achternaam
            string fileName = $"{student.Voornaam}_{student.Achternaam}.json";
            string filePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), fileName);

            // Schrijf de JSON naar een bestand
            File.WriteAllText(filePath, json);
            MessageBox.Show($"Formulier succesvol verzonden en gegevens opgeslagen in {fileName} !", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);

            // Maak het scherm leeg na het verzenden
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

            this.Close();
        }
            
        public class Student
        {
            public string Voornaam { get; set; }
            public string Achternaam { get; set; }
            public string Straat { get; set; }
            public string Huisnr { get; set; }
            public string Toevoeging { get; set; }
            public string Postcode { get; set; }
            public string Plaats { get; set; }
            public string Gsm { get; set; }
            public string Email { get; set; }
            public string Rijks { get; set; }
            public string Cursus { get; set; }
        }

        // Validatie emailadres
        private static bool IsValidEmail(string email)
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
        public class CourseB
        {
            public string Name { get; set; }
            public string Level { get; set; }
            public string Location { get; set; }
            public int AvailablePlaces { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
        }

        // Verzenden knop Bedrijven Cursusgedeelte
        private void BtnSubmit2_Click(object sender, RoutedEventArgs e)
        {
            // Check of een van de radioButtons is geselecteerd
            if (RbnCursus1B.IsChecked == true ||
                 RbnCursus2B.IsChecked == true ||
                 RbnCursus3B.IsChecked == true ||
                 RbnCursus4B.IsChecked == true ||
                 RbnCursus5B.IsChecked == true ||
                 RbnCursus6B.IsChecked == true ||
                 RbnCursus7B.IsChecked == true)
            {
                // Check of de "nieuwe cursus" radioButton is geselecteerd
                if (RbnCursus7B.IsChecked == true)
                {
                    // Lees en valideer de ingevoerde gegevens
                    string naam = TxtCursusNaam.Text;
                    string niveau = (CbxNiveau.SelectedItem as ComboBoxItem)?.Content.ToString();
                    string locatie = (CbxLocatie.SelectedItem as ComboBoxItem)?.Content.ToString();
                    string beschikbarePlaatsenText = TxtCursusBeschikbaar.Text;
                    DateTime? beginDatum = TxtCursusStart.SelectedDate;
                    DateTime? eindDatum = TxtCursusEind.SelectedDate;

                    if (string.IsNullOrWhiteSpace(naam) || string.IsNullOrWhiteSpace(niveau) ||
                    string.IsNullOrWhiteSpace(locatie) || string.IsNullOrWhiteSpace(beschikbarePlaatsenText) ||
                    !beginDatum.HasValue || !eindDatum.HasValue)
                    {
                        MessageBox.Show("Gelieve alle velden correct in te vullen voordat je doorgaat.");
                        return;
                    }

                    if (!int.TryParse(beschikbarePlaatsenText, out int beschikbarePlaatsen))
                    {
                        MessageBox.Show("Beschikbare plaatsen moet een getal zijn.");
                        return;
                    }

                    // Sla op als Course object
                    _tempSelectedCourseNieuweCursus = new CourseB
                    {
                        Name = naam,
                        Level = niveau,
                        Location = locatie,
                        AvailablePlaces = beschikbarePlaatsen,
                        StartDate = beginDatum.Value,
                        EndDate = eindDatum.Value
                    };
                }
            }
            else
            {
                MessageBox.Show("Gelieve een radiobutton te selecteren.");
                return;
            }

            Grid4.Visibility = Visibility.Collapsed;
            Grid5.Visibility = Visibility.Visible;
        }

        private void RadioButtonB1_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton radioButton)
            {
                // Tijdelijke bewaar van de geselecteerde cursus
                string gekozenCursus = radioButton.Content.ToString();
                _tempSelectedCourseName = gekozenCursus; // Tijdelijke bewaar voor BtnSubmit3

                BtnVerder.IsEnabled = true;
            }
        }
        private void RadioButtonNieuweCursus_Checked(object sender, RoutedEventArgs e)
        {
            // Lees de ingevoerde waarden uit
            string naam = TxtCursusNaam.Text;
            string niveau = (CbxNiveau.SelectedItem as ComboBoxItem)?.Content.ToString();
            string locatie = (CbxLocatie.SelectedItem as ComboBoxItem)?.Content.ToString();
            string beschikbarePlaatsenText = TxtCursusBeschikbaar.Text;
            DateTime? beginDatum = TxtCursusStart.SelectedDate;
            DateTime? eindDatum = TxtCursusEind.SelectedDate;

            // Valideer input indien nodig
            if (string.IsNullOrWhiteSpace(naam) || string.IsNullOrWhiteSpace(niveau) ||
                string.IsNullOrWhiteSpace(locatie) || string.IsNullOrWhiteSpace(beschikbarePlaatsenText) ||
                !beginDatum.HasValue || !eindDatum.HasValue)
            {
                MessageBox.Show("Gelieve alle velden correct in te vullen voordat je doorgaat.");
                return;
            }

            if (!int.TryParse(beschikbarePlaatsenText, out int beschikbarePlaatsen))
            {
                MessageBox.Show("Beschikbare plaatsen moet een getal zijn.");
                return;
            }

            // Maak tijdelijke course object of sla in losse velden op
            _tempSelectedCourseNieuweCursus = new CourseB
            {
                Name = naam,
                Level = niveau,
                Location = locatie,
                AvailablePlaces = beschikbarePlaatsen,
                StartDate = beginDatum.Value,
                EndDate = eindDatum.Value
            };

            BtnSubmit2.IsEnabled = true;
        }

        // Verzenden knop Bedrijven
        private void BtnSubmit3_Click(object sender, RoutedEventArgs e)
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
            if (!int.TryParse(TxtBedrijfPostcode.Text, out _) || TxtBedrijfPostcode.Text.Length != 4)
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
            if (TxtKBO.Text.Length != 10 || !long.TryParse(TxtKBO.Text, out _) || (TxtKBO.Text[0] != '0' && TxtKBO.Text[0] != '1'))
            {
                MessageBox.Show("KBO-nummer moet uit 10 cijfers bestaan en beginnen met 0 of 1.", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Maak een nieuw Bedrijf object en vul het met de gegevens van de TextBoxen
            Bedrijf bedrijf = new ()
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

            object gekozenCursus = _tempSelectedCourseNieuweCursus != null
            ? (object)_tempSelectedCourseNieuweCursus
            : _tempSelectedCourseName;

            // Anoniem object
            var gegevens = new
            {
                Bedrijf = bedrijf,
                GekozenCursus = gekozenCursus
            };

            string json = JsonSerializer.Serialize(gegevens, jsonOptions);

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

            Grid4.Visibility = Visibility.Collapsed;
            Grid5.Visibility = Visibility.Visible;

            this.Close();
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

        // Update het eigenschapstype van `GekozenCursus` in de `Inschrijving` klasse om zowel `CursusB` als `string` typen te kunnen verwerken.
        public class Inschrijving
        {
            public Bedrijf Bedrijf { get; set; }
            public object GekozenCursus { get; set; } 
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

        // Knop wissen bedrijven cursus
        private void BtnClear2_Click(object sender, RoutedEventArgs e)
        {
            // Deselecteer radiobuttons
            if (RbnCursus1B.IsChecked == true)
            {
                RbnCursus1B.IsChecked = false;
            }

            if (RbnCursus2B.IsChecked == true)
            {
                RbnCursus2B.IsChecked = false;
            }

            if (RbnCursus3B.IsChecked == true)
            {
                RbnCursus3B.IsChecked = false;
            }

            if (RbnCursus4B.IsChecked == true)
            {
                RbnCursus4B.IsChecked = false;
            }

            if (RbnCursus5B.IsChecked == true)
            {
                RbnCursus5B.IsChecked = false;
            }

            if (RbnCursus6B.IsChecked == true)
            {
                RbnCursus6B.IsChecked = false;
            }

            if (RbnCursus7B.IsChecked == true)
            {
                RbnCursus7B.IsChecked = false;
            }

            // Reset velden van de nieuwe cursus input
            TxtCursusNaam.Clear();
            CbxNiveau.SelectedIndex = -1;
            CbxLocatie.SelectedIndex = -1;
            TxtCursusBeschikbaar.Clear();
            TxtCursusStart.SelectedDate = null;
            TxtCursusEind.SelectedDate = null;
        }

        // Knop wissen bedrijven
        private void BtnClear3_Click(object sender, RoutedEventArgs e)
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

        private void BtnStop_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
