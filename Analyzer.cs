using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbfSharp;
using AbfSharp.NativeReader;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace Nanopore_Analyzer
{
    public class Analyzer
    {
        ABF _abf;

        public Analyzer(string filepath, bool readdata)
        {
            _abf = new AbfSharp.ABF(filepath, readdata);
        }

        public string getHeaderInfo()
        {
            return _abf.ToString(); // use ABF to toString function
        }

        public int SweepCount()
        {
            return _abf.Header.SweepCount;
        }

        public int ChannelCount()
        {
            return _abf.Header.ChannelCount;
        }

        public float[] GetAllData()
        {
            return _abf.GetAllData();
        }

        public PlotModel GetSweep(int sweepIndex, int channelIndex)
        {
            PlotModel model = new PlotModel();
            float[] data = _abf.GetSweep(sweepIndex, channelIndex);
            LineSeries lineSeries = new LineSeries();
            for (int i = 0; i < data.Length; i++)
            {
                float time = ((float) data.Length*((float)sweepIndex) + ((float) i))/_abf.Header.SampleRate;
                lineSeries.Points.Add(new DataPoint(time, data[i]));
             }
            model.Series.Add(lineSeries);
            var xAxis = new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Title = "time (s)"
            };
            model.Axes.Add(xAxis);
            var yAxis = new LinearAxis
            {
                Position = AxisPosition.Left,
                Title = "I (nA)"
            };
            model.Axes.Add(yAxis);

            return model;
        }

        public float getSamplePeriod()
        {
            return _abf.Header.SamplePeriodMS;
        }

        public void SaveDataToAbf1(string filename)
        {
            // saves the currently open file in the abf1 format
        }

    }
}
