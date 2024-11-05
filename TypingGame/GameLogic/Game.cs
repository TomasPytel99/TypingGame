using System.Windows.Controls;
using System.IO;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows;

namespace TypingGame.GameLogic
{
    public class Game
    {
        private readonly Timer generationTimer;
        private readonly Timer animationTimer;
        private readonly Generator generator;
        private readonly List<Item> items;
        private readonly List<string> fruitImagePaths;
        private readonly Canvas myCanvas;
        private int generationInterval;
        private readonly int animationInterval;
        private readonly Random random;
        private bool terminated;
        public int Lives { get; set; }
        public int RowChoice { get; }
        public int Difficulty { get; }
        public Game(Canvas canvas, int rowChoice, int difficultyChoice)
        {
            generator = new(rowChoice);
            random = new Random();
            fruitImagePaths = [];
            items = [];
            Lives = 5;
            terminated = false;
            RowChoice = rowChoice;
            Difficulty = difficultyChoice;
            myCanvas = canvas;
            this.LoadImages();
            this.InitializeGame(difficultyChoice);
            generationTimer = new(GenerateLetter, null, 0, generationInterval);
            animationInterval = 300;
            animationTimer = new(Animate, null, 0, animationInterval);
            
        }

        private void LoadImages()
        {
            StreamReader stream = new(@"..\\..\\..\\Images\\images.txt");
            while (!stream.EndOfStream) 
            {
                string? line = stream.ReadLine();
                if (line != null)
                {
                    fruitImagePaths.Add(line);
                }
            }
            stream.Close();
        }

        private void Animate(object? state)
        {
            for (int i = 0; i < items.Count; i++) 
            {
                if (items[i].Ended)
                {
                    if (items[i].ReachedBottom && items[i] is Fruit)
                    {
                        Lives--;
                    }
                    items.Remove(items[i]);
                }
                else
                {
                    items[i].Animate();
                }
            }
        }

        private void InitializeGame(int difficultyChoice)
        {
            switch (difficultyChoice)
            {
                case 1:
                    generationInterval = 1000;
                    break;
                case 2:
                    generationInterval = 800;
                    break;
                case 3:
                    generationInterval = 500;
                    break;
                case 4:
                    generationInterval = 400;
                    break;
            }
        }

        public void TerminateGame(string? time)
        {
            if (!this.terminated)//vymaže canvas, zastaví stopky a vypíše výsledok
            {
                generationTimer.Dispose();
                animationTimer.Dispose();
                myCanvas.Dispatcher.Invoke(new Action(() => 
                {
                    myCanvas.Children.Clear(); 
                }));
                string? result = (this.Lives == 0) ? "You lost" : "You won";
                myCanvas.Dispatcher.Invoke(() =>
                {
                    TextBlock textBlock = new()
                    {
                        Text = result,
                        FontSize = 30,
                        FontFamily = new FontFamily("Arial"),
                        FontWeight = FontWeights.Bold,
                        Foreground = Brushes.White
                    };
                    myCanvas.Children.Add(textBlock);
                    Canvas.SetTop(textBlock, 200);
                    Canvas.SetLeft(textBlock, 350);
                });
                HistoryWriter.WriteToFile(@"..\\..\\..\\History.csv", this, time);
                this.terminated = true;
            }
        }

        public void Check(Key pressed, bool shift)
        {
            char key;
            if (shift)
            {
                key = pressed.ToString()[0]; //https://stackoverflow.com/questions/486927/how-do-i-get-the-normal-characters-from-a-wpf-keydown-event
            }
            else
            {
                key = pressed.ToString().ToLower()[0];
            }

            Item? target = null;
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Letter == key && items[i] is Fruit)
                {
                    target = items[i];
                    target.Destroy();
                    break;
                }
            }
            if (target == null)
            {
                for (int i = 0; i < items.Count; i++)
                {
                    if (items[i].Letter == key && items[i] is Bomb)
                    {
                        target = items[i];
                        target.Destroy();
                        Lives--;
                        break;
                    }
                }
            }
            if (target != null)
            { 
                items.Remove(target);
            }
        }

        private void GenerateLetter(object? state)
        {
            char value = generator.Generate();
            string image = fruitImagePaths[random.Next(0, fruitImagePaths.Count - 1)];
            Item item;
            if (random.Next(0, 10) == 0)
            {
                item = new Bomb(myCanvas, @"..\\..\\..\\Images\\bomb.png", value, generator.GenerateX(), 0);
            }
            else
            {
                item = new Fruit(myCanvas, image, value, generator.GenerateX(), 0);
            }
            items.Add(item);
        }
    }
}
