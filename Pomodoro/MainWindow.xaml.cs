using System;
using System.Windows;
using System.Windows.Threading;

namespace Pomodoro
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TimeSpan time;
        private int minutes = 25;
        private int seconds = 0;
        private DispatcherTimer timer;
        private int interval = 1;
        private string separator = ":";
        private string previousSeconds = "00";
        private bool running = false;

        public MainWindow()
        {
            InitializeComponent();
            time = new TimeSpan(0, 0, minutes, seconds);
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            if (running)
            {
                Start.Content = FindResource("Play");
                Reset.IsEnabled = true;
                Reset.Content = FindResource("Reset");
                running = false;
                timer.Stop();
            }
            else
            {
                if (Display.Text == "Done")
                {
                    Reset_Click(this, null);
                }

                Start.Content = FindResource("Pause");

                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(interval);
                timer.Tick += new EventHandler(timer_Tick);
                timer.Start();
                running = true;
                Reset.IsEnabled = false;
                Reset.Content = FindResource("Reset_Disabled");
            }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            //separator = (separator == ":") ? " " : ":";

            time = time.Subtract(new TimeSpan(0, 0, 0, interval));
            if (time <= new TimeSpan(0,0,0,0))
            {
                Display.Text = "Done";
                Start.IsEnabled = true;
                Start.Content = FindResource("Play");
                running = false;
                Reset.IsEnabled = true;
                Reset.Content = FindResource("Reset");
                timer.Stop();
            }
            else
            {
                int numberOfSeconds = time.Seconds;
                if (numberOfSeconds % interval == 0)
                {
                    Display.Text = time.Minutes.ToString() + separator + time.Seconds.ToString().PadLeft(2, '0');
                    previousSeconds = numberOfSeconds.ToString();
                }
                else
                {
                    Display.Text = time.Minutes.ToString() + separator + previousSeconds;
                }
                
            }
            
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            running = false;
            Display.Text = "25:00";
            time = new TimeSpan(0, 0, minutes, seconds);
            Start.Content = FindResource("Play");
        }
    }
}
