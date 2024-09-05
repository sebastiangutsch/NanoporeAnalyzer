using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Nanopore_Analyzer
{
    public class Event(int id, int startindex, int endindex)
    {
        public int Id { get; set; } = id;
        public int idxstart { get; set; } = startindex;
        public int idxend { get; set; } = endindex;

        public bool hasPreBlock { get; set; } = false;

        public bool hasPostBlock { get; set; } = false;

        public List<Transition> transitions { get; set; } = new List<Transition>();

        public List<CurrentLevel> currentlevels { get; set; } = new List<CurrentLevel>();

        public int eventlength()
        {
            if (idxstart == idxend) return 1;
            else
                return idxend - idxstart;
        }

        public int blocklength()
        {
            //int length =  transitions.Last().idxstart - transitions[0].idxend;
            int length = transitions.Last().idxstart - idxstart;
            if (length == 0) return 1;
            else
                return length;
        }

        public double meancurrentlevel()
        {
            if (currentlevels.Count == 0) return transitions[0].val_end;
            else
            // Calculate the sum of products of lengths and values
            {
                double totalValue = currentlevels.Sum(item => item.length() * item.Imean);

                // Calculate the sum of lengths
                double totalLength = currentlevels.Sum(item => item.length());

                // Return the weighted average
                return totalValue / totalLength;
            }
        }
    }
}
