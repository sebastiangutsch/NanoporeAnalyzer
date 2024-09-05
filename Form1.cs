namespace Nanopore_Analyzer
{
    using ABF_Reader_and_Writer;
    using ScottPlot;
    using ScottPlot.Colormaps;
    using ScottPlot.Plottables;
    using System.Diagnostics;
    using System.Diagnostics.Metrics;
    using System.IO;
    using System.Runtime.InteropServices;
    using static System.Runtime.InteropServices.JavaScript.JSType;
    using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

    public partial class mainform : Form
    {
        private string? filepath = null;
        private Analyzer? analyzer = null;
        private Scatter ev_scatter;
        private Crosshair MyCrosshair;
        private int[] EventIDs;
        private bool PlotEventsClickable = false;
        AxisSpanUnderMouse? SpanBeingDragged = null;

        public mainform()
        {
            InitializeComponent();
            cb_detection_method.SelectedIndex = 2;
            cb_selectfilter.SelectedIndex = 0;
            cb_select_plot.SelectedIndex = 0;
            gb_eventdetection.Enabled = false;
            gb_eventanalysis.Enabled = false;

        }

        // ------------- Menu functions-----------------
        // ---------------------------------------------
        // ---------------------------------------------
        private void menu_load_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select a File";
            openFileDialog.Filter = "Axon Binary File (*.abf)|*.abf|TDSM-File (*.tdms)|*.tdms";
            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                ClearAll();
                try
                {
                    filepath = openFileDialog.FileName;
                    if (Path.GetExtension(filepath) == ".abf")
                    {
                        // open file and read abf header
                        this.Text = filepath;
                        analyzer = new Analyzer(filepath);
                        analyzer.Info += Analyzer_Info;
                        // rtb_info.Text = $"ABF Version: {analyzer.abfversion}\n";
                        rtb_info.Text += "Sample Period: " + analyzer.SamplePeriod.ToString() + "\n";
                        rtb_info.Text += "Sampling Rate: " + analyzer.SampleRate.ToString() + "\n";
                        rtb_info.Text += "Sweep Count: " + analyzer.SweepCount + "\n";
                        cb_channelnumber.Items.Clear();
                        //rtb_info.Text += analyzer.ChannelCount().ToString();
                        for (int i = 1; i <= analyzer.ChannelCount; i++)
                        {
                            cb_channelnumber.Items.Add("Channel " + i.ToString());
                        }
                        cb_channelnumber.SelectedIndex = 0;
                        nud_starttime.Value = 0;
                        nud_endtime.Value = 1;
                        nud_starttime.Maximum = analyzer.MeasurementLength();
                        nud_endtime.Maximum = analyzer.MeasurementLength();
                        nud_BLfrom.Maximum = analyzer.MeasurementLength();
                        nud_BLto.Maximum = analyzer.MeasurementLength();
                        nud_BLfrom.Value = 0;
                        nud_BLto.Value = analyzer.MeasurementLength();
                        nud_edfrom.Minimum = 0;
                        nud_edfrom.Maximum = analyzer.MeasurementLength();
                        nud_edfrom.Value = 0;
                        nud_edto.Minimum = 0;
                        nud_edto.Maximum = analyzer.MeasurementLength();
                        nud_edto.Value = nud_edto.Maximum;
                        gb_eventdetection.Enabled = false;
                        gb_eventanalysis.Enabled = false;
                        plot_current();
                    }
                    else if (Path.GetExtension(filepath) == ".tdms")
                    {
                        // open file and read tdms header
                        MessageBox.Show("Nice try, but NI file support is not implemented!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Unknown file type selected!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void saveAsAbfv1ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        // -------- Data Plot functions-----------------
        // ---------------------------------------------
        // ---------------------------------------------
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    if (nud_starttime.Value < 1)
                    {
                        nud_starttime.Value = 0;
                    }
                    else
                    {
                        nud_starttime.Value -= 1;
                    }
                    if (nud_endtime.Value <= 2)
                    {
                        nud_endtime.Value = 1;
                    }
                    else
                    {
                        nud_endtime.Value -= 1;
                    }
                    plot_current();
                    break;
                case Keys.Right:
                    if (nud_starttime.Value >= nud_starttime.Maximum - 1)
                    {
                        nud_starttime.Value = nud_starttime.Maximum - 1;
                    }
                    else
                    {
                        nud_starttime.Value += 1;
                    }
                    if (nud_endtime.Value >= nud_endtime.Maximum - 1)
                    {
                        nud_endtime.Value = nud_endtime.Maximum;
                    }
                    else
                    {
                        nud_endtime.Value += 1;
                    }
                    plot_current();
                    break;
            }
        }

        private void pb_arrowleft_Click(object sender, EventArgs e)
        {
            if (nud_starttime.Value <= 1)
            {
                nud_starttime.Value = 0;
            }
            else
            {
                nud_starttime.Value -= 1;
            }
            if (nud_endtime.Value <= 2)
            {
                nud_endtime.Value = 1;
            }
            else
            {
                nud_endtime.Value -= 1;
            }
            plot_current();
        }

        private void pb_arrowright_Click(object sender, EventArgs e)
        {
            if (nud_starttime.Value >= nud_starttime.Maximum - 1)
            {
                nud_starttime.Value = nud_starttime.Maximum - 1;
            }
            else
            {
                nud_starttime.Value += 1;
            }
            if (nud_endtime.Value >= nud_endtime.Maximum - 1)
            {
                nud_endtime.Value = nud_endtime.Maximum;
            }
            else
            {
                nud_endtime.Value += 1;
            }
            plot_current();
        }

        private void bt_update_plot_Click(object sender, EventArgs e)
        {
            plot_current();
        }
        private void plot_current()
        {
            if (nud_starttime.Value >= nud_endtime.Value)
            {
                MessageBox.Show("Are you kidding? Please select a proper range!",
                "Information",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            }
            (double[] x, double[] y) = analyzer.GetInterval(nud_starttime.Value, nud_endtime.Value);
            plot_data.Plot.Clear();
            plot_data.Plot.Add.SignalXY(x, y);
            plot_data.Plot.Axes.SetLimitsX(x.First(), x.Last());
            plot_data.Plot.Axes.AutoScaleY();
            plot_data.Refresh();

            if (chk_calc_rms.Checked)
            {
                (double rms, double sigma) = analyzer.AutoDetectRMSandSigma(nud_starttime.Value, nud_endtime.Value);
                nud_baseline_rms.Value = (decimal)rms;
                nud_baseline_sigma.Value = (decimal)sigma;
            }

            if (chk_show_rms.Checked)
            {
                var vs = plot_data.Plot.Add.VerticalSpan((double)nud_baseline_rms.Value - (double)nud_baseline_sigma.Value * 3, (double)nud_baseline_rms.Value + (double)nud_baseline_sigma.Value * 3);
                vs.LegendText = "RMS+/-3sigma";
                vs.LineStyle.Width = 1;
                vs.LineStyle.Color = Colors.DarkRed;
                vs.LineStyle.Pattern = LinePattern.Solid;
                vs.FillStyle.Color = Colors.DarkRed.WithAlpha(.2);
                plot_data.Refresh();
            }

            if (chk_show_rect.Checked)
            {
                (double[] t, double[] y1, double[] y2) = analyzer.GetRect(nud_starttime.Value, nud_endtime.Value);
                plot_derivative.Plot.Clear();
                var p1 = plot_derivative.Plot.Add.SignalXY(t, y1);
                p1.LineColor = Colors.Green;
                var p2 = plot_derivative.Plot.Add.SignalXY(t, y2);
                p2.LineColor = Colors.Red;
                plot_derivative.Plot.Axes.SetLimitsX((double)nud_starttime.Value, (double)nud_endtime.Value);
                plot_derivative.Plot.Axes.AutoScaleY();
                plot_derivative.Refresh();
            }
        }

        // ------------- InfoBox -----------------------
        // ---------------------------------------------
        // ---------------------------------------------
        private void Analyzer_Info(object sender, Analyzer.InfoEvent e)
        {
            // Update the UI with the new data
            if (rtb_info.InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    rtb_info.Text += e.Info.ToString();
                    rtb_info.SelectionStart = rtb_info.Text.Length;
                    rtb_info.ScrollToCaret();
                }));
            }
            else
            {
                rtb_info.Text += e.Info.ToString();
                rtb_info.SelectionStart = rtb_info.Text.Length;
                rtb_info.ScrollToCaret();
            }
        }

        // ------------- Filter Box --------------------
        // ---------------------------------------------
        // ---------------------------------------------
        private void bt_filter_Click(object sender, EventArgs e)
        {
            (double[] x, double[] y) = analyzer.GetFilter(nud_starttime.Value, nud_endtime.Value, (int)nud_cutoff.Value);
            plot_data.Plot.Add.SignalXY(x, y, color: Colors.HotPink);
            plot_data.Refresh();
        }

        // ------------- Baseline ----------------------
        // ---------------------------------------------
        // ---------------------------------------------
        private void bt_getbaseline_Click(object sender, EventArgs e)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            (double[] time, double[] baseline) = analyzer.AutoDetectBaseline(nud_baseline_interval.Value, nud_baseline_stepsize.Value, nud_BLfrom.Value, nud_BLto.Value);
            plot_baseline.Plot.Clear();
            var bl = plot_baseline.Plot.Add.SignalXY(time, baseline);
            bl.LineWidth = 2;
            plot_baseline.Plot.Axes.AutoScale();
            plot_baseline.Refresh();
            stopwatch.Stop();
            rtb_info.Text += "Baseline determined in " + stopwatch.Elapsed.Minutes + "min and " + stopwatch.Elapsed.Seconds + "s\n";
            gb_eventdetection.Enabled = true;
            nud_edfrom.Minimum = nud_BLfrom.Value;
            nud_edfrom.Maximum = nud_BLto.Value;
            nud_edfrom.Value = nud_BLfrom.Value;
            nud_edto.Minimum = nud_BLfrom.Value;
            nud_edto.Maximum = nud_BLto.Value;
            nud_edto.Value = nud_BLto.Value;
        }

        private void bt_save_baseline_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = "bl";
            saveFileDialog.Filter = "Baseline files (*.bl)|*.bl";

            // Set a suggested file name
            saveFileDialog.FileName = filepath;

            // Show the dialog and check if the user clicked OK
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string file = saveFileDialog.FileName;
                analyzer.SaveBaseline(file);
            }
        }

        private void bt_load_baseline_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select a File";
            openFileDialog.Filter = "bl (*.bl)|*.bl";
            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    filepath = openFileDialog.FileName;
                    if (Path.GetExtension(filepath) == ".bl")
                    {
                        // open file and read 
                        this.Text = filepath;
                        (double[] time, double[] baseline) = analyzer.LoadBaseline(filepath);
                        plot_baseline.Plot.Clear();
                        var bl = plot_baseline.Plot.Add.SignalXY(time, baseline);
                        bl.LineWidth = 2;
                        plot_baseline.Plot.Axes.AutoScale();
                        plot_baseline.Refresh();
                        nud_baseline_interval.Value = analyzer.bl_interval;
                        nud_baseline_stepsize.Value = analyzer.bl_stepsize;
                        nud_BLfrom.Value = analyzer.bl_start;
                        nud_BLto.Value = analyzer.bl_end;
                        nud_edfrom.Minimum = nud_BLfrom.Value;
                        nud_edfrom.Maximum = nud_BLto.Value;
                        nud_edfrom.Value = nud_BLfrom.Value;
                        nud_edto.Minimum = nud_BLfrom.Value;
                        nud_edto.Maximum = nud_BLto.Value;
                        nud_edto.Value = nud_BLto.Value;
                        gb_eventdetection.Enabled = true;
                    }
                }
                catch
                {

                }
            }
        }

        // ------------- Event Detection ---------------
        // ---------------------------------------------
        // ---------------------------------------------
        private void bt_detectevents_Click(object sender, EventArgs e)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            switch (cb_detection_method.SelectedIndex)
            {
                case 0: // simple threshold
                    {
                        if (chk_edrange.Checked)
                        {
                            analyzer.DetectEventsTh((float)nud_th.Value, (int)nud_minlength.Value, nud_edfrom.Minimum, nud_edto.Maximum, false);
                        }
                        else
                        {
                            analyzer.DetectEventsTh((float)nud_th.Value, (int)nud_minlength.Value, nud_edfrom.Value, nud_edto.Value, false);
                        }
                        PlotEvent(0);
                        nud_events.Maximum = analyzer.getEventCount();
                        gb_eventanalysis.Enabled = true;
                        break;
                    }
                case 1: // threshold with baseline
                    {
                        if (chk_edrange.Checked)
                        {
                            analyzer.DetectEventsTh((float)nud_th.Value, (int)nud_minlength.Value, nud_edfrom.Minimum, nud_edto.Maximum, true);
                        }
                        else
                        {
                            analyzer.DetectEventsTh((float)nud_th.Value, (int)nud_minlength.Value, nud_edfrom.Value, nud_edto.Value, true);
                        }
                        nud_events.Maximum = analyzer.getEventCount();
                        PlotEvent(0);
                        gb_eventanalysis.Enabled = true;
                        break;
                    }
                case 2:
                    {
                        if (chk_edrange.Checked)
                        {
                            analyzer.DetectTransistions((float)nud_th.Value, (int)nud_minlength.Value, nud_edfrom.Minimum, nud_edto.Maximum);
                        }
                        else
                        {
                            analyzer.DetectTransistions((float)nud_th.Value, (int)nud_minlength.Value, nud_edfrom.Value, nud_edto.Value);
                        }
                        nud_events.Maximum = analyzer.getEventCount();
                        PlotEvent(0);
                        gb_eventanalysis.Enabled = true;
                        break;
                    }
                default:
                    MessageBox.Show("The selected method is inactivated for now. Please only use the rectification method.",
                                    "Information",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    break;
            }
            stopwatch.Stop();
            rtb_info.Text += "Events determined in " + stopwatch.Elapsed.Minutes + "min and " + stopwatch.Elapsed.Seconds + "s\n";
        }

        private void PlotEvent(int id)
        {
            if (id < 0)
            {
                id = 0;
            }
            (double[] t, double[] y, Event ev, int evidx) = analyzer.getEvent(id);
            plot_event.Plot.Clear();
            var sig = plot_event.Plot.Add.SignalXY(t, y); // add event data
            sig.LineWidth = 2;
            plot_event.Plot.Axes.SetLimitsX(t.First(), t.Last());
            plot_event.Plot.Axes.AutoScaleY();
            // add baseline
            var vs = plot_event.Plot.Add.VerticalSpan(ev.transitions[0].BLRMS - ev.transitions[0].BLSIG, ev.transitions[0].BLRMS + 3 * ev.transitions[0].BLSIG);
            vs.LineStyle.Width = 1;
            vs.LineStyle.Color = Colors.DarkRed;
            vs.LineStyle.Pattern = LinePattern.Solid;
            vs.FillStyle.Color = Colors.DarkRed.WithAlpha(.2);
            // add event levels
            foreach (CurrentLevel lev in ev.currentlevels)
            {
                double x1 = lev.tstart_s, y1 = lev.Irms;
                double x2 = lev.tend_s, y2 = lev.Irms;
                var line = plot_event.Plot.Add.Line(x1, y1, x2, y2);
                line.LineWidth = 2;
                line.Color = Colors.Red;
            }
            // add transition points
            List<double> tr_x = new List<double>();
            List<double> tr_y = new List<double>();
            if (ev.transitions.Count > 0)
            {
                foreach (Transition tr in ev.transitions)
                {
                    tr_x.Add(tr.tstart_s);
                    tr_y.Add(tr.val_start);
                    tr_x.Add(tr.tend_s);
                    tr_y.Add(tr.val_end);
                }
                var scatter = plot_event.Plot.Add.Scatter(tr_x, tr_y);
                scatter.LineWidth = 0;                 // No connecting lines
                scatter.MarkerShape = MarkerShape.OpenSquare; // Set markers to empty rectangles
                scatter.MarkerSize = 10;               // Set marker size
                scatter.MarkerColor = Colors.Green;      // Set marker color
            }
            plot_event.Refresh();
            lb_events.Text = (evidx +1) + "/" + analyzer.getEventCount().ToString();
            //nud_events.Value = (decimal)id+1;
        }

        private void chk_edrange_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_edrange.Checked)
            {
                nud_edfrom.Enabled = false;
                nud_edto.Enabled = false;
            }
            else
            {
                nud_edfrom.Enabled = true;
                nud_edto.Enabled = true;
            }
        }

        private void nud_events_ValueChanged(object sender, EventArgs e)
        {
            PlotEvent((int)nud_events.Value-1);
        }

        // ------------- Event Analysis ----------------
        // ---------------------------------------------
        // ---------------------------------------------
        private void bt_plot_selected_graph_Click(object sender, EventArgs e)
        {
            switch (cb_select_plot.SelectedIndex)
            {
                case 0:
                    PlotDwellTimeOverCurrentLevel();
                    break;
                case 1:
                    PlotDwellTimeOverCurrentRatio();
                    break;
                case 2:
                    PlotDwellTimeHistogram();
                    break;
                case 3:
                    PlotLogDwellTimeHistogram();
                    break;
                case 4:
                    PlotSigmaOverCurrentRatio();
                    break;
                case 5:
                    PlotEventDurationOverCurrentRatio();
                    break;
                case 6:
                    PlotEventDurationHistogram();
                    break;

            }
        }

        private void PlotDwellTimeOverCurrentLevel()
        {
            plot_event_analysis.Reset();
            (double[] lev, double[] dwell, int[] eventID, double[] bin, double[] counts) = analyzer.getDToverCurrentLevel((float)nud_plotmin.Value, (float)nud_plotmax.Value);
            
            // set Event IDs reference
            EventIDs = new int[eventID.Length];
            EventIDs = eventID;

            // log-scale the data and account for negative values, necessary for Scottplot
            double[] logdt = dwell.Select(Math.Log10).ToArray();

            // add first scatter plot with points
            var p1 = plot_event_analysis.Plot.Add.ScatterPoints(lev, logdt);
            p1.LineWidth = 0;
            p1.MarkerShape = MarkerShape.FilledCircle; // Set markers to filled circles
            p1.MarkerSize = 1;                // Set marker size to small
            p1.MarkerColor = Colors.Black;

            // add histogramm
            var p2 = plot_event_analysis.Plot.Add.ScatterLine(bin, counts);
            // Plot using the secondary axes (right)
            p2.Axes.YAxis = plot_event_analysis.Plot.Axes.Right;
            p2.LineWidth = 2;
            p2.LineColor = Colors.Red;
            p2.LineStyle.Pattern = LinePattern.Solid;

            // set Axes and Grid
            plot_event_analysis.Plot.Grid.XAxisStyle.IsVisible = false;
            plot_event_analysis.Plot.Grid.YAxisStyle.IsVisible = false;
            plot_event_analysis.Plot.Axes.AutoScale();
            plot_event_analysis.Plot.Axes.Right.Min = 0;
            plot_event_analysis.Plot.Axes.Left.TickGenerator = GetLogTicks();
            plot_event_analysis.Plot.Axes.Left.Label.Text = "dwell time (ms)";
            plot_event_analysis.Plot.Axes.Bottom.Label.Text = "current level (" + analyzer.unitI + ")";
            plot_event_analysis.Plot.Axes.Right.Label.Text = "counts";

            // add crosshair to be able to click Event points
            MyCrosshair = plot_event_analysis.Plot.Add.Crosshair(0, 0);
            MyCrosshair.IsVisible = false;
            MyCrosshair.MarkerShape = MarkerShape.OpenCircle;
            MyCrosshair.MarkerSize = 15;
            ev_scatter = p1;

            plot_event_analysis.Refresh();
            PlotEventsClickable = true;
        }

        private void PlotDwellTimeOverCurrentRatio()
        {
            plot_event_analysis.Reset();
            (double[] lev, double[] dwell, int[] eventID, double[] bin, double[] counts) = analyzer.getDToverCurrentRatio((float)nud_plotmin.Value, (float)nud_plotmax.Value);

            // set Event IDs reference
            EventIDs = new int[eventID.Length];
            EventIDs = eventID;

            // log-scale the data and account for negative values
            double[] logdt = dwell.Select(Math.Log10).ToArray();

            // add first scatter plot with points
            var p1 = plot_event_analysis.Plot.Add.ScatterPoints(lev, logdt);
            
            p1.LineWidth = 0;
            p1.MarkerShape = MarkerShape.FilledCircle; // Set markers to filled circles
            p1.MarkerSize = 1;                // Set marker size to small
            p1.MarkerColor = Colors.Black;

            // add histogram
            var p2 = plot_event_analysis.Plot.Add.ScatterLine(bin, counts);
            // Plot using the secondary axes (right)
            p2.Axes.YAxis = plot_event_analysis.Plot.Axes.Right;
            p2.LineWidth = 2;
            p2.LineColor = Colors.Red;
            p2.LineStyle.Pattern = LinePattern.Solid;

            // set Axes and Grid
            plot_event_analysis.Plot.Grid.XAxisStyle.IsVisible = false;
            plot_event_analysis.Plot.Grid.YAxisStyle.IsVisible = false;
            plot_event_analysis.Plot.Axes.AutoScale();
            plot_event_analysis.Plot.Axes.Left.TickGenerator = GetLogTicks();
            plot_event_analysis.Plot.Axes.Left.Label.Text = "dwell time (ms)";
            plot_event_analysis.Plot.Axes.Bottom.Label.Text = "I/I0";
            plot_event_analysis.Plot.Axes.Right.Label.Text = "counts";

            // add crosshair to be able to click Event points 
            MyCrosshair = plot_event_analysis.Plot.Add.Crosshair(0, 0);
            MyCrosshair.IsVisible = false;
            MyCrosshair.MarkerShape = MarkerShape.OpenCircle;
            MyCrosshair.MarkerSize = 15;
            ev_scatter = p1;

            plot_event_analysis.Refresh();
            PlotEventsClickable = true;
        }

        private void PlotDwellTimeHistogram()
        {
            plot_event_analysis.Reset();
            (double[] bincenter, double[] counts, double[] xfit, double[] expfit) = analyzer.getDwellTimeHistogram((float)nud_plotmin.Value, (float)nud_plotmax.Value, (float)nud_bin_width.Value);

            // log-scale the data and account for negative values
            for (int i = 0; i < counts.Length; i++)
            {
                if (counts[i] < 1) counts[i] = 0.1;
            }
            double[] logcts = counts.Select(Math.Log10).ToArray();
            double[] logfit = expfit.Select(Math.Log10).ToArray();

            // add the histogram
            var p1 = plot_event_analysis.Plot.Add.SignalXY(bincenter, logcts, color: Colors.Black);
            p1.LineWidth = 2;

            // add fit
            
            var p2 = plot_event_analysis.Plot.Add.SignalXY(xfit, logfit, color: Colors.Red);
            p2.LineWidth = 2;

            // set Axes and Grid
            plot_event_analysis.Plot.Grid.XAxisStyle.IsVisible = false;
            plot_event_analysis.Plot.Grid.YAxisStyle.IsVisible = false;
            plot_event_analysis.Plot.Axes.AutoScale();
            plot_event_analysis.Plot.Axes.Left.TickGenerator = GetLogTicks();
            plot_event_analysis.Plot.Axes.Left.Label.Text = "counts";
            plot_event_analysis.Plot.Axes.Bottom.Label.Text = "dwell time (ms)";

            plot_event_analysis.Refresh();
            PlotEventsClickable = false;
        }

        private void PlotLogDwellTimeHistogram()
        {
            plot_event_analysis.Reset();
            (double[] x, double[] y) = analyzer.getLogDwellTimeHistogram((float)nud_plotmin.Value, (float)nud_plotmax.Value);

            
            var plt = plot_event_analysis.Plot.Add.Scatter(x, y);
            plt.LineWidth = 2;

            // set Axes and Grid
            plot_event_analysis.Plot.Grid.XAxisStyle.IsVisible = false;
            plot_event_analysis.Plot.Grid.YAxisStyle.IsVisible = false;
            plot_event_analysis.Plot.Axes.SetLimitsX(x.First(), x.Last());
            plot_event_analysis.Plot.Axes.AutoScaleY();
            plot_event_analysis.Plot.Axes.Left.Label.Text = "Counts";
            plot_event_analysis.Plot.Axes.Bottom.Label.Text = "log(dwell time /(ms))";

            plot_event_analysis.Refresh();
            PlotEventsClickable = false;
        }

        private void PlotSigmaOverCurrentRatio()
        {
            plot_event_analysis.Reset();
            (double[] levels, double[] sigmas, int[] eventID) = analyzer.getSigmaOverCurrentRatio();

            // set Event IDs reference
            EventIDs = new int[eventID.Length];
            EventIDs = eventID;

            // add plot
            var p1 = plot_event_analysis.Plot.Add.ScatterPoints(levels, sigmas);
            p1.LineWidth = 0;
            p1.MarkerShape = MarkerShape.FilledCircle; // Set markers to filled circles
            p1.MarkerSize = 1;                // Set marker size to small
            p1.MarkerColor = Colors.Black;

            // set Axes and Grid
            plot_event_analysis.Plot.Grid.XAxisStyle.IsVisible = false;
            plot_event_analysis.Plot.Grid.YAxisStyle.IsVisible = false;
            plot_event_analysis.Plot.Axes.AutoScale();
            plot_event_analysis.Plot.Axes.Left.Label.Text = "σ (" + analyzer.unitI + ")";
            plot_event_analysis.Plot.Axes.Bottom.Label.Text = "I/I0";

            // add crosshair to be able to click Event points 
            MyCrosshair = plot_event_analysis.Plot.Add.Crosshair(0, 0);
            MyCrosshair.IsVisible = false;
            MyCrosshair.MarkerShape = MarkerShape.OpenCircle;
            MyCrosshair.MarkerSize = 15;
            ev_scatter = p1;

            plot_event_analysis.Refresh();
            PlotEventsClickable = true;
        }

        private void PlotEventDurationOverCurrentRatio()
        {
            plot_event_analysis.Reset();
            (double[] lev, double[] dwell, int[] eventID, double[] bin, double[] counts) = analyzer.getEventDurationOverCurrentRatio((float)nud_plotmin.Value, (float)nud_plotmax.Value);

            // set Event IDs reference
            EventIDs = new int[eventID.Length];
            EventIDs = eventID;

            // log-scale the data and account for negative values
            double[] logdt = dwell.Select(Math.Log10).ToArray();

            // add plot
            var p1 = plot_event_analysis.Plot.Add.ScatterPoints(lev, logdt);
            
            p1.LineWidth = 0;
            p1.MarkerShape = MarkerShape.FilledCircle; // Set markers to filled circles
            p1.MarkerSize = 1;                // Set marker size to small
            p1.MarkerColor = Colors.Black;

            var p2 = plot_event_analysis.Plot.Add.ScatterLine(bin, counts);
            // Plot using the secondary axes (right)
            p2.Axes.YAxis = plot_event_analysis.Plot.Axes.Right;
            p2.LineWidth = 2;
            p2.LineColor = Colors.Red;
            p2.LineStyle.Pattern = LinePattern.Solid;

            // set Axes and Grid
            plot_event_analysis.Plot.Grid.XAxisStyle.IsVisible = false;
            plot_event_analysis.Plot.Grid.YAxisStyle.IsVisible = false;
            plot_event_analysis.Plot.Axes.AutoScale();
            plot_event_analysis.Plot.Axes.Left.TickGenerator = GetLogTicks();
            plot_event_analysis.Plot.Axes.Left.Label.Text = "Event duration (ms)";
            plot_event_analysis.Plot.Axes.Bottom.Label.Text = "I/I0";
            plot_event_analysis.Plot.Axes.Right.Label.Text = "counts";

            // add crosshair to be able to click Event points
            MyCrosshair = plot_event_analysis.Plot.Add.Crosshair(0, 0);
            MyCrosshair.IsVisible = false;
            MyCrosshair.MarkerShape = MarkerShape.OpenCircle;
            MyCrosshair.MarkerSize = 15;
            ev_scatter = p1;

            plot_event_analysis.Refresh();
            PlotEventsClickable = true;
        }

        private void PlotEventDurationHistogram()
        {
            plot_event_analysis.Reset();
            (double[] bincenter, double[] counts, double[] xfit, double[] expfit) = analyzer.getEventDurationHistogram((float)nud_plotmin.Value, (float)nud_plotmax.Value, (float)nud_bin_width.Value);

            // log-scale the data and account for negative values
            for (int i = 0; i < counts.Length; i++)
            {
                if (counts[i] < 1) counts[i] = 0.1;
            }
            double[] logcts = counts.Select(Math.Log10).ToArray();
            double[] logfit = expfit.Select(Math.Log10).ToArray();

            // add the histogram
            var p1 = plot_event_analysis.Plot.Add.SignalXY(bincenter, logcts, color: Colors.Black);
            p1.LineWidth = 2;

            // add fit

            var p2 = plot_event_analysis.Plot.Add.SignalXY(xfit, logfit, color: Colors.Red);
            p2.LineWidth = 2;

            // set Axes and Grid
            plot_event_analysis.Plot.Grid.XAxisStyle.IsVisible = false;
            plot_event_analysis.Plot.Grid.YAxisStyle.IsVisible = false;
            plot_event_analysis.Plot.Axes.AutoScale();
            plot_event_analysis.Plot.Axes.Left.TickGenerator = GetLogTicks();
            plot_event_analysis.Plot.Axes.Left.Label.Text = "counts";
            plot_event_analysis.Plot.Axes.Bottom.Label.Text = "dwell time (ms)";

            plot_event_analysis.Refresh();
            PlotEventsClickable = false;
        }
        private void bt_save_events_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = "csv";
            saveFileDialog.Filter = "CSV files (*.csv)|*.csv";

            // Set a suggested file name
            saveFileDialog.FileName = filepath + "_currentlevels.csv";

            // Show the dialog and check if the user clicked OK
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string file = saveFileDialog.FileName;
                analyzer.SaveAllCurrentLevelsToCSV(file);
            }
        }

        private void pb_plot_options_Click(object sender, EventArgs e)
        {
            PlotParamForm pform = new PlotParamForm();
            if (pform.ShowDialog() == DialogResult.OK)
            {
                rtb_info.Text += "new parameters saved \n";
            }
        }

        private void plot_event_analysis_MouseMove(object sender, MouseEventArgs e)
        {
            if (SpanBeingDragged is not null)
            {
                // currently dragging something so update it
                Coordinates mouseNow = plot_event_analysis.Plot.GetCoordinates(e.X, e.Y);
                SpanBeingDragged.DragTo(mouseNow);
                plot_event_analysis.Refresh();
            }
            else
            {
                // not dragging anything so just set the cursor based on what's under the mouse
                var spanUnderMouse = GetSpanUnderMouse(e.X, e.Y);
                if (spanUnderMouse is null) Cursor = Cursors.Default;
                else if (spanUnderMouse.IsResizingHorizontally) Cursor = Cursors.SizeWE;
                else if (spanUnderMouse.IsResizingVertically) Cursor = Cursors.SizeNS;
                else if (spanUnderMouse.IsMoving) Cursor = Cursors.SizeAll;
            }
            if (PlotEventsClickable)
            {
                // determine where the mouse is and get the nearest point
                Pixel mousePixel = new(e.Location.X, e.Location.Y);
                Coordinates mouseLocation = plot_event_analysis.Plot.GetCoordinates(mousePixel);
                DataPoint nearest = ev_scatter.Data.GetNearest(mouseLocation, plot_event_analysis.Plot.LastRender);

                // place the crosshair over the highlighted point
                if (nearest.IsReal)
                {
                    MyCrosshair.IsVisible = true;
                    MyCrosshair.Position = nearest.Coordinates;
                    plot_event_analysis.Refresh();
                    //PlotEvent(EventIDs[nearest.Index]);
                    lb_selected_point.Text = $"Selected Index={nearest.Index}, X={nearest.X:0.##}, Y={nearest.Y:0.##}";
                }

                // hide the crosshair when no point is selected
                if (!nearest.IsReal && MyCrosshair.IsVisible)
                {
                    MyCrosshair.IsVisible = false;
                    plot_event_analysis.Refresh();
                    lb_selected_point.Text = "";
                }
            }
        }

        private void plot_event_analysis_MouseDown(object sender, MouseEventArgs e)
        {
            var thingUnderMouse = GetSpanUnderMouse(e.X, e.Y);
            if (thingUnderMouse is not null)
            {
                SpanBeingDragged = thingUnderMouse;
                plot_event_analysis.Interaction.Disable(); // disable panning while dragging
            }
            if (PlotEventsClickable)
            {
                // determine where the mouse is and get the nearest point
                Pixel mousePixel = new(e.Location.X, e.Location.Y);
                Coordinates mouseLocation = plot_event_analysis.Plot.GetCoordinates(mousePixel);
                DataPoint nearest = ev_scatter.Data.GetNearest(mouseLocation, plot_event_analysis.Plot.LastRender);

                // place the crosshair over the highlighted point
                if (nearest.IsReal)
                {

                    PlotEvent(EventIDs[nearest.Index]);
                }
            }
        }
        private void plot_event_analysis_MouseUp(object sender, MouseEventArgs e)
        {
            if (SpanBeingDragged != null)
            {
                var limits = SpanBeingDragged.Span.GetAxisLimits();
                nud_plotmin.Value = (decimal)limits.Left;
                nud_plotmax.Value = (decimal)limits.Right;
            }

            SpanBeingDragged = null;
            plot_event_analysis.Interaction.Enable(); // enable panning
            plot_event_analysis.Refresh();
        }
        private AxisSpanUnderMouse? GetSpanUnderMouse(float x, float y)
        {
            CoordinateRect rect = plot_event_analysis.Plot.GetCoordinateRect(x, y, radius: 10);

            foreach (AxisSpan span in plot_event_analysis.Plot.GetPlottables<AxisSpan>().Reverse())
            {
                AxisSpanUnderMouse? spanUnderMouse = span.UnderMouse(rect);
                if (spanUnderMouse is not null)
                    return spanUnderMouse;
            }

            return null;
        }

        private void bt_select_range_Click(object sender, EventArgs e)
        {
            double min = plot_event_analysis.Plot.Axes.Bottom.Min;
            double max = plot_event_analysis.Plot.Axes.Bottom.Max;
            double span = max - min;
            var hs = plot_event_analysis.Plot.Add.HorizontalSpan(min + span / 3, max - span / 3);
            hs.IsDraggable = true;
            hs.IsResizable = true;
            plot_event_analysis.Interaction.Disable();

            plot_event_analysis.Refresh();
        }

        private void bt_reset_range_Click(object sender, EventArgs e)
        {
            nud_plotmin.Value = 0;
            nud_plotmax.Value = 1000;
        }

        private void bt_export_range_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = "csv";
            saveFileDialog.Filter = "CSV files (*.csv)|*.csv";

            // Set a suggested file name
            saveFileDialog.FileName = filepath + "_currentlevels.csv";

            // Show the dialog and check if the user clicked OK
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string file = saveFileDialog.FileName;
                analyzer.SaveCurrentLevelRangeToCSV(file, (double)nud_plotmin.Value, (double)nud_plotmax.Value);
            }
        }



        // ----------- Helper Functions ----------------------
        // ---------------------------------------------------
        // ---------------------------------------------------

        private ScottPlot.TickGenerators.NumericAutomatic GetLogTicks()
        {
            // create a minor tick generator that places log-distributed minor ticks
            ScottPlot.TickGenerators.LogMinorTickGenerator minorTickGen = new();

            // create a numeric tick generator that uses our custom minor tick generator
            ScottPlot.TickGenerators.NumericAutomatic tickGen = new();
            tickGen.MinorTickGenerator = minorTickGen;

            // create a custom tick formatter to set the label text for each tick
            // static string LogTickLabelFormatter(double y) => $"{Math.Pow(10, y):N2}";
            static string LogTickLabelFormatter(double y)
            {
                // Calculate the value in standard form (10^y)
                double value = Math.Pow(10, y);

                // Format values >= 1 as integers, otherwise use scientific notation with two decimals
                if (value >= 1)
                    return $"{value:N0}"; // Display as an integer without decimals
                else if (value == 0.1) return "0.1";
                else if (value == 0.01) return "0.01";
                else if (value == 0.001) return "0.001";
                else return "";
            }
            tickGen.IntegerTicksOnly = false;
            // tell our custom tick generator to use our new label formatter
            tickGen.LabelFormatter = LogTickLabelFormatter;
            tickGen.TickDensity = 0.5;
            // tell the left axis to use our custom tick generator
            return tickGen;
        }
        private void ClearAll()
        {
            plot_data.Reset();
            plot_derivative.Reset();
            plot_baseline.Reset();
            plot_event.Reset();
            rtb_info.Text = string.Empty;
        }
    }
}
