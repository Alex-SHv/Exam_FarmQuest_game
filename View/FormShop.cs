using FarmQuest.Model;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace FarmQuest.View
{
    public partial class FormShop : Form
    {
        private GameModel model;
        private ListBox listBox1;
        private Button btnSell;

        public FormShop(GameModel m)
        {
            InitializeComponent();
            model = m;
            InitializeManualComponents();
            UpdateList();
        }

        private void InitializeManualComponents()
        {
            var listBox1 = new ListBox();
            listBox1.Location = new Point(12, 12);
            listBox1.Size = new Size(254, 160);
            listBox1.Name = "listBox1";
            this.Controls.Add(listBox1);

            var btnSell = new Button();
            btnSell.Text = "Продать всё";
            btnSell.Location = new Point(12, 180);
            btnSell.Size = new Size(254, 40);
            btnSell.Click += btnSell_Click;
            this.Controls.Add(btnSell);

            this.Text = "Магазин — Продажа урожая";
            this.Size = new Size(300, 300);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
        }

        private void UpdateList()
        {
            var listBox1 = this.Controls["listBox1"] as ListBox;
            listBox1.Items.Clear();

            bool hasItems = false;
            foreach (var p in model.Plants)
            {
                int count = model.Storage[p.Name];

                if (count > 0)
                {
                    listBox1.Items.Add($"{p.Name} × {count} | Цена {p.Profit}");
                    hasItems = true;
                }
            }

            if (!hasItems)
                listBox1.Items.Add("Склад пустой...");
        }

        private void btnSell_Click(object sender, EventArgs e)
        {
            foreach (var p in model.Plants)
            {
                int count = model.Storage[p.Name];

                if (count > 0)
                {
                    model.Player.Money += count * p.Profit;
                    model.Storage[p.Name] = 0;
                }
            }

            UpdateList();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ClientSize = new System.Drawing.Size(278, 244);
            this.Name = "FormShop";
            this.Load += new System.EventHandler(this.FormShop_Load);
            this.ResumeLayout(false);

        }

        private void FormShop_Load(object sender, EventArgs e)
        {

        }
    }
}