using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace DASH
{
   public partial class NewRomForm : Form
   {
      private NewRomForm()
      {
         InitializeComponent();
      }

      private void btnClose_Click(object sender, EventArgs e)
      {
         this.Close();
      }

      internal static void ShowGeneratedRom(string RomPath, RandoDifficulty RomDifficulty, int Seed)
      {
         NewRomForm NewForm = new NewRomForm();

         NewForm.textBoxROM.Text = RomPath;
         NewForm.textBoxDifficulty.Text = RomDifficulty.Mode;
         NewForm.textBoxType.Text = RomDifficulty.Text;
         NewForm.textBoxSeed.Text = Seed.ToString();
         NewForm.textBoxROM.Select(0, 0);

         NewForm.ShowDialog();
      }

      private void btnOpenFolder_Click(object sender, EventArgs e)
      {
         Process.Start("explorer.exe", String.Format("/select, \"{0}\"",
            Path.GetFullPath(textBoxROM.Text)));
      }

      private void btnCopyFile_Click(object sender, EventArgs e)
      {
         var FileList = new StringCollection();
         FileList.Add(Path.GetFullPath(textBoxROM.Text));
         Clipboard.SetFileDropList(FileList);
      }
   }
}
