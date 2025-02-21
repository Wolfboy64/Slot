using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Text;
using static System.Net.Mime.MediaTypeNames;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        Button plusbet1 = new Button();
        Button plusbet2 = new Button();
        Button plusbet3 = new Button();
        Button zerobet = new Button();
        Button minusbet1 = new Button();
        Button minusbet2 = new Button();
        Button minusbet3 = new Button();
        public Form1()
        {
            InitializeComponent();
            Setup();
        }

        public static PictureBox one = new PictureBox();
        public static PictureBox two = new PictureBox();
        public static PictureBox three = new PictureBox();
        public static PictureBox four = new PictureBox();
        public static PictureBox five = new PictureBox();
        public static PictureBox six = new PictureBox();
        public static PictureBox seven = new PictureBox();
        public static PictureBox eight = new PictureBox();
        public static PictureBox nine = new PictureBox();

        List<PictureBox> line1 = new List<PictureBox>() {one, two, three};
        List<PictureBox> line2 = new List<PictureBox>() {four, five, six};
        List<PictureBox> line3 = new List<PictureBox>() {seven, eight, nine};

        List<PictureBox> roow1 = new List<PictureBox>() {one, four, seven };
        Label lbl = new Label();
        Label result = new Label();

        


        public static Button spin = new Button();
        int balance = 10000;
        int bet = 0;
        public void Setup()
        {
            // Setup the spin button
            spin.Location = new Point(0, 0);
            spin.AutoSize = true;
            spin.Text = "Spin";
            spin.Click += Spin_Click;
            Controls.Add(spin);

            // Add bet buttons
            SetupBetButton(plusbet1, new Point(spin.Location.X + 200, spin.Location.Y), "+1", 1);
            SetupBetButton(plusbet2, new Point(plusbet1.Location.X + 75, plusbet1.Location.Y), "+10000", 10000);
            SetupBetButton(plusbet3, new Point(plusbet2.Location.X + 75, plusbet2.Location.Y), "+100000", 100000);
            SetupBetButton(zerobet, new Point(plusbet3.Location.X + 75, plusbet3.Location.Y), "0", 0, true);
            SetupBetButton(minusbet1, new Point(zerobet.Location.X + 75, zerobet.Location.Y), "-1000", -1000);
            SetupBetButton(minusbet2, new Point(minusbet1.Location.X + 75, minusbet1.Location.Y), "-10000", -10000);
            SetupBetButton(minusbet3, new Point(minusbet2.Location.X + 75, minusbet2.Location.Y), "-100000", -100000);

            // Setup PictureBoxes
            SetupPictureBox(one, new Point(10, 50));
            SetupPictureBox(two, new Point(120, 50));
            SetupPictureBox(three, new Point(230, 50));
            SetupPictureBox(four, new Point(10, 160));
            SetupPictureBox(five, new Point(120, 160));
            SetupPictureBox(six, new Point(230, 160));
            SetupPictureBox(seven, new Point(10, 270));
            SetupPictureBox(eight, new Point(120, 270));
            SetupPictureBox(nine, new Point(230, 270));

            lbl.Location = new Point(six.Location.X + 200, six.Location.Y + 35);
            lbl.AutoSize = true;
            lbl.Text = "jelenlegi egyenleged: " + balance.ToString() +
                "\nJelenlegi tét: " + bet;
            Controls.Add(lbl);

            result.Location = new Point(lbl.Location.X, lbl.Location.Y + 50);
            result.AutoSize = true;
            Controls.Add(result);

            FontSetup();
        }

        private void FontSetup() 
        {
            PrivateFontCollection pfc = new PrivateFontCollection();
            pfc.AddFontFile("Font/BungeeSpice-Regular.ttf");
            lbl.Font = new Font(pfc.Families[0], 25, FontStyle.Regular);
            lbl.ForeColor = Color.DarkBlue;
            //lbl.BackColor = Color.Orange;
        
        }

        private void SetupBetButton(Button button, Point location, string text, int betChange, bool reset = false)
        {
            button.Location = location;
            button.Text = text;
            button.Click += (sender, e) =>
            {
                if (reset)
                {
                    bet = 0;
                }
                else
                {
                    bet += betChange;
                    if (bet < 0) bet = 0; // Prevent negative bet values
                }
                lbl.Text = "jelenlegi egyenleged: " + balance.ToString() +
                    "\nJelenlegi tét: " + bet;
            };
            Controls.Add(button);
        }

        private void SetupPictureBox(PictureBox pictureBox, Point location)
        {
            pictureBox.Size = new Size(100, 100);
            pictureBox.Location = location;
            pictureBox.BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(pictureBox);
        }
        public Random rnd = new Random();
        List<Color> colors = new List<Color>() { Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Black };
        private void Spin_Click(object sender, EventArgs e)
        {
            int a = 0;
            int b = 0;
            int c = 0;

            int d = 0;
            int f = 0;
            int g = 0;

            int x = 0;
            int y = 0;
            int z = 0;
            if (bet != 0)
            {
                for (int i = 0; i < colors.Count; i++)
                {
                    x = rnd.Next(0, colors.Count);
                    y = rnd.Next(0, colors.Count);
                    z = rnd.Next(0, colors.Count);

                    a = rnd.Next(0, colors.Count);
                    b = rnd.Next(0, colors.Count);
                    c = rnd.Next(0, colors.Count);

                    d = rnd.Next(0, colors.Count);
                    f = rnd.Next(0, colors.Count);
                    g = rnd.Next(0, colors.Count);
                }
                for (int i = 0; i < line1.Count; i++)
                {
                    line1[0].BackColor = colors[x];
                    line1[1].BackColor = colors[y];
                    line1[2].BackColor = colors[z];
                }
                for (int i = 0; i < line2.Count; i++)
                {
                    line2[0].BackColor = colors[a];
                    line2[1].BackColor = colors[b];
                    line2[2].BackColor = colors[c];
                }
                for (int i = 0; i < line3.Count; i++)
                {
                    line3[0].BackColor = colors[d];
                    line3[1].BackColor = colors[f];
                    line3[2].BackColor = colors[g];
                }

                // Check result and update balance
                result.Text = WinCheck();
                if (result.Text == "You win!")
                {
                    balance = winmoney();
                }
                else
                {
                    balance = losemoney();
                }
                bet = 0;
                // Update label text
                
                lbl.Text = "jelenlegi egyenleged: " + balance.ToString() +
                            "\nJelenlegi tét: " + bet;
                
            }
            
        }

        private int losemoney()
        {
            int backmoney = balance - bet;
            return backmoney;
        }

        private int winmoney()
        {
            int winmoney = balance + bet;
            return winmoney;
        }

        private string WinCheck()
        {
            // Initialize message with a losing result
            string message = "You lose!";

            // Check line1
            if (line1[0].BackColor == line1[1].BackColor && line1[1].BackColor == line1[2].BackColor)
            {
                return "You win!";
            }

            // Check line2
            if (line2[0].BackColor == line2[1].BackColor && line2[1].BackColor == line2[2].BackColor)
            {
                return "You win!";
            }

            // Check line3
            if (line3[0].BackColor == line3[1].BackColor && line3[1].BackColor == line3[2].BackColor)
            {
                return "You win!";
            }

            // Return the result
            return message;
        }

    }
}
