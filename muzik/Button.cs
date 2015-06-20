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

        public void setCachedSound( CachedSound cachedSound)
        {
            this.cachedSound = cachedSound;
            progressBar.Maximum = (int)cachedSound.playTime;
        }

        public bool is_playing { set; get; }

        public Button(RadioButton radioButton, ProgressBar progressBar)
        {
            progressBar.MarqueeAnimationSpeed = 0;
            t = new System.Threading.Timer(new System.Threading.TimerCallback(delegate (object state){
                is_playing = false;
            }));
            this.radioButton = radioButton;
            this.progressBar = progressBar;
        }

        public void playSample()
        {
            if( !is_playing )
            {
                AudioPlaybackEngine.Instance.PlaySound(cachedSound);
                t.Change((int)cachedSound.playTime, 0);
                is_playing = true;
                startProgressBar();
            }
        }

        public void startProgressBar()
        {
            progressBar.Style = ProgressBarStyle.Marquee;
            progressBar.MarqueeAnimationSpeed = 10;
        }

        public void stopProgressBar()
        {
            progressBar.Style = ProgressBarStyle.Continuous;
            progressBar.MarqueeAnimationSpeed = 0;
        }

        
    }
}
