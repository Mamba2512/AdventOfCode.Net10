namespace AdventOfCodeNet10
{
    partial class FormEditInput
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
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            splitContainer1 = new SplitContainer();
            rtbTestExample = new RichTextBox();
            label1 = new Label();
            rtbInput = new RichTextBox();
            label2 = new Label();
            statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1 });
            statusStrip1.Location = new Point(0, 397);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(547, 22);
            statusStrip1.TabIndex = 0;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.BackColor = SystemColors.MenuHighlight;
            toolStripStatusLabel1.ForeColor = SystemColors.HighlightText;
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(94, 17);
            toolStripStatusLabel1.Text = "(Save and Close)";
            toolStripStatusLabel1.Click += tssl_Click;
            // 
            // splitContainer1
            // 
            splitContainer1.BackColor = SystemColors.ControlDark;
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(rtbTestExample);
            splitContainer1.Panel1.Controls.Add(label1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(rtbInput);
            splitContainer1.Panel2.Controls.Add(label2);
            splitContainer1.Size = new Size(547, 397);
            splitContainer1.SplitterDistance = 275;
            splitContainer1.SplitterWidth = 5;
            splitContainer1.TabIndex = 1;
            // 
            // richTextBox1
            // 
            rtbTestExample.Dock = DockStyle.Fill;
            rtbTestExample.Location = new Point(0, 23);
            rtbTestExample.Name = "richTextBox1";
            rtbTestExample.Size = new Size(275, 374);
            rtbTestExample.TabIndex = 1;
            rtbTestExample.Text = "";
            // 
            // label1
            // 
            label1.BackColor = SystemColors.Control;
            label1.Dock = DockStyle.Top;
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(275, 23);
            label1.TabIndex = 0;
            label1.Text = "label1";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // richTextBox2
            // 
            rtbInput.Dock = DockStyle.Fill;
            rtbInput.Location = new Point(0, 23);
            rtbInput.Name = "richTextBox2";
            rtbInput.Size = new Size(267, 374);
            rtbInput.TabIndex = 2;
            rtbInput.Text = "";
            // 
            // label2
            // 
            label2.BackColor = SystemColors.Control;
            label2.Dock = DockStyle.Top;
            label2.Location = new Point(0, 0);
            label2.Name = "label2";
            label2.Size = new Size(267, 23);
            label2.TabIndex = 1;
            label2.Text = "label2";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // FormEditInput
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(547, 419);
            Controls.Add(splitContainer1);
            Controls.Add(statusStrip1);
            Name = "FormEditInput";
            Text = "FormEditInput";
            FormClosing += FormEditInput_FormClosing;
            Shown += FormEditInput_Shown;
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private StatusStrip statusStrip1;
        private SplitContainer splitContainer1;
        private Label label1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private Label label2;
        private RichTextBox rtbTestExample;
        private RichTextBox rtbInput;
    }
}