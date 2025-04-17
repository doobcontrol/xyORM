namespace TestBench
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
            panel1 = new Panel();
            dataGridView1 = new DataGridView();
            panel2 = new Panel();
            btnDeleteC = new Button();
            btnAddC = new Button();
            panel3 = new Panel();
            dataGridView2 = new DataGridView();
            panel4 = new Panel();
            btnDeleteE = new Button();
            btnAddE = new Button();
            splitter1 = new Splitter();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            panel4.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(dataGridView1);
            panel1.Controls.Add(panel2);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 159);
            panel1.TabIndex = 0;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(133, 0);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(667, 159);
            dataGridView1.TabIndex = 1;
            dataGridView1.CellValueChanged += dataGridView1_CellValueChanged;
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            // 
            // panel2
            // 
            panel2.Controls.Add(btnDeleteC);
            panel2.Controls.Add(btnAddC);
            panel2.Dock = DockStyle.Left;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(133, 159);
            panel2.TabIndex = 0;
            // 
            // btnDeleteC
            // 
            btnDeleteC.Location = new Point(12, 47);
            btnDeleteC.Name = "btnDeleteC";
            btnDeleteC.Size = new Size(94, 29);
            btnDeleteC.TabIndex = 5;
            btnDeleteC.Text = "Delete";
            btnDeleteC.UseVisualStyleBackColor = true;
            btnDeleteC.Click += btnDeleteC_Click;
            // 
            // btnAddC
            // 
            btnAddC.Location = new Point(12, 12);
            btnAddC.Name = "btnAddC";
            btnAddC.Size = new Size(94, 29);
            btnAddC.TabIndex = 4;
            btnAddC.Text = "Add";
            btnAddC.UseVisualStyleBackColor = true;
            btnAddC.Click += btnAddC_Click;
            // 
            // panel3
            // 
            panel3.Controls.Add(dataGridView2);
            panel3.Controls.Add(panel4);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(0, 159);
            panel3.Name = "panel3";
            panel3.Size = new Size(800, 291);
            panel3.TabIndex = 1;
            // 
            // dataGridView2
            // 
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Dock = DockStyle.Fill;
            dataGridView2.Location = new Point(133, 0);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.RowHeadersWidth = 51;
            dataGridView2.Size = new Size(667, 291);
            dataGridView2.TabIndex = 1;
            dataGridView2.CellValueChanged += dataGridView2_CellValueChanged;
            // 
            // panel4
            // 
            panel4.Controls.Add(btnDeleteE);
            panel4.Controls.Add(btnAddE);
            panel4.Dock = DockStyle.Left;
            panel4.Location = new Point(0, 0);
            panel4.Name = "panel4";
            panel4.Size = new Size(133, 291);
            panel4.TabIndex = 0;
            // 
            // btnDeleteE
            // 
            btnDeleteE.Location = new Point(12, 45);
            btnDeleteE.Name = "btnDeleteE";
            btnDeleteE.Size = new Size(94, 29);
            btnDeleteE.TabIndex = 5;
            btnDeleteE.Text = "Delete";
            btnDeleteE.UseVisualStyleBackColor = true;
            btnDeleteE.Click += btnDeleteE_Click;
            // 
            // btnAddE
            // 
            btnAddE.Location = new Point(12, 10);
            btnAddE.Name = "btnAddE";
            btnAddE.Size = new Size(94, 29);
            btnAddE.TabIndex = 4;
            btnAddE.Text = "Add";
            btnAddE.UseVisualStyleBackColor = true;
            btnAddE.Click += btnAddE_Click;
            // 
            // splitter1
            // 
            splitter1.BackColor = SystemColors.ControlDarkDark;
            splitter1.Dock = DockStyle.Top;
            splitter1.Location = new Point(0, 159);
            splitter1.Name = "splitter1";
            splitter1.Size = new Size(800, 4);
            splitter1.TabIndex = 2;
            splitter1.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(splitter1);
            Controls.Add(panel3);
            Controls.Add(panel1);
            Name = "Form1";
            Text = "Form1";
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel2.ResumeLayout(false);
            panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            panel4.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private DataGridView dataGridView1;
        private Panel panel2;
        private Panel panel3;
        private DataGridView dataGridView2;
        private Panel panel4;
        private Splitter splitter1;
        private Button btnDeleteC;
        private Button btnAddC;
        private Button btnDeleteE;
        private Button btnAddE;
    }
}
