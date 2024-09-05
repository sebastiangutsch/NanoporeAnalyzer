using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABF_Reader_and_Writer
{
    internal class ABF2Header : ABFHeader
    {
        // Properties to hold the data read from the file
        public string FileSignature { get; set; }
        public float FileVersionNumber { get; set; }
        public short OperationMode { get; set; }
        public int ActualAcqLength { get; set; }
        public short NumPointsIgnored { get; set; }
        public int ActualEpisodes { get; set; }
        public int FileStartDate { get; set; }
        public int FileStartTime { get; set; }
        public int DataSectionPtr { get; set; }
        public int TagSectionPtr { get; set; }
        public int NumTagEntries { get; set; }
        public int SynchArrayPtr { get; set; }
        public int SynchArraySize { get; set; }
        public short DataFormat { get; set; }
        public short ADCNumChannels { get; set; }
        public float ADCSampleInterval { get; set; }
        public float SynchTimeUnit { get; set; }
        public int NumSamplesPerEpisode { get; set; }
        public int PreTriggerSamples { get; set; }
        public int EpisodesPerRun { get; set; }
        public float ADCRange { get; set; }
        public int ADCResolution { get; set; }
        public short FileStartMillisecs { get; set; }
        public short[] ADCPtoLChannelMap { get; set; } = new short[16];
        public short[] ADCSamplingSeq { get; set; } = new short[16];
        public string[] ADCChannelName { get; set; } = new string[16];
        public string[] ADCUnits { get; set; } = new string[16];
        public float[] ADCProgrammableGain { get; set; } = new float[16];
        public float[] InstrumentScaleFactor { get; set; } = new float[16];
        public float[] InstrumentOffset { get; set; } = new float[16];
        public float[] SignalGain { get; set; } = new float[16];
        public float[] SignalOffset { get; set; } = new float[16];
        public short DigitalEnable { get; set; }
        public short ActiveDACChannel { get; set; }
        public short DigitalHolding { get; set; }
        public short DigitalInterEpisode { get; set; }
        public short[] DigitalValue { get; set; } = new short[10];
        public int[] DACFilePtr { get; set; } = new int[2];
        public int[] DACFileNumEpisodes { get; set; } = new int[2];
        public float[] DACCalibrationFactor { get; set; } = new float[4];
        public float[] DACCalibrationOffset { get; set; } = new float[4];
        public short[] WaveformEnable { get; set; } = new short[2];
        public short[] WaveformSource { get; set; } = new short[2];
        public short[] InterEpisodeLevel { get; set; } = new short[2];
        public short[] EpochType { get; set; } = new short[20];
        public float[] EpochInitLevel { get; set; } = new float[20];
        public float[] EpochLevelInc { get; set; } = new float[20];
        public int[] EpochInitDuration { get; set; } = new int[20];
        public int[] EpochDurationInc { get; set; } = new int[20];
        public short[] TelegraphEnable { get; set; } = new short[16];
        public float[] TelegraphAdditGain { get; set; } = new float[16];
        public string ProtocolPath { get; set; }

        public ABF2Header(string filePath) // Method to read values from an ABF1 file and populate the properties
        {
            ReadFromFile(filePath);
        }

        // Method to read values from an ABF1 file and populate the properties
        private void ReadFromFile(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            using (BinaryReader reader = new BinaryReader(fs))
            {
                FileSignature = new string(reader.ReadChars(4));
                FileVersionNumber = reader.ReadSingle();
                OperationMode = reader.ReadInt16();
                ActualAcqLength = reader.ReadInt32();
                NumPointsIgnored = reader.ReadInt16();
                ActualEpisodes = reader.ReadInt32();
                FileStartDate = reader.ReadInt32();

                reader.BaseStream.Seek(40, SeekOrigin.Begin);
                DataSectionPtr = reader.ReadInt32();

                reader.BaseStream.Seek(44, SeekOrigin.Begin);
                TagSectionPtr = reader.ReadInt32();

                reader.BaseStream.Seek(48, SeekOrigin.Begin);
                NumTagEntries = reader.ReadInt32();

                reader.BaseStream.Seek(92, SeekOrigin.Begin);
                SynchArrayPtr = reader.ReadInt32();

                reader.BaseStream.Seek(96, SeekOrigin.Begin);
                SynchArraySize = reader.ReadInt32();

                reader.BaseStream.Seek(100, SeekOrigin.Begin);
                DataFormat = reader.ReadInt16();

                reader.BaseStream.Seek(120, SeekOrigin.Begin);
                ADCNumChannels = reader.ReadInt16();

                reader.BaseStream.Seek(122, SeekOrigin.Begin);
                ADCSampleInterval = reader.ReadSingle();

                reader.BaseStream.Seek(130, SeekOrigin.Begin);
                SynchTimeUnit = reader.ReadSingle();

                reader.BaseStream.Seek(138, SeekOrigin.Begin);
                NumSamplesPerEpisode = reader.ReadInt32();

                reader.BaseStream.Seek(142, SeekOrigin.Begin);
                PreTriggerSamples = reader.ReadInt32();

                reader.BaseStream.Seek(146, SeekOrigin.Begin);
                EpisodesPerRun = reader.ReadInt32();

                reader.BaseStream.Seek(244, SeekOrigin.Begin);
                ADCRange = reader.ReadSingle();

                reader.BaseStream.Seek(252, SeekOrigin.Begin);
                ADCResolution = reader.ReadInt32();

                reader.BaseStream.Seek(366, SeekOrigin.Begin);
                FileStartMillisecs = reader.ReadInt16();

                reader.BaseStream.Seek(378, SeekOrigin.Begin);
                for (int i = 0; i < 16; i++) ADCPtoLChannelMap[i] = reader.ReadInt16();

                reader.BaseStream.Seek(410, SeekOrigin.Begin);
                for (int i = 0; i < 16; i++) ADCSamplingSeq[i] = reader.ReadInt16();

                reader.BaseStream.Seek(442, SeekOrigin.Begin);
                for (int i = 0; i < 16; i++) ADCChannelName[i] = new string(reader.ReadChars(10));

                reader.BaseStream.Seek(602, SeekOrigin.Begin);
                for (int i = 0; i < 16; i++) ADCUnits[i] = new string(reader.ReadChars(8));

                reader.BaseStream.Seek(730, SeekOrigin.Begin);
                for (int i = 0; i < 16; i++) ADCProgrammableGain[i] = reader.ReadSingle();

                reader.BaseStream.Seek(922, SeekOrigin.Begin);
                for (int i = 0; i < 16; i++) InstrumentScaleFactor[i] = reader.ReadSingle();

                reader.BaseStream.Seek(986, SeekOrigin.Begin);
                for (int i = 0; i < 16; i++) InstrumentOffset[i] = reader.ReadSingle();

                reader.BaseStream.Seek(1050, SeekOrigin.Begin);
                for (int i = 0; i < 16; i++) SignalGain[i] = reader.ReadSingle();

                reader.BaseStream.Seek(1114, SeekOrigin.Begin);
                for (int i = 0; i < 16; i++) SignalOffset[i] = reader.ReadSingle();

                reader.BaseStream.Seek(1436, SeekOrigin.Begin);
                DigitalEnable = reader.ReadInt16();

                reader.BaseStream.Seek(1440, SeekOrigin.Begin);
                ActiveDACChannel = reader.ReadInt16();

                reader.BaseStream.Seek(1584, SeekOrigin.Begin);
                DigitalHolding = reader.ReadInt16();

                reader.BaseStream.Seek(1586, SeekOrigin.Begin);
                DigitalInterEpisode = reader.ReadInt16();

                reader.BaseStream.Seek(2588, SeekOrigin.Begin);
                for (int i = 0; i < 10; i++) DigitalValue[i] = reader.ReadInt16();

                reader.BaseStream.Seek(2048, SeekOrigin.Begin);
                for (int i = 0; i < 2; i++) DACFilePtr[i] = reader.ReadInt32();

                reader.BaseStream.Seek(2056, SeekOrigin.Begin);
                for (int i = 0; i < 2; i++) DACFileNumEpisodes[i] = reader.ReadInt32();

                reader.BaseStream.Seek(2074, SeekOrigin.Begin);
                for (int i = 0; i < 4; i++) DACCalibrationFactor[i] = reader.ReadSingle();

                reader.BaseStream.Seek(2090, SeekOrigin.Begin);
                for (int i = 0; i < 4; i++) DACCalibrationOffset[i] = reader.ReadSingle();

                reader.BaseStream.Seek(2296, SeekOrigin.Begin);
                for (int i = 0; i < 2; i++) WaveformEnable[i] = reader.ReadInt16();

                reader.BaseStream.Seek(2300, SeekOrigin.Begin);
                for (int i = 0; i < 2; i++) WaveformSource[i] = reader.ReadInt16();

                reader.BaseStream.Seek(2304, SeekOrigin.Begin);
                for (int i = 0; i < 2; i++) InterEpisodeLevel[i] = reader.ReadInt16();

                reader.BaseStream.Seek(2308, SeekOrigin.Begin);
                for (int i = 0; i < 20; i++) EpochType[i] = reader.ReadInt16();

                reader.BaseStream.Seek(2348, SeekOrigin.Begin);
                for (int i = 0; i < 20; i++) EpochInitLevel[i] = reader.ReadSingle();

                reader.BaseStream.Seek(2428, SeekOrigin.Begin);
                for (int i = 0; i < 20; i++) EpochLevelInc[i] = reader.ReadSingle();

                reader.BaseStream.Seek(2508, SeekOrigin.Begin);
                for (int i = 0; i < 20; i++) EpochInitDuration[i] = reader.ReadInt32();

                reader.BaseStream.Seek(2588, SeekOrigin.Begin);
                for (int i = 0; i < 20; i++) EpochDurationInc[i] = reader.ReadInt32();

                reader.BaseStream.Seek(4512, SeekOrigin.Begin);
                for (int i = 0; i < 16; i++) TelegraphEnable[i] = reader.ReadInt16();

                reader.BaseStream.Seek(4576, SeekOrigin.Begin);
                for (int i = 0; i < 16; i++) TelegraphAdditGain[i] = reader.ReadSingle();

                reader.BaseStream.Seek(4898, SeekOrigin.Begin);
                ProtocolPath = new string(reader.ReadChars(384));
            }
        }

    }
}
