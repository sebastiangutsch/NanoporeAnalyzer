using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABF_Reader_and_Writer
{
    internal class ABF
    {
        private string filename;
        public ABFHeader header;
        public int SamplingRate;
        public decimal SamplePeriod; // data rate
        public int ChannelCount;
        public List<string> ChannelNames = new List<string>();
        public List<string> ChannelUnits = new List<string>();
        public int SweepCount;

        private static int BLOCK_SIZE = 512;
        private int DataByteStart;
        public int DataPointCount;
        public int SweepLength;
        public List<float> ScaleFactors = new List<float>();


        public ABF(string filepath)
        {
            filename = filepath;
            header = ABFHeader.getABFHeader(filename);
            DetermineDataProperties();
            DetermineDataScaling();
        }

        private void DetermineDataProperties()
        {
            DataByteStart = header.DataSectionPtr * BLOCK_SIZE;
            DataByteStart += header.NumPointsIgnored;
            DataPointCount = header.ActualAcqLength;
            ChannelCount = header.ADCNumChannels;
            SamplingRate = (int)(1e6 / header.ADCSampleInterval);
            SamplePeriod = (1 / (decimal)SamplingRate);
            SweepCount = header.ActualEpisodes;
            if (SweepCount == 0)
            {
                SweepCount = 1;
            }
            SweepLength = (int) (DataPointCount / (SweepCount * ChannelCount));
            ReadChannelNames();
            ReadChannelUnits();
        }
        public double[] getSweep(int channelnumber, int sweepnumber)
        {
            using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
            using (BinaryReader reader = new BinaryReader(fs))
            {
                // Move to the start position
                fs.Seek(DataByteStart, SeekOrigin.Begin);

                // List to store the processed values
                double[] data = new double[SweepLength];
                int index = 0;

                try
                {
                    // Read the first value derived from channelNumber
                    fs.Seek(DataByteStart + 2*(channelnumber)+2*(sweepnumber-1)*SweepLength, SeekOrigin.Begin);

                    while (index < SweepLength)
                    {
                        // Read a two-byte integer (assuming little-endian format)
                        int raw_data = reader.ReadInt16();
                        // Multiply the raw value by the scale factor
                        data[index] = (double)(raw_data * ScaleFactors[channelnumber]);
                        index++;
                        // Skip ahead to the next value at every channelNumber-th position
                        fs.Seek(2*(ChannelCount-1), SeekOrigin.Current);
                    }
                }
                catch (EndOfStreamException)
                {
                    // End of stream reached before filling the maximum values
                    Array.Resize(ref data, index); // Resize the array to fit actual number of read values
                }
                return data;
            }
        }

        public double[] getDataInterval(int channelnumber, int idxstart, int idxend)
        {
            using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
            using (BinaryReader reader = new BinaryReader(fs))
            {
                int length = idxend - idxstart;
                // Move to the start position
                fs.Seek(DataByteStart, SeekOrigin.Begin);

                // List to store the processed values
                double[] data = new double[length];
                int index = 0;

                try
                {
                    // Read the first value derived from channelNumber
                    fs.Seek(DataByteStart + 2 * channelnumber + 2 * idxstart, SeekOrigin.Begin);

                    while (index < length)
                    {
                        // Read a two-byte integer (assuming little-endian format)
                        int raw_data = reader.ReadInt16();
                        // Multiply the raw value by the scale factor
                        data[index] = (double)(raw_data * ScaleFactors[channelnumber]);
                        index++;
                        // Skip ahead to the next value at every channelNumber-th position
                        fs.Seek(2 * (ChannelCount - 1), SeekOrigin.Current);
                    }
                }
                catch (EndOfStreamException)
                {
                    // End of stream reached before filling the maximum values
                    Array.Resize(ref data, index); // Resize the array to fit actual number of read values
                }
                return data;
            }
        }

        public double[] getAllSweeps(int channelnumber)
        {
            using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
            using (BinaryReader reader = new BinaryReader(fs))
            {
                int length = SweepLength*SweepCount;
                // Move to the start position
                fs.Seek(DataByteStart, SeekOrigin.Begin);

                // List to store the processed values
                double[] data = new double[length];
                int index = 0;

                try
                {
                    // Read the first value derived from channelNumber
                    fs.Seek(DataByteStart + 2 * channelnumber, SeekOrigin.Begin);

                    while (index < length)
                    {
                        // Read a two-byte integer (assuming little-endian format)
                        int raw_data = reader.ReadInt16();
                        // Multiply the raw value by the scale factor
                        data[index] = (double)(raw_data * ScaleFactors[channelnumber]);
                        index++;
                        // Skip ahead to the next value at every channelNumber-th position
                        fs.Seek(2 * (ChannelCount - 1), SeekOrigin.Current);
                    }
                }
                catch (EndOfStreamException)
                {
                    // End of stream reached before filling the maximum values
                    Array.Resize(ref data, index); // Resize the array to fit actual number of read values
                }
                return data;
            }
        }

        public float[] getAllSweepsF(int channelnumber)
        {
            using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
            using (BinaryReader reader = new BinaryReader(fs))
            {
                int length = SweepLength * SweepCount;
                // Move to the start position
                fs.Seek(DataByteStart, SeekOrigin.Begin);

                // List to store the processed values
                float[] data = new float[length];
                int index = 0;

                try
                {
                    // Read the first value derived from channelNumber
                    fs.Seek(DataByteStart + 2 * channelnumber, SeekOrigin.Begin);

                    while (index < length)
                    {
                        // Read a two-byte integer (assuming little-endian format)
                        int raw_data = reader.ReadInt16();
                        // Multiply the raw value by the scale factor
                        data[index] = (raw_data * ScaleFactors[channelnumber]);
                        index++;
                        // Skip ahead to the next value at every channelNumber-th position
                        fs.Seek(2 * (ChannelCount - 1), SeekOrigin.Current);
                    }
                }
                catch (EndOfStreamException)
                {
                    // End of stream reached before filling the maximum values
                    Array.Resize(ref data, index); // Resize the array to fit actual number of read values
                }
                return data;
            }
        }

        public short[] getAllSweepsRaw(int channelnumber)
        {
            using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
            using (BinaryReader reader = new BinaryReader(fs))
            {
                int length = SweepLength * SweepCount;
                // Move to the start position
                fs.Seek(DataByteStart, SeekOrigin.Begin);

                // List to store the processed values
                short[] data = new short[length];
                int index = 0;

                try
                {
                    // Read the first value derived from channelNumber
                    fs.Seek(DataByteStart + 2 * channelnumber, SeekOrigin.Begin);

                    while (index < length)
                    {
                        // Read a two-byte integer (assuming little-endian format)
                        data[index] = reader.ReadInt16();
                        index++;
                        // Skip ahead to the next value at every channelNumber-th position
                        fs.Seek(2 * (ChannelCount - 1), SeekOrigin.Current);
                    }
                }
                catch (EndOfStreamException)
                {
                    // End of stream reached before filling the maximum values
                    Array.Resize(ref data, index); // Resize the array to fit actual number of read values
                }
                return data;
            }
        }
        public double[] getTime(int sweepnumber)
        {
            double[] time = new double[SweepLength];
            double starttime = (double)(SweepLength * (sweepnumber-1) * SamplePeriod);
            for (int i = 0; i < SweepLength; i++)
            {
                time[i] = (starttime + i * (double)SamplePeriod);
            }
            return time;
        }



        public double[] getTime(int idxstart, int idxend)
        {
            int length = idxend - idxstart;
            double[] time = new double[length];
            for (int i = 0; i < length; i++)
            {
                time[i] = ((double)(idxstart*SamplePeriod) + i * (double)SamplePeriod);
            }
            return time;
        }

        public float[] getTimeF(int idxstart, int idxend)
        {
            int length = idxend - idxstart;
            float[] time = new float[length];
            for (int i = 0; i < length; i++)
            {
                time[i] = ((float)(idxstart * SamplePeriod) + i * (float)SamplePeriod);
            }
            return time;
        }



        private void ReadChannelNames()
        {
            ChannelNames.Clear();
            for (int i = 0; i < ChannelCount; i++)
            {
                ChannelNames.Add(header.ADCChannelName[i]);
            }
        }

        private void ReadChannelUnits()
        {
            ChannelUnits.Clear();
            for (int i = 0; i < ChannelCount; i++)
            {
                ChannelUnits.Add(header.ADCUnits[i].Trim());
            }
        }

        private void DetermineDataScaling()
        {
            for (int i = 0; i < ChannelCount; i++)
            {
                ScaleFactors.Add(1/header.InstrumentScaleFactor[i]);
            }
        }
        

    }
}
