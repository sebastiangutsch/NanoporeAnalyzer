namespace Nanopore_Analyzer
{
    using OxyPlot;
    using OxyPlot.Series;
    using System.IO;
    using static System.Runtime.InteropServices.JavaScript.JSType;

    public partial class Form1 : Form
    {
        string? filepath;
        string? filetype;
        Analyzer? analyzer;
        int activeSweep = 1;
        int SweepCount = 1;

        public Form1()
        {
            InitializeComponent();

        }
        private void menu_load_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select a File";
            openFileDialog.Filter = "Axon Binary File (*.abf)|*.abf|TDSM-File (*.tdms)|*.tdms";
            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    filepath = openFileDialog.FileName;
                    if (Path.GetExtension(filepath) == ".abf")
                    {
                        // open file and read abf header
                        this.Text = filepath;
                        analyzer = new Analyzer(filepath, true);
                        rtb_info.Text = analyzer.getHeaderInfo();
                        rtb_info.Text += analyzer.getSamplePeriod().ToString();
                        cb_channelnumber.Items.Clear();
                        //rtb_info.Text += analyzer.ChannelCount().ToString();
                        for (int i = 1; i <= analyzer.ChannelCount(); i++)
                        {
                            cb_channelnumber.Items.Add("Channel " + i.ToString());
                        }
                        cb_channelnumber.SelectedIndex = 0;
                        SweepCount = analyzer.SweepCount();
                        PlotSweep(1); // plot the first Sweep

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
                catch { }
            }
        }

        private void saveAsAbfv1ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left: PlotSweep(activeSweep -= 1); break;
                case Keys.Right: PlotSweep(activeSweep += 1); break;
            }
        }

        private void PlotSweep(int number)
        {
            activeSweep = number;
            if (activeSweep > SweepCount)
            {
                activeSweep = SweepCount;
            }
            else if (activeSweep < 1)
            {
                activeSweep = 1;
            }
            else
            {
                lb_sweeps.Text = "Sweep: " + (number).ToString() + "/" + SweepCount.ToString();
                plot1.Model = analyzer.GetSweep(number - 1, cb_channelnumber.SelectedIndex);
            }
        }

        private void pb_arrowleft_Click(object sender, EventArgs e)
        {
            PlotSweep(activeSweep -= 1);
        }

        private void pb_arrowright_Click(object sender, EventArgs e)
        {
            PlotSweep(activeSweep += 1);
        }
    }
}
