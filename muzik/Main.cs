using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using NAudio;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace muzik
{
    public partial class Main : Form
    {
        CachedSound[] cachedSound;

        public Main()
        {
            InitializeComponent();
            
            for( int i = 0; i < 19; i++)
            {
                cachedSound = new CachedSound[19];
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            this.AllowDrop = true;
            tabControl1.AllowDrop = true;
            groupBox1.AllowDrop = true;


            foreach (Control c in groupBox1.Controls)
            {
                if (c.GetType() == typeof(RadioButton))
                {
                    RadioButton rb = c as RadioButton;
                    rb.DragDrop += new DragEventHandler(radioButtons_DragDrop);
                    rb.DragEnter += new DragEventHandler(radioButtons_DragEnter);
                }
            }
            l1_0.Select();
        }

        private void Main_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void songSelectButton_Click(object sender, EventArgs e)
        {
            if( openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string filePath = openFileDialog1.FileName;
                int lastIndex = filePath.LastIndexOf('\\');
                string fileName = filePath.Substring( lastIndex + 1, filePath.Length - ( lastIndex + 1));

                try
                {
                    if (!fileName.EndsWith(".wav")) throw new ArgumentOutOfRangeException("Invalid parameter");
                    int radioTag = getSelectedRadioButtonTagIn(groupBox1, fileName);

                    if (radioTag > -1) throw new ArgumentNullException("null control");
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

                rb.Text = rb.Name + "\n\n" + fileName;
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
                        rb.Text = rb.Name + "\n\n" + fileName;
                        return Convert.ToInt32(rb.Tag.ToString());
                    }
                }
            }
            return -1;
        }

        private void setAudioSourceByPathAt(int tag, string filePath)
        {
            try
            {
                cachedSound[ tag ] = new CachedSound( filePath );
            }
            catch ( Exception ex )
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Main_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    AudioPlaybackEngine.Instance.PlaySound(cachedSound[0]); break;
                case Keys.E:
                    AudioPlaybackEngine.Instance.PlaySound(cachedSound[1]); break;
                case Keys.R:
                    AudioPlaybackEngine.Instance.PlaySound(cachedSound[2]); break;
                case Keys.U:
                    AudioPlaybackEngine.Instance.PlaySound(cachedSound[3]); break;
                case Keys.I:
                    AudioPlaybackEngine.Instance.PlaySound(cachedSound[4]); break;
                case Keys.O:
                    AudioPlaybackEngine.Instance.PlaySound(cachedSound[5]); break;
                case Keys.P:
                    AudioPlaybackEngine.Instance.PlaySound(cachedSound[6]); break;
                case Keys.S:
                    AudioPlaybackEngine.Instance.PlaySound(cachedSound[7]); break;
                case Keys.D:
                    AudioPlaybackEngine.Instance.PlaySound(cachedSound[8]); break;
                case Keys.F:
                    AudioPlaybackEngine.Instance.PlaySound(cachedSound[9]); break;
                case Keys.J:
                    AudioPlaybackEngine.Instance.PlaySound(cachedSound[10]); break;
                case Keys.K:
                    AudioPlaybackEngine.Instance.PlaySound(cachedSound[11]); break;
                case Keys.L:
                    AudioPlaybackEngine.Instance.PlaySound(cachedSound[12]); break;
                case Keys.OemSemicolon:
                    AudioPlaybackEngine.Instance.PlaySound(cachedSound[13]); break;
                case Keys.X:
                    AudioPlaybackEngine.Instance.PlaySound(cachedSound[9]); break;
                case Keys.C:
                    AudioPlaybackEngine.Instance.PlaySound(cachedSound[10]); break;
                case Keys.V:
                    AudioPlaybackEngine.Instance.PlaySound(cachedSound[11]); break;
                case Keys.M:
                    AudioPlaybackEngine.Instance.PlaySound(cachedSound[12]); break;
                case Keys.Oemcomma:
                    AudioPlaybackEngine.Instance.PlaySound(cachedSound[13]); break;
            }
        }
    }
}
