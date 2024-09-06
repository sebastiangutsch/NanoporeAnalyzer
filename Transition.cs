using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Nanopore_Analyzer
{
    public class Transition(int id, int startindex, int endindex, bool up):ICloneable
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
        // Implementation of the ICloneable interface
        public object Clone()
        {
            // Create a new instance of Transition with copied values
            return new Transition(Id, idxstart, idxend, IsUp)
            {
                tstart_s = this.tstart_s,
                tend_s = this.tend_s,
                val_start = this.val_start,
                val_end = this.val_end,
                FromBL = this.FromBL,
                ToBL = this.ToBL,
                BLRMS = this.BLRMS,
                BLSIG = this.BLSIG,
                IsValid = this.IsValid
            };
        }
    }
}
