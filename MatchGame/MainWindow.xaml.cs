namespace MatchGame
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Threading;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new();
        int tenthsOfSecondsElapsed;
        int matchesFound;

        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;

        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            tenthsOfSecondsElapsed++;
            timeTextBlock.Text = (tenthsOfSecondsElapsed / 10F).ToString("0.0s");

            if (matchesFound == 8)
            {
                timer.Stop();
                timeTextBlock.Text = timeTextBlock.Text + " - Play again?";
                ChangeButtonLabel();
                CalcBestTime();
            }
        }

        private void SetUpGame()
        {
            List<string> animalEmoji = new()
            {
                "🐦‍🔥","🐦‍🔥",
                "🦩","🦩",
                "🦤","🦤",
                "🦕","🦕",
                "🦖","🦖",
                "🦣","🦣",
                "🐋","🐋",
                "🐇","🐇",
            };

            Random random = new();

            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
            {
                if (textBlock.Name == "timeTextBlock" || textBlock.Name == "bestTimeTextBlock")
                {
                    continue;
                }

                textBlock.Visibility = Visibility.Visible;
                int index = random.Next(animalEmoji.Count);
                string nextEmoji = animalEmoji[index];
                textBlock.Text = nextEmoji;
                animalEmoji.RemoveAt(index);
            }

            timer.Start();
            tenthsOfSecondsElapsed = 0;
            matchesFound = 0;
        }

        TextBlock lastTextBlockClicked;
        bool findingMatch = false;

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;

            if (!findingMatch)
            {
                textBlock.Visibility = Visibility.Hidden;
                lastTextBlockClicked = textBlock;
                findingMatch = true;
            }
            else if (textBlock.Text == lastTextBlockClicked.Text)
            {
                matchesFound++;
                textBlock.Visibility = Visibility.Hidden;
                findingMatch = false;
            }
            else
            {
                lastTextBlockClicked.Visibility = Visibility.Visible;
                findingMatch = false;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            if (button.Content.ToString() == "Start")
            {
                SetUpGame();
            }
            else
            {
                timer.Stop();
                CalcBestTime();
            }

            ChangeButtonLabel();
        }

        private void ChangeButtonLabel()
        {
            BrushConverter brushConverter = new();
            startStopButton.Background = startStopButton.Background.ToString() == "#FFB6FF9C"
                ? (Brush)brushConverter.ConvertFrom("#FFFF9C9C")
                : (Brush)brushConverter.ConvertFrom("#FFB6FF9C");

            startStopButton.Content = startStopButton.Content.ToString() == "Start" ? "Stop" : "Start";
        }

        float bestTime = 0;

        private void CalcBestTime()
        {
            var currentTime = (tenthsOfSecondsElapsed / 10F);

            if (matchesFound == 8)
            {
                bestTime = bestTime == 0 ? currentTime :
                    bestTime < currentTime ? bestTime : currentTime;
            }

            bestTimeTextBlock.Text = bestTime.ToString("0.0s");
        }
    }
}