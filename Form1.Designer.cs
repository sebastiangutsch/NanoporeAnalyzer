namespace Nanopore_Analyzer
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            menuStrip1 = new MenuStrip();
            toolStripMenuItem1 = new ToolStripMenuItem();
            menu_load = new ToolStripMenuItem();
            closeToolStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            saveAsAbfv1ToolStripMenuItem = new ToolStripMenuItem();
            rtb_info = new RichTextBox();
            plot1 = new OxyPlot.WindowsForms.PlotView();
            cb_channelnumber = new ComboBox();
            pb_arrowright = new PictureBox();
            pb_arrowleft = new PictureBox();
            lb_sweeps = new Label();
            plot2 = new OxyPlot.WindowsForms.PlotView();
            pb_delete_sweep = new PictureBox();
            label1 = new Label();
            gb_filtering = new GroupBox();
            nUD_cutoff = new NumericUpDown();
            label2 = new Label();
            nUD_filterorder = new NumericUpDown();
            bt_filter = new Button();
            gb_bgsubstract = new GroupBox();
            gb_eventdetection = new GroupBox();
            gb_eventanalysis = new GroupBox();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pb_arrowright).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pb_arrowleft).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pb_delete_sweep).BeginInit();
            gb_filtering.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nUD_cutoff).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nUD_filterorder).BeginInit();
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
            rtb_info.Location = new Point(12, 27);
            rtb_info.Name = "rtb_info";
            rtb_info.ReadOnly = true;
            rtb_info.Size = new Size(208, 47);
            rtb_info.TabIndex = 1;
            rtb_info.Text = "";
            // 
            // plot1
            // 
            plot1.BackColor = SystemColors.Window;
            plot1.Location = new Point(12, 80);
            plot1.Name = "plot1";
            plot1.PanCursor = Cursors.Hand;
            plot1.Size = new Size(1760, 151);
            plot1.TabIndex = 2;
            plot1.Text = "Data Plot";
            plot1.ZoomHorizontalCursor = Cursors.SizeWE;
            plot1.ZoomRectangleCursor = Cursors.SizeNWSE;
            plot1.ZoomVerticalCursor = Cursors.SizeNS;
            // 
            // cb_channelnumber
            // 
            cb_channelnumber.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_channelnumber.FormattingEnabled = true;
            cb_channelnumber.Location = new Point(245, 27);
            cb_channelnumber.Name = "cb_channelnumber";
            cb_channelnumber.Size = new Size(121, 23);
            cb_channelnumber.TabIndex = 3;
            // 
            // pb_arrowright
            // 
            pb_arrowright.Image = (Image)resources.GetObject("pb_arrowright.Image");
            pb_arrowright.InitialImage = (Image)resources.GetObject("pb_arrowright.InitialImage");
            pb_arrowright.Location = new Point(466, 27);
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
            pb_arrowleft.Location = new Point(390, 27);
            pb_arrowleft.Name = "pb_arrowleft";
            pb_arrowleft.Size = new Size(32, 32);
            pb_arrowleft.SizeMode = PictureBoxSizeMode.Zoom;
            pb_arrowleft.TabIndex = 7;
            pb_arrowleft.TabStop = false;
            pb_arrowleft.Click += pb_arrowleft_Click;
            // 
            // lb_sweeps
            // 
            lb_sweeps.AutoSize = true;
            lb_sweeps.Location = new Point(390, 62);
            lb_sweeps.Name = "lb_sweeps";
            lb_sweeps.Size = new Size(76, 15);
            lb_sweeps.TabIndex = 8;
            lb_sweeps.Text = "no of sweeps";
            // 
            // plot2
            // 
            plot2.BackColor = SystemColors.Window;
            plot2.Location = new Point(12, 237);
            plot2.Name = "plot2";
            plot2.PanCursor = Cursors.Hand;
            plot2.Size = new Size(1760, 151);
            plot2.TabIndex = 9;
            plot2.Text = "Data Plot";
            plot2.ZoomHorizontalCursor = Cursors.SizeWE;
            plot2.ZoomRectangleCursor = Cursors.SizeNWSE;
            plot2.ZoomVerticalCursor = Cursors.SizeNS;
            // 
            // pb_delete_sweep
            // 
            pb_delete_sweep.Image = (Image)resources.GetObject("pb_delete_sweep.Image");
            pb_delete_sweep.InitialImage = (Image)resources.GetObject("pb_delete_sweep.InitialImage");
            pb_delete_sweep.Location = new Point(428, 27);
            pb_delete_sweep.Name = "pb_delete_sweep";
            pb_delete_sweep.Size = new Size(32, 32);
            pb_delete_sweep.SizeMode = PictureBoxSizeMode.Zoom;
            pb_delete_sweep.TabIndex = 10;
            pb_delete_sweep.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 26);
            label1.Name = "label1";
            label1.Size = new Size(66, 15);
            label1.TabIndex = 11;
            label1.Text = "Cutoff (Hz)";
            // 
            // gb_filtering
            // 
            gb_filtering.Controls.Add(bt_filter);
            gb_filtering.Controls.Add(nUD_filterorder);
            gb_filtering.Controls.Add(label2);
            gb_filtering.Controls.Add(nUD_cutoff);
            gb_filtering.Controls.Add(label1);
            gb_filtering.Location = new Point(12, 394);
            gb_filtering.Name = "gb_filtering";
            gb_filtering.Size = new Size(237, 153);
            gb_filtering.TabIndex = 12;
            gb_filtering.TabStop = false;
            gb_filtering.Text = "Digital Filtering";
            // 
            // nUD_cutoff
            // 
            nUD_cutoff.Increment = new decimal(new int[] { 1000, 0, 0, 0 });
            nUD_cutoff.InterceptArrowKeys = false;
            nUD_cutoff.Location = new Point(83, 24);
            nUD_cutoff.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            nUD_cutoff.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nUD_cutoff.Name = "nUD_cutoff";
            nUD_cutoff.Size = new Size(120, 23);
            nUD_cutoff.TabIndex = 12;
            nUD_cutoff.Value = new decimal(new int[] { 500000, 0, 0, 0 });
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(7, 57);
            label2.Name = "label2";
            label2.Size = new Size(66, 15);
            label2.TabIndex = 13;
            label2.Text = "Filter Order";
            // 
            // nUD_filterorder
            // 
            nUD_filterorder.Location = new Point(83, 55);
            nUD_filterorder.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nUD_filterorder.Name = "nUD_filterorder";
            nUD_filterorder.Size = new Size(120, 23);
            nUD_filterorder.TabIndex = 14;
            nUD_filterorder.Value = new decimal(new int[] { 5, 0, 0, 0 });
            // 
            // bt_filter
            // 
            bt_filter.Location = new Point(69, 95);
            bt_filter.Name = "bt_filter";
            bt_filter.Size = new Size(75, 23);
            bt_filter.TabIndex = 15;
            bt_filter.Text = "Filter now";
            bt_filter.UseVisualStyleBackColor = true;
            // 
            // gb_bgsubstract
            // 
            gb_bgsubstract.Location = new Point(255, 395);
            gb_bgsubstract.Name = "gb_bgsubstract";
            gb_bgsubstract.Size = new Size(237, 152);
            gb_bgsubstract.TabIndex = 13;
            gb_bgsubstract.TabStop = false;
            gb_bgsubstract.Text = "Background Substraction";
            // 
            // gb_eventdetection
            // 
            gb_eventdetection.Location = new Point(498, 395);
            gb_eventdetection.Name = "gb_eventdetection";
            gb_eventdetection.Size = new Size(237, 152);
            gb_eventdetection.TabIndex = 14;
            gb_eventdetection.TabStop = false;
            gb_eventdetection.Text = "Event Detection";
            // 
            // gb_eventanalysis
            // 
            gb_eventanalysis.Location = new Point(741, 395);
            gb_eventanalysis.Name = "gb_eventanalysis";
            gb_eventanalysis.Size = new Size(237, 152);
            gb_eventanalysis.TabIndex = 15;
            gb_eventanalysis.TabStop = false;
            gb_eventanalysis.Text = "Event Analysis";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1784, 961);
            Controls.Add(gb_eventanalysis);
            Controls.Add(gb_eventdetection);
            Controls.Add(gb_bgsubstract);
            Controls.Add(gb_filtering);
            Controls.Add(pb_delete_sweep);
            Controls.Add(plot2);
            Controls.Add(lb_sweeps);
            Controls.Add(pb_arrowleft);
            Controls.Add(pb_arrowright);
            Controls.Add(cb_channelnumber);
            Controls.Add(plot1);
            Controls.Add(rtb_info);
            Controls.Add(menuStrip1);
            KeyPreview = true;
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Nanolyzer";
            KeyDown += Form1_KeyDown;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pb_arrowright).EndInit();
            ((System.ComponentModel.ISupportInitialize)pb_arrowleft).EndInit();
            ((System.ComponentModel.ISupportInitialize)pb_delete_sweep).EndInit();
            gb_filtering.ResumeLayout(false);
            gb_filtering.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nUD_cutoff).EndInit();
            ((System.ComponentModel.ISupportInitialize)nUD_filterorder).EndInit();
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
        private OxyPlot.WindowsForms.PlotView plot1;
        private ToolStripMenuItem saveAsAbfv1ToolStripMenuItem;
        private ComboBox cb_channelnumber;
        private PictureBox pb_arrowright;
        private PictureBox pb_arrowleft;
        private Label lb_sweeps;
        private OxyPlot.WindowsForms.PlotView plot2;
        private PictureBox pb_delete_sweep;
        private Label label1;
        private GroupBox gb_filtering;
        private NumericUpDown nUD_filterorder;
        private Label label2;
        private NumericUpDown nUD_cutoff;
        private Button bt_filter;
        private GroupBox gb_bgsubstract;
        private GroupBox gb_eventdetection;
        private GroupBox gb_eventanalysis;
    }
}
