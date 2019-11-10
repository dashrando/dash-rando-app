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
      private int numSeeds;
      private bool generateSpoiler;
      private bool testMode;

      public MainForm(int NumSeeds, bool GenerateSpoiler, bool TestMode)
      {
         InitializeComponent();

         // Keep track of our inputs
         numSeeds = NumSeeds;
         generateSpoiler = GenerateSpoiler;
         testMode = TestMode;

         // Populate our Game Mode and Randomization options
         comboBoxRandomization.Items.Add(new RandoDifficulty(Types.Difficulty.Tournament, "Standard", "Major / Minor", "SM"));
         comboBoxRandomization.Items.Add(new RandoDifficulty(Types.Difficulty.Full, "Standard", "Full", "SF"));
         comboBoxRandomization.SelectedIndex = 0;
         comboBoxGameMode.SelectedIndex = 0;

         // Clear the Seed field since we default to automatically generate a seed
         numericUpDownSeed.Controls[1].Text = "";

         // Are we creating more than one seed?
         if (numSeeds > 1)
         {
            radioButtonManual.Enabled = false;
         }

         // Set the focus to one of the radio buttons instead of the ROM text box because it looks better
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
            SpecifiedSeed = Convert.ToInt32(numericUpDownSeed.Value);

         var RandoDifficulty = comboBoxRandomization.SelectedItem as RandoDifficulty;

         if (RandoDifficulty == null)
         {
            MessageBox.Show("Invalid difficulty selection!");
            return;
         }

         // Pick the patches to apply based on the selected Game Mode and Randomization
         var IpsPatchesToApply = Patches.IpsPatches.Where(p => (p.Difficulty == RandoDifficulty.Difficulty ||
            p.Difficulty == Types.Difficulty.Any) && p.Default);
         var RomPatchesToApply = Patches.RomPatches.Where(p => (p.Difficulty == RandoDifficulty.Difficulty ||
            p.Difficulty == Types.Difficulty.Any) && p.Default);

         // Generate the appropriate number of seeds
         for (int i = 0; i < numSeeds; i++)
         {
            // Read the vanilla ROM into memory (needs to be done each time)
            var RomData = File.ReadAllBytes(RomFile);

            // Randomize the ROM
            var Results = Randomizer.Randomize(SpecifiedSeed, RandoDifficulty.Difficulty, generateSpoiler,
               "", RomData, ListModule.OfSeq(IpsPatchesToApply), ListModule.OfSeq(RomPatchesToApply));

            // Get the results
            int TheSeed = Results.Item1;
            string RomPath = Path.GetDirectoryName(RomFile);
            string OutputPath = Path.Combine(RomPath, RandoDifficulty.GetFileName(TheSeed));

            // Do not write the file to the disk in test mode
            if (!testMode)
               File.WriteAllBytes(OutputPath, Results.Item2);

            // Only generating one seed? Show the New ROM form
            if (numSeeds == 1)
               NewRomForm.ShowGeneratedRom(GetShortPath(Path.GetFullPath(OutputPath)), RandoDifficulty, TheSeed);
         }

         // Generating more than one seed? Show the generic completion message
         if (numSeeds > 1)
            MessageBox.Show(String.Format("Generated {0} seeds!", numSeeds), "Created ROMs");
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

      private void radioButtonManual_CheckedChanged(object sender, EventArgs e)
      {
         if (radioButtonManual.Checked)
         {
            numericUpDownSeed.Enabled = true;
            numericUpDownSeed.Controls[1].Text = numericUpDownSeed.Value.ToString();
         }
         else
         {
            numericUpDownSeed.Enabled = false;
            numericUpDownSeed.Controls[1].Text = "";
         }
      }
   }
}
