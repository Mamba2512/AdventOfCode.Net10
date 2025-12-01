using System.Diagnostics;
namespace AdventOfCodeNet10
{
  public partial class MainForm : Form
  {
    #region not interesting

    public MainForm()
    {
      tabPages = new List<TabPage>();
      InitializeComponent();
    }

    private List<TabPage> tabPages;

    private void tsmi_Click(object? sender, EventArgs? e)
    {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
      var tsmi = (ToolStripMenuItem)sender;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
      var chunks = tsmi.Tag!.ToString()!.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

      //  "2015 1"  // Year Day
      //  "2015 25" // Year Day
      int year = Int32.Parse(chunks[0]);
      int day = Int32.Parse(chunks[1]);

      FormEditInput frmInput = new FormEditInput(year, day);
      try
      {
        frmInput.ShowDialog();
      }
      catch { }
    }

    private void MainForm_Load(object sender, EventArgs e)
    {
      // Create all Buttons !!
      this.tabControl1.SuspendLayout();
      this.SuspendLayout();

      this.tabControl1.Controls.Clear();

      int x, y;

      int x_left = 7;
      int x_offset = 160;
      int x_gap = 190;
      int y_bottom = 550;
      int y_offset = 60;

      int tabindex = 0;
      for (int year = 2015; year <= 2025; year++)
      {
        // new TabPage
        TabPage tp = new System.Windows.Forms.TabPage();

        tp.SuspendLayout();

        tp.AutoScroll = true;
        tp.Location = new System.Drawing.Point(4, 22);
        tp.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        tp.Name = "tabPage" + year.ToString();
        tp.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
        tp.Size = new System.Drawing.Size(1200, 1000);
        tp.TabIndex = tabindex++;
        tp.Text = year.ToString();
        tp.UseVisualStyleBackColor = true;

        tabPages.Add(tp);
        x = x_left;
        y = y_bottom;

        for (int day = 1; day <= 25; day++)
        {
          for (int part = 1; part <= 2; part++)
          {
            // Add new Button
            Button btn = new Button();
            ContextMenuStrip cms = new ContextMenuStrip();
            ToolStripMenuItem tsmi = new ToolStripMenuItem();

            // 
            // editInputToolStripMenuItem
            // 
            tsmi.Name = $"ToolStripMenuItem{year}_{day}_{part}";
            tsmi.Size = new Size(125, 22);
            tsmi.Text = $"Edit &Input for {year} Day {day}";
            tsmi.Tag = $"{year} {day}";
            tsmi.Click += tsmi_Click;
            // 
            // cmsEditInput
            // 
            cms.Items.AddRange(new ToolStripItem[] { tsmi });
            cms.Name = $"ContextMenuStrip{year}_{day}_{part}";
            cms.Size = new Size(126, 26);
            cms.TabIndex = tabindex++;

            btn.Location = new Point(x, y);

            btn.Name = $"button{year}_{day}_{part}";
            btn.Size = new System.Drawing.Size(150, 50);
            btn.TabIndex = tabindex++;
            btn.Tag = $"{year} {day} {part}";
            btn.Text = $"Day {day:00} {part}";
            btn.UseVisualStyleBackColor = true;
            btn.Click += new System.EventHandler(this.button_Click!);
            btn.ContextMenuStrip = cms;

            tp.Controls.Add(btn);

            if (day <= 20) // next 2 each other
            {
              if (part == 1)
              {
                // shift right
                x = x + x_offset;
              }
              else // part == 2
              {
                // shift left and up
                x = x - x_offset;
                y = y - y_offset;
              }

              if ((day == 10 || day == 20) && part == 2)
              {
                x = x + x_offset + x_gap;
                y = y_bottom;
              }
            }
            else // on Top of each other
            {
              y = y - y_offset;
            }
          }
        }
        // Add TagPage to TabControl
        this.tabControl1.Controls.Add(tp);
      }

      foreach (var page in tabPages)
      {
        page.ResumeLayout(false);
      }

      this.tabControl1.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    private void MainForm_Shown(object sender, EventArgs e)
    {
      toolStripStatusLabel1.Text = "";
      //tabControl1.SelectTab(0); // 2015
      //tabControl1.SelectTab(1); // 2016
      //tabControl1.SelectTab(2); // 2017
      //tabControl1.SelectTab(3); // 2018
      //tabControl1.SelectTab(4); // 2019
      //tabControl1.SelectTab(5); // 2020
      //tabControl1.SelectTab(6); // 2021
      //tabControl1.SelectTab(7); // 2022
      tabControl1.SelectTab(8); // 2023
      //tabControl1.SelectTab(9); // 2024
      //tabControl1.SelectTab(10); // 2025

      // last tab
      //this.tabControl1.SelectedTab = tabPages[tabPages.Count - 1];
    }

    #endregion not interesting

    private void button_Click(object sender, EventArgs e)
    {
      Button btn = (Button)sender;
      var chunks = btn.Tag!.ToString()!.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

      //  "2015 1 1"  // Year Day Part
      //  "2015 25 2" // Year Day Part
      int day = Int32.Parse(chunks[1]);
      Type type = Type.GetType($"AdventOfCodeNet10._{chunks[0]}.Day_{day:00}.Part_{chunks[2]}_{chunks[0]}_Day_{day:00}")!;
      try
      {
#pragma warning disable CS8604 // Possible null reference argument.
        RunDay(Activator.CreateInstance(type) as Days,
          $@".\{chunks[0]}\Day_{day:00}\Test-Example-Input_{chunks[0]}_Day_{day:00}.txt",
          $@".\{chunks[0]}\Day_{day:00}\Input_{chunks[0]}_Day_{day:00}.txt"
        );
#pragma warning restore CS8604 // Possible null reference argument.
      }
      catch (ArgumentNullException aex)
      {
        MessageBox.Show($"Run FileGeneratorForAocNet10 first !!\n\n{aex.StackTrace}",
          "ArgumentNullException - File Not Found!?", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
      catch (OverflowException oex)
      {
        MessageBox.Show($"{oex.Message}\n\n{oex.StackTrace}",
          "OverflowException", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
      catch (Exception ex)
      {
        MessageBox.Show($"{ex.Message}\n\n{ex.StackTrace}",
          "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void RunDay(Days day, string testExampleInput, string input)
    {
      String resTestExample = "";
      try
      {
        day.Init(testExampleInput);
        resTestExample = day.Execute();
      }
      catch
      {
        resTestExample = "could not execute";
        throw;
      }

      day.Init(input);
      Stopwatch sw = new Stopwatch();
      sw.Start();
      String result = day.Execute();
      sw.Stop();

      if (!String.IsNullOrEmpty(result))
      {
        Clipboard.Clear();
        Clipboard.SetText(result);
      }

      toolStripStatusLabel1.Text =
        $"{DateTime.Now.ToString("yyyy-MM-dd_HH:mm:ss")}\t Solution:\t \"{result}\"\t Calculation Time: '{sw.Elapsed}'";

      MessageBox.Show($"Result\t'{result}'\n\n((Test:{resTestExample}))", sw.Elapsed.ToString());
    }

    private void tsslConvertInstructions_Click(object sender, EventArgs e)
    {
      var fci = new FormConvertInstructions();
      fci.ShowDialog();
    }


  }
}
