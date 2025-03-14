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
using System.Media;
using System.IO;



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

        private Timer spinTimer = new Timer();

        Button Hitel = new Button();
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
        List<PictureBox> roow2 = new List<PictureBox>() {two, five, eight };
        List<PictureBox> roow3 = new List<PictureBox>() {three, six, nine };
        List<PictureBox> diagonal1 = new List<PictureBox>() {one, five, nine};
        List<PictureBox> diagonal2 = new List<PictureBox>() {three, five, seven};
        List<PictureBox> allpic = new List<PictureBox>() { one, two, three, four, five, six, seven, eight, nine };
        Label lbl = new Label();
        Label result = new Label();

        TextBox custombet = new TextBox();
        Label helplabel = new Label();

        SoundPlayer jackpot = new SoundPlayer("jackpot.wav");

        public static Button spin = new Button();
        int balance = 10000;
        int bet = 0;
        public void Setup()
        {
            this.BackColor = Color.FromArgb(51, 153, 255);
            // Setup the spin button
            spin.Location = new Point(0, 0);
            spin.AutoSize = true;
            spin.Text = "Spin";
            spin.BackColor = Color.White;
            spin.Click += Spin_Click;
            Controls.Add(spin);

            Hitel.Text = "Hitel felvétele: ";
            Hitel.BackColor = Color.White;
            Hitel.Location = new Point(spin.Location.X + 100, minusbet2.Location.Y);
            Hitel.AutoSize = true;
            Hitel.Enabled = false;
            Hitel.Click += (sender, e) =>
            {
                Form3 form = new Form3();
                form.Show();
            };
            Controls.Add(Hitel);

            spinTimer.Interval = 100; // 100 ms időközönként frissítjük a képeket
            spinTimer.Tick += SpinTimer_Tick;

            // Add bet buttons
            SetupBetButton(plusbet1, new Point(spin.Location.X + 200, spin.Location.Y), "+1", 1);
            SetupBetButton(plusbet2, new Point(plusbet1.Location.X + 75, plusbet1.Location.Y), "+10000", 10000);
            SetupBetButton(plusbet3, new Point(plusbet2.Location.X + 75, plusbet2.Location.Y), "+100000", 100000);
            SetupBetButton(zerobet, new Point(plusbet3.Location.X + 75, plusbet3.Location.Y), "0", 0, true);
            SetupBetButton(minusbet1, new Point(zerobet.Location.X + 75, zerobet.Location.Y), "-1000", -1000);
            SetupBetButton(minusbet2, new Point(minusbet1.Location.X + 75, minusbet1.Location.Y), "-10000", -10000);
            SetupBetButton(minusbet3, new Point(minusbet2.Location.X + 75, minusbet2.Location.Y), "-100000", -100000);

            helplabel.Text = "custom bet (1 - 99999999): ";
            helplabel.Location = new Point(minusbet3.Location.X + 90, minusbet3.Location.Y + 10);
            helplabel.ForeColor = Color.White;
            helplabel.AutoSize = true;
            Controls.Add(helplabel);

            custombet.Location = new Point(minusbet3.Location.X + 235, minusbet3.Location.Y + 5);
            custombet.Size = new Size(100, 25);
            custombet.KeyPress += (sender, e) =>
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    if (Regex.IsMatch(custombet.Text, @"^\d+$"))
                    {
                        bet = Int32.Parse(custombet.Text);
                    }
                    else
                    {
                        MessageBox.Show("Invalid input");
                    }
                    lbl.Text = "jelenlegi egyenleged: " + balance.ToString() +
                        "\nJelenlegi tét: " + bet;
                }
            };
            Controls.Add(custombet);

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

            result.Location = new Point(lbl.Location.X, lbl.Location.Y + 500);
            result.AutoSize = true;
            Controls.Add(result);

            FontSetup();
        }
        private void SpinTimer_Tick(object sender, EventArgs e)
        {
            // Véletlenszerűen változtatjuk a képeket
            foreach (var pictureBox in line1.Concat(line2).Concat(line3))
            {
                pictureBox.Image = icons[rnd.Next(0, icons.Count)];
            }
        }
        private void FontSetup() 
        {
            PrivateFontCollection pfc = new PrivateFontCollection();
            pfc.AddFontFile("Font/BungeeSpice-Regular.ttf");
            lbl.Font = new Font(pfc.Families[0], 25, FontStyle.Regular);
            lbl.ForeColor = Color.DarkBlue;

            result.Font = new Font(pfc.Families[0], 25, FontStyle.Regular);
        }
        int winpiece;
        private void SetupBetButton(Button button, Point location, string text, int betChange, bool reset = false)
        {
            button.Location = location;
            button.Text = text;
            button.BackColor = Color.White;
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
        List<System.Drawing.Image> icons = new List<System.Drawing.Image>()
        {

            System.Drawing.Image.FromFile("icon1.png"),
            System.Drawing.Image.FromFile("icon2.png"), 
            //System.Drawing.Image.FromFile("icon3.png"),ű
            System.Drawing.Image.FromFile("icon5.png"),
            System.Drawing.Image.FromFile("icon4.png"),

        };
        private async void Spin_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < allpic.Count; i++)
            {
                allpic[i].BackColor = Color.White;
            }
            jackpot.Stop();
            int a = 0;
            int b = 0;
            int c = 0;

            int d = 0;
            int f = 0;
            int g = 0;

            int x = 0;
            int y = 0;
            int z = 0;

            spinTimer.Start(); // Animáció indítása

            // Várunk egy kicsit, hogy az animáció látható legyen
            await Task.Delay(2000); // 2 másodpercig fut az animáció

            spinTimer.Stop(); // Animáció leállítása
            if (bet != 0)
            {
                for (int i = 0; i < icons.Count; i++)
                {
                    x = rnd.Next(0, icons.Count);
                    y = rnd.Next(0, icons.Count);
                    z = rnd.Next(0, icons.Count);

                    a = rnd.Next(0, icons.Count);
                    b = rnd.Next(0, icons.Count);
                    c = rnd.Next(0, icons.Count);

                    d = rnd.Next(0, icons.Count);
                    f = rnd.Next(0, icons.Count);
                    g = rnd.Next(0, icons.Count);
                }

                for (int i = 0; i < line1.Count; i++)
                {
                    line1[0].Image = icons[x];
                    line1[1].Image = icons[y];
                    line1[2].Image = icons[z];
                }
                for (int i = 0; i < line2.Count; i++)
                {
                    line2[0].Image = icons[a];
                    line2[1].Image = icons[b];
                    line2[2].Image = icons[c];
                }
                for (int i = 0; i < line3.Count; i++)
                {
                    line3[0].Image = icons[d];
                    line3[1].Image = icons[f];
                    line3[2].Image = icons[g];
                }

                // Check result and update balance
                result.Text = WinCheck();

                // Ha nyert, a nyereményhez adódik hozzá
                if (result.Text == "You win!")
                {
                    balance = winmoney();
                }
                else
                {
                    balance = losemoney();
                }

                bet = 0;
                string print = "";

                bool linecheck = LineChecker();
                bool rowcheck = RowChecker();
                bool diagonalcheck = diagonalChecker();

                if (linecheck || rowcheck || diagonalcheck || (linecheck && diagonalcheck && rowcheck) || (linecheck && diagonalcheck) || (diagonalcheck && rowcheck) || (rowcheck && linecheck))
                {
                    print = "Nyert összeg: " + winningmoney.ToString();
                    jackpot.Play();
                }
                else
                {
                    print = "Veszített összeg: " + losingmoney.ToString(); // A veszteség helyes kiszámítása
                }
                // Frissítjük a nyereményeket és veszteségeket
                lbl.Text = "jelenlegi egyenleged: " + balance.ToString() +
                            "\nJelenlegi tét: " + bet.ToString() + "\n" + print;
            }
        }


        public int winningmoney;
        public int losingmoney;

        private int losemoney()
        {
            int backmoney = balance - bet; // Calculate the balance after losing the bet
            losingmoney = bet;  // A veszteség az adott tét, hiszen a játékos elbukta a tétet
            return backmoney;  // Return the updated balance
        }

        private int winmoney()
        {
            int Winmoney = balance + bet; // Calculate the balance after winning the bet
            winningmoney = bet;  // A nyeremény az adott tét, mivel a játékos ennyit nyert
            return Winmoney;  // Return the updated balance
        }


        private string WinCheck()
        {
            // Initialize message with a losing result
            string message = "You lose!";
            winpiece = 0;

            // Check for winning combinations
            bool lineWin = LineChecker();
            bool rowWin = RowChecker();
            bool diagonalWin = diagonalChecker();

            if (lineWin && rowWin && diagonalWin)
            {
                message = "You win!";
                winpiece += 3; // Három nyerő kombináció
            }
            else if (lineWin && rowWin)
            {
                message = "You win!";
                winpiece += 2; // Két nyerő kombináció
            }
            else if (lineWin || rowWin || diagonalWin)
            {
                message = "You win!";
                winpiece += 1; // Egy nyerő kombináció
            }

            // Ha van nyerő kombináció, hívjuk meg a winmoney() függvényt
            if (message == "You win!")
            {
                winmoney();
            }

            return message;
        }
        private bool LineChecker() 
        {
            bool win = false;
            // Check line1
            if (line1[0].Image == line1[1].Image && line1[1].Image == line1[2].Image)
            {
                for (int i = 0; i < line1.Count; i++)
                {
                    line1[i].BackColor = Color.Yellow;
                }
                win =  true;
            }

            // Check line2
            if (line2[0].Image == line2[1].Image && line2[1].Image == line2[2].Image)
            {
                for (int i = 0; i < line2.Count; i++)
                {
                    line2[i].BackColor = Color.Yellow;
                }
                win = true;
            }

            // Check line3
            if (line3[0].Image == line3[1].Image && line3[1].Image == line3[2].Image)
            {
                for (int i = 0; i < line3.Count; i++)
                {
                    line3[i].BackColor = Color.Yellow;
                }
                win = true;
            }
            return win;
        }
        private bool diagonalChecker() 
        {
            bool win = false;
            if (diagonal1[0].Image == diagonal1[1].Image && diagonal1[1].Image == diagonal1[2].Image)
            {
                for (int i = 0; i < diagonal1.Count; i++)
                {
                    diagonal1[i].BackColor = Color.Yellow;
                }
                win = true;
            }
            if (diagonal2[0].Image == diagonal2[1].Image && diagonal2[1].Image == diagonal2[2].Image)
            {
                for (int i = 0; i < diagonal2.Count; i++)
                {
                    diagonal2[i].BackColor = Color.Yellow;

                }
                win = true;
            }

            return win;
        
        }
        private bool RowChecker() 
        {
            bool win = false;
            if (roow1[0].Image == roow1[1].Image && roow1[1].Image == roow1[2].Image)
            {
                for (int i = 0; i < roow1.Count; i++)
                {
                    roow1[i].BackColor = Color.Yellow;
                }
                win = true;
            }
            if (roow2[0].Image == roow2[1].Image && roow2[1].Image == roow2[2].Image)
            {
                for (int i = 0; i < roow2.Count; i++)
                {
                    roow2[i].BackColor = Color.Yellow;
                }
                win = true;
            }
            if (roow3[0].Image == roow3[1].Image && roow3[1].Image == roow3[2].Image)
            {
                for (int i = 0; i < roow3.Count; i++)
                {
                    roow3[i].BackColor = Color.Yellow;
                }
                win = true;
            }
            return win;
        }
    }
    public partial class Form2 : Form
    {
        public Form2()
        {
            Setup();
        }

        public Button btn = new Button();
        string filepath = Path.Combine(Application.StartupPath, "sound", "sound.wav");
        SoundPlayer sound;
        PictureBox mainmenu = new PictureBox();
        private void Setup()
        {
            
            this.Size = new Size(400, 500);
            btn.Location = new Point(10, 30);
            btn.Size = new Size(350, 25);
            btn.Text = "Start";
            btn.Click += btn_Click;
            Controls.Add(btn);

            mainmenu.Image = Image.FromFile("Casiiii.gif");
            mainmenu.Size = this.Size;
            mainmenu.Location = this.Location;
            Controls.Add(mainmenu);
            mainmenu.SendToBack();
            mainmenu.SizeMode = PictureBoxSizeMode.StretchImage;

            // Hangfájl betöltése
            if (File.Exists(filepath))
            {
                sound = new SoundPlayer(filepath);
                sound.Load();
                SoundStart();
            }
            else
            {
                MessageBox.Show("Hibás fájl elérési útvonal vagy hiányzó fájl: " + filepath);
            }
        }

        public void SoundStart()
        {
            sound.PlayLooping();
            MessageBox.Show("Aktuális zene: " + filepath);
        }

        public void SoundStop()
        {
            sound.Stop();
        }

        private void btn_Click(object sender, EventArgs e)
        {
            
            Form1 form = new Form1();
            //this.Close(); // Az aktuális ablakot zárja be
            form.Show();
        }
    }
    public partial class Form3 : Form
    {
        Label lbl = new Label();
        Button btn = new Button();
        public Form3()
        {
            Setup();
        }
        public void Setup() 
        {
            this.AutoSize = true;
            this.Text = "Hitel felvétel";


            lbl.Text = "Hitel felvétele";
            lbl.Location = new Point(10, 10);
            lbl.AutoSize = true;
            Controls.Add(lbl);

            btn.Text = "Hitel felvétele";
            btn.AutoSize = true;
            btn.Location = new Point(lbl.Location.X + 100, lbl.Location.Y);
            Controls.Add(btn);



        }

    }
}
