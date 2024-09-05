using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABF_Reader_and_Writer
{
    internal class ABF1Header:ABFHeader
    {
        // Properties to hold the data read from the file
        public string FileSignature { get; set; } 
        public float FileVersionNumber { get; set; }
        public short OperationMode { get; set; } // Operation mode: 1 = Event-driven, variable length; 2 = Oscilloscope, loss free (Same as Event-driven, fixed length); 3 = Gap-free; 4 = Oscilloscope, high-speed; 5 = episodic stimulation (Clampex only).
        public int ActualAcqLength { get; set; } // Actual number of ADC samples(aggregate) in data file.See lAcqLength.Averaged sweeps are included.
        public short NumPointsIgnored { get; set; } // Number of points ignored at data start. Normally zero, but non-zero for gap-free acquisition using AXOLAB configurations with one or more ADS boards.
        public int ActualEpisodes { get; set; } // Actual number of sweeps in the file including averaged sweeps. See lEpisodesPerRun. If nOperationMode = 3 (gap-free) the value of this parameter is 1.
        public int FileStartDate { get; set; } // Date when data portion of this file was first written to. Stored as YYMMDD. If YY is in the range 80–99, prefix with “19” to get the year. If YY is in the range 00–79, prefix with “20” to get the year.
        public int FileStartTime { get; set; } // Time of day in seconds past midnight when data portion of this file was first written to.
        public int DataSectionPtr { get; set; } // Block number of start of Data section.
        public int TagSectionPtr { get; set; } // Block number of start of Tag section.
        public int NumTagEntries { get; set; } // Number of Tag entries.
        public int SynchArrayPtr { get; set; } // Block number of start of the Synch Array section.
        public int SynchArraySize { get; set; } // Number of pairs of entries in the Synch Array section. If averaging is enabled, this includes the entry for the averaged sweep.
        public short DataFormat { get; set; } // Data representation. 0 = 2-byte integer; 1 = IEEE 4 byte float.
        public short ADCNumChannels { get; set; } // number of input channels recorded

        /**
        *  The two sample intervals must be an integer multiple (or submultiple) of each other.
        *  The documentation says these two sample intervals are the interval between multiplexed samples, but not all digitisers work like that.
        *  Instead, these are the per-channel sample rate divided by the number of channels.
        *  If the user chose 100uS and has two channels, this value will be 50uS.
        */
        public float ADCSampleInterval { get; set; }
        public float SynchTimeUnit { get; set; }

        /**
        * The total number of samples per episode, for the recorded channels only.
        * This does not include channels which are acquired but not recorded.
        * This is the number of samples per episode per channel, times the number of recorded channels.
        * If you want the samples per episode for one channel, you must divide this by get_channel_count_recorded().
        */
        public int NumSamplesPerEpisode { get; set; }
        public int PreTriggerSamples { get; set; }
        public int EpisodesPerRun { get; set; }
        public float ADCRange { get; set; } // ADC positive full-scale input in volts (e.g. 10.00V).
        public int ADCResolution { get; set; } // Number of ADC counts corresponding to the positive full-scale voltage in ADCRange (e.g. 2000, 2048, 32000 or 32768). 
        public short FileStartMillisecs { get; set; } // Milliseconds portion of lFileStartTime
        public short[] ADCPtoLChannelMap { get; set; } = new short[16];  // ADC physical-to-logical channel map. The entries are in the physical order 0, 1, 2,…, 14, 15. If there are fewer than 16 logical channels in the system, the array is padded with -1. All channels supported by the hardware are present, even if only a subset is used. For example, for the TL-2 the entries would be 7, 6, 5, 4, 3, 2, 1, 0, -1,…, -1.
        public short[] ADCSamplingSeq { get; set; } = new short[16]; // ADC channel sampling sequence. This is the order in which the physical ADC channels are sampled. If fewer than the maximum number of channels are sampled, pad with -1. For example, if two channels are sampled on the TL-2, this array will contain 6, 7, -1,…, -1. If two channels are sampled on the TL-1, this array will contain 14, 15, -1,…, -1.
        public string[] ADCChannelName { get; set; } = new string[16]; // ADC channel name in physical channel number order. Default = spaces.
        public string[] ADCUnits { get; set; } = new string[16]; // The user units for ADC channels in physical channel number order. Default = spaces.
        public float[] ADCProgrammableGain { get; set; } = new float[16]; // ADC programmable gain in physical channel number order(dimensionless). Default = 1. 
        public float[] InstrumentScaleFactor { get; set; } = new float[16]; // Instrument scale factor in physical ADC channel number order (Volts at ADC / user unit). (Programs would normally display this information to the user as user units / volt at ADC).
        public float[] InstrumentOffset { get; set; } = new float[16]; // Instrument offset in physical ADC channel number order (user units corresponding to 0 V at the ADC). Default is zero.
        public float[] SignalGain { get; set; } = new float[16]; // Signal conditioner gain in physical ADC channel number order (dimensionless). Default = 1.
        public float[] SignalOffset { get; set; } = new float[16]; // Signal conditioner offset in physical ADC channel number order (user units). Default = 0.
        public short DigitalEnable { get; set; } // Enable digital outputs: 0 = No; 1 = Yes.
        public short ActiveDACChannel { get; set; } // Active DAC channel, i.e. the one used for waveform generation.
        public short DigitalHolding { get; set; } // Holding value for digital output.
        public short DigitalInterEpisode { get; set; } // Inter-sweep digital holding value: 0 = Use holding value; 1 = Use last epoch value.
        public short[] DigitalValue { get; set; } = new short[10]; // Epoch value for digital output
        public int[] DACFilePtr { get; set; } = new int[2]; // Block number of start of DAC file section.
        public int[] DACFileNumEpisodes { get; set; } = new int[2]; // Number of sweeps in the DAC file section. Sweeps are not multiplexed
        public float[] DACCalibrationFactor { get; set; } = new float[4]; // Calibration factor for each DAC.
        public float[] DACCalibrationOffset { get; set; } = new float[4]; // Calibration offset for each DAC.
        public short[] WaveformEnable { get; set; } = new short[2];// Analog waveform enabled: 0 = No; 1 = Yes.
        public short[] WaveformSource { get; set; } = new short[2]; // Analog waveform source: 0 = Disable; 1 = Generate waveform from epoch definitions; 2 = Generate waveform from a DAC file.
        public short[] InterEpisodeLevel { get; set; } = new short[2]; // Inter-sweep holding level: 0 = Use holding level; 1 = Use last epoch amplitude.
        public short[] EpochType { get; set; } = new short[20]; // Epoch type: 0 = Disabled; 1 = Step; 2 = Ramp.Indexes: analog out waveform, epoch number.
        public float[] EpochInitLevel { get; set; } = new float[20];// Epoch initial level (user units).
        public float[] EpochLevelInc { get; set; } = new float[20];// Epoch level increment (user units).
        public int[] EpochInitDuration { get; set; } = new int[20]; // Epoch initial duration (in sequence counts).
        public int[] EpochDurationInc { get; set; } = new int[20]; // Epoch duration increment (in sequence counts).
        public short[] TelegraphEnable { get; set; } = new short[16]; // Telegraphs enabled in ADC channels: 0 = No; 1 = Yes.Index: ADC channel
        public float[] TelegraphAdditGain { get; set; } = new float[16]; // Additional gain multiplier of Instrument. (Default = 1.)Index: ADC channel
        public string ProtocolPath { get; set; } // File path of protocol.



        public ABF1Header(string filePath) // Method to read values from an ABF1 file and populate the properties
        {
            ReadFromFile(filePath);
        }
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
                FileStartTime = reader.ReadInt32();

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
