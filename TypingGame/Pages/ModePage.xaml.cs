using System.Windows;
using System.Windows.Controls;

namespace TypingGame.Pages
{
    /// <summary>
    /// Interaction logic for ModePage.xaml
    /// </summary>
    public partial class ModePage : Page
    {
        private readonly MainWindow parent;
        public int Choice { get; set; }
        public int PreviousChoice { get; set; }
        public ModePage(MainWindow mainWindow, int previousChoice)
        {
            InitializeComponent();
            parent = mainWindow;
            PreviousChoice = previousChoice;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            parent.PageOrder--;
            parent.Frame.Content = parent.Pages[parent.PageOrder];
        }

        private void Easy_Click(object sender, RoutedEventArgs e)
        {
            Choice = 1;
            this.CreateGamePage();
        }

        private void Medium_Click(object sender, RoutedEventArgs e)
        {
            Choice = 2;
            this.CreateGamePage();
        }

        private void Hard_Click(object sender, RoutedEventArgs e)
        {
            Choice = 3;
            this.CreateGamePage();
        }

        private void Insane_Click(object sender, RoutedEventArgs e)
        {
            Choice = 4;
            this.CreateGamePage();
        }

        private void CreateGamePage()
        {
            GamePage gamePage = new(parent, PreviousChoice, Choice);
            parent.Frame.Content = gamePage;
            gamePage.Focus();
        }
    }
}
