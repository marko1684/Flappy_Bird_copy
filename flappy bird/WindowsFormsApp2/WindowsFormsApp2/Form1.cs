using System;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        bool jumping = false;
        int pipeSpeed = 5;
        int gravity = 3;
        int Inscore = 0;
        public Form1()
        {
            InitializeComponent();
            textBox1.Visible = false;
            button1.Visible = false;
            label1.Visible = false;
            listBox1.Visible = false;
        }

        private void GameKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                jumping = false;
                gravity = 3;

            } 
        }

        private void inGameKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                jumping = true;
                gravity = -6;

            }
        }
        int gap = 150;
        Random r = new Random();
        private void gameTimer_Tick(object sender, EventArgs e)
        {
            pipeBottom.Left -= pipeSpeed;
            pipeTop.Left -= pipeSpeed;
            flappyBird.Top += gravity;
            scoreText.Text = "" + Inscore;

            if (pipeBottom.Left < -80)
            {
                pipeBottom.Left = 500;
                pipeBottom.Top = r.Next(200, (ClientRectangle.Height - 300));

                Inscore += 1;
            }
            else if (pipeTop.Left < -80)
            {
                pipeTop.Left = 500;
                pipeTop.Top = pipeBottom.Top - pipeTop.Height - gap;
                Inscore += 1;
            }

            if (flappyBird.Bounds.IntersectsWith(ground.Bounds))
            {
                endGame();
                textBox1.Visible = true;
                button1.Visible = true;
                label1.Visible = true;
            }
            else if (flappyBird.Bounds.IntersectsWith(pipeBottom.Bounds))
            {
                endGame();
                textBox1.Visible = true;
                button1.Visible = true;
                label1.Visible = true;
            }
            else if (flappyBird.Bounds.IntersectsWith(pipeTop.Bounds))
            {
                endGame();
                textBox1.Visible = true;
                button1.Visible = true;
                label1.Visible = true;
            }
        }
        private void endGame()
        {
            gameTimer.Stop();
        }
        struct score
        {
          public  string imes;
          public  int scores;
        }
        score[] skorovi = new score[100];
        private void button1_Click(object sender, EventArgs e)
        {      
            StreamWriter sw = new StreamWriter("D:\\leaderboard.txt",true);
            string ime = textBox1.Text;
            string score = Inscore.ToString();
            string upis = ime + " " + score;
            sw.WriteLine(upis);
            sw.Close();
            StreamReader sr = new StreamReader("D:\\leaderboard.txt");
            int k = 0;
            while (!sr.EndOfStream)
            {
                string[] s = new string[2];
                string a = sr.ReadLine();
                s = a.Split(' ');
                skorovi[k].imes = s[0];
                skorovi[k].scores = Convert.ToInt32(s[1]);
                k++;
            }
            for(int i = 0; i < k; i++)
            {
                for(int j = 0; j < k-1; j++)
                {
                    if (skorovi[j].scores < skorovi[j + 1].scores)
                    {
                        score temp = skorovi[j + 1];
                        skorovi[j + 1] = skorovi[j];
                        skorovi[j] = temp;
                    }
                }
            }
            textBox1.Visible = false;
            button1.Visible = false;
            label1.Visible = false;

            
            for(int i = 0; i < k; i++)
            {
                listBox1.Items.Add(skorovi[i].imes + "---" + skorovi[i].scores);
            }
            listBox1.Visible = true;
        }
    }
}
