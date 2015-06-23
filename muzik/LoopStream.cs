using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NAudio.Wave;

namespace muzik
{
    class LoopStream : WaveStream
    {
        WaveStream sourceStream;
        
        /// Creates a new Loop stream
        public LoopStream(WaveStream sourceStream)
        {
            this.sourceStream = sourceStream;
            this.EnableLooping = true;
        }
        
        /// Use this to turn looping on or off
        public bool EnableLooping { get; set; }
        
        /// Return source stream's wave format
        public override WaveFormat WaveFormat
        {
            get { return sourceStream.WaveFormat; }
        }
        
        /// LoopStream simply returns
        public override long Length
        {
            get { return sourceStream.Length; }
        }
        
        /// LoopStream simply passes on positioning to source stream
        public override long Position
        {
            get { return sourceStream.Position; }
            set { sourceStream.Position = value; }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int totalBytesRead = 0;

            while (totalBytesRead < count)
            {
                int bytesRead = sourceStream.Read(buffer, offset + totalBytesRead, count - totalBytesRead);
                if (bytesRead == 0)
                {
                    if (sourceStream.Position == 0 || !EnableLooping)
                    {
                        // something wrong with the source stream
                        break;
                    }
                    // loop
                    sourceStream.Position = 0;
                }
                totalBytesRead += bytesRead;
            }
            return totalBytesRead;
        }
    }
}
