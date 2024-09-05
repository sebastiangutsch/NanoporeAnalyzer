namespace Nanopore_Analyzer
{
    partial class mainform
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainform));
            menuStrip1 = new MenuStrip();
            toolStripMenuItem1 = new ToolStripMenuItem();
            menu_load = new ToolStripMenuItem();
            closeToolStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            saveAsAbfv1ToolStripMenuItem = new ToolStripMenuItem();
            rtb_info = new RichTextBox();
            cb_channelnumber = new ComboBox();
            pb_delete_sweep = new PictureBox();
            label1 = new Label();
            gb_filtering = new GroupBox();
            cb_selectfilter = new ComboBox();
            chk_usefilter = new CheckBox();
            bt_filter = new Button();
            nUD_filterorder = new NumericUpDown();
            label2 = new Label();
            nud_cutoff = new NumericUpDown();
            gb_background = new GroupBox();
            bt_save_baseline = new Button();
            bt_load_baseline = new Button();
            label19 = new Label();
            nud_baseline_stepsize = new NumericUpDown();
            label18 = new Label();
            label17 = new Label();
            label16 = new Label();
            nud_BLto = new NumericUpDown();
            nud_BLfrom = new NumericUpDown();
            label15 = new Label();
            nud_baseline_interval = new NumericUpDown();
            bt_getbaseline = new Button();
            gb_eventdetection = new GroupBox();
            plot_event = new ScottPlot.WinForms.FormsPlot();
            chk_edrange = new CheckBox();
            label13 = new Label();
            label12 = new Label();
            label11 = new Label();
            nud_edto = new NumericUpDown();
            nud_edfrom = new NumericUpDown();
            bt_save_events = new Button();
            label5 = new Label();
            cb_detection_method = new ComboBox();
            nud_th = new NumericUpDown();
            nud_minlength = new NumericUpDown();
            label4 = new Label();
            label3 = new Label();
            lb_events = new Label();
            nud_events = new NumericUpDown();
            bt_detectevents = new Button();
            gb_eventanalysis = new GroupBox();
            bt_reset_range = new Button();
            bt_export_range = new Button();
            bt_select_range = new Button();
            lb_selected_point = new Label();
            plot_event_analysis = new ScottPlot.WinForms.FormsPlot();
            pb_plot_options = new PictureBox();
            label14 = new Label();
            nud_plotmax = new NumericUpDown();
            nud_plotmin = new NumericUpDown();
            bt_plot_selected_graph = new Button();
            cb_select_plot = new ComboBox();
            nud_maxsweeppoints = new NumericUpDown();
            label6 = new Label();
            nud_starttime = new NumericUpDown();
            nud_endtime = new NumericUpDown();
            label7 = new Label();
            label8 = new Label();
            pb_arrowright = new PictureBox();
            pb_arrowleft = new PictureBox();
            bt_update_plot = new Button();
            chk_show_rect = new CheckBox();
            label9 = new Label();
            nud_baseline_rms = new NumericUpDown();
            label10 = new Label();
            nud_baseline_sigma = new NumericUpDown();
            chk_show_rms = new CheckBox();
            chk_calc_rms = new CheckBox();
            pb_logo = new PictureBox();
            plot_data = new ScottPlot.WinForms.FormsPlot();
            plot_derivative = new ScottPlot.WinForms.FormsPlot();
            plot_baseline = new ScottPlot.WinForms.FormsPlot();
            nud_bin_width = new NumericUpDown();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pb_delete_sweep).BeginInit();
            gb_filtering.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nUD_filterorder).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nud_cutoff).BeginInit();
            gb_background.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nud_baseline_stepsize).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nud_BLto).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nud_BLfrom).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nud_baseline_interval).BeginInit();
            gb_eventdetection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nud_edto).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nud_edfrom).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nud_th).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nud_minlength).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nud_events).BeginInit();
            gb_eventanalysis.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pb_plot_options).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nud_plotmax).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nud_plotmin).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nud_maxsweeppoints).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nud_starttime).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nud_endtime).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pb_arrowright).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pb_arrowleft).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nud_baseline_rms).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nud_baseline_sigma).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pb_logo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nud_bin_width).BeginInit();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { toolStripMenuItem1 });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1784, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { menu_load, closeToolStripMenuItem, saveToolStripMenuItem, saveAsAbfv1ToolStripMenuItem });
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(37, 20);
            toolStripMenuItem1.Text = "File";
            // 
            // menu_load
            // 
            menu_load.Name = "menu_load";
            menu_load.Size = new Size(147, 22);
            menu_load.Text = "Load";
            menu_load.Click += menu_load_Click;
            // 
            // closeToolStripMenuItem
            // 
            closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            closeToolStripMenuItem.Size = new Size(147, 22);
            closeToolStripMenuItem.Text = "Close";
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(147, 22);
            saveToolStripMenuItem.Text = "Save";
            // 
            // saveAsAbfv1ToolStripMenuItem
            // 
            saveAsAbfv1ToolStripMenuItem.Name = "saveAsAbfv1ToolStripMenuItem";
            saveAsAbfv1ToolStripMenuItem.Size = new Size(147, 22);
            saveAsAbfv1ToolStripMenuItem.Text = "Save as abf.v1";
            saveAsAbfv1ToolStripMenuItem.Click += saveAsAbfv1ToolStripMenuItem_Click;
            // 
            // rtb_info
            // 
            rtb_info.Location = new Point(12, 738);
            rtb_info.Name = "rtb_info";
            rtb_info.ReadOnly = true;
            rtb_info.Size = new Size(480, 234);
            rtb_info.TabIndex = 1;
            rtb_info.Text = "";
            // 
            // cb_channelnumber
            // 
            cb_channelnumber.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_channelnumber.FormattingEnabled = true;
            cb_channelnumber.Location = new Point(117, 27);
            cb_channelnumber.Name = "cb_channelnumber";
            cb_channelnumber.Size = new Size(121, 23);
            cb_channelnumber.TabIndex = 3;
            // 
            // pb_delete_sweep
            // 
            pb_delete_sweep.Enabled = false;
            pb_delete_sweep.Image = (Image)resources.GetObject("pb_delete_sweep.Image");
            pb_delete_sweep.InitialImage = (Image)resources.GetObject("pb_delete_sweep.InitialImage");
            pb_delete_sweep.Location = new Point(526, 27);
            pb_delete_sweep.Name = "pb_delete_sweep";
            pb_delete_sweep.Size = new Size(32, 32);
            pb_delete_sweep.SizeMode = PictureBoxSizeMode.Zoom;
            pb_delete_sweep.TabIndex = 10;
            pb_delete_sweep.TabStop = false;
            pb_delete_sweep.Visible = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(4, 51);
            label1.Name = "label1";
            label1.Size = new Size(66, 15);
            label1.TabIndex = 11;
            label1.Text = "Cutoff (Hz)";
            // 
            // gb_filtering
            // 
            gb_filtering.Controls.Add(cb_selectfilter);
            gb_filtering.Controls.Add(chk_usefilter);
            gb_filtering.Controls.Add(bt_filter);
            gb_filtering.Controls.Add(nUD_filterorder);
            gb_filtering.Controls.Add(label2);
            gb_filtering.Controls.Add(nud_cutoff);
            gb_filtering.Controls.Add(label1);
            gb_filtering.Location = new Point(12, 559);
            gb_filtering.Name = "gb_filtering";
            gb_filtering.Size = new Size(217, 173);
            gb_filtering.TabIndex = 12;
            gb_filtering.TabStop = false;
            gb_filtering.Text = "Digital Filtering";
            // 
            // cb_selectfilter
            // 
            cb_selectfilter.FormattingEnabled = true;
            cb_selectfilter.Items.AddRange(new object[] { "Butterworth" });
            cb_selectfilter.Location = new Point(6, 20);
            cb_selectfilter.Name = "cb_selectfilter";
            cb_selectfilter.Size = new Size(195, 23);
            cb_selectfilter.TabIndex = 17;
            // 
            // chk_usefilter
            // 
            chk_usefilter.AutoSize = true;
            chk_usefilter.Enabled = false;
            chk_usefilter.Location = new Point(103, 117);
            chk_usefilter.Name = "chk_usefilter";
            chk_usefilter.Size = new Size(71, 19);
            chk_usefilter.TabIndex = 16;
            chk_usefilter.Text = "use filter";
            chk_usefilter.UseVisualStyleBackColor = true;
            // 
            // bt_filter
            // 
            bt_filter.Location = new Point(6, 114);
            bt_filter.Name = "bt_filter";
            bt_filter.Size = new Size(75, 23);
            bt_filter.TabIndex = 15;
            bt_filter.Text = "Apply Filter";
            bt_filter.UseVisualStyleBackColor = true;
            bt_filter.Click += bt_filter_Click;
            // 
            // nUD_filterorder
            // 
            nUD_filterorder.Location = new Point(81, 80);
            nUD_filterorder.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            nUD_filterorder.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nUD_filterorder.Name = "nUD_filterorder";
            nUD_filterorder.Size = new Size(120, 23);
            nUD_filterorder.TabIndex = 14;
            nUD_filterorder.Value = new decimal(new int[] { 5, 0, 0, 0 });
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(5, 82);
            label2.Name = "label2";
            label2.Size = new Size(66, 15);
            label2.TabIndex = 13;
            label2.Text = "Filter Order";
            // 
            // nud_cutoff
            // 
            nud_cutoff.Increment = new decimal(new int[] { 5000, 0, 0, 0 });
            nud_cutoff.InterceptArrowKeys = false;
            nud_cutoff.Location = new Point(81, 49);
            nud_cutoff.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            nud_cutoff.Minimum = new decimal(new int[] { 5000, 0, 0, 0 });
            nud_cutoff.Name = "nud_cutoff";
            nud_cutoff.Size = new Size(120, 23);
            nud_cutoff.TabIndex = 12;
            nud_cutoff.Value = new decimal(new int[] { 25000, 0, 0, 0 });
            // 
            // gb_background
            // 
            gb_background.Controls.Add(bt_save_baseline);
            gb_background.Controls.Add(bt_load_baseline);
            gb_background.Controls.Add(label19);
            gb_background.Controls.Add(nud_baseline_stepsize);
            gb_background.Controls.Add(label18);
            gb_background.Controls.Add(label17);
            gb_background.Controls.Add(label16);
            gb_background.Controls.Add(nud_BLto);
            gb_background.Controls.Add(nud_BLfrom);
            gb_background.Controls.Add(label15);
            gb_background.Controls.Add(nud_baseline_interval);
            gb_background.Controls.Add(bt_getbaseline);
            gb_background.Location = new Point(255, 560);
            gb_background.Name = "gb_background";
            gb_background.Size = new Size(237, 164);
            gb_background.TabIndex = 13;
            gb_background.TabStop = false;
            gb_background.Text = "Background";
            // 
            // bt_save_baseline
            // 
            bt_save_baseline.Location = new Point(6, 135);
            bt_save_baseline.Name = "bt_save_baseline";
            bt_save_baseline.Size = new Size(75, 23);
            bt_save_baseline.TabIndex = 11;
            bt_save_baseline.Text = "Save";
            bt_save_baseline.UseVisualStyleBackColor = true;
            bt_save_baseline.Click += bt_save_baseline_Click;
            // 
            // bt_load_baseline
            // 
            bt_load_baseline.Location = new Point(87, 135);
            bt_load_baseline.Name = "bt_load_baseline";
            bt_load_baseline.Size = new Size(75, 23);
            bt_load_baseline.TabIndex = 10;
            bt_load_baseline.Text = "Load";
            bt_load_baseline.UseVisualStyleBackColor = true;
            bt_load_baseline.Click += bt_load_baseline_Click;
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Location = new Point(4, 79);
            label19.Name = "label19";
            label19.Size = new Size(68, 15);
            label19.TabIndex = 9;
            label19.Text = "Stepsize (s):";
            // 
            // nud_baseline_stepsize
            // 
            nud_baseline_stepsize.DecimalPlaces = 1;
            nud_baseline_stepsize.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            nud_baseline_stepsize.Location = new Point(75, 77);
            nud_baseline_stepsize.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            nud_baseline_stepsize.Name = "nud_baseline_stepsize";
            nud_baseline_stepsize.Size = new Size(48, 23);
            nud_baseline_stepsize.TabIndex = 8;
            nud_baseline_stepsize.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new Point(188, 21);
            label18.Name = "label18";
            label18.Size = new Size(12, 15);
            label18.TabIndex = 7;
            label18.Text = "s";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(99, 22);
            label17.Name = "label17";
            label17.Size = new Size(26, 15);
            label17.TabIndex = 6;
            label17.Text = "s to";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(4, 21);
            label16.Name = "label16";
            label16.Size = new Size(33, 15);
            label16.TabIndex = 5;
            label16.Text = "from";
            // 
            // nud_BLto
            // 
            nud_BLto.Location = new Point(132, 19);
            nud_BLto.Name = "nud_BLto";
            nud_BLto.Size = new Size(50, 23);
            nud_BLto.TabIndex = 4;
            // 
            // nud_BLfrom
            // 
            nud_BLfrom.Location = new Point(43, 19);
            nud_BLfrom.Name = "nud_BLfrom";
            nud_BLfrom.Size = new Size(50, 23);
            nud_BLfrom.TabIndex = 3;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(4, 50);
            label15.Name = "label15";
            label15.Size = new Size(65, 15);
            label15.TabIndex = 2;
            label15.Text = "Interval (s):";
            // 
            // nud_baseline_interval
            // 
            nud_baseline_interval.DecimalPlaces = 1;
            nud_baseline_interval.Location = new Point(75, 48);
            nud_baseline_interval.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            nud_baseline_interval.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
            nud_baseline_interval.Name = "nud_baseline_interval";
            nud_baseline_interval.Size = new Size(48, 23);
            nud_baseline_interval.TabIndex = 1;
            nud_baseline_interval.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // bt_getbaseline
            // 
            bt_getbaseline.Location = new Point(6, 106);
            bt_getbaseline.Name = "bt_getbaseline";
            bt_getbaseline.Size = new Size(156, 23);
            bt_getbaseline.TabIndex = 0;
            bt_getbaseline.Text = "Calculate Baseline";
            bt_getbaseline.UseVisualStyleBackColor = true;
            bt_getbaseline.Click += bt_getbaseline_Click;
            // 
            // gb_eventdetection
            // 
            gb_eventdetection.Controls.Add(plot_event);
            gb_eventdetection.Controls.Add(chk_edrange);
            gb_eventdetection.Controls.Add(label13);
            gb_eventdetection.Controls.Add(label12);
            gb_eventdetection.Controls.Add(label11);
            gb_eventdetection.Controls.Add(nud_edto);
            gb_eventdetection.Controls.Add(nud_edfrom);
            gb_eventdetection.Controls.Add(bt_save_events);
            gb_eventdetection.Controls.Add(label5);
            gb_eventdetection.Controls.Add(cb_detection_method);
            gb_eventdetection.Controls.Add(nud_th);
            gb_eventdetection.Controls.Add(nud_minlength);
            gb_eventdetection.Controls.Add(label4);
            gb_eventdetection.Controls.Add(label3);
            gb_eventdetection.Controls.Add(lb_events);
            gb_eventdetection.Controls.Add(nud_events);
            gb_eventdetection.Controls.Add(bt_detectevents);
            gb_eventdetection.Location = new Point(498, 560);
            gb_eventdetection.Name = "gb_eventdetection";
            gb_eventdetection.Size = new Size(492, 412);
            gb_eventdetection.TabIndex = 14;
            gb_eventdetection.TabStop = false;
            gb_eventdetection.Text = "Event Detection";
            // 
            // plot_event
            // 
            plot_event.DisplayScale = 1F;
            plot_event.Location = new Point(6, 108);
            plot_event.Name = "plot_event";
            plot_event.Size = new Size(480, 259);
            plot_event.TabIndex = 43;
            // 
            // chk_edrange
            // 
            chk_edrange.AutoSize = true;
            chk_edrange.Checked = true;
            chk_edrange.CheckState = CheckState.Checked;
            chk_edrange.Location = new Point(320, 52);
            chk_edrange.Name = "chk_edrange";
            chk_edrange.Size = new Size(124, 19);
            chk_edrange.TabIndex = 34;
            chk_edrange.Text = "analyze everything";
            chk_edrange.UseVisualStyleBackColor = true;
            chk_edrange.CheckedChanged += chk_edrange_CheckedChanged;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(460, 25);
            label13.Name = "label13";
            label13.Size = new Size(12, 15);
            label13.TabIndex = 33;
            label13.Text = "s";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(387, 25);
            label12.Name = "label12";
            label12.Size = new Size(26, 15);
            label12.TabIndex = 32;
            label12.Text = "s to";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(303, 25);
            label11.Name = "label11";
            label11.Size = new Size(33, 15);
            label11.TabIndex = 31;
            label11.Text = "from";
            // 
            // nud_edto
            // 
            nud_edto.Enabled = false;
            nud_edto.Location = new Point(414, 22);
            nud_edto.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            nud_edto.Name = "nud_edto";
            nud_edto.Size = new Size(44, 23);
            nud_edto.TabIndex = 30;
            nud_edto.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // nud_edfrom
            // 
            nud_edfrom.Enabled = false;
            nud_edfrom.Location = new Point(336, 22);
            nud_edfrom.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            nud_edfrom.Name = "nud_edfrom";
            nud_edfrom.Size = new Size(49, 23);
            nud_edfrom.TabIndex = 29;
            // 
            // bt_save_events
            // 
            bt_save_events.Location = new Point(361, 373);
            bt_save_events.Name = "bt_save_events";
            bt_save_events.Size = new Size(111, 23);
            bt_save_events.TabIndex = 33;
            bt_save_events.Tag = "Exports all current levels to a csv file";
            bt_save_events.Text = "Export All to CSV";
            bt_save_events.UseVisualStyleBackColor = true;
            bt_save_events.Click += bt_save_events_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(18, 82);
            label5.Name = "label5";
            label5.Size = new Size(106, 15);
            label5.TabIndex = 28;
            label5.Text = "Detection Method:";
            // 
            // cb_detection_method
            // 
            cb_detection_method.FormattingEnabled = true;
            cb_detection_method.Items.AddRange(new object[] { "Simple Threshold", "Simple Threshold with BG Substraction", "Differential Rectification" });
            cb_detection_method.Location = new Point(141, 79);
            cb_detection_method.Name = "cb_detection_method";
            cb_detection_method.Size = new Size(226, 23);
            cb_detection_method.TabIndex = 27;
            // 
            // nud_th
            // 
            nud_th.DecimalPlaces = 3;
            nud_th.Location = new Point(69, 22);
            nud_th.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            nud_th.Name = "nud_th";
            nud_th.Size = new Size(57, 23);
            nud_th.TabIndex = 26;
            nud_th.Value = new decimal(new int[] { 20, 0, 0, 0 });
            // 
            // nud_minlength
            // 
            nud_minlength.Location = new Point(235, 22);
            nud_minlength.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            nud_minlength.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nud_minlength.Name = "nud_minlength";
            nud_minlength.Size = new Size(60, 23);
            nud_minlength.TabIndex = 25;
            nud_minlength.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(141, 25);
            label4.Name = "label4";
            label4.Size = new Size(88, 15);
            label4.TabIndex = 24;
            label4.Text = "MinLength(µs):";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(17, 24);
            label3.Name = "label3";
            label3.Size = new Size(49, 15);
            label3.TabIndex = 23;
            label3.Text = "Th (pA):";
            // 
            // lb_events
            // 
            lb_events.AutoSize = true;
            lb_events.Location = new Point(59, 375);
            lb_events.Name = "lb_events";
            lb_events.Size = new Size(67, 15);
            lb_events.TabIndex = 3;
            lb_events.Text = "0 / 0 events";
            // 
            // nud_events
            // 
            nud_events.Location = new Point(235, 373);
            nud_events.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nud_events.Name = "nud_events";
            nud_events.Size = new Size(101, 23);
            nud_events.TabIndex = 2;
            nud_events.Value = new decimal(new int[] { 1, 0, 0, 0 });
            nud_events.ValueChanged += nud_events_ValueChanged;
            // 
            // bt_detectevents
            // 
            bt_detectevents.Location = new Point(397, 79);
            bt_detectevents.Name = "bt_detectevents";
            bt_detectevents.Size = new Size(75, 23);
            bt_detectevents.TabIndex = 0;
            bt_detectevents.Text = "Detect Events";
            bt_detectevents.UseVisualStyleBackColor = true;
            bt_detectevents.Click += bt_detectevents_Click;
            // 
            // gb_eventanalysis
            // 
            gb_eventanalysis.Controls.Add(nud_bin_width);
            gb_eventanalysis.Controls.Add(bt_reset_range);
            gb_eventanalysis.Controls.Add(bt_export_range);
            gb_eventanalysis.Controls.Add(bt_select_range);
            gb_eventanalysis.Controls.Add(lb_selected_point);
            gb_eventanalysis.Controls.Add(plot_event_analysis);
            gb_eventanalysis.Controls.Add(pb_plot_options);
            gb_eventanalysis.Controls.Add(label14);
            gb_eventanalysis.Controls.Add(nud_plotmax);
            gb_eventanalysis.Controls.Add(nud_plotmin);
            gb_eventanalysis.Controls.Add(bt_plot_selected_graph);
            gb_eventanalysis.Controls.Add(cb_select_plot);
            gb_eventanalysis.Location = new Point(996, 560);
            gb_eventanalysis.Name = "gb_eventanalysis";
            gb_eventanalysis.Size = new Size(489, 412);
            gb_eventanalysis.TabIndex = 15;
            gb_eventanalysis.TabStop = false;
            gb_eventanalysis.Text = "Event Analysis";
            // 
            // bt_reset_range
            // 
            bt_reset_range.Location = new Point(258, 54);
            bt_reset_range.Name = "bt_reset_range";
            bt_reset_range.Size = new Size(51, 23);
            bt_reset_range.TabIndex = 47;
            bt_reset_range.Text = "Reset";
            bt_reset_range.UseVisualStyleBackColor = true;
            bt_reset_range.Click += bt_reset_range_Click;
            // 
            // bt_export_range
            // 
            bt_export_range.Location = new Point(14, 83);
            bt_export_range.Name = "bt_export_range";
            bt_export_range.Size = new Size(127, 23);
            bt_export_range.TabIndex = 46;
            bt_export_range.Text = "Export Range to CSV";
            bt_export_range.UseVisualStyleBackColor = true;
            bt_export_range.Click += bt_export_range_Click;
            // 
            // bt_select_range
            // 
            bt_select_range.AccessibleName = "";
            bt_select_range.Location = new Point(201, 54);
            bt_select_range.Name = "bt_select_range";
            bt_select_range.Size = new Size(51, 23);
            bt_select_range.TabIndex = 45;
            bt_select_range.Text = "Select";
            bt_select_range.UseVisualStyleBackColor = true;
            bt_select_range.Click += bt_select_range_Click;
            // 
            // lb_selected_point
            // 
            lb_selected_point.AutoSize = true;
            lb_selected_point.Location = new Point(52, 121);
            lb_selected_point.Name = "lb_selected_point";
            lb_selected_point.Size = new Size(66, 15);
            lb_selected_point.TabIndex = 44;
            lb_selected_point.Text = "Point Label";
            lb_selected_point.Visible = false;
            // 
            // plot_event_analysis
            // 
            plot_event_analysis.DisplayScale = 1F;
            plot_event_analysis.Location = new Point(6, 104);
            plot_event_analysis.Name = "plot_event_analysis";
            plot_event_analysis.Size = new Size(477, 302);
            plot_event_analysis.TabIndex = 43;
            plot_event_analysis.MouseDown += plot_event_analysis_MouseDown;
            plot_event_analysis.MouseMove += plot_event_analysis_MouseMove;
            plot_event_analysis.MouseUp += plot_event_analysis_MouseUp;
            // 
            // pb_plot_options
            // 
            pb_plot_options.Enabled = false;
            pb_plot_options.Image = Properties.Resources._params;
            pb_plot_options.Location = new Point(348, 16);
            pb_plot_options.Name = "pb_plot_options";
            pb_plot_options.Size = new Size(32, 32);
            pb_plot_options.SizeMode = PictureBoxSizeMode.Zoom;
            pb_plot_options.TabIndex = 37;
            pb_plot_options.TabStop = false;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(14, 56);
            label14.Name = "label14";
            label14.Size = new Size(43, 15);
            label14.TabIndex = 36;
            label14.Text = "Range:";
            // 
            // nud_plotmax
            // 
            nud_plotmax.DecimalPlaces = 3;
            nud_plotmax.Location = new Point(132, 54);
            nud_plotmax.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            nud_plotmax.Name = "nud_plotmax";
            nud_plotmax.Size = new Size(63, 23);
            nud_plotmax.TabIndex = 35;
            nud_plotmax.Value = new decimal(new int[] { 1000, 0, 0, 0 });
            // 
            // nud_plotmin
            // 
            nud_plotmin.DecimalPlaces = 3;
            nud_plotmin.Location = new Point(63, 54);
            nud_plotmin.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            nud_plotmin.Name = "nud_plotmin";
            nud_plotmin.Size = new Size(63, 23);
            nud_plotmin.TabIndex = 34;
            // 
            // bt_plot_selected_graph
            // 
            bt_plot_selected_graph.Location = new Point(315, 54);
            bt_plot_selected_graph.Name = "bt_plot_selected_graph";
            bt_plot_selected_graph.Size = new Size(51, 23);
            bt_plot_selected_graph.TabIndex = 31;
            bt_plot_selected_graph.Text = "Plot";
            bt_plot_selected_graph.UseVisualStyleBackColor = true;
            bt_plot_selected_graph.Click += bt_plot_selected_graph_Click;
            // 
            // cb_select_plot
            // 
            cb_select_plot.FormattingEnabled = true;
            cb_select_plot.Items.AddRange(new object[] { "Dwelltime (ms) vs. Current Level", "Dwelltime (ms) vs. I/I0", "Dwelltime(ms) Histogram", "log Dwelltime (ms) Histogram", "Standard Deviation vs. I/I0", "Event Duration vs. I(level average) / I0", "Event Duration Histogram" });
            cb_select_plot.Location = new Point(14, 25);
            cb_select_plot.Name = "cb_select_plot";
            cb_select_plot.Size = new Size(314, 23);
            cb_select_plot.TabIndex = 30;
            // 
            // nud_maxsweeppoints
            // 
            nud_maxsweeppoints.Enabled = false;
            nud_maxsweeppoints.Increment = new decimal(new int[] { 1000, 0, 0, 0 });
            nud_maxsweeppoints.Location = new Point(320, 34);
            nud_maxsweeppoints.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            nud_maxsweeppoints.Minimum = new decimal(new int[] { 2000, 0, 0, 0 });
            nud_maxsweeppoints.Name = "nud_maxsweeppoints";
            nud_maxsweeppoints.Size = new Size(65, 23);
            nud_maxsweeppoints.TabIndex = 23;
            nud_maxsweeppoints.Value = new decimal(new int[] { 1000000, 0, 0, 0 });
            nud_maxsweeppoints.Visible = false;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Enabled = false;
            label6.Location = new Point(249, 36);
            label6.Name = "label6";
            label6.Size = new Size(69, 15);
            label6.TabIndex = 24;
            label6.Text = "Max Points:";
            label6.Visible = false;
            // 
            // nud_starttime
            // 
            nud_starttime.DecimalPlaces = 3;
            nud_starttime.Location = new Point(441, 34);
            nud_starttime.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            nud_starttime.Name = "nud_starttime";
            nud_starttime.RightToLeft = RightToLeft.No;
            nud_starttime.Size = new Size(67, 23);
            nud_starttime.TabIndex = 26;
            nud_starttime.TextAlign = HorizontalAlignment.Right;
            nud_starttime.UpDownAlign = LeftRightAlignment.Left;
            // 
            // nud_endtime
            // 
            nud_endtime.DecimalPlaces = 3;
            nud_endtime.Location = new Point(567, 34);
            nud_endtime.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            nud_endtime.Name = "nud_endtime";
            nud_endtime.RightToLeft = RightToLeft.No;
            nud_endtime.Size = new Size(67, 23);
            nud_endtime.TabIndex = 27;
            nud_endtime.TextAlign = HorizontalAlignment.Right;
            nud_endtime.UpDownAlign = LeftRightAlignment.Left;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(510, 36);
            label7.Name = "label7";
            label7.Size = new Size(12, 15);
            label7.TabIndex = 29;
            label7.Text = "s";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(636, 36);
            label8.Name = "label8";
            label8.Size = new Size(12, 15);
            label8.TabIndex = 30;
            label8.Text = "s";
            // 
            // pb_arrowright
            // 
            pb_arrowright.Image = (Image)resources.GetObject("pb_arrowright.Image");
            pb_arrowright.InitialImage = (Image)resources.GetObject("pb_arrowright.InitialImage");
            pb_arrowright.Location = new Point(653, 27);
            pb_arrowright.Name = "pb_arrowright";
            pb_arrowright.Size = new Size(32, 32);
            pb_arrowright.SizeMode = PictureBoxSizeMode.Zoom;
            pb_arrowright.TabIndex = 6;
            pb_arrowright.TabStop = false;
            pb_arrowright.Click += pb_arrowright_Click;
            // 
            // pb_arrowleft
            // 
            pb_arrowleft.Image = (Image)resources.GetObject("pb_arrowleft.Image");
            pb_arrowleft.InitialImage = (Image)resources.GetObject("pb_arrowleft.InitialImage");
            pb_arrowleft.Location = new Point(403, 27);
            pb_arrowleft.Name = "pb_arrowleft";
            pb_arrowleft.Size = new Size(32, 32);
            pb_arrowleft.SizeMode = PictureBoxSizeMode.Zoom;
            pb_arrowleft.TabIndex = 7;
            pb_arrowleft.TabStop = false;
            pb_arrowleft.Click += pb_arrowleft_Click;
            // 
            // bt_update_plot
            // 
            bt_update_plot.Location = new Point(702, 32);
            bt_update_plot.Name = "bt_update_plot";
            bt_update_plot.Size = new Size(95, 23);
            bt_update_plot.TabIndex = 31;
            bt_update_plot.Text = "Update Plot";
            bt_update_plot.UseVisualStyleBackColor = true;
            bt_update_plot.Click += bt_update_plot_Click;
            // 
            // chk_show_rect
            // 
            chk_show_rect.AutoSize = true;
            chk_show_rect.Location = new Point(1063, 31);
            chk_show_rect.Name = "chk_show_rect";
            chk_show_rect.Size = new Size(78, 19);
            chk_show_rect.TabIndex = 32;
            chk_show_rect.Text = "Show rect";
            chk_show_rect.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(808, 33);
            label9.Name = "label9";
            label9.Size = new Size(80, 15);
            label9.TabIndex = 33;
            label9.Text = "Baseline RMS:";
            // 
            // nud_baseline_rms
            // 
            nud_baseline_rms.DecimalPlaces = 3;
            nud_baseline_rms.Location = new Point(894, 30);
            nud_baseline_rms.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            nud_baseline_rms.Name = "nud_baseline_rms";
            nud_baseline_rms.Size = new Size(65, 23);
            nud_baseline_rms.TabIndex = 34;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(808, 56);
            label10.Name = "label10";
            label10.Size = new Size(63, 15);
            label10.TabIndex = 35;
            label10.Text = "Baseline σ:";
            // 
            // nud_baseline_sigma
            // 
            nud_baseline_sigma.DecimalPlaces = 3;
            nud_baseline_sigma.Location = new Point(894, 54);
            nud_baseline_sigma.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            nud_baseline_sigma.Name = "nud_baseline_sigma";
            nud_baseline_sigma.Size = new Size(65, 23);
            nud_baseline_sigma.TabIndex = 36;
            // 
            // chk_show_rms
            // 
            chk_show_rms.AutoSize = true;
            chk_show_rms.Checked = true;
            chk_show_rms.CheckState = CheckState.Checked;
            chk_show_rms.Location = new Point(965, 32);
            chk_show_rms.Name = "chk_show_rms";
            chk_show_rms.Size = new Size(82, 19);
            chk_show_rms.TabIndex = 37;
            chk_show_rms.Text = "Show RMS";
            chk_show_rms.UseVisualStyleBackColor = true;
            // 
            // chk_calc_rms
            // 
            chk_calc_rms.AutoSize = true;
            chk_calc_rms.Checked = true;
            chk_calc_rms.CheckState = CheckState.Checked;
            chk_calc_rms.Location = new Point(965, 55);
            chk_calc_rms.Name = "chk_calc_rms";
            chk_calc_rms.Size = new Size(76, 19);
            chk_calc_rms.TabIndex = 38;
            chk_calc_rms.Text = "Calc RMS";
            chk_calc_rms.UseVisualStyleBackColor = true;
            // 
            // pb_logo
            // 
            pb_logo.Image = Properties.Resources.Hahn_Schickard_Logo;
            pb_logo.Location = new Point(7, 26);
            pb_logo.Name = "pb_logo";
            pb_logo.Size = new Size(100, 50);
            pb_logo.SizeMode = PictureBoxSizeMode.Zoom;
            pb_logo.TabIndex = 39;
            pb_logo.TabStop = false;
            // 
            // plot_data
            // 
            plot_data.DisplayScale = 1F;
            plot_data.Location = new Point(12, 82);
            plot_data.Name = "plot_data";
            plot_data.Size = new Size(1760, 150);
            plot_data.TabIndex = 40;
            // 
            // plot_derivative
            // 
            plot_derivative.DisplayScale = 1F;
            plot_derivative.Location = new Point(12, 238);
            plot_derivative.Name = "plot_derivative";
            plot_derivative.Size = new Size(1760, 150);
            plot_derivative.TabIndex = 41;
            // 
            // plot_baseline
            // 
            plot_baseline.DisplayScale = 1F;
            plot_baseline.Location = new Point(12, 394);
            plot_baseline.Name = "plot_baseline";
            plot_baseline.Size = new Size(1760, 150);
            plot_baseline.TabIndex = 42;
            // 
            // nud_bin_width
            // 
            nud_bin_width.DecimalPlaces = 3;
            nud_bin_width.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            nud_bin_width.Location = new Point(160, 82);
            nud_bin_width.Name = "nud_bin_width";
            nud_bin_width.Size = new Size(92, 23);
            nud_bin_width.TabIndex = 48;
            nud_bin_width.Value = new decimal(new int[] { 1, 0, 0, 196608 });
            // 
            // mainform
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonFace;
            ClientSize = new Size(1784, 981);
            Controls.Add(plot_baseline);
            Controls.Add(plot_derivative);
            Controls.Add(plot_data);
            Controls.Add(pb_logo);
            Controls.Add(chk_calc_rms);
            Controls.Add(chk_show_rms);
            Controls.Add(nud_baseline_sigma);
            Controls.Add(label10);
            Controls.Add(nud_baseline_rms);
            Controls.Add(label9);
            Controls.Add(chk_show_rect);
            Controls.Add(bt_update_plot);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(nud_endtime);
            Controls.Add(nud_starttime);
            Controls.Add(label6);
            Controls.Add(nud_maxsweeppoints);
            Controls.Add(gb_eventanalysis);
            Controls.Add(gb_background);
            Controls.Add(gb_filtering);
            Controls.Add(pb_delete_sweep);
            Controls.Add(pb_arrowleft);
            Controls.Add(pb_arrowright);
            Controls.Add(cb_channelnumber);
            Controls.Add(rtb_info);
            Controls.Add(menuStrip1);
            Controls.Add(gb_eventdetection);
            KeyPreview = true;
            MainMenuStrip = menuStrip1;
            MaximizeBox = false;
            Name = "mainform";
            Text = "Nanopore Analyzer";
            KeyDown += Form1_KeyDown;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pb_delete_sweep).EndInit();
            gb_filtering.ResumeLayout(false);
            gb_filtering.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nUD_filterorder).EndInit();
            ((System.ComponentModel.ISupportInitialize)nud_cutoff).EndInit();
            gb_background.ResumeLayout(false);
            gb_background.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nud_baseline_stepsize).EndInit();
            ((System.ComponentModel.ISupportInitialize)nud_BLto).EndInit();
            ((System.ComponentModel.ISupportInitialize)nud_BLfrom).EndInit();
            ((System.ComponentModel.ISupportInitialize)nud_baseline_interval).EndInit();
            gb_eventdetection.ResumeLayout(false);
            gb_eventdetection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nud_edto).EndInit();
            ((System.ComponentModel.ISupportInitialize)nud_edfrom).EndInit();
            ((System.ComponentModel.ISupportInitialize)nud_th).EndInit();
            ((System.ComponentModel.ISupportInitialize)nud_minlength).EndInit();
            ((System.ComponentModel.ISupportInitialize)nud_events).EndInit();
            gb_eventanalysis.ResumeLayout(false);
            gb_eventanalysis.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pb_plot_options).EndInit();
            ((System.ComponentModel.ISupportInitialize)nud_plotmax).EndInit();
            ((System.ComponentModel.ISupportInitialize)nud_plotmin).EndInit();
            ((System.ComponentModel.ISupportInitialize)nud_maxsweeppoints).EndInit();
            ((System.ComponentModel.ISupportInitialize)nud_starttime).EndInit();
            ((System.ComponentModel.ISupportInitialize)nud_endtime).EndInit();
            ((System.ComponentModel.ISupportInitialize)pb_arrowright).EndInit();
            ((System.ComponentModel.ISupportInitialize)pb_arrowleft).EndInit();
            ((System.ComponentModel.ISupportInitialize)nud_baseline_rms).EndInit();
            ((System.ComponentModel.ISupportInitialize)nud_baseline_sigma).EndInit();
            ((System.ComponentModel.ISupportInitialize)pb_logo).EndInit();
            ((System.ComponentModel.ISupportInitialize)nud_bin_width).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem menu_load;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem closeToolStripMenuItem;
        private RichTextBox rtb_info;
        private ToolStripMenuItem saveAsAbfv1ToolStripMenuItem;
        private ComboBox cb_channelnumber;
        private PictureBox pb_delete_sweep;
        private Label label1;
        private GroupBox gb_filtering;
        private NumericUpDown nUD_filterorder;
        private Label label2;
        private NumericUpDown nud_cutoff;
        private Button bt_filter;
        private GroupBox gb_background;
        private GroupBox gb_eventdetection;
        private GroupBox gb_eventanalysis;
        private Button bt_getbaseline;
        private Button bt_detectevents;
        private NumericUpDown nud_events;
        private Label lb_events;
        private Label label4;
        private Label label3;
        private NumericUpDown nud_th;
        private NumericUpDown nud_minlength;
        private Label label5;
        private ComboBox cb_detection_method;
        private ComboBox cb_select_plot;
        private Button bt_plot_selected_graph;
        private NumericUpDown nud_maxsweeppoints;
        private Label label6;
        private NumericUpDown nud_starttime;
        private NumericUpDown nud_endtime;
        private Label label7;
        private Label label8;
        private PictureBox pb_arrowright;
        private PictureBox pb_arrowleft;
        private CheckBox chk_usefilter;
        private ComboBox cb_selectfilter;
        private Button bt_update_plot;
        private CheckBox chk_show_rect;
        private Label label9;
        private NumericUpDown nud_baseline_rms;
        private Label label10;
        private NumericUpDown nud_baseline_sigma;
        private CheckBox chk_show_rms;
        private CheckBox chk_calc_rms;
        private Label label11;
        private NumericUpDown nud_edto;
        private NumericUpDown nud_edfrom;
        private Label label13;
        private Label label12;
        private CheckBox chk_edrange;
        private Button bt_save_events;
        private NumericUpDown nud_plotmin;
        private Label label14;
        private NumericUpDown nud_plotmax;
        private PictureBox pb_plot_options;
        private NumericUpDown nud_baseline_interval;
        private Label label15;
        private Label label18;
        private Label label17;
        private Label label16;
        private NumericUpDown nud_BLto;
        private NumericUpDown nud_BLfrom;
        private Label label19;
        private NumericUpDown nud_baseline_stepsize;
        private Button bt_load_baseline;
        private Button bt_save_baseline;
        private PictureBox pb_logo;
        private ScottPlot.WinForms.FormsPlot plot_data;
        private ScottPlot.WinForms.FormsPlot plot_derivative;
        private ScottPlot.WinForms.FormsPlot plot_baseline;
        private ScottPlot.WinForms.FormsPlot plot_event;
        private ScottPlot.WinForms.FormsPlot plot_event_analysis;
        private Label lb_selected_point;
        private Button bt_select_range;
        private Button bt_export_range;
        private Button bt_reset_range;
        private NumericUpDown nud_bin_width;
    }
}
