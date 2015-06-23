using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace muzik
{
    class Button
    {
        private System.Threading.Timer t;
        private RadioButton radioButton;
        private ProgressBar progressBar;
        private CachedSound cachedSound;
        private WaveFileReader reader;
        private LoopStream loop;
        private WaveOut waveOut;
        private CheckBox checkBox;

        public void setCachedSound( string filePath )
        {
            this.cachedSound = new CachedSound(filePath);
            reader = new WaveFileReader(filePath);
            loop = new LoopStream(reader);


            progressBar.Maximum = (int)cachedSound.playTime;
        }
        
        public bool is_playing { set; get; }

        public Button(RadioButton radioButton, ProgressBar progressBar, CheckBox checkBox)
        {
            progressBar.MarqueeAnimationSpeed = 0;
            t = new System.Threading.Timer(new System.Threading.TimerCallback(delegate (object state){
                is_playing = false;
            }));
            this.radioButton = radioButton;
            this.progressBar = progressBar;
            this.checkBox = checkBox;
        }

        public void playSample()
        {
            if(checkBox.Checked == true)
            {
                playSampleLoopToggle();
            }
            else
            {
                playSampleOnce();
            }
        }

        public void playSampleOnce()
        {
            if( !is_playing )
            {
                AudioPlaybackEngine.Instance.PlaySound(cachedSound);
                t.Change((int)cachedSound.playTime, 0);
                is_playing = true;
                startProgressBar();
            }
        }

        public void playSampleLoopToggle()
        {
            if (!is_playing)
            {
                waveOut = new WaveOut();
                loop.Position = 0;
                waveOut.Init(loop);
                waveOut.Play();
                is_playing = true;
                startProgressBar();
            } else
            {
                is_playing = false;
                waveOut.Stop();
                waveOut.Dispose();
                waveOut = null;
            }
        }

        public void startProgressBar()
        {
            progressBar.Style = ProgressBarStyle.Marquee;
            progressBar.MarqueeAnimationSpeed = 1;
        }

        public void stopProgressBar()
        {
            progressBar.Style = ProgressBarStyle.Continuous;
            progressBar.MarqueeAnimationSpeed = 0;
        }
    }
}
