using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using DashRandomizer;

namespace DASH
{
   public partial class PracRomForm : Form
   {
      string vanillaPath;
      GameMode gameMode;

      string GeneratedRomPath { get; set; }

      private PracRomForm(GameMode RandoGameMode, string VanillaPath)
      {
         InitializeComponent();

         vanillaPath = VanillaPath;
         gameMode = RandoGameMode;

         textBoxDifficulty.Text = RandoGameMode.ToString ();

         labelDetails.Text =
            "- Based on SM Practice Hack v2.1.3 (https://smpractice.speedga.me/)\n" +
            "- Includes Common Randomizer Patches\n" +
            "- Includes " + RandoGameMode.Mode + " DASH Patches";

         comboBoxType.Items.Add ("Save States (SD2SNES/FXPAK)");
         comboBoxType.Items.Add ("No Save States (Emu/VC/Classic)");
         comboBoxType.SelectedIndex = 0;
         }

      private void btnClose_Click(object sender, EventArgs e)
      {
         this.Close();
      }

      internal static void ShowPracHackForm(GameMode RandoGameMode, string VanillaPath)
      {
         PracRomForm PracForm = new PracRomForm(RandoGameMode, VanillaPath);

         if (DialogResult.OK != PracForm.ShowDialog ())
            return;

         NewRomForm.ShowPracRom (PracForm.GeneratedRomPath, RandoGameMode);
      }

      private void btnGenerate_Click (object sender, EventArgs e)
         {
         // Update the ROM based on the game mode
         GeneratedRomPath = gameMode.PatchForPractice (vanillaPath,
            comboBoxType.SelectedIndex == 1);

         //
         this.DialogResult = DialogResult.OK;
         this.Close ();
         }
      }
}
