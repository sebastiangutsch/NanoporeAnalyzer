using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ABF_Reader_and_Writer
{
    internal interface ABFHeader
    {
        public string FileSignature { get; set; } // File type used for format identification. Possible values are: “ABF “, “CLPX” and “FTCX”. Used to create the numbers in nFileType. (In old pCLAMP and Axotape data files, the first four bytes were a float: 1 = CLAMPEX, 10 = FETCHEX/AxoTape. This is translated on reading into either CLPX or FTCX as appropriate.)
        public float FileVersionNumber { get; set; } // File format version stored in the data file during acquisition. The present version is 1.65. (In old pCLAMP and Axotape data files, this parameter is in the range 2.0–5.3.)
        public short ADCNumChannels { get; set; }

        public float ADCSampleInterval { get; set; }
        public string[] ADCChannelName { get; set; }
        public string[] ADCUnits { get; set; }
        public int ActualEpisodes { get; set; }
        public int DataSectionPtr { get; set; }
        public short NumPointsIgnored { get; set; }
        public int ActualAcqLength { get; set; }

        public float[] InstrumentScaleFactor { get; set; }

        // Common method to read the file signature and version number
        public virtual void ReadCommonHeaders(BinaryReader reader)
        {
            reader.BaseStream.Seek(0, SeekOrigin.Begin);
            FileSignature = new string(reader.ReadChars(4));
            FileVersionNumber = reader.ReadSingle();
        }

        // Abstract method to be implemented by derived classes

        // Factory method to decide which class to use
        public static ABFHeader getABFHeader(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            using (BinaryReader reader = new BinaryReader(fs))
            {
                // Read the common headers first to determine the version number
                reader.BaseStream.Seek(0, SeekOrigin.Begin);
                string fileSignature = new string(reader.ReadChars(4));
                float fileVersionNumber = reader.ReadSingle();

                // Use the file version to decide which derived class to instantiate
                ABFHeader header;
                if (fileVersionNumber < 2.0)
                {
                    header = new ABF1Header(filePath);
                }
                else
                {
                    header = new ABF2Header(filePath);
                }
                return header;
            }
        }
    }
}
