using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using ABF_Reader_and_Writer;
using MathNet.Numerics;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using NationalInstruments.Tdms;
using MathNet.Numerics.Distributions;

namespace Nanopore_Analyzer
{
    public class Analyzer
    {
        // DATA
        private ABF abf; // ABF Data Interface Handler
        private short[] raw_data; // whole channel raw data in 2-byte format (memory needed ~size of ABF File)
        private double[] filtered_data;
        public string unitI;
        private double factorI;
        public int SweepCount { get; } // total number of sweeps in ABF file
        private int tp; // total points per channel 
        public int ChannelCount { get; } // total number of channels
        private int activechannel; // selected channel
        public decimal SampleRate { get; set; } // sample rate in Hz
        public decimal SamplePeriod { get; set; } // sample period in s

        // BASELINE
        private List<double> baseline = new List<double>(); // stored actual baseline values
        private List<double> bl_sigma = new List<double>(); // stored standard deviation for baseline values
        private List<double> bl_time = new List<double>(); // accompanying time data for x-axis
        public decimal bl_start; // time value from where the baseline is stored -> valid range minimum
        public decimal bl_end; // time value until where the baseline is stored -> valid range maximum
        public decimal bl_interval; // averaging interval that was used
        public decimal bl_stepsize; // stepsize that was used to calculate the baseline in the interval
        public float Iref { get; set; } // reference current
        public bool useBLcorr { get; set; } = false; // indicates if baseline corrected data is used


        // DETECTION
        private int f_sigma = 3; // parameter for rectification method
        public List<Transition> transitions = new List<Transition>(); // detected transitions
        public List<CurrentLevel> currentlevels = new List<CurrentLevel>(); // detected currentlevels
        public List<Event> events = new List<Event>(); // detected events
 
        public Analyzer(string filepath)
        {
            abf = new ABF(filepath);
            unitI = abf.ChannelUnits[0];
            switch (unitI)
            {
                case "pA": factorI = 1; break;
                case "nA": factorI = 1000; break;
                default: factorI = 1; break;
            }
            SweepCount = abf.SweepCount;
            ChannelCount = abf.ChannelCount;
            raw_data = abf.getAllSweepsRaw(0); // preload all data of first channel
            //data = data.Select(Math.Abs).ToArray(); // make all data positive
            tp = abf.DataPointCount/ChannelCount;
            //tp = 0;
            //for (int i = 0; i < sweepcount; i++)
            //{
            //    data = _abf.GetSweepF(i, 0);
            //    tp += data.Length;
            //}
            activechannel = 0;
            SampleRate = abf.SamplingRate;
            SamplePeriod = abf.SamplePeriod;
        }

        // ----------- File Information -------------
        // ------------------------------------------
        // ------------------------------------------
        public string abfversion()
        {
            return "ABF Version: " + abf.header.FileVersionNumber;
        }

        public void SetChannel(int channel) // note the channel number from 1...N
        {
            activechannel = channel;
            raw_data = abf.getAllSweepsRaw(activechannel); // preload all data of new channel
        }
   
        public decimal MeasurementLength()
        {
            return tp * SamplePeriod;
        }

        // ------------- Plots ----------------------
        // ------------------------------------------
        // ------------------------------------------

        public (double[], double[]) GetInterval(decimal starttime, decimal endtime) // start and end in s
        {
            // determine startindex and endindex
            int startidx = GetIndex(starttime);
            int endidx = GetIndex(endtime) - 1;
            if (startidx < 0 || endidx >= raw_data.Length || startidx > endidx)
                throw new ArgumentOutOfRangeException("Invalid start or end index.");
            int length = endidx - startidx;
            double[] x = abf.getTime(startidx, endidx);
            double[] y = new double[length];
            for (int i = 0; i < length; i++) y[i] = DData(startidx+i);
            return (x, y);
        }

        public (double[], double[]) GetFilter(decimal starttime, decimal endtime, int cutoff)
        {
            // determine startindex and endindex
            int startidx = GetIndex(starttime);
            int endidx = GetIndex(endtime) - 1;

            List<double> tmp_data = new List<double>();
            for (int i = startidx; i <= endidx; i++)
            {
                tmp_data.Add(DData(i));
            }
            filtered_data = Butterworth(tmp_data.ToArray(), (double)SamplePeriod, (double)cutoff);

            double[] time = new double[filtered_data.Length];
            for (int i = 0; i < filtered_data.Length; i++)
            {
                time[i] = (double)(starttime + SamplePeriod*i);
            }
            return (time, filtered_data);
        }

        public (double[], double[], double[]) GetRect(decimal starttime, decimal endtime)
        {
            // determine startindex and endindex
            int startidx = GetIndex(starttime);
            int endidx = GetIndex(endtime) - 1;

            List<float> tmp_data = new List<float>();
            for (int i = startidx; i <= endidx; i++)
            {
                tmp_data.Add(FData(i));
            }
            int length = tmp_data.Count;
            double[] time = new double[length];
            double[] dataup = new double[length];
            double[] datadown = new double[length];
            dataup[0] = 0;
            datadown[0] = 0;
            int j;
            for (int i = 0; i < (length - 1); i++)
            {
                j = i + 1;
                if (Math.Abs(tmp_data[j]) > Math.Abs(tmp_data[i]))
                {
                    dataup[j] = dataup[i] + (Math.Abs(tmp_data[j]) - Math.Abs(tmp_data[i]));
                }
                else dataup[j] = 0;

                if (Math.Abs(tmp_data[j]) < Math.Abs(tmp_data[i]))
                {
                    datadown[j] = datadown[i] + (Math.Abs(tmp_data[j]) - Math.Abs(tmp_data[i]));
                }
                else datadown[j] = 0;
                time[i] = (double)(starttime + SamplePeriod * i);
            }

            return (time, dataup, datadown);
        }

        public (double[], double[]) GetBaselineFromBaseLevels()
        {
            List<CurrentLevel> baselinelevels = currentlevels.Where(e => e.isBL == true).ToList();
            double[] time = new double[baselinelevels.Count];
            double[] values = new double[baselinelevels.Count];

            for (int i = 0; i < baselinelevels.Count; i++)
            {
                CurrentLevel lev = baselinelevels[i];
                time[i] = (double)(((decimal)lev.idxstart + (decimal)lev.idxend) / 2 * SamplePeriod);
                values[i] = lev.Irms;
            }
            return (time, values);
        }

        public (double[], double[]) AutoDetectBaseline(decimal interval, decimal stepsize, decimal starttime, decimal endtime)
        {
            // determine number of datapoints per interval and interval size

            int numberofpoints = (int)Math.Round((endtime - starttime) / stepsize);

            bl_time.Clear();
            baseline.Clear();
            bl_sigma.Clear();

            bl_start = starttime;
            bl_end = endtime;
            bl_interval = interval;
            bl_stepsize = stepsize;


            for (int i = 0; i < numberofpoints; i++)
            {
                decimal time = (starttime + (i + (decimal)0.5) * stepsize);
                bl_time.Add((double)time);
                // (double rms, double sigma) = AutoDetectRMSandSigma(starttime + i * stepsize, starttime + (i + 1) * stepsize); // without moving average
                decimal start = starttime + i * stepsize - interval / 2;
                decimal end = starttime + i * stepsize+interval/2;
                if (start < starttime) start = starttime;
                if (end > endtime) end = endtime;
                (double rms, double sigma) = AutoDetectRMSandSigma(start, end); // include moving average
                baseline.Add(rms);
                bl_sigma.Add(sigma);
            }
            return (bl_time.ToArray(), baseline.ToArray());
        }

        public (double[], double[], Event, int) getEvent(int id)
        {
            Event ev = events[0];
            int eventIndex = 0;
            for (int i = 0; i < events.Count; i++)
            {
                if (events[i].Id == id)
                {
                    ev = events[i];
                    eventIndex = i;
                    break; // break the loop once the event is found
                }
            }
            int start = ev.idxstart - 200;
            if (start < 0) start = 0; // avoid negative index for very early peaks
            int end = ev.idxend + 200;
            if (end >= tp) end = tp - 1; // avoid out of bounds for peaks at data end
            List<double> ev_data = new List<double>();
            List<double> ev_time = new List<double>();
            for (int i = start; i < end; i++)
            {
                ev_data.Add((double)FData(i));
                ev_time.Add((double)GetTime(i));
            }
            
            return (ev_time.ToArray(), ev_data.ToArray(), ev, eventIndex);
        }

        public (double[], double[], int[], double[], double[]) getDToverCurrentLevel(float min = 0, float max = 100000, double binWidth = 0.1)
        {
            List<CurrentLevel> sel_levels = currentlevels.Where(e => e.Irms >= min && e.Irms <= max && e.length() > 0 && e.EventId > 0).ToList();

            if (sel_levels.Count == 0)
            {
                sel_levels = currentlevels;
            }
            double[] current = new double[sel_levels.Count];
            double[] dwell = new double[sel_levels.Count];
            int[] eventID = new int[sel_levels.Count];

            for (int i = 0; i < sel_levels.Count; i++)
            {
                current[i] = sel_levels[i].Irms;
                dwell[i] = (double)sel_levels[i].length() / (double)SampleRate * 1000;
                eventID[i] = sel_levels[i].EventId;
            }

            // Define bin width
            binWidth /= factorI; // Adjust bin width for current factor
            double[] Irms = sel_levels.Select(x=>x.Irms).ToArray(); 

            (double[] binCenter, double[] binCounts) = CalculateHistogram(Irms, binWidth);

            return (current, dwell, eventID, binCenter, binCounts);
        }

        public (double[], double[], int[], double[], double[]) getDToverCurrentRatio(float min = 0, float max = 1, double binWidth = 0.001)
        {
            List<CurrentLevel> sel_levels = currentlevels.Where(e => e.IoverIo >= min && e.IoverIo <= max && e.length() > 0).ToList();

            if (sel_levels.Count == 0)
            {
                sel_levels = currentlevels;
            }
            double[] current = new double[sel_levels.Count];
            double[] dwell = new double[sel_levels.Count];
            int[] eventID = new int[sel_levels.Count];

            for (int i = 0; i < sel_levels.Count; i++)
            {
                current[i] = sel_levels[i].IoverIo;
                dwell[i] = (double)sel_levels[i].length() / (double)SampleRate * 1000;
                eventID[i] = sel_levels[i].EventId;
            }

            double[] IoverIo = sel_levels.Select(x => x.IoverIo).ToArray();

            (double[] binCenter, double[] binCounts) = CalculateHistogram(IoverIo, binWidth);

            return (current, dwell, eventID, binCenter, binCounts);
        }

        public (double[], double[], double[], double[]) getDwellTimeHistogram(float min = 0, float max = 1, double binWidth = 0.2)
        {
            // Get relevant levels

            List<CurrentLevel> sel_levels = currentlevels.Where(e => e.IoverIo >= min && e.IoverIo <= max && e.length() > 0).ToList();

            if (sel_levels.Count == 0)
            {
                sel_levels = currentlevels;
            }

            double[] dwell = sel_levels.Select(x => x.length()*(double)SamplePeriod*1000).ToArray();

            (double[] binCenter, double[] binCounts) = CalculateHistogram(dwell, binWidth);

            // do fits
            int numberOfBins = binCounts.Count();

            // Log-transform the observed values for linear fitting

            List<double> t = new List<double>();
            List<double> log = new List<double>();
            List<double> X = new List<double>();
            List<double> Y = new List<double>();
            List<double> pXY = new List<double>();
            for (int i = 0; i < numberOfBins; i++)
            {
                if (binCounts[i] > 4) // force fit at better range
                {
                    t.Add(binCenter[i]);
                    log.Add(Math.Log(binCounts[i]));
                    X.Add(binCenter[i]); // bincenter
                    Y.Add(binCounts[i]); // bincounts
                    pXY.Add(binCenter[i] * binCounts[i]);
                }
            }
            // Fit the data using linear regression (least squares)

            // (double intercept, double slope) = Fit.Line(t.ToArray(), log.ToArray());

            // The slope corresponds to -1/τ
            // double tau = -1.0 / slope;

            // OnInfo(new InfoEvent("Time constant by linear regression: " + Math.Round(tau, 2).ToString() + " ms\n"));

            // Fit the data by exponential function

            (double intercept, double tau) = Fit.Exponential(X.ToArray(), Y.ToArray());
            OnInfo(new InfoEvent("Time constant by exponential fit: " + Math.Round(-1.0 / tau, 2).ToString() + " ms\n"));

            Func<double, double> expfit = Fit.ExponentialFunc(X.ToArray(), Y.ToArray());

            List<double> valexp = new List<double>();

            foreach (var time in X)
            {
                valexp.Add(expfit(time));
            }

            // use method of momentum
            int maxYIndex = pXY.IndexOf(pXY.Max());
            OnInfo(new InfoEvent("Time constant by momentum method: " + Math.Round(X[maxYIndex], 2).ToString() + " ms\n"));

            return (binCenter, binCounts, X.ToArray(), valexp.ToArray());
        }

        public (double[], double[]) getLogDwellTimeHistogram(float min = 0, float max = 1, double binWidth = 0.05)
        {
            // Get relevant levels

            List<CurrentLevel> sel_levels = currentlevels.Where(e => e.IoverIo >= min && e.IoverIo <= max && e.length() > 0).ToList();

            if (sel_levels.Count == 0)
            {
                sel_levels = currentlevels;
            }

            double[] dwell = sel_levels.Select(x => Math.Log10(x.length() * (double)SamplePeriod * 1000)).ToArray();

            (double[] binCenter, double[] binCounts) = CalculateHistogram(dwell, binWidth);

            return (binCenter, binCounts);
        }

        public (double[], double[], int[]) getSigmaOverCurrentRatio()
        {
            List<CurrentLevel> sel_levels = currentlevels.Where(e => e.length() > 0).ToList();

            if (sel_levels.Count == 0)
            {
                sel_levels = currentlevels;
            }
            double[] levels = new double[sel_levels.Count];
            double[] sigmas = new double[sel_levels.Count];
            int[] eventids = new int[sel_levels.Count];

            for (int i = 0; i < sel_levels.Count; i++)
            {
                levels[i] = sel_levels[i].IoverIo;
                sigmas[i] = sel_levels[i].Isig;
                eventids[i] = sel_levels[i].EventId;
            }
            return (levels, sigmas, eventids);
        }

        public (double[], double[], int[], double[], double[]) getEventDurationOverCurrentRatio(float min = 0, float max = 1, double binWidth = 0.001)
        {
            List<Event> sel_ev = new List<Event>();
            sel_ev = events.Where(e => e.meancurrentlevel() / e.transitions[0].BLRMS > min && e.meancurrentlevel() / e.transitions[0].BLRMS < max).ToList();
            double[] Iratio = new double[sel_ev.Count];
            double[] duration = new double[sel_ev.Count];
            int[] eventID = new int[sel_ev.Count];

            for (int i =0; i<sel_ev.Count; i++)
            {
                Iratio[i] = sel_ev[i].meancurrentlevel() / events[i].transitions[0].BLRMS;
                duration[i] = sel_ev[i].blocklength()*(double)SamplePeriod*1000;
                eventID[i] = sel_ev[i].Id;
            }

            (double[] binCenter, double[] binCounts) = CalculateHistogram(Iratio, binWidth);

            return (Iratio, duration, eventID, binCenter, binCounts);
        }

        public (double[], double[], double[], double[]) getEventDurationHistogram(float min = 0, float max = 1, double binWidth = 0.2)
        {
            List<Event> sel_ev = new List<Event>();
            sel_ev = events.Where(e => e.meancurrentlevel() / e.transitions[0].BLRMS > min && e.meancurrentlevel() / e.transitions[0].BLRMS < max).ToList();
            double[] Iratio = new double[sel_ev.Count];
            double[] duration = new double[sel_ev.Count];

            for (int i = 0; i < sel_ev.Count; i++)
            {
                duration[i] = sel_ev[i].blocklength() * (double)SamplePeriod * 1000;
            }

            (double[] binCenter, double[] binCounts) = CalculateHistogram(duration, binWidth);

            // do fits
            int numberOfBins = binCounts.Count();

            // Log-transform the observed values for linear fitting

            List<double> t = new List<double>();
            List<double> log = new List<double>();
            List<double> X = new List<double>();
            List<double> Y = new List<double>();
            List<double> pXY = new List<double>();
            for (int i = 0; i < numberOfBins; i++)
            {
                if (binCounts[i] > 4) // force fit at better range
                {
                    t.Add(binCenter[i]);
                    log.Add(Math.Log(binCounts[i]));
                    X.Add(binCenter[i]); // bincenter
                    Y.Add(binCounts[i]); // bincounts
                    pXY.Add(binCenter[i] * binCounts[i]);
                }
            }
            // Fit the data using linear regression (least squares)

            // (double intercept, double slope) = Fit.Line(t.ToArray(), log.ToArray());

            // The slope corresponds to -1/τ
            // double tau = -1.0 / slope;

            // OnInfo(new InfoEvent("Time constant by linear regression: " + Math.Round(tau, 2).ToString() + " ms\n"));

            // Fit the data by exponential function

            (double intercept, double tau) = Fit.Exponential(X.ToArray(), Y.ToArray());
            OnInfo(new InfoEvent("Time constant by exponential fit: " + Math.Round(-1.0 / tau, 2).ToString() + " ms\n"));

            Func<double, double> expfit = Fit.ExponentialFunc(X.ToArray(), Y.ToArray());

            List<double> valexp = new List<double>();

            foreach (var time in X)
            {
                valexp.Add(expfit(time));
            }

            // use method of momentum
            int maxYIndex = pXY.IndexOf(pXY.Max());
            OnInfo(new InfoEvent("Time constant by momentum method: " + Math.Round(X[maxYIndex], 2).ToString() + " ms\n"));

            return (binCenter, binCounts, X.ToArray(), valexp.ToArray());
        }
        private (double[], double[]) CalculateHistogram(double[] values, double binWidth)
        {
            double minValue = values.Min();
            double maxValue = values.Max();
            int numberOfBins = (int)((maxValue - minValue) / binWidth) + 1;
            // Array to hold bin counts
            double[] binCounts = new double[numberOfBins];

            // Populate bin counts
            foreach (var value in values)
            {
                int binIndex = (int)((value - minValue) / binWidth);
                if (binIndex >= 0 && binIndex < numberOfBins)
                {
                    binCounts[binIndex]++;
                }
            }

            // Calculate Bin Center
            double[] binCenter = new double[numberOfBins];
            for (int i = 0; i < numberOfBins; i++)
            {
                double binStart = minValue + i * binWidth;
                binCenter[i] = (double)(binStart + binWidth / 2);
            }
            return ((binCenter, binCounts));
        }

        // ----------- Baseline Functions -----------
        // ------------------------------------------
        // ------------------------------------------

        private (double rms, double sigma) getRMSandSigma(decimal starttime, decimal endtime, decimal rms_init, decimal sigma_init)
        {
            // determine startindex and endindex
            int startidx = GetIndex(starttime);
            int endidx = GetIndex(endtime) - 1;

            double threshold_low = (double)(rms_init - (3*sigma_init));
            double threshold_high = (double)(rms_init + (3*sigma_init));

            double rms_tmp = 0;
            double rms = (double)rms_init;
            double sigma = (double)sigma_init;

            while (Math.Abs(rms - rms_tmp) > 0.01)
            {
                rms_tmp = rms;
                double sum = 0.0;
                double sumOfSquares = 0.0;
                int count = 0;

                // Iterate through the data and add elements above the threshold
                for (int i = startidx; i <= endidx; i++)
                {
                    double value = (double)FData(i);
                    if (value >= threshold_low && value <= threshold_high)
                    {
                        // Calculate the sum and sum of squares
                        sum += value;
                        sumOfSquares += value * value;
                        count++;
                    }
                }
                if (count == 0)
                {
                    // No data points fall within the threshold, exit the loop to prevent division by zero
                    break;
                }

                double mean = sum / count;
                double meanOfSquares = sumOfSquares / count;

                // Calculate RMS
                rms = Math.Sqrt(meanOfSquares);

                // Calculate Sigma (Standard Deviation)
                double variance = meanOfSquares - (mean * mean);
                sigma = Math.Sqrt(variance);

                // Update thresholds
                threshold_low = rms - 3 * sigma;
                threshold_high = rms + 3 * sigma;
            }

            return (rms,  sigma);
        }

        public (double, double) AutoDetectRMSandSigma(decimal starttime, decimal endtime) // start and end in s
        {
            // determine startindex and endindex
            int startidx = GetIndex(starttime);
            int endidx = GetIndex(endtime) - 1;

            double mean;
            double sigma;

            if (baseline.Count > 0)
            {
                mean = baseline.Last();
                sigma = bl_sigma.Last();
            }
            else
            {


                List<double> datalist = new List<double>();

                for (int i = startidx; i <= endidx; i++)
                {
                    datalist.Add(DData(i));
                }

                datalist = datalist.OrderByDescending(x => x).ToList();
                int nBy5 = datalist.Count / 5;
                // calculate initial values
                mean = datalist.Take(nBy5).Average();
                sigma = Math.Sqrt(datalist.Take(nBy5).Select(x => Math.Pow(x - mean, 2)).Average());
            }
            return getRMSandSigma(starttime, endtime, (decimal)mean, (decimal)sigma);
        }

        private (double, double) getBaselineValue(int index)
        {
            decimal time = GetTime(index);
            int idx = (int)Math.Round((time - (decimal)bl_time[0]) / bl_stepsize);
            if (idx >= baseline.Count)
            {
                return (baseline.Last(), bl_sigma.Last());
            }
            else if (idx < 0)
            {
                return (baseline[0], bl_sigma[0]);
            }
            else
            {
                return (baseline[idx], bl_sigma[idx]);
            }
            
        }

        private (double rms, double sigma, double mean) CalculateRmsAndSigma(int startIndex, int endIndex)
        {
            // Validate indexes
            if (startIndex < 0 || endIndex >= raw_data.Length || startIndex > endIndex)
            {
                throw new ArgumentOutOfRangeException("Invalid start or end index.");
            }
            
            // Calculate the number of elements in the range
            int count = endIndex - startIndex + 1;

            // Calculate the mean of the selected range
            double sum = 0;
            for (int i = startIndex; i <= endIndex; i++)
            {
                sum += DData(i);
            }
            double mean = sum / count;

            // Calculate RMS (Root Mean Square)
            double sumOfSquares = 0;
            for (int i = startIndex; i <= endIndex; i++)
            {
                sumOfSquares += DData(i) * DData(i);
            }
            double rms = Math.Sqrt(sumOfSquares / count);

            // Calculate Sigma (Standard Deviation)
            double sumOfSquaredDifferences = 0;
            for (int i = startIndex; i <= endIndex; i++)
            {
                sumOfSquaredDifferences += Math.Pow(DData(i) - mean, 2);
            }
            double sigma = Math.Sqrt(sumOfSquaredDifferences / count);

            return (rms, sigma, mean);
        }

        // --------- Event Detection  ---------------
        // ------------------------------------------
        // ------------------------------------------
        public void DetectEventsTh(float thvalue, int minlength, decimal starttime, decimal endtime, bool use_bl)
        {
            // determine startindex and endindex
            int startj = GetIndex(starttime);
            int endj = GetIndex(endtime) - 1;
            float threshold;
            int startIndex = -1;
            int endIndex = -1;
            int id = 0;
            int minimumeventlength = (int)((decimal)minlength / 1000000 * (int)SampleRate); // minlength in µs -> convert to points
            events.Clear(); // clear previous sweeps
            transitions.Clear();
            currentlevels.Clear();

            // detect the events
            for (int j = startj; j < endj; j++)
            {
                if (use_bl == true)
                {
                    (double rms, double sigma) = getBaselineValue(j);
                    threshold = (float)rms - thvalue;
                }
                else
                {
                    threshold = thvalue;
                }
                if (DData(j) * factorI < threshold) //iterate over datapoints
                {
                    if (startIndex == -1)
                    {
                        // Start of a new event
                        startIndex = j;
                    }
                    endIndex = j;
                }
                else
                {
                    if (startIndex != -1)
                    {
                        // End of the current event
                        if (endIndex - startIndex + 1 > minimumeventlength)
                        {
                            // Valid event, add to list
                            events.Add(new Event(id, startIndex, endIndex));
                            id++;
                        }
                        // Reset for the next potential event
                        startIndex = -1;
                        endIndex = -1;

                    }
                }
            }

            // add parameters and calculate currentlevels
            int id_lev = 0;
            int id_tr = 0;
            foreach (Event ev in events)
            {
                // 1. Step, refine front edge, find start of transition
                int i = 0;
                int tr1_start = ev.idxstart;
                while (FData(ev.idxstart - i - 1) - FData(ev.idxstart - i) > 0)
                {
                    tr1_start = ev.idxstart - i;
                    i++;
                }
                i = 0;
                int tr1_end = ev.idxstart;
                while (FData(ev.idxstart + i) - FData(ev.idxstart + i + 1) > 0)
                {
                    tr1_end = ev.idxstart + i;
                    i++;
                }
                i = 0;
                int tr2_start = ev.idxend;
                while (FData(ev.idxend - i) - FData(ev.idxend - i - 1) > 0)
                {
                    tr2_start = ev.idxend - i;
                    i++;
                }
                i = 0;
                int tr2_end = ev.idxend;
                while (FData(ev.idxend + i + 1) - FData(ev.idxend + i) > 0)
                {
                    tr2_end = ev.idxend + i;
                    i++;
                }
                // populate transitions

                ev.transitions.Add(new Transition(id_tr, tr1_start, tr1_end, false));
                ev.transitions[0].tstart_s = (double)GetTime(ev.transitions[0].idxstart);
                ev.transitions[0].tend_s = (double)GetTime(ev.transitions[0].idxend);
                ev.transitions[0].val_start = DData(ev.transitions[0].idxstart);
                ev.transitions[0].val_end = DData(ev.transitions[0].idxend);
                id_tr++;
                ev.transitions.Add(new Transition(id_tr, tr2_start, tr2_end, true));
                ev.transitions[1].tstart_s = (double)GetTime(ev.transitions[1].idxstart);
                ev.transitions[1].tend_s = (double)GetTime(ev.transitions[1].idxend);
                ev.transitions[1].val_start = DData(ev.transitions[1].idxstart);
                ev.transitions[1].val_end = DData(ev.transitions[1].idxend);
                id_tr++;
                ev.idxstart = tr1_start;
                ev.idxend = tr2_end;

                // populate single currentlevel
                currentlevels.Add(new CurrentLevel(id_lev, tr1_end, tr2_start)); // add a single whole level currentlevel
                (double rms, double sigma, double mean) = CalculateRmsAndSigma(tr1_end, tr2_start);
                (double bl_rms, double bl_sig) = getBaselineValue(ev.idxstart);
                currentlevels.Last().Irms = rms;
                currentlevels.Last().Isig = sigma;
                currentlevels.Last().Imean = mean;
                currentlevels.Last().isBL = false;
                currentlevels.Last().BLRMS = bl_rms;
                currentlevels.Last().BLSIG = bl_sig;
                currentlevels.Last().IoverIo = rms / bl_rms;
                currentlevels.Last().risetime = 0;
                currentlevels.Last().falltime = 0;
                currentlevels.Last().EventId = ev.Id;
                currentlevels.Last().tstart_s = (double)GetTime(tr1_end);
                currentlevels.Last().tend_s = (double)GetTime(tr2_start);
                ev.currentlevels.Add(currentlevels.Last());
                id_lev++;
            }
        }

        public void DetectTransistions(float thval, int minlength, decimal starttime, decimal endtime)
        {
            // determine startindex and endindex
            int starti = GetIndex(starttime);
            int endi = GetIndex(endtime) - 1;

            double thvalue = thval / factorI;

            int id = 0;
            int minimumlength = (int)((decimal)minlength / 1000000 * (int)SampleRate); // minlength in µs -> convert to points
            transitions.Clear();
            // double blth = blrms - 5 * blsig;

            float diffup = 0;
            float diffdown = 0;
            int upcnt = 0;
            int downcnt = 0;
            for (int i = starti; i < endi; i++) // iterate over all data
            {
                // get actual value at i and next value
                float val = FData(i);
                float nextval = FData(i+1);

                if (nextval > val)
                {
                    if (Math.Abs(diffdown) >= thvalue) // downtransition detected
                    {
                        transitions.Add(new Transition(id, i - downcnt, i, false));
                        (double blrms, double blsig) = getBaselineValue(i);
                        transitions.Last().BLRMS = blrms;
                        transitions.Last().BLSIG = blsig;
                        transitions.Last().tstart_s = (double)GetTime(i - downcnt);
                        transitions.Last().tend_s = (double)GetTime(i);
                        transitions.Last().val_start = DData(i - downcnt);
                        transitions.Last().val_end = DData(i);

                        double blth = blrms - f_sigma * blsig;
                        if (DData(i-downcnt) > blth) // transition start is @ baseline
                        {
                            transitions.Last().FromBL = true;
                        }
                        if (DData(i) > blth) // transition within baseline
                        {
                            transitions.Last().IsValid = false;
                        }
                        id++;
                    }
                    upcnt++;
                    diffup = diffup + (nextval - val);
                    diffdown = 0;
                    downcnt = 0;
                }
                else  // nextval <= val
                {
                    if (Math.Abs(diffup) >= thvalue) // uptransition detected
                    {
                        transitions.Add(new Transition(id, i-upcnt, i, true));
                        (double blrms, double blsig) = getBaselineValue(i);
                        transitions.Last().BLRMS = blrms;
                        transitions.Last().BLSIG = blsig;
                        transitions.Last().tstart_s = (double)GetTime(i - upcnt);
                        transitions.Last().tend_s = (double)GetTime(i);
                        transitions.Last().val_start = DData(i - upcnt);
                        transitions.Last().val_end = DData(i);
                        double blth = blrms - f_sigma * blsig;
                        if (DData(i) > blth) // transition end falls within @ baseline
                        {
                            transitions.Last().ToBL = true;
                        }
                        if (DData(i-upcnt) > blth) // transition within baseline
                        {
                            transitions.Last().IsValid = false;
                        }
                        id++;
                    }
                    downcnt++;
                    diffdown = diffdown + (nextval - val);
                    diffup = 0;
                    upcnt = 0;
                }
            }
            OnInfo(new InfoEvent("Total Transitions: " + transitions.Count() + "\n"));
            OnInfo(new InfoEvent("Upward Transitions: " + transitions.Where(e => e.IsUp == true && e.IsValid).Count() + "\n"));
            OnInfo(new InfoEvent("Downward Transitions: " + transitions.Where(e => e.IsDown == true && e.IsValid).Count() + "\n"));
            OnInfo(new InfoEvent("From Baseline Transitions: " + transitions.Where(e=>e.FromBL==true && e.IsValid).Count() + "\n"));
            OnInfo(new InfoEvent("To Baseline Transitions: " + transitions.Where(e => e.ToBL == true && e.IsValid).Count() + "\n"));
            OnInfo(new InfoEvent("Invalid Transitions: " + transitions.Where(e => e.IsValid == false).Count() + "\n"));

            BuildLevelsFromTransitions(minimumlength);
            BuildEvents();
        }

        public int getTranistionCount()
        {
            return transitions.Count;
        }

        private void BuildLevelsFromTransitions(int minlength = 0)
        {
            currentlevels.Clear();
            List<Transition> valtr = transitions.Where(e => e.IsValid).ToList();
            for (int id = 0; id < valtr.Count - 1; id++) // create currentlevels
            {
                int length = valtr[id + 1].idxstart - valtr[id].idxend;
                if (length == 0) length = 1; // immediate down is defined as 1µs time.
                if (length >= minlength)
                {
                    currentlevels.Add(new CurrentLevel(id, valtr[id].idxend, valtr[id + 1].idxstart));
                    currentlevels.Last().risetime = valtr[id].length();
                    currentlevels.Last().falltime = valtr[id + 1].length();
                    currentlevels.Last().BLRMS = valtr[id].BLRMS;
                    currentlevels.Last().BLSIG = valtr[id].BLSIG;
                    currentlevels.Last().tstart_s = (double)GetTime(valtr[id].idxend);
                    currentlevels.Last().tend_s = (double)GetTime(valtr[id + 1].idxstart);
                    if (valtr[id].ToBL == true)
                    {
                        currentlevels.Last().isBL = true;
                    }
                }
            }
            for (int i = 0; i < currentlevels.Count; i++) // calculate currentlevels
            {
                if (currentlevels[i].idxend - currentlevels[i].idxstart == 0) // immediate down
                {
                    currentlevels[i].Irms = DData(currentlevels[i].idxstart);
                    currentlevels[i].Imean = DData(currentlevels[i].idxstart);
                    currentlevels[i].Isig = 0;
                    currentlevels[i].IoverIo = DData(currentlevels[i].idxstart) / currentlevels[i].BLRMS;
                }
                else
                {
                    double sum = 0;
                    double sumOfSquares = 0;
                    int count = 0;
                    double rms = 0;

                    for (int j = currentlevels[i].idxstart; j <= currentlevels[i].idxend; j++)
                    {
                        double val = DData(j);
                        sum += val;
                        sumOfSquares += val * val;
                        count++;
                    }

                    double mean = sum / count;
                    double meanOfSquares = sumOfSquares / count;
                    rms = Math.Sqrt(meanOfSquares);
                    double variance = meanOfSquares - (mean * mean);
                    double sigma = Math.Sqrt(variance);
                    currentlevels[i].Irms = rms;
                    currentlevels[i].Imean = mean;
                    currentlevels[i].Isig = sigma;
                    currentlevels[i].IoverIo = rms / currentlevels[i].BLRMS;
                }
            }
            OnInfo(new InfoEvent("Current Levels: " + currentlevels.Count + "\n"));

        }

        private void BuildEvents()
        {
            events.Clear();
            bool activeevent = false;
            int id = 1;
            foreach (var tr in transitions) // create events and determine their start and endpoint by transitions to and from the baseline
            {
                if(tr.FromBL && !activeevent) // transition from baseline and no current event
                {
                    events.Add(new Event(id, tr.idxstart, tr.idxend)); // create new event
                    events.Last().transitions.Add(tr); // add the first transition
                    activeevent = true;
                    id++;
                }
                else if (tr.FromBL && activeevent) // last event has no return to baseline detected
                {
                    events.RemoveAt(events.Count-1); // remove last event
                    events.Add(new Event(id, tr.idxstart, tr.idxend)); // create new event, 
                    events.Last().transitions.Add(tr); // add the first transition
                    id++;
                }
                else if (!tr.FromBL && !tr.ToBL && activeevent) // neither from nor to baseline, i.e. intraevent transition
                {
                    events.Last().transitions.Add(tr);
                    events.Last().idxend = tr.idxend;
                }
                else if (tr.ToBL && activeevent) // end of an event
                {
                    events.Last().transitions.Add(tr);
                    events.Last().idxend = tr.idxend;
                    activeevent = false;
                }
                else if (tr.ToBL && !activeevent)
                {
                    tr.IsValid = false;// do nothing, discard transition
                }
                else if (!tr.FromBL && !tr.ToBL && !activeevent) // neither from nor to baseline and no present event
                {
                    tr.IsValid = false;// do nothing, discard transition
                }
            }
            foreach (var ev in events) // add all current levels
            {
                List<CurrentLevel> Ilevels = currentlevels.Where(lev => (lev.idxstart > ev.idxstart) && (lev.idxend < ev.idxend)).ToList();
                ev.currentlevels = Ilevels; // short code, but slow
                foreach (var level in ev.currentlevels)
                {
                    level.EventId = ev.Id;
                }
            }

            // remove events, where meancurrentlevel is within baseline
            events = events.Where(e => e.meancurrentlevel() < (e.transitions[0].BLRMS - 3 * e.transitions[0].BLSIG)).ToList();

            // remove all events with 0 levels
            events = events.Where(e => e.currentlevels.Count > 0).ToList();


            // IMPORTANT: after reassigning event numbers, the associated currentlevels must also be reassigned
            for (int i = 0; i < events.Count; i++)
            {
                events[i].Id = i;
                for (int j = 0; j < events[i].currentlevels.Count; j++)
                {
                    events[i].currentlevels[j].EventId = i;
                }
            }


            OnInfo(new InfoEvent("Total Number of Events: " + events.Count() + "\n"));
            OnInfo(new InfoEvent("Events with 1 Level: " + events.Where(e=>e.currentlevels.Count()==1).Count() + "\n"));
            OnInfo(new InfoEvent("Events with 2 Levels: " + events.Where(e => e.currentlevels.Count() == 2).Count() + "\n"));
            OnInfo(new InfoEvent("Events with more than 2 Levels: " + events.Where(e => e.currentlevels.Count() > 2).Count() + "\n"));

        }
                
        public int getEventCount()
        {
            return events.Count;
        }

        // --------- Data Interface -----------------
        // ------------------------------------------
        // ------------------------------------------

        //private float Data(int index)
        //{
        //    // Validate the input number
        //    if (index < 0 || index >= tp)
        //    {
        //        throw new ArgumentOutOfRangeException("Number is out of range.");
        //    }
        //    int sweepidx = index/pps; // determine the relevant sweep
        //    int dataidx = index%pps; // determine relevant index within the sweep
        //    if (sweepidx == activesweep)
        //    {
        //        return data[dataidx];
        //    }
        //    else
        //    {
        //        data = _abf.GetSweepF(sweepidx, activechannel);
        //        activesweep = sweepidx;
        //        return data[dataidx];
        //    }
        //}

        private int GetIndex(decimal time)
        {
            return (int)Math.Round(time / SamplePeriod);
        }

        private decimal GetTime(int index)
        {
            return (decimal) index*SamplePeriod;
        }

        private float FData(int index)
        {
            return Math.Abs(raw_data[index] * abf.ScaleFactors[activechannel]);
        }

        private double DData(int index)
        {
            return (double)Math.Abs(raw_data[index] * abf.ScaleFactors[activechannel]);
        }

        // --------- Data Import/Export -------------
        // ------------------------------------------
        // ------------------------------------------

        public void SaveAllCurrentLevelsToCSV(string filename)
        {
            StreamWriter writer = new StreamWriter(filename);
            string header = "Id; EventId;  idxstart; idxend; risetime; falltime; length; Irms; Io; I/I0; Imean; Isig; isBL?";
            writer.WriteLine(header);
            foreach (var lev in currentlevels)
            {
                writer.WriteLine(lev.ToCSV());
            }
            writer.Close();
        }

        public void SaveCurrentLevelRangeToCSV(string filename, double min, double max)
        {
            StreamWriter writer = new StreamWriter(filename);
            string header = "Id; EventId;  idxstart; idxend; risetime; falltime; length; Irms; Io; I/I0; Imean; Isig; isBL?";
            List<CurrentLevel> levels = currentlevels.Where(e => e.IoverIo<=max && e.IoverIo>=min).ToList();
            writer.WriteLine(header);
            foreach (var lev in levels)
            {
                writer.WriteLine(lev.ToCSV());
            }
            writer.Close();
        }

        public void SaveBaseline(string filename)
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                // Save parameters in header
                writer.WriteLine(bl_start);
                writer.WriteLine(bl_end);
                writer.WriteLine(bl_stepsize);
                writer.WriteLine(bl_interval);

                // Save array data
                int i = 0;
                foreach (var time in bl_time)
                {
                    writer.WriteLine(bl_time[i] + ";" + baseline[i] + ";" + bl_sigma[i]);
                    i++;
                }
            }
        }

        public (double[], double[]) LoadBaseline(string filename)
        {
            using (StreamReader reader = new StreamReader(filename))
            {
                try
                {
                    bl_start = decimal.Parse(reader.ReadLine());
                    bl_end = decimal.Parse(reader.ReadLine());
                    bl_stepsize = decimal.Parse(reader.ReadLine());
                    bl_interval = decimal.Parse(reader.ReadLine());

                    bl_time.Clear();
                    baseline.Clear();
                    bl_sigma.Clear();

                    // Read array data
                    string line;
                    line = reader.ReadLine();
                    while (line != null)
                    {
                        string[] line_data = line.Split(';');
                        bl_time.Add(double.Parse(line_data[0]));
                        baseline.Add(double.Parse(line_data[1]));
                        bl_sigma.Add(double.Parse(line_data[2]));
                        line = reader.ReadLine();
                    }
                }
                catch
                {
                    MessageBox.Show("Invalid Baseline File!");
                }
            }
            return (bl_time.ToArray(), baseline.ToArray());
        }
        public void SaveDataToAbf1(string filename)
        {
            // saves the currently open file in the abf1 format
        }

        // --------- Filter Functions ---------------
        // ------------------------------------------
        // ------------------------------------------

        // Method to apply Savitzky-Golay filter to a float array
        private float[] ApplySavitzkyGolayFilter(float[] data, float[] coefficients, int windowSize)
        {
            int halfWindow = windowSize / 2;
            int dataLength = data.Length;
            float[] filteredData = new float[dataLength];

            // Apply the filter to each point in the data array
            for (int i = 0; i < dataLength; i++)
            {
                float filteredValue = 0.0f;
                for (int j = -halfWindow; j <= halfWindow; j++)
                {
                    int index = Math.Clamp(i + j, 0, dataLength - 1);
                    filteredValue += data[index] * coefficients[halfWindow + j];
                }
                filteredData[i] = filteredValue;
            }

            return filteredData;
        }

        //--------------------------------------------------------------------------
        // This function returns the data filtered. Converted to C# 2 July 2014.
        // Original source written in VBA for Microsoft Excel, 2000 by Sam Van
        // Wassenbergh (University of Antwerp), 6 june 2007.
        //--------------------------------------------------------------------------
        private double[] Butterworth(double[] indata, double deltaTimeinsec, double CutOff)
        {
            if (indata == null) return null;
            if (CutOff == 0) return indata;

            double Samplingrate = 1 / deltaTimeinsec;
            long dF2 = indata.Length - 1;        // The data range is set with dF2
            double[] Dat2 = new double[dF2 + 4]; // Array with 4 extra points front and back
            double[] fdata = indata; // Ptr., changes passed data

            // Copy indata to Dat2
            for (long r = 0; r < dF2; r++)
            {
                Dat2[2 + r] = indata[r];
            }
            Dat2[1] = Dat2[0] = indata[0];
            Dat2[dF2 + 3] = Dat2[dF2 + 2] = indata[dF2];

            const double pi = 3.14159265358979;
            double wc = Math.Tan(CutOff * pi / Samplingrate);
            double k1 = 1.414213562 * wc; // Sqrt(2) * wc
            double k2 = wc * wc;
            double a = k2 / (1 + k1 + k2);
            double b = 2 * a;
            double c = a;
            double k3 = b / k2;
            double d = -2 * a + k3;
            double e = 1 - (2 * a) - k3;

            // RECURSIVE TRIGGERS - ENABLE filter is performed (first, last points constant)
            double[] DatYt = new double[dF2 + 4];
            DatYt[1] = DatYt[0] = indata[0];
            for (long s = 2; s < dF2 + 2; s++)
            {
                DatYt[s] = a * Dat2[s] + b * Dat2[s - 1] + c * Dat2[s - 2]
                           + d * DatYt[s - 1] + e * DatYt[s - 2];
            }
            DatYt[dF2 + 3] = DatYt[dF2 + 2] = DatYt[dF2 + 1];

            // FORWARD filter
            double[] DatZt = new double[dF2 + 2];
            DatZt[dF2] = DatYt[dF2 + 2];
            DatZt[dF2 + 1] = DatYt[dF2 + 3];
            for (long t = -dF2 + 1; t <= 0; t++)
            {
                DatZt[-t] = a * DatYt[-t + 2] + b * DatYt[-t + 3] + c * DatYt[-t + 4]
                            + d * DatZt[-t + 1] + e * DatZt[-t + 2];
            }

            // Calculated points copied for return
            for (long p = 0; p < dF2; p++)
            {
                fdata[p] = (float) DatZt[p];
            }

            return fdata;
        }

        //------------Event Handlers----------------
        //------------------------------------------
        //------------------------------------------

        // Define a delegate for the event
        public delegate void InfoEventHandler(object sender, InfoEvent e);

        // Define the event using the delegate
        public event InfoEventHandler Info;

        // Method to raise the event
        protected virtual void OnInfo(InfoEvent e)
        {
            Info?.Invoke(this, e);
        }

        public class InfoEvent : EventArgs
        {
            public string Info { get; private set; }

            public InfoEvent(string info)
            {
                Info = info;
            }
        }

    }
}
