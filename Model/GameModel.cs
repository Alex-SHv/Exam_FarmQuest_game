using System;
using System.Collections.Generic;

namespace FarmQuest.Model
{
    public class GameModel
    {
        public Player Player = new Player();
        public Dictionary<string, int> Storage = new Dictionary<string, int>();
        public Plant[] Plants;
        public Cell[,] Field;

        public GameModel()
        {
            Field = new Cell[6, 6];

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    Field[i, j] = new Cell();
                }
            }

            Plants = new Plant[]
            {
                new Plant(){ Name="Морковь", LevelRequired=1, Cost=2, Profit=5, GrowTime=5},
                new Plant(){ Name="Картофель", LevelRequired=2, Cost=5, Profit=12, GrowTime=7},
                new Plant(){ Name="Кукуруза", LevelRequired=3, Cost=7, Profit=18, GrowTime=10},
                new Plant(){ Name="Тыква", LevelRequired=5, Cost=12, Profit=30, GrowTime=15}
            };

            for (int i = 0; i < Plants.Length; i++)
            {
                Storage[Plants[i].Name] = 0;
            }
        }
    }

    public class Cell
    {
        public string State = "Empty";
        public Plant Plant;
        public DateTime StartTime;
    }
}
