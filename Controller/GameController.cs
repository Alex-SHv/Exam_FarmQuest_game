using FarmQuest.Model;
using System;

namespace FarmQuest.Controller
{
    public class GameController
    {
        private GameModel model;
        public string Mode = "Plant"; 
        public Plant SelectedPlant;

        public GameController(GameModel m)
        {
            model = m;
        }

        public void SelectPlant(Plant p)
        {
            SelectedPlant = p;
            Mode = "Plant";
        }

        public void SetWaterMode()
        {
            Mode = "Water";
        }

        public void SetCollectMode()
        {
            Mode = "Collect";
        }

        public void CellClick(int x, int y)
        {
            Cell c = model.Field[x, y];

            if (Mode == "Plant")
            {
                if (c.State == "Empty")
                {
                    if (model.Player.Level >= SelectedPlant.LevelRequired)
                    {
                        if (model.Player.Money >= SelectedPlant.Cost)
                        {
                            c.State = "Seed";
                            c.Plant = SelectedPlant;
                            model.Player.Money -= SelectedPlant.Cost;
                        }
                    }
                }
            }

            if (Mode == "Water")
            {
                if (c.State == "Seed")
                {
                    c.State = "Growing";
                    c.StartTime = DateTime.Now;
                }
            }

            if (Mode == "Collect")
            {
                if (c.State == "Ready")
                {
                    model.Storage[c.Plant.Name] += 1;
                    model.Player.AddExp(2);
                    c.State = "Empty";
                    c.Plant = null;
                }
            }
        }

        public void UpdateGrowth()
        {
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    Cell c = model.Field[i, j];

                    if (c.State == "Growing")
                    {
                        TimeSpan t = DateTime.Now - c.StartTime;
                        if (t.TotalSeconds >= c.Plant.GrowTime)
                        {
                            c.State = "Ready";
                        }
                    }
                }
            }
        }

        public GameModel GetModel()
        {
            return model;
        }
    }
}
