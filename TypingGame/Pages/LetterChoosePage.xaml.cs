using System.Windows;
using System.Windows.Controls;

namespace TypingGame.Pages
{
    /// <summary>
    /// Interaction logic for LetterChoosePage.xaml
    /// </summary>
    public partial class LetterChoosePage : Page
    {
        private readonly MainWindow parent;
        public int Choice { get; set; }
        public LetterChoosePage(MainWindow mainWindow)
        {
            InitializeComponent();
            this.parent = mainWindow;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            parent.MovePageBack();
        }

        private void Upper_Click(object sender, RoutedEventArgs e)
        {
            Choice = 1;
            this.CreateModePage();
        }

        private void Middle_Click(object sender, RoutedEventArgs e)
        {
            Choice = 2;
            this.CreateModePage();
        }

        private void Down_Click(object sender, RoutedEventArgs e)
        {
            Choice = 3;
            this.CreateModePage();
        }

        private void All_Click(object sender, RoutedEventArgs e)
        {
            Choice = 4;
            this.CreateModePage();
        }

        private void CreateModePage()
        {
            ModePage modePage = new(parent, Choice);
            parent.Pages.Add(modePage);
            parent.Frame.Content = modePage;
            parent.PageOrder++;
        }
    }
}
