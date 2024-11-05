using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace TypingGame.GameLogic
{
    public class Item
    {
        public char Letter { get; set; }
        private Canvas? myCanvas;
        private Image? fruitImage;
        private TextBlock? myTextBlock;
        private int coordinateX;
        private int coordinateY;
        public bool Ended { get; set; }
        public bool ReachedBottom { get; set; }
        public Item(Canvas canvas, string pathToImage, char letter, int x, int y)
        {
            //Dispatcher.Invoke je od chat gpt a aj bitmap image 
            App.Current.Dispatcher.Invoke(() =>
            {
                myCanvas = canvas;
                Letter = letter;
                coordinateX = x;
                coordinateY = y;
                Ended = false;
                BitmapImage bitmapImage = new();
                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri(pathToImage, UriKind.RelativeOrAbsolute);
                bitmapImage.EndInit();
                fruitImage = new Image
                {
                    Source = bitmapImage,
                    Width = 50,
                    Height = bitmapImage.Height * (50 / bitmapImage.Width)
                };
                Canvas.SetLeft(fruitImage, x);
                Canvas.SetTop(fruitImage, y);
                ///////Chat gpt
                myTextBlock = new TextBlock
                {
                    Text = Letter + "",
                    FontSize = 16,
                    FontFamily = new FontFamily("Arial"),
                    FontWeight = FontWeights.Bold,
                    Foreground = Brushes.White
                };
                Canvas.SetLeft(myTextBlock, coordinateX + 20);
                Canvas.SetTop(myTextBlock, coordinateY + 20);
                ///////
                myCanvas.Children.Add(fruitImage);
                myCanvas.Children.Add(myTextBlock);
            });
        }

        public void Animate()
        {
            //chat gpt
            App.Current.Dispatcher.BeginInvoke(() =>
            {
                double initialY = coordinateY; // Initial Y position

                // Create a DoubleAnimation for smooth movement
                DoubleAnimation animation = new()
                {
                    From = initialY, // Start position
                    To = initialY + 1, // End position (adjust as needed)
                    Duration = TimeSpan.FromSeconds(0.5) // Animation duration
                };

                /////My
                DoubleAnimation textAnimation = new()
                {
                    From = initialY + 20,
                    To = initialY + 25
                };
                animation.Duration = TimeSpan.FromSeconds(0.5);
                /////
                // Create a Storyboard to repeat the animation
                Storyboard storyboard = new();
                storyboard.Children.Add(animation);
                storyboard.Children.Add(textAnimation);

                // Set the target property and object
                Storyboard.SetTarget(animation, fruitImage);
                Storyboard.SetTargetProperty(animation, new PropertyPath(Canvas.TopProperty));
                //////My
                Storyboard.SetTarget(textAnimation, myTextBlock);
                Storyboard.SetTargetProperty(textAnimation, new PropertyPath(Canvas.TopProperty));
                /////
                // Set the animation to repeat forever
                storyboard.RepeatBehavior = RepeatBehavior.Forever;
                storyboard.CurrentTimeInvalidated += (s, ev) =>
                {
                    /////My
                    if (coordinateY + 10 >= 400)
                    {
                        storyboard.Stop();
                        this.ReachedBottom = true;
                        this.Destroy();
                    }
                    /////
                };
                
                // Start the animation
                this.coordinateY += 5;
                storyboard.Begin();
            });
            //
        }

        public void Destroy()
        {
            myCanvas?.Children.Remove(myTextBlock);
            myCanvas?.Children.Remove(fruitImage);
            this.Ended = true;
        }
    }
}
