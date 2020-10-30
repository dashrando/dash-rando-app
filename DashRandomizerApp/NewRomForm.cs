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
      private NewRomForm(string Title, string RomPath, string Difficulty,
         string ModeType, string SeedString)
      {
         InitializeComponent();

         if (!String.IsNullOrEmpty (Title))
            this.Text = Title;

         textBoxROM.Text = RomPath;
         textBoxDifficulty.Text = Difficulty;
         textBoxType.Text = ModeType;
         textBoxSeed.Text = SeedString;

         textBoxROM.Select (0, 0);
         }

      private void btnClose_Click(object sender, EventArgs e)
      {
         this.Close();
      }

      internal static void ShowGeneratedRom(string RomPath, GameMode RandoGameMode, int Seed)
      {
         string SeedString = String.Empty;

         if (Seed > 0)
            SeedString = Seed.ToString ();

         NewRomForm NewForm = new NewRomForm(null, RomPath, RandoGameMode.Mode,
            RandoGameMode.Randomization, SeedString);

         NewForm.ShowDialog();
      }

      internal static void ShowPracRom (string RomPath, GameMode RandoGameMode)
         {
         NewRomForm NewForm = new NewRomForm ("Practice Hack Generated!", RomPath,
            RandoGameMode.Mode, "N/A", "N/A");

         NewForm.ShowDialog ();
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
