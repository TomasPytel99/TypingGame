using System.IO;

namespace TypingGame.GameLogic
{
    class HistoryWriter
    {
        public static void WriteToFile(string fileName, Game game, string? time)
        {
            FileInfo file = new(fileName);
            if (file.Exists)
            {
                List<string?> rows = [];
                StreamReader reader = new(fileName);
                while (!reader.EndOfStream)
                {
                    rows.Add(reader.ReadLine());
                }
                reader.Close();
                StreamWriter writer = new(fileName);
                Write(writer, game, time);
                for (int i = 0; i < rows.Count; i++)
                {
                    writer.WriteLine(rows[i]);
                }
                writer.Close();
            }
            else
            {
                StreamWriter writer = new(@"..\\..\\..\\History.csv");//relative path chat gpt
                Write(writer, game, time);
                writer.Close();
            }
        }

        private static void Write(StreamWriter writer, Game game, string? time)
        {
            writer.Write(GetRowChoice(game.RowChoice) + ";");
            writer.Write(GetDifficultyChoice(game.Difficulty) + ";");
            writer.Write(time + ";");
            writer.Write(game.Lives + ";");
            string result;
            if (game.Lives == 0)
            {
                result = "Lose";
            }
            else
            {
                result = "Win";
            }
            writer.Write(result + ";" + '\n');
        }
        private static string GetRowChoice(int choice)
        {
            switch (choice)
            {
                case 1:
                    return "Upper Row";
                case 2:
                    return "Middle Row";
                case 3:
                    return "Down Row";
                case 4:
                    return "All Rows";
                default:
                    break;
            }
            return "Error";
        }
        private static string GetDifficultyChoice(int choice)
        {
            switch (choice)
            {
                case 1:
                    return "Easy";
                case 2:
                    return "Medium";
                case 3:
                    return "Hard";
                case 4:
                    return "Insane";
                default:
                    break;
            }
            return "Error";
        }
    }
}
