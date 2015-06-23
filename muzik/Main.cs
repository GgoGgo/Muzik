using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace muzik
{
    public partial class Main : Form
    {
        List<Button> button;

        public Main()
        {
            InitializeComponent();
            button = new List<Button>();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            // allow drag-drop at main form -> it cannot be set in design view
            this.AllowDrop = true;

            // find radio button and progressBar then put them into button(List)
            for(int i = 0; i < 19; i++)
            {
                RadioButton rb = (RadioButton)groupBox1.Controls.Find("l" + (i / 7+1) + "_" + i % 7, false)[0];
                ProgressBar pb = (ProgressBar)groupBox1.Controls.Find("l" + (i / 7 + 1) + "_" + i % 7+"_bar", false)[0];
                CheckBox cb = (CheckBox)groupBox1.Controls.Find("l" + (i / 7 + 1) + "_" + i % 7 + "_cb", false)[0];
                rb.DragDrop += new DragEventHandler(radioButtons_DragDrop);
                rb.DragEnter += new DragEventHandler(radioButtons_DragEnter);
                button.Insert(i, new Button(rb, pb, cb));
            }
            l1_0.Select();  // select default radiobutton
            timer1.Start(); // start timer to stop progress bar
        }

        private void songSelectButton_Click(object sender, EventArgs e)
        {
            if( openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog1.FileName;
                int lastIndex = filePath.LastIndexOf('\\');
                string fileName = filePath.Substring( lastIndex + 1, filePath.Length - ( lastIndex + 1));

                try
                {
                    if (!fileName.EndsWith(".wav")) throw new ArgumentOutOfRangeException("Invalid parameter");

                    int radioTag = getSelectedRadioButtonTagIn(groupBox1, fileName);

                    if (radioTag == -1) throw new ArgumentNullException("null control");
                    setAudioSourceByPathAt(radioTag, filePath);
                }
                catch (ArgumentNullException ex)
                {
                    MessageBox.Show("cannot find proper radiobutton");
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    MessageBox.Show("only can accept .wav file");
                }
            }
        }

        void radioButtons_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        void radioButtons_DragDrop(object sender, DragEventArgs e)
        {
            string[] filePath = (string[])e.Data.GetData( DataFormats.FileDrop, false );
            RadioButton rb = new RadioButton();

            if (sender.Equals(l1_0)) rb = l1_0;
            else if (sender.Equals(l1_1)) rb = l1_1;
            else if (sender.Equals(l1_2)) rb = l1_2;
            else if (sender.Equals(l1_3)) rb = l1_3;
            else if (sender.Equals(l1_4)) rb = l1_4;
            else if (sender.Equals(l1_5)) rb = l1_5;
            else if (sender.Equals(l1_6)) rb = l1_6;
            else if (sender.Equals(l2_0)) rb = l2_0;
            else if (sender.Equals(l2_1)) rb = l2_1;
            else if (sender.Equals(l2_2)) rb = l2_2;
            else if (sender.Equals(l2_3)) rb = l2_3;
            else if (sender.Equals(l2_4)) rb = l2_4;
            else if (sender.Equals(l2_5)) rb = l2_5;
            else if (sender.Equals(l2_6)) rb = l2_6;
            else if (sender.Equals(l3_0)) rb = l3_0;
            else if (sender.Equals(l3_1)) rb = l3_1;
            else if (sender.Equals(l3_2)) rb = l3_2;
            else if (sender.Equals(l3_3)) rb = l3_3;
            else if (sender.Equals(l3_4)) rb = l3_4;

            int lastIndex = filePath[0].LastIndexOf('\\');
            string fileName = filePath[0].Substring(lastIndex + 1, filePath[0].Length - (lastIndex + 1));
            
            try
            {
                if (!fileName.EndsWith(".wav")) throw new System.ArgumentException("Invalid parameter");

                rb.Text = rb.Name + "\n" + fileName;
                setAudioSourceByPathAt(Convert.ToInt32(rb.Tag), filePath[0]);
            }
            catch(Exception ex)
            {
                MessageBox.Show("only can accept .wav file");
            }
        }
        
        private int getSelectedRadioButtonTagIn( Control o, string fileName )
        {
            foreach (Control c in o.Controls)
            {
                if (c.GetType() == typeof(RadioButton))
                {
                    RadioButton rb = c as RadioButton;
                    if (rb.Checked)
                    {
                        rb.Text = rb.Name + "\n" + fileName;
                        return Convert.ToInt32(rb.Tag.ToString());
                    }
                }
            }
            return -1;
        }

        int i = 0;
        private void setAudioSourceByPathAt(int tag, string filePath)
        {
            try
            {
                if(cb_is_trim.Checked == true)
                {
                    Trim.TrimWavFile(filePath, @"C:\dev\test\a"+i.ToString()+".wav", 0, Convert.ToInt32(tb_trimInterval.Text));
                    button.ElementAt(tag).setCachedSound(@"C:\dev\test\a" + i.ToString() + ".wav");
                    i++;
                }
                else
                {
                    button.ElementAt(tag).setCachedSound(filePath);
                }
                
            }
            catch ( Exception ex )
            {
                MessageBox.Show("At setAudioSouceByPathAt: "+ex.Message);
            }
        }

        private void Main_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    button.ElementAt<Button>(0).playSample(); break;
                case Keys.E:
                    button.ElementAt<Button>(1).playSample(); break;
                case Keys.R:
                    button.ElementAt<Button>(2).playSample(); break;
                case Keys.U:
                    button.ElementAt<Button>(3).playSample(); break;
                case Keys.I:
                    button.ElementAt<Button>(4).playSample(); break;
                case Keys.O:
                    button.ElementAt<Button>(5).playSample(); break;
                case Keys.P:
                    button.ElementAt<Button>(6).playSample(); break;
                case Keys.S:
                    button.ElementAt<Button>(7).playSample(); break;
                case Keys.D:
                    button.ElementAt<Button>(8).playSample(); break;
                case Keys.F:
                    button.ElementAt<Button>(9).playSample(); break;
                case Keys.J:
                    button.ElementAt<Button>(10).playSample(); break;
                case Keys.K:
                    button.ElementAt<Button>(11).playSample(); break;
                case Keys.L:
                    button.ElementAt<Button>(12).playSample(); break;
                case Keys.OemSemicolon:
                    button.ElementAt<Button>(13).playSample(); break;
                case Keys.X:
                    button.ElementAt<Button>(14).playSample(); break;
                case Keys.C:
                    button.ElementAt<Button>(15).playSample(); break;
                case Keys.V:
                    button.ElementAt<Button>(16).playSample(); break;
                case Keys.M:
                    button.ElementAt<Button>(17).playSample(); break;
                case Keys.Oemcomma:
                    button.ElementAt<Button>(18).playSample(); break;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            for(int i = 0; i < 19; i++)
            {
                if(button.ElementAt(i).is_playing == false)
                {
                    button.ElementAt(i).stopProgressBar();
                }
            }
        }


        private const int APPCOMMAND_VOLUME_MUTE = 0x80000;
        private const int APPCOMMAND_VOLUME_UP = 0xA0000;
        private const int APPCOMMAND_VOLUME_DOWN = 0x90000;
        private const int WM_APPCOMMAND = 0x319;

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessageW(IntPtr hWnd, int Msg,
            IntPtr wParam, IntPtr lParam);

        private void btnMute_Click(object sender, EventArgs e)
        {
            SendMessageW(this.Handle, WM_APPCOMMAND, this.Handle,
                (IntPtr)APPCOMMAND_VOLUME_MUTE);
        }

        private void btnDecVol_Click(object sender, EventArgs e)
        {
            SendMessageW(this.Handle, WM_APPCOMMAND, this.Handle,
                (IntPtr)APPCOMMAND_VOLUME_DOWN);
        }

        private void btnIncVol_Click(object sender, EventArgs e)
        {
            SendMessageW(this.Handle, WM_APPCOMMAND, this.Handle,
                (IntPtr)APPCOMMAND_VOLUME_UP);
        }
    }
}
