namespace TypingGame.GameLogic
{
    public class Generator
    {
        private readonly Random randomGenerator;
        private readonly List<char> generationDomain;
        public Generator(int rowChoice)
        {
            randomGenerator = new Random();
            generationDomain = [];
            this.InitializeGenerator(rowChoice);
        }
        public char Generate()
        {
            char value = generationDomain[randomGenerator.Next(0, generationDomain.Count)];
            int i = randomGenerator.Next(0, 2);
            if (i == 1) 
            {
                value = (char)(value + 32);
            }
            return value;
        }

        public int GenerateX()
        {
            return randomGenerator.Next(0, 800);
        }
        private void InitializeGenerator(int rowChoice)
        {
            switch (rowChoice)
            {
                case 1:
                    this.CreateUpperRow();
                    break;
                case 2:
                    this.CreateMiddleRow();
                    break;
                case 3:
                    this.CreateLowerRow();
                    break;
                case 4:
                    this.CreateUpperRow();
                    this.CreateMiddleRow();
                    this.CreateLowerRow();
                    break;
            }
        }

        private void CreateUpperRow()
        {
            generationDomain.Add('Q');
            generationDomain.Add('W');
            generationDomain.Add('E');
            generationDomain.Add('R');
            generationDomain.Add('T');
            generationDomain.Add('Y');
            generationDomain.Add('U');
            generationDomain.Add('I');
            generationDomain.Add('O');
            generationDomain.Add('P');
        }

        private void CreateMiddleRow()
        {
            generationDomain.Add('A');
            generationDomain.Add('S');
            generationDomain.Add('D');
            generationDomain.Add('F');
            generationDomain.Add('G');
            generationDomain.Add('H');
            generationDomain.Add('J');
            generationDomain.Add('K');
            generationDomain.Add('L');
        }

        private void CreateLowerRow() 
        {
            generationDomain.Add('Z');
            generationDomain.Add('X');
            generationDomain.Add('C');
            generationDomain.Add('V');
            generationDomain.Add('B');
            generationDomain.Add('N');
            generationDomain.Add('M');
        }
    }
}
