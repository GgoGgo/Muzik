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

namespace muzik
{
    public partial class Main : Form
    {
        private LinkedList<IWavePlayer> waveOutDevice;
        private AudioFileReader audioFileReader;

        public Main()
        {
            InitializeComponent();

            waveOutDevice = new LinkedList<IWavePlayer>();

            for(int i = 0; i < 5; i++)
            {
                waveOutDevice.AddLast(new WaveOut());
            }
            
            groupBox1.Controls.Add(radioButton1);
            groupBox1.Controls.Add(radioButton2);
            groupBox1.Controls.Add(radioButton3);
            groupBox1.Controls.Add(radioButton4);
            groupBox1.Controls.Add(radioButton5);
            
        }

        private void Main_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            radioButton1.Select();
        }

        private void Main_KeyPress(object sender, KeyPressEventArgs e)
        {
            MessageBox.Show("Event called");
            switch (e.KeyChar)
            {
                case 's':
                    waveOutDevice.ElementAt<IWavePlayer>(0).Play();
                    break;
                case 'd':
                    waveOutDevice.ElementAt<IWavePlayer>(1).Play();
                    break;
                case 'f':
                    waveOutDevice.ElementAt<IWavePlayer>(2).Play();
                    break;
                case 'j':
                    waveOutDevice.ElementAt<IWavePlayer>(3).Play();
                    break;
                case 'k':
                    waveOutDevice.ElementAt<IWavePlayer>(4).Play();
                    break;
                case 'l':
                    waveOutDevice.ElementAt<IWavePlayer>(5).Play();
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if( openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach( Control c in groupBox1.Controls)
                {
                    if(c.GetType() == typeof(RadioButton))
                    {
                        RadioButton rb = c as RadioButton;
                        if (rb.Checked)
                        {
                            audioFileReader = new AudioFileReader( openFileDialog1.FileName );
                            try
                            {
                                waveOutDevice.ElementAt<IWavePlayer>(Convert.ToInt32(rb.Tag.ToString())-1).Init(audioFileReader);
                            }
                            catch(Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                    }
                }
            }
        }

        
    }
}
