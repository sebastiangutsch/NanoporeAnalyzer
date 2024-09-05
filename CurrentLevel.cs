using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nanopore_Analyzer
{
    public class CurrentLevel(int id, int startindex, int endindex)
    {
        public int Id { get; } = id;

        public int EventId { get; set; }

        public int idxstart { get;} = startindex;

        public int idxend { get; } = endindex;

        public double tstart_s { get; set; }

        public double tend_s { get; set; }

        public int risetime { get; set; }

        public int falltime { get; set; }

        public double Irms { get; set; }

        public double Imean { get; set; }

        public double Isig { get; set; }

        public bool isBL { get; set; }

        public double BLRMS { get; set; }

        public double BLSIG { get; set; }

        public int length()
        {
            if (idxstart == idxend)
            {
                return 1;
            }
            else
            {
                return idxend - idxstart;
            }
            //return idxend - idxstart + risetime;

        }

        public double IoverIo {  get; set; }

        public string ToCSV()
        {
            return $"{Id};{EventId}; {idxstart}; {idxend}; {risetime}; {falltime}; {length()}; {Irms.ToString()}; {BLRMS.ToString()}; {IoverIo.ToString()}; {Imean.ToString()}; {Isig.ToString()}; {isBL}";
        }

    }
}
