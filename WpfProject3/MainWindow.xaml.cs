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
    }
}