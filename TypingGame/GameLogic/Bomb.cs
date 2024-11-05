using System.Windows.Controls;

namespace TypingGame.GameLogic
{
    class Bomb(Canvas canvas, string pathToImage, char letter, int x, int y) : Item(canvas, pathToImage, letter, x, y)
    {
    }
}
