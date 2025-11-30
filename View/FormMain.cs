using FarmQuest.Controller;
using FarmQuest.Model;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace FarmQuest.View
{
    public partial class FormMain : Form
    {
        Button[,] buttons = new Button[6, 6];
        GameModel model;
        GameController controller;
        Timer timer = new Timer();

        public FormMain()
        {
            InitializeComponent();
            model = new GameModel();
            controller = new GameController(model);

            CreateField();
            CreatePlantButtons();
            CreateActionButtons();

            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void CreateField()
        {
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    int n = 100;
                    Button b = new Button();
                    b.Width = n;
                    b.Height = n;
                    b.Left = j * n;
                    b.Top = i * n;
                    b.BackColor = Color.FromArgb(158, 93, 0);

                    int x = i;
                    int y = j;

                    b.Click += (s, e) =>
                    {
                        controller.CellClick(x, y);
                        UpdateField();
                        UpdatePlayerInfo();
                        UpdatePlantButtons();
                    };

                    buttons[i, j] = b;
                    Controls.Add(b);
                }
            }
        }

        private void CreatePlantButtons()
        {
            for (int i = 0; i < model.Plants.Length; i++)
            {
                Plant p = model.Plants[i];

                Button b = new Button();
                b.Text = p.Name;
                b.Top = 680;
                b.Left = i * 110;
                b.Width = 100;
                b.Height = 40;

                b.Click += (s, e) =>
                {
                    controller.SelectPlant(p);
                };

                b.Name = "PlantBtn" + p.Name;

                Controls.Add(b);
            }

            UpdatePlantButtons();
        }

        private void CreateActionButtons()
        {
            int top = 620;
            int width = 100;
            int height = 40;

            Button water = new Button() { Text = "Полить", Top = top, Left = 0, Width = width, Height = height, BackColor = Color.FromArgb(193, 255, 0) };
            water.Click += (s, e) => controller.SetWaterMode();
            Controls.Add(water);

            Button collect = new Button() { Text = "Собрать", Top = top, Left = 110, Width = width, Height = height, BackColor = Color.FromArgb(0, 170, 0), ForeColor = Color.FromArgb(255, 255, 255) };
            collect.Click += (s, e) => controller.SetCollectMode();
            Controls.Add(collect);

            Button btnShop = new Button() { Text = "Магазин", Top = top, Left = 220, Width = width, Height = height, BackColor = Color.FromArgb(200, 0, 0), ForeColor = Color.FromArgb(255, 255, 255) };
            btnShop.Click += (s, e) =>
            {
                FormShop shopForm = new FormShop(model);  
                shopForm.ShowDialog();                   

                UpdatePlayerInfo(); 
            };

            Controls.Add(btnShop);

            UpdatePlayerInfo();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            controller.UpdateGrowth();
            UpdateField();
        }

        private void UpdateField()
        {
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    string s = model.Field[i, j].State;

                    if (s == "Empty") buttons[i, j].BackColor = Color.FromArgb(158, 93, 0);
                    if (s == "Seed") buttons[i, j].BackColor = Color.FromArgb(255, 255, 0);
                    if (s == "Growing") buttons[i, j].BackColor = Color.FromArgb(193, 255, 0);
                    if (s == "Ready") buttons[i, j].BackColor = Color.FromArgb(0, 170, 0);
                }
            }
        }

        private void UpdatePlantButtons()
        {
            for (int i = 0; i < model.Plants.Length; i++)
            {
                Plant p = model.Plants[i];
                Button b = Controls["PlantBtn" + p.Name] as Button;

                if (model.Player.Level >= p.LevelRequired) b.Enabled = true;
                else b.Enabled = false;
            }
        }

        private void UpdatePlayerInfo()
        {
            this.Text = $"Money: {model.Player.Money} | Level: {model.Player.Level} | Exp: {model.Player.Exp}/{model.Player.NextLevelExp}";
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ClientSize = new System.Drawing.Size(600, 800);
            this.Name = "FormMain";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.ResumeLayout(false);

        }

        private void FormMain_Load(object sender, EventArgs e)
        {

        }
    }
}