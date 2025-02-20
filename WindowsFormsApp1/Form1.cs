using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
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

        Label lbl = new Label();

        public static Button spin = new Button();

        public void Setup()
        {
            // Setup the spin button
            spin.Location = new Point(0, 0);
            spin.AutoSize = true;
            spin.Text = "Spin";
            spin.Click += Spin_Click;
            Controls.Add(spin);

            

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
            lbl.Text = "Teszt";
            Controls.Add(lbl);
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
            lbl.Text = WinCheck();
        }
        private string WinCheck() 
        {
            string message = "";
            for (int i = 0; i < line1.Count; i++)
            {
                if (line1[0].BackColor == line1[1].BackColor && line1[1].BackColor == line1[2].BackColor)
                {
                    message = "You win!";
                }
                else 
                {
                    message = "You lose!";
                }
            }
            for (int i = 0; i < line2.Count; i++)
            {
                if (line2[0].BackColor == line2[1].BackColor && line2[1].BackColor == line2[2].BackColor)
                {
                    message = "You win!";
                }
                else
                {
                    message = "You lose!";
                }
            }
            for (int i = 0; i < line3.Count; i++)
            {
                if (line3[0].BackColor == line3[1].BackColor && line3[1].BackColor == line3[2].BackColor)
                {
                    message = "You win!";
                }
                else
                {
                    message = "You lose!";
                }
            }
            return message;
        }
    }
}
