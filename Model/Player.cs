namespace FarmQuest.Model
{
    public class Player
    {
        public int Money = 10;
        public int Level = 1;
        public int Exp = 0;
        public int NextLevelExp = 10;

        public void AddExp(int value)
        {
            Exp += value;

            if (Exp >= NextLevelExp)
            {
                Level++;
                Exp = 0;
                NextLevelExp += 10;
            }
        }
    }
}
