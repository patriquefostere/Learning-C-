using System;
using System.IO;
using System.Text;

// stolen from here: https://www.noobdoaaa.com/read=3
namespace wavReversing
{
    public class wavReverser
    {
        #region declarations

        //public static string file = @"C:\Users\Surface\Desktop\coding\C#\messing with audio\my code\please.wav";

        public string wavFile;

        byte[] allData;
        byte[] header;
        byte[] data;// to reverse
        byte[] dataReversed;

        int channels, sampleRate, byteRate, bitDepth, allDataSize;

        #endregion


        public void readWav()
        {
            FileStream f = new FileStream(wavFile, FileMode.Open, FileAccess.Read, FileShare.Read);
            allDataSize = (int)f.Length;

            allData = new byte[allDataSize];
            f.Read(allData, 0, allDataSize);

            header = new byte[44]; //RIFF headers are 44 bytes long
            data = new byte[allDataSize-44];

            channels = BitConverter.ToInt16(allData, 22);

            sampleRate = BitConverter.ToInt32(allData, 24);

            byteRate = BitConverter.ToInt32(allData, 28);
            bitDepth = BitConverter.ToInt16(allData, 34);

            if(channels != 1 && channels != 2)
            {
                Console.WriteLine("error reading .wav file");
            }

            for(int i =0; i<allDataSize;i++)
            {
                if(i<44)
                {
                    header[i] = allData[i];
                }
                else
                {
                data[i-44] = allData[i];
                }
            }

            f.Close(); 
        }

        private void reverse()
        {
            //wave files are either mono or stereo (ie channels = 1 or 2) and have a bit depth of 8 or 16
            // so we have 4 cases that we must consider
            int block = 0;

            if (channels == 1 && bitDepth == 8)
            {
                //each sample is 1 byte
                block = 1;
            }else if(channels == 2 && bitDepth == 8 || channels == 1 && bitDepth == 16)
            {
                // each sample is two bytes, one for each channel; 
                // or each sample is two bytes, one being a 'low order byte' and the other 'high order'
                block = 2;
            }else if(channels == 2 && bitDepth == 16)
            {
                // each sample is 4 bytes, with bytes bytes for each channel
                block = 4;
            }
        

            int length = data.Length;
            dataReversed = new byte[length];

            for(int i = 1; i<(length/block)+1; i++)
            {
                for(int j = 0; j<block; j++)
                {
                    dataReversed[block*(i-1)+j] = data[length - i*block + j];
                }  
            } 
        }

        public string writeWav(byte[] someData)
        {
            byte[] finalData = new byte[allDataSize];
            header.CopyTo(finalData, 0);
            someData.CopyTo(finalData, header.Length);

            string outputFile = wavFile.Substring(0, wavFile.Length-4)+"_Reversed.wav";
            
            FileStream fs = new FileStream(outputFile, FileMode.Create);
            BinaryWriter wr = new BinaryWriter(fs);
            wr.Write(finalData);
            fs.Close();

            return outputFile;
        }

        public string semiMain(string input)
        {
            wavFile = input;

            readWav();
            reverse();
            return writeWav(dataReversed);
        }
    }   
}