using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TypingGame.GameLogic;

namespace TypingGame.Pages
{
    /// <summary>
    /// Interaction logic for GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
    {
        private readonly MainWindow parent;
        private Game? game;
        private readonly int letterChoice;
        private readonly int difficultyChoice;
        private readonly System.Diagnostics.Stopwatch clocks;
        private readonly Timer timer;
        private bool played;
        public GamePage(MainWindow mainWindow, int letterChoice, int difficultyChoice)
        {
            InitializeComponent();
            this.parent = mainWindow;
            this.letterChoice = letterChoice;
            this.difficultyChoice = difficultyChoice;
            this.timer = new(UpdateTime, null, 0, 1000);
            clocks = new System.Diagnostics.Stopwatch();
            TimeShow.Content = "0:00";
            LivesShow.Content = "0";
            played = false;
        }

        private void UpdateTime(object? state)
        {
            if (clocks != null)//posunie čas a zistí stav hry, prípadne ju ukončí
            {
                string minutes = clocks.Elapsed.Minutes.ToString();
                string seconds = clocks.Elapsed.Seconds.ToString();
                if (clocks.Elapsed.Seconds < 10)
                {
                    seconds = "0" + seconds;
                }
                
                Dispatcher.Invoke(() =>
                {
                    TimeShow.Content = minutes + ":" + seconds;
                });
                Dispatcher.Invoke(() =>
                {
                    if (game?.Lives <= 0)
                    {
                        LivesShow.Content = "0";
                        game.Lives = 0;
                    }
                    else
                    {
                        LivesShow.Content = game?.Lives;
                    }
                });
                if (clocks.Elapsed.Minutes == 2 || game?.Lives <= 0)
                {
                    string? time = null;
                    Dispatcher.Invoke(() =>
                    {
                        time = TimeShow.Content.ToString();
                    });
                    this.game?.TerminateGame(time);
                    this.clocks.Stop();
                    this.timer.Dispose();
                }
            }
        }

        private void WhenKeyDown(object sender, KeyEventArgs e)
        {
            bool isShiftPressed = (Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift;//chat gpt
            game?.Check(e.Key, isShiftPressed);
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            parent.PageOrder--;
            parent.Frame.Content = parent.Pages[parent.PageOrder];
            parent.Pages.Remove(parent.Pages[^1]);
            game?.TerminateGame(TimeShow.Content.ToString());
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            if (!played) 
            {
                this.game = new(MyCanvas, letterChoice, difficultyChoice);
                LivesShow.Content = game.Lives;
                clocks.Start();
                played = true;
            }
        }
    }
}
