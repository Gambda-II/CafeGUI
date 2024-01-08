using System.CodeDom;
using System.Diagnostics;
using System.Text;
using System.Timers;
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
    const int specialPrice = 300, cappuccinoPrice = 400;

    public MainWindow()
    {
        InitializeComponent();
        StartUp();

    }

    public void StartUp()
    {
        tbPreparing.Visibility = Visibility.Hidden;
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

        if (value < 0)
        {
            tbDisplayMessage.Foreground = Brushes.Red;
        }
        else
        {
            tbDisplayMessage.Foreground = Brushes.LimeGreen;
        }

        if (value >= 100 || value <= -100)
        {
            tbDisplayMessage.Text = $" {value / 100} Euro";
        }
        else
        {
            tbDisplayMessage.Text = $" {value} Cent";
        }
        UpdateTotal();
    }

    private void UpdateTotal()
    {
        string euro = (sum / 100).ToString();
        string cent = (sum % 100).ToString("00");

        string total = euro + "," + cent + " €";
        tbDisplayTotal.Text = "Summe: " + total;
    }

    private void btnSpecialCoffee_Click(object sender, RoutedEventArgs e)
    {
        if (IsSumBiggerThanPrice(specialPrice))
        {
            AddValueToSum(-specialPrice);
            PrepareCoffee();
            return;
        }
        tbDisplayMessage.Foreground = Brushes.Red;
        tbDisplayMessage.Text = "Mehr Geld einzahlen";
    }

    private void btnCappuccino_Click(object sender, RoutedEventArgs e)
    {
        if (IsSumBiggerThanPrice(cappuccinoPrice))
        {
            AddValueToSum(-cappuccinoPrice);
            PrepareCoffee();
            return;
        }
        tbDisplayMessage.Text = "Mehr Geld einzahlen";
    }

    private bool IsSumBiggerThanPrice(int price)
    {
        return price <= sum;
    }

    private void PrepareCoffee()
    {
        tbPreparing.Visibility = Visibility.Visible;
        tbPreparing.Text = "Wird zubereitet! Bitte warten.";

        // Wait
        StartMachine();


        tbDisplayMessage.Text = "Bitteschön!";
        tbPreparing.Visibility = Visibility.Hidden;
    }

    private async void StartMachine()
    {

    }

}