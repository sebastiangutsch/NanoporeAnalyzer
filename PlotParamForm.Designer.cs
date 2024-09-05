namespace Nanopore_Analyzer
{
    partial class PlotParamForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            bt_ok = new Button();
            bt_cancel = new Button();
            gb_filter = new GroupBox();
            clb_boolfilter = new CheckedListBox();
            groupBox1 = new GroupBox();
            gb_filter.SuspendLayout();
            SuspendLayout();
            // 
            // bt_ok
            // 
            bt_ok.Location = new Point(713, 415);
            bt_ok.Name = "bt_ok";
            bt_ok.Size = new Size(75, 23);
            bt_ok.TabIndex = 0;
            bt_ok.Text = "Ok";
            bt_ok.UseVisualStyleBackColor = true;
            bt_ok.Click += bt_ok_Click;
            // 
            // bt_cancel
            // 
            bt_cancel.Location = new Point(617, 415);
            bt_cancel.Name = "bt_cancel";
            bt_cancel.Size = new Size(75, 23);
            bt_cancel.TabIndex = 1;
            bt_cancel.Text = "Cancel";
            bt_cancel.UseVisualStyleBackColor = true;
            bt_cancel.Click += bt_cancel_Click;
            // 
            // gb_filter
            // 
            gb_filter.Controls.Add(clb_boolfilter);
            gb_filter.Location = new Point(12, 12);
            gb_filter.Name = "gb_filter";
            gb_filter.Size = new Size(273, 384);
            gb_filter.TabIndex = 2;
            gb_filter.TabStop = false;
            gb_filter.Text = "Current Level Filter Options";
            // 
            // clb_boolfilter
            // 
            clb_boolfilter.FormattingEnabled = true;
            clb_boolfilter.Items.AddRange(new object[] { "Exclude Baseline", "Only Valid", "Only Invalid" });
            clb_boolfilter.Location = new Point(21, 39);
            clb_boolfilter.Name = "clb_boolfilter";
            clb_boolfilter.Size = new Size(162, 130);
            clb_boolfilter.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.Location = new Point(302, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(259, 384);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "gb_event_filter_options";
            // 
            // PlotParamForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(groupBox1);
            Controls.Add(gb_filter);
            Controls.Add(bt_cancel);
            Controls.Add(bt_ok);
            Name = "PlotParamForm";
            Text = "PlotParamForm";
            gb_filter.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button bt_ok;
        private Button bt_cancel;
        private GroupBox gb_filter;
        private CheckedListBox clb_boolfilter;
        private GroupBox groupBox1;
    }
}