using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ItemRandomizer;
using Microsoft.FSharp.Collections;
using System.Security.Cryptography;

namespace DASH
{
   public partial class MainForm : Form
   {
      private int numSeeds;
      private bool generateSpoiler;
      private bool testMode;
      private string romPath = string.Empty;
      private bool romVerified = false;

      public MainForm(int NumSeeds, bool GenerateSpoiler, bool TestMode)
      {
         InitializeComponent();

         // Keep track of our inputs
         numSeeds = NumSeeds;
         generateSpoiler = GenerateSpoiler;
         testMode = TestMode;

         // Populate our Game Mode and Randomization options
         comboBoxRandomization.Items.Add(new RandoDifficulty(Types.Difficulty.Tournament, "SG Live 2020", "Major / Minor", "SGL20"));
         //comboBoxRandomization.Items.Add (new RandoDifficulty (Types.Difficulty.Tournament, "Standard", "Major / Minor", "v9_SM"));
         //comboBoxRandomization.Items.Add(new RandoDifficulty(Types.Difficulty.Full, "Standard", "Full", "v9_SF"));
         //comboBoxRandomization.Items.Add(new RandoDifficulty(Types.Difficulty.Any, "Standard", "Vanilla", "v9_VN"));
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
         var RomFile = romPath;

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
         var IpsPatchesToApply = ListModule.OfSeq (Patches.IpsPatches.Where(p =>
            (p.Difficulty == RandoDifficulty.Difficulty || p.Difficulty == Types.Difficulty.Any) && p.Default));
         var RomPatchesToApply = ListModule.OfSeq (Patches.RomPatches.Where(p =>
            (p.Difficulty == RandoDifficulty.Difficulty || p.Difficulty == Types.Difficulty.Any) && p.Default));

         // Generate the appropriate number of seeds
         for (int i = 0; i < numSeeds; i++)
         {
            // Read the vanilla ROM into memory (needs to be done each time)
            var RomData = File.ReadAllBytes(RomFile);

            //
            byte[] NewRomData = null;
            int TheSeed = 0;

            if (RandoDifficulty.Text != "Vanilla")
               {
               // Randomize the ROM
               var Results = Randomizer.Randomize (SpecifiedSeed, RandoDifficulty.Difficulty, generateSpoiler,
                  "", RomData, IpsPatchesToApply, RomPatchesToApply);

               // Get the results
               TheSeed = Results.Item1;
               NewRomData = Results.Item2;
               }
            else
               NewRomData = Patches.ApplyPatches (IpsPatchesToApply, RomPatchesToApply, RomData);

            string RomPath = Path.GetDirectoryName(RomFile);
            string OutputPath = Path.Combine(RomPath, RandoDifficulty.GetFileName(TheSeed));

            // Do not write the file to the disk in test mode
            if (!testMode)
               File.WriteAllBytes(OutputPath, NewRomData);

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
            romPath = Path.GetFileName(BrowseForVanilla.FileName);

            File.Copy(BrowseForVanilla.FileName,
               Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), romPath), true);

            romVerified = VerifyRomChecksum(romPath);

            UpdateRomStatusUI();
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

      private void UpdateRomStatus()
      {
         string ExecDir = Path.GetDirectoryName(Application.ExecutablePath);

         foreach (var RomFile in Directory.GetFiles(ExecDir).Where(p =>
            p.ToLower().EndsWith(".sfc") || p.ToLower().EndsWith(".smc")))
         {
            if (!VerifyRomChecksum(RomFile))
               continue;

            romPath = RomFile;
            romVerified = true;
            break;
         }

         UpdateRomStatusUI();
      }

      private void UpdateRomStatusUI()
      {
         if (String.IsNullOrEmpty(romPath))
         {
            labelRomStatus.BackColor = Color.Red;
            labelRomStatus.ForeColor = Color.White;
            labelRomStatus.Text = "Missing";
            btnRandomize.Enabled = false;
            toolTip1.SetToolTip(labelRomStatus,
               "Super Metroid unheadered ROM missing. Press Browse to find the ROM.");
            return;
         }

         btnRandomize.Enabled = true;

         if (romVerified)
         {
            labelRomStatus.BackColor = Color.Green;
            labelRomStatus.ForeColor = Color.White;
            labelRomStatus.Text = "Verified";
            toolTip1.SetToolTip(labelRomStatus,
               "Super Metroid unheadered ROM found. Ready to Randomize!");
            return;
         }

         labelRomStatus.BackColor = Color.Yellow;
         labelRomStatus.ForeColor = Color.Black;
         labelRomStatus.Text = "Unverified";
         toolTip1.SetToolTip(labelRomStatus,
            "Specified ROM does match known Super Metroid unheadered ROM checksum. Results may vary.");
         return;
      }

      private bool VerifyRomChecksum(string RomPath)
      {
         using (var stream = File.OpenRead(RomPath))
         {
            SHA256Managed Temp = new SHA256Managed();
            byte[] checksum = Temp.ComputeHash(stream);
            string checksumString = BitConverter.ToString(checksum).Replace("-", String.Empty);

            if (checksumString.ToLower() != "12b77c4bc9c1832cee8881244659065ee1d84c70c3d29e6eaf92e6798cc2ca72")
               return false;

            return true;
         }
      }

      private void MainForm_Load(object sender, EventArgs e)
      {
         UpdateRomStatus();
      }

      private void comboBoxRandomization_SelectedIndexChanged (object sender, EventArgs e)
      {
         if (comboBoxRandomization.Text == "Vanilla")
         {
            radioButtonManual.Enabled = false;
            radioButtonRandom.Enabled = false;
            numericUpDownSeed.Enabled = false;
         }
         else
         {
            if (radioButtonManual.Checked)
               numericUpDownSeed.Enabled = true;
            else
               numericUpDownSeed.Enabled = false;

            radioButtonManual.Enabled = true;
            radioButtonRandom.Enabled = true;
         }
      }
   }
}
