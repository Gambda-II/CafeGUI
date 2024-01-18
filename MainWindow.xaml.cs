using System.CodeDom;
using System.ComponentModel;
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
using System.Windows.Threading;

namespace CafeGUI;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    int sum = 0;
    int globalCounter = 0;
    const int specialPrice = 300, cappuccinoPrice = 400;

    public MainWindow()
    {
        InitializeComponent();
        StartUp();

    }

    public void StartUp()
    {
        //tbPreparing.Visibility = Visibility.Hidden;
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
        string sign = "";

        if (value < 0)
        {
            tbDisplayMessage.Foreground = Brushes.Red;
        }
        else
        {
            tbDisplayMessage.Foreground = Brushes.LimeGreen;
            sign = "+";
        }

        if (value >= 100 || value <= -100)
        {
            tbDisplayMessage.Text = $" {sign} {value / 100} Euro";
        }
        else
        {
            tbDisplayMessage.Text = $" {sign} {value} Cent";
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
        //tbPreparing.Visibility = Visibility.Visible;
        tbPreparing.Text = $"Wird zubereitet! Bitte warten. \n{globalCounter * 10} %";

        // Wait
        var dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        StartMachine(dispatcherTimer);


        //tbDisplayMessage.Text = "Bitteschön!";
        //tbPreparing.Visibility = Visibility.Hidden;
    }

    private void StartMachine(DispatcherTimer dispatcherTimer)
    {

        SetButtonsVisibility(Visibility.Hidden);

        ChangeBackground("brewing");
        int secondsToIdle = 1;
        tbDisplayMessage.HorizontalAlignment = HorizontalAlignment.Left;
        dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
        dispatcherTimer.Interval = TimeSpan.FromSeconds(secondsToIdle);
        dispatcherTimer.Start();

        //dispatcherTimer.IsEnabled = false;
    }

    private void ChangeBackground(string v)
    {
        ImageBrush image = new ImageBrush();
        if (v == "brewing")
        {

        image.ImageSource = new BitmapImage(new Uri("A:\\GrundlagenOOP\\GUII\\coffee BG new brewing.png", UriKind.Relative));
        }
        else
        {
            image.ImageSource = new BitmapImage(new Uri("A:\\GrundlagenOOP\\GUII\\coffee BG new.png", UriKind.Relative));
        }
        wndMain.Background = image;
    }

    private void SetButtonsVisibility(Visibility v)
    {
        if (v == Visibility.Visible || v == Visibility.Hidden)
        {
            cnvMain.Children.OfType<Canvas>().ToList().ForEach(c => c.Children.OfType<Button>().ToList().ForEach(b => b.Visibility = v));
        }
    }

    private void StopMachine(DispatcherTimer dispatcherTimer)
    {
        ChangeBackground("reset");
        dispatcherTimer.Stop();
        dispatcherTimer.IsEnabled = false;
        tbDisplayMessage.Text = "Bitteschön!";
        globalCounter = 0;

        SetButtonsVisibility(Visibility.Visible);
    }


    private void dispatcherTimer_Tick(object? sender, EventArgs e)
    {
        // ▓▒░█
        tbDisplayMessage.Text = "";
        string loadingBar = "";//"░░░░░░░░░░";
        globalCounter++;

        tbPreparing.Text = $"Wird zubereitet! Bitte warten. \n{Math.Max(globalCounter * 10,0)} %";

        for (int i = 1; i <= globalCounter; i++)
        {
            loadingBar += "█";

        }
            tbDisplayMessage.Text = loadingBar.PadRight(10 , '░');

        if (globalCounter == 10)
        {
            StopMachine(sender as DispatcherTimer);
            return;
        }
        //tbPreparing.Text += "\nSchubidubidu";
    }

}