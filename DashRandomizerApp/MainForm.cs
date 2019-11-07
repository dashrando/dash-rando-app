using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ItemRandomizer;
using Microsoft.FSharp.Core;
using Microsoft.FSharp.Collections;

namespace SuperMetroidRandoApp
{
   public partial class MainForm : Form
   {
      public MainForm()
      {
         InitializeComponent();

         //comboBoxDifficulty.Items.Add(new RandoDifficulty(Types.Difficulty.Casual, "Casual", "CX"));
         //comboBoxDifficulty.Items.Add(new RandoDifficulty(Types.Difficulty.Normal, "Normal", "X"));
         //comboBoxDifficulty.Items.Add(new RandoDifficulty(Types.Difficulty.Hard, "Hard", "HX"));
         comboBoxDifficulty.Items.Add(new RandoDifficulty(Types.Difficulty.Tournament, "Standard", "Major / Minor", "SM"));
         //comboBoxDifficulty.Items.Add(new RandoDifficulty(Types.Difficulty.Open, "Open Mode", "OX"));
         comboBoxDifficulty.Items.Add(new RandoDifficulty(Types.Difficulty.Full, "Standard", "Full", "SF"));
         comboBoxDifficulty.SelectedIndex = 0;

         comboBoxMode.SelectedIndex = 0;
         numericUpDown1.Controls[1].Text = "";
         //textBoxBrowse.Text = "SuperMetroid.sfc";
         //textBoxBrowse.Select(0, 0);

         radioButtonRandom.Focus();
      }

      private void btnRandomize_Click(object sender, EventArgs e)
      {
         var RomFile = textBoxBrowse.Text;

         if (!File.Exists(RomFile))
         {
            MessageBox.Show("The specified Vanilla ROM does not exist!");
            return;
         }

         int SpecifiedSeed = 0;

         if (radioButtonManual.Checked)
            SpecifiedSeed = Convert.ToInt32(numericUpDown1.Value);

         var RomData = File.ReadAllBytes(RomFile);
         var RandoDifficulty = comboBoxDifficulty.SelectedItem as RandoDifficulty;

         if (RandoDifficulty == null)
         {
            MessageBox.Show("Invalid difficulty selection!");
            return;
         }

         var IpsPatchesToApply = Patches.IpsPatches.Where(p => (p.Difficulty == RandoDifficulty.Difficulty ||
            p.Difficulty == Types.Difficulty.Any) && p.Default);

         var RomPatchesToApply = Patches.RomPatches.Where(p => (p.Difficulty == RandoDifficulty.Difficulty ||
            p.Difficulty == Types.Difficulty.Any) && p.Default);

         //for (int i = 0; i < 100; i++)
         //{
            var Results = Randomizer.Randomize(SpecifiedSeed, RandoDifficulty.Difficulty, true, "", RomData,
               ListModule.OfSeq(IpsPatchesToApply), ListModule.OfSeq(RomPatchesToApply));

            int TheSeed = Results.Item1;
            string RomPath = Path.GetDirectoryName(RomFile);
            string OutputPath = Path.Combine(RomPath, RandoDifficulty.GetFileName(TheSeed));

            File.WriteAllBytes(OutputPath, Results.Item2);
         //}

         NewRomForm.ShowGeneratedRom(GetShortPath(Path.GetFullPath(OutputPath)), RandoDifficulty, TheSeed);
      }

      private void btnBrowse_Click(object sender, EventArgs e)
      {
         OpenFileDialog BrowseForVanilla = new OpenFileDialog();

         BrowseForVanilla.Title = "Select Vanilla Super Metroid ROM";
         BrowseForVanilla.Filter = "ROMs (*.SFC;*.SMC)|*.SFC;*.SMC|All Files (*.*)|*.*";

         if (DialogResult.OK == BrowseForVanilla.ShowDialog())
         {
            textBoxBrowse.Text = GetShortPath(BrowseForVanilla.FileName);
         }
      }

      public static string GetShortPath(string fullPath)
      {
         string RelativePath = GetRelativePath(fullPath, Directory.GetCurrentDirectory());

         if (RelativePath.Length < fullPath.Length)
            return RelativePath;

         return fullPath;
      }

      public static string GetRelativePath(string fullPath, string basePath)
      {
         // Require trailing backslash for path
         if (!basePath.EndsWith("\\"))
            basePath += "\\";

         Uri baseUri = new Uri(basePath);
         Uri fullUri = new Uri(fullPath);

         Uri relativeUri = baseUri.MakeRelativeUri(fullUri);

         // Uri's use forward slashes so convert back to backward slashes
         return relativeUri.ToString().Replace("/", "\\");
      }
   }
}
