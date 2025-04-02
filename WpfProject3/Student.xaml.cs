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
            public string Rijksregisternummer { get; set; }
        }
    }