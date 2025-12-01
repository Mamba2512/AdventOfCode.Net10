namespace AdventOfCodeNet10
{
  partial class FormConvertInstructions
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
      components = new System.ComponentModel.Container();
      splitContainer1 = new SplitContainer();
      rtbInput = new RichTextBox();
      contextMenuStrip2 = new ContextMenuStrip(components);
      pasteToolStripMenuItem = new ToolStripMenuItem();
      rtbOutput = new RichTextBox();
      contextMenuStrip1 = new ContextMenuStrip(components);
      contentToClipboadcopyToolStripMenuItem = new ToolStripMenuItem();
      timer1 = new System.Windows.Forms.Timer(components);
      toolStrip1 = new ToolStrip();
      toolStripLabel1 = new ToolStripLabel();
      toolStripSeparator1 = new ToolStripSeparator();
      toolStripTextBox1 = new ToolStripTextBox();
      toolStripSeparator2 = new ToolStripSeparator();
      tslRecalculate = new ToolStripLabel();
      toolStripSeparator3 = new ToolStripSeparator();
      ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
      splitContainer1.Panel1.SuspendLayout();
      splitContainer1.Panel2.SuspendLayout();
      splitContainer1.SuspendLayout();
      contextMenuStrip2.SuspendLayout();
      contextMenuStrip1.SuspendLayout();
      toolStrip1.SuspendLayout();
      SuspendLayout();
      // 
      // splitContainer1
      // 
      splitContainer1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      splitContainer1.BackColor = SystemColors.ControlDark;
      splitContainer1.Location = new Point(12, 42);
      splitContainer1.Name = "splitContainer1";
      splitContainer1.Orientation = Orientation.Horizontal;
      // 
      // splitContainer1.Panel1
      // 
      splitContainer1.Panel1.Controls.Add(rtbInput);
      // 
      // splitContainer1.Panel2
      // 
      splitContainer1.Panel2.Controls.Add(rtbOutput);
      splitContainer1.Size = new Size(984, 840);
      splitContainer1.SplitterDistance = 377;
      splitContainer1.SplitterWidth = 10;
      splitContainer1.TabIndex = 0;
      // 
      // rtbInput
      // 
      rtbInput.ContextMenuStrip = contextMenuStrip2;
      rtbInput.Dock = DockStyle.Fill;
      rtbInput.Font = new Font("Courier New", 10.125F);
      rtbInput.Location = new Point(0, 0);
      rtbInput.Name = "rtbInput";
      rtbInput.Size = new Size(984, 377);
      rtbInput.TabIndex = 0;
      rtbInput.Text = "";
      rtbInput.TextChanged += rtbInput_TextChanged;
      // 
      // contextMenuStrip2
      // 
      contextMenuStrip2.ImageScalingSize = new Size(32, 32);
      contextMenuStrip2.Items.AddRange(new ToolStripItem[] { pasteToolStripMenuItem });
      contextMenuStrip2.Name = "contextMenuStrip2";
      contextMenuStrip2.Size = new Size(144, 42);
      // 
      // pasteToolStripMenuItem
      // 
      pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
      pasteToolStripMenuItem.Size = new Size(143, 38);
      pasteToolStripMenuItem.Text = "Paste";
      pasteToolStripMenuItem.Click += pasteToolStripMenuItem_Click;
      // 
      // rtbOutput
      // 
      rtbOutput.BackColor = SystemColors.ControlLight;
      rtbOutput.ContextMenuStrip = contextMenuStrip1;
      rtbOutput.Dock = DockStyle.Fill;
      rtbOutput.Font = new Font("Courier New", 10.125F, FontStyle.Regular, GraphicsUnit.Point, 0);
      rtbOutput.Location = new Point(0, 0);
      rtbOutput.Name = "rtbOutput";
      rtbOutput.ReadOnly = true;
      rtbOutput.Size = new Size(984, 453);
      rtbOutput.TabIndex = 0;
      rtbOutput.Text = "";
      // 
      // contextMenuStrip1
      // 
      contextMenuStrip1.ImageScalingSize = new Size(32, 32);
      contextMenuStrip1.Items.AddRange(new ToolStripItem[] { contentToClipboadcopyToolStripMenuItem });
      contextMenuStrip1.Name = "contextMenuStrip1";
      contextMenuStrip1.Size = new Size(378, 42);
      contextMenuStrip1.Opening += contextMenuStrip1_Opening;
      // 
      // contentToClipboadcopyToolStripMenuItem
      // 
      contentToClipboadcopyToolStripMenuItem.Name = "contentToClipboadcopyToolStripMenuItem";
      contentToClipboadcopyToolStripMenuItem.Size = new Size(377, 38);
      contentToClipboadcopyToolStripMenuItem.Text = "Content to Clipboad (copy)";
      contentToClipboadcopyToolStripMenuItem.Click += contentToClipboadToolStripMenuItem_Click;
      // 
      // timer1
      // 
      timer1.Interval = 500;
      timer1.Tick += timer1_Tick;
      // 
      // toolStrip1
      // 
      toolStrip1.ImageScalingSize = new Size(32, 32);
      toolStrip1.Items.AddRange(new ToolStripItem[] { toolStripLabel1, toolStripSeparator1, toolStripTextBox1, toolStripSeparator2, tslRecalculate, toolStripSeparator3 });
      toolStrip1.Location = new Point(0, 0);
      toolStrip1.Name = "toolStrip1";
      toolStrip1.Size = new Size(1015, 39);
      toolStrip1.TabIndex = 1;
      toolStrip1.Text = "toolStrip1";
      // 
      // toolStripLabel1
      // 
      toolStripLabel1.Name = "toolStripLabel1";
      toolStripLabel1.Size = new Size(189, 33);
      toolStripLabel1.Text = "Max LineLength:";
      // 
      // toolStripSeparator1
      // 
      toolStripSeparator1.Name = "toolStripSeparator1";
      toolStripSeparator1.Size = new Size(6, 39);
      // 
      // toolStripTextBox1
      // 
      toolStripTextBox1.Name = "toolStripTextBox1";
      toolStripTextBox1.Size = new Size(100, 39);
      toolStripTextBox1.Text = "80";
      toolStripTextBox1.TextBoxTextAlign = HorizontalAlignment.Right;
      // 
      // toolStripSeparator2
      // 
      toolStripSeparator2.Name = "toolStripSeparator2";
      toolStripSeparator2.Size = new Size(6, 39);
      // 
      // tslRecalculate
      // 
      tslRecalculate.Name = "tslRecalculate";
      tslRecalculate.Size = new Size(151, 33);
      tslRecalculate.Text = "(ReCalculate)";
      tslRecalculate.Click += tslRecalculate_Click;
      // 
      // toolStripSeparator3
      // 
      toolStripSeparator3.Name = "toolStripSeparator3";
      toolStripSeparator3.Size = new Size(6, 39);
      // 
      // FormConvertInstructions
      // 
      AutoScaleDimensions = new SizeF(13F, 32F);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(1015, 894);
      Controls.Add(toolStrip1);
      Controls.Add(splitContainer1);
      Name = "FormConvertInstructions";
      Text = "FormConvertInstructions";
      splitContainer1.Panel1.ResumeLayout(false);
      splitContainer1.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
      splitContainer1.ResumeLayout(false);
      contextMenuStrip2.ResumeLayout(false);
      contextMenuStrip1.ResumeLayout(false);
      toolStrip1.ResumeLayout(false);
      toolStrip1.PerformLayout();
      ResumeLayout(false);
      PerformLayout();
    }

    #endregion

    private SplitContainer splitContainer1;
    private RichTextBox rtbInput;
    private RichTextBox rtbOutput;
    private System.Windows.Forms.Timer timer1;
    private ToolStrip toolStrip1;
    private ToolStripLabel toolStripLabel1;
    private ToolStripSeparator toolStripSeparator1;
    private ToolStripTextBox toolStripTextBox1;
    private ToolStripSeparator toolStripSeparator2;
    private ToolStripLabel tslRecalculate;
    private ToolStripSeparator toolStripSeparator3;
    private ContextMenuStrip contextMenuStrip2;
    private ToolStripMenuItem pasteToolStripMenuItem;
    private ContextMenuStrip contextMenuStrip1;
    private ToolStripMenuItem contentToClipboadcopyToolStripMenuItem;
  }
}