using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AdventOfCodeNet10
{
  public partial class FormEditInput : Form
  {
    private readonly string testExampleInputFile;
    private readonly string inputFile;

    private string testExampleInput = "";
    private string input = "";

    public FormEditInput(int year, int day)
    {
      InitializeComponent();

      testExampleInputFile = $@".\{year}\Day_{day:00}\Test-Example-Input_{year}_Day_{day:00}.txt";
      inputFile = $@".\{year}\Day_{day:00}\Input_{year}_Day_{day:00}.txt";

      FileInfo fi = new FileInfo(testExampleInputFile);
      if (!fi.Exists)
      {
        MessageBox.Show($"Run FileGeneratorForAocNet10 first !!",
          "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
#pragma warning disable CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
        this.FormClosing -= FormEditInput_FormClosing;
#pragma warning restore CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
        this.Close();
      }
    }

    private void FormEditInput_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (testExampleInput != rtbTestExample.Text ||
          input != rtbInput.Text)
      {
        var result = MessageBox.Show("You have unsaved changes. Do you want to save before closing?",
          "Unsaved Changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
        switch (result)
        {
          case DialogResult.Yes:
            // Save changes
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            tssl_Click(null, null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
            break;
          case DialogResult.No:
            // Close without saving
            break;
          case DialogResult.Cancel:
            e.Cancel = true;
            break;
        }
      }
    }

    private void FormEditInput_Shown(object sender, EventArgs e)
    {
      StringBuilder sb = new StringBuilder();

      label1.Text = testExampleInputFile;
      using (FileStream fs = new FileStream(testExampleInputFile, FileMode.Open))
      {
        while (fs.Position < fs.Length)
        {
          sb.Append((char)fs.ReadByte());
        }
      }
      rtbTestExample.Text = sb.ToString();
      testExampleInput = sb.ToString();

      label2.Text = inputFile;
      sb.Clear();
      using (FileStream fs = new FileStream(inputFile, FileMode.Open))
      {
        while (fs.Position < fs.Length)
        {
          sb.Append((char)fs.ReadByte());
        }
      }
      rtbInput.Text = sb.ToString();
      input = sb.ToString();
      sb.Clear();
    }

    private void tssl_Click(object sender, EventArgs e)
    {
      // Save and Close
      using (FileStream fs = new FileStream(testExampleInputFile, FileMode.Create))
      {
        byte[] bytes = Encoding.UTF8.GetBytes(rtbTestExample.Text);
        fs.Write(bytes, 0, bytes.Length);
      }
      using (FileStream fs = new FileStream(inputFile, FileMode.Create))
      {
        byte[] bytes = Encoding.UTF8.GetBytes(rtbInput.Text);
        fs.Write(bytes, 0, bytes.Length);
      }
      if (sender != null)
      {
        testExampleInput = rtbTestExample.Text;
        input = rtbInput.Text;
        this.Close();
      }
    }
  }
}
