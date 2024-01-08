using System.Diagnostics;
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

namespace CafeGUI;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    int sum = 0;

    public MainWindow()
    {
        InitializeComponent();
        StartUp();

    }

    public void StartUp()
    {
        wndMain.Background.Opacity = 1.0;
    }

    private void Button_MouseEnter(object sender, MouseEventArgs e)
    {
        //MessageBox.Show("Hallo");

    }

    private void btnTenCent_Click(object sender, RoutedEventArgs e)
    {
        AddValueToSum(10);
    }

    private void btnTwentyCent_Click(object sender, RoutedEventArgs e)
    {
        AddValueToSum(20);
    }

    private void btnFiftyCent_Click(object sender, RoutedEventArgs e)
    {
        AddValueToSum(50);
    }

    private void btnOneEuro_Click(object sender, RoutedEventArgs e)
    {
        AddValueToSum(100);
    }

    private void btnTwoEuro_Click(object sender, RoutedEventArgs e)
    {
        AddValueToSum(200);
    }

    private void AddValueToSum(int value)
    {
        sum += value;
        tbDisplayMessage.Text = "";
        if (value >= 100)
        {
            tbDisplayMessage.Text = $"Added {value / 100} Euro";
        }
        else
        {
            tbDisplayMessage.Text = $"Added {value} Cent";
        }
        UpdateTotal();
    }

    private void UpdateTotal()
    {
        string euro = (sum / 100).ToString();
        string cent = (sum % 100).ToString("00");

        string total = euro + "," + cent + " €";
        tbDisplayTotal.Text = "Einbezahlt: " + total;
    }
}