using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Nanopore_Analyzer
{
    public class Transition(int id, int startindex, int endindex, bool up)
    {
        public int Id { get; } = id;
        public int idxstart { get;} = startindex;
        public int idxend { get; } = endindex;
        public double tstart_s { get; set; }
        public double tend_s { get; set; }
        public double val_start {  get; set; }
        public double val_end { get; set; }
        public bool IsUp {  get; } = up;
        public bool IsDown { get; } = !up;

        public bool FromBL { get; set; } = false;

        public bool ToBL { get; set; } = false;

        public double BLRMS { get; set; }

        public double BLSIG {  get; set; }

        public bool IsValid { get; set; } = true;
        public int length() { return idxend - idxstart; }

        public string ToCSV()
        {
            return $"{Id},{idxstart},{idxend},{IsUp}, {FromBL}, {ToBL}, {IsValid}";
        }
    }
}
