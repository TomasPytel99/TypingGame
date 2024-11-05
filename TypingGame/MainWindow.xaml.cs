using System.Windows;
using System.Windows.Controls;
using TypingGame.Pages;

namespace TypingGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Page> Pages { get; }
        public int PageOrder { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            Pages = [];
            MainPage mainPage = new(this);
            Pages.Add(mainPage);
            Frame.Content = mainPage;
            Frame.Focus();
        }

        public void MovePageForward()
        {
            if (Pages.Count == 1)
            {
                LetterChoosePage letterChoosePage = new(this);
                Pages.Add(letterChoosePage);
                Frame.Content = letterChoosePage;
                PageOrder++;
            }
            else
            {
                PageOrder++;
                Frame.Content = Pages[PageOrder];
            }
        }

        public void MovePageBack()
        {
            if (PageOrder > 0)
            { 
                PageOrder--;
                Frame.Content = Pages[PageOrder];
            }
        }
    }
}