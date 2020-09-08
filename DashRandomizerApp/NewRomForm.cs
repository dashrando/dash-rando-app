using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using DashRandomizer;

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

      internal static void ShowGeneratedRom(string RomPath, GameMode RandoGameMode, int Seed)
      {
         NewRomForm NewForm = new NewRomForm();

         NewForm.textBoxROM.Text = RomPath;
         NewForm.textBoxDifficulty.Text = RandoGameMode.Mode;
         NewForm.textBoxType.Text = RandoGameMode.ToString ();

         if (Seed > 0)
            NewForm.textBoxSeed.Text = Seed.ToString ();
         else
            NewForm.textBoxSeed.Text = String.Empty;

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
