using System.ComponentModel;
using System.Text;

namespace AdventOfCodeNet10
{
  public partial class FormConvertInstructions : Form
  {
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int LineLength { get; set; }

    public FormConvertInstructions()
    {
      LineLength = 80;
      InitializeComponent();
    }

    private void rtbInput_TextChanged(object sender, EventArgs e)
    {
      timer1.Stop();
      timer1.Start();
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
      timer1.Stop();
      string rtbText = rtbInput.Text;

      var Lines = rtbText.Split('\n'); // include empty Lines
      StringBuilder sb = new StringBuilder();
      foreach (var line in Lines)
      {
        var input = line.Trim('\r', ' ');

        while (input.Length > LineLength)
        {
          int spacepos = input.Substring(0, LineLength).LastIndexOf(' ');
          sb.Append("    ");
          sb.AppendLine(input.Substring(0, spacepos).Trim());
          input = input.Substring(spacepos + 1);
        }
        sb.Append("    ");
        sb.AppendLine(input);
      }
      rtbOutput.Text = sb.ToString();
    }

    private void tslRecalculate_Click(object sender, EventArgs e)
    {
      if (Int32.TryParse(toolStripTextBox1.Text, out var num))
      {
        tslRecalculate.Text = "(ReCalculate)";
        LineLength = num;
        timer1_Tick(sender, e);
      }
      else
      {
        tslRecalculate.Text = "Input not a Number !! fix and click here -> (+)";
      }
    }

    private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (Clipboard.ContainsText())
      {
        rtbInput.Text = Clipboard.GetText();
      }
    }

    private void contentToClipboadToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Clipboard.Clear();
      Clipboard.SetText(rtbOutput.Text);
    }

    private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
    {
      bool bHasText = !String.IsNullOrEmpty(rtbOutput.Text); ;
      contentToClipboadcopyToolStripMenuItem.Enabled = bHasText;
    }
  }
}
