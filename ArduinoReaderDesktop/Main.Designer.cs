namespace ArduinoReaderDesktop
{
    partial class Main
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
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            newGraphToolStripMenuItem = new ToolStripMenuItem();
            openGraphToolStripMenuItem = new ToolStripMenuItem();
            saveGraphToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            exitToolStripMenuItem = new ToolStripMenuItem();
            startMeasuringToolStripMenuItem = new ToolStripMenuItem();
            stopMeasuringToolStripMenuItem = new ToolStripMenuItem();
            testToolStripMenuItem = new ToolStripMenuItem();
            testAsyncToolStripMenuItem = new ToolStripMenuItem();
            pictureBox1 = new PictureBox();
            groupBox1 = new GroupBox();
            checkedListBox1 = new CheckedListBox();
            numericUpDown1 = new NumericUpDown();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, startMeasuringToolStripMenuItem, stopMeasuringToolStripMenuItem, testToolStripMenuItem, testAsyncToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(907, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newGraphToolStripMenuItem, openGraphToolStripMenuItem, saveGraphToolStripMenuItem, toolStripSeparator1, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // newGraphToolStripMenuItem
            // 
            newGraphToolStripMenuItem.Name = "newGraphToolStripMenuItem";
            newGraphToolStripMenuItem.Size = new Size(138, 22);
            newGraphToolStripMenuItem.Text = "New Graph";
            newGraphToolStripMenuItem.Click += newGraphToolStripMenuItem_Click;
            // 
            // openGraphToolStripMenuItem
            // 
            openGraphToolStripMenuItem.Name = "openGraphToolStripMenuItem";
            openGraphToolStripMenuItem.Size = new Size(138, 22);
            openGraphToolStripMenuItem.Text = "Open Graph";
            openGraphToolStripMenuItem.Click += openGraphToolStripMenuItem_Click;
            // 
            // saveGraphToolStripMenuItem
            // 
            saveGraphToolStripMenuItem.Name = "saveGraphToolStripMenuItem";
            saveGraphToolStripMenuItem.Size = new Size(138, 22);
            saveGraphToolStripMenuItem.Text = "Save Graph";
            saveGraphToolStripMenuItem.Click += saveMeasuringToFileToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(135, 6);
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(138, 22);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // startMeasuringToolStripMenuItem
            // 
            startMeasuringToolStripMenuItem.Name = "startMeasuringToolStripMenuItem";
            startMeasuringToolStripMenuItem.Size = new Size(102, 20);
            startMeasuringToolStripMenuItem.Text = "Start Measuring";
            startMeasuringToolStripMenuItem.Click += startMeasuringToolStripMenuItem_Click;
            // 
            // stopMeasuringToolStripMenuItem
            // 
            stopMeasuringToolStripMenuItem.Name = "stopMeasuringToolStripMenuItem";
            stopMeasuringToolStripMenuItem.Size = new Size(102, 20);
            stopMeasuringToolStripMenuItem.Text = "Stop Measuring";
            stopMeasuringToolStripMenuItem.Click += stopMeasuringToolStripMenuItem_Click;
            // 
            // testToolStripMenuItem
            // 
            testToolStripMenuItem.Name = "testToolStripMenuItem";
            testToolStripMenuItem.Size = new Size(39, 20);
            testToolStripMenuItem.Text = "Test";
            testToolStripMenuItem.Click += testToolStripMenuItem_Click;
            // 
            // testAsyncToolStripMenuItem
            // 
            testAsyncToolStripMenuItem.Name = "testAsyncToolStripMenuItem";
            testAsyncToolStripMenuItem.Size = new Size(71, 20);
            testAsyncToolStripMenuItem.Text = "TestAsync";
            testAsyncToolStripMenuItem.Click += testAsyncToolStripMenuItem_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pictureBox1.BackColor = SystemColors.ActiveCaptionText;
            pictureBox1.Location = new Point(12, 36);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(681, 454);
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            pictureBox1.Paint += pictureBox1_Paint;
            pictureBox1.MouseDown += pictureBox1_MouseDown;
            pictureBox1.MouseEnter += pictureBox1_MouseEnter;
            pictureBox1.MouseMove += pictureBox1_MouseMove;
            pictureBox1.MouseUp += pictureBox1_MouseUp;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            groupBox1.Controls.Add(checkedListBox1);
            groupBox1.Location = new Point(699, 36);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(196, 222);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Readers";
            // 
            // checkedListBox1
            // 
            checkedListBox1.Dock = DockStyle.Fill;
            checkedListBox1.FormattingEnabled = true;
            checkedListBox1.Location = new Point(3, 19);
            checkedListBox1.Name = "checkedListBox1";
            checkedListBox1.Size = new Size(190, 200);
            checkedListBox1.TabIndex = 0;
            checkedListBox1.ItemCheck += checkedListBox1_ItemCheck;
            // 
            // numericUpDown1
            // 
            numericUpDown1.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            numericUpDown1.Location = new Point(702, 264);
            numericUpDown1.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(190, 23);
            numericUpDown1.TabIndex = 3;
            numericUpDown1.ValueChanged += numericUpDown1_ValueChanged;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(907, 502);
            Controls.Add(numericUpDown1);
            Controls.Add(groupBox1);
            Controls.Add(pictureBox1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Main";
            Text = "PulseAnalyzer";
            Resize += Main_Resize;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem newGraphToolStripMenuItem;
        private ToolStripMenuItem openGraphToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem exitToolStripMenuItem;
        private PictureBox pictureBox1;
        private ToolStripMenuItem saveGraphToolStripMenuItem;
        private ToolStripMenuItem startMeasuringToolStripMenuItem;
        private ToolStripMenuItem stopMeasuringToolStripMenuItem;
        private ToolStripMenuItem testToolStripMenuItem;
        private ToolStripMenuItem testAsyncToolStripMenuItem;
        private GroupBox groupBox1;
        private CheckedListBox checkedListBox1;
        private NumericUpDown numericUpDown1;
    }
}
