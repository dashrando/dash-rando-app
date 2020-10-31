using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DashRandomizer;
using System.Security.Cryptography;

namespace DASH
   {
   public partial class MainForm : Form
      {
      private bool generateSpoiler;
      private string romPath = string.Empty;
      private bool romVerified = false;

      public MainForm (bool GenerateSpoiler)
         {
         InitializeComponent ();

         // Keep track of our inputs
         generateSpoiler = GenerateSpoiler;

         // Populate our Game Mode and Randomization options
         comboBoxRandomization.Items.Add (new GameModeSGL20 ());
         comboBoxRandomization.Items.Add (new GameModeMajorMinor ());
         //comboBoxRandomization.Items.Add (new GameModeFull ());
         //comboBoxRandomization.Items.Add (new GameModeVanilla ());
         comboBoxRandomization.SelectedIndex = 0;

         // Clear the Seed field since we default to automatically generate a seed
         numericUpDownSeed.Controls[1].Text = "";

         // Set the focus to one of the radio buttons instead of the ROM text box because it looks better
         radioButtonRandom.Focus ();
         }

      private void btnRandomize_Click (object sender, EventArgs e)
         {
         if (!File.Exists (romPath))
            {
            MessageBox.Show ("DASH", "The specified Vanilla ROM does not exist!",
               MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
            }

         int SpecifiedSeed = 0;

         if (radioButtonManual.Checked)
            SpecifiedSeed = Convert.ToInt32 (numericUpDownSeed.Value);

         var RandoGameMode = comboBoxRandomization.SelectedItem as GameMode;

         if (RandoGameMode == null)
            {
            MessageBox.Show ("DASH", "Invalid randomization selection!", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            return;
            }

         // Read the vanilla ROM into memory
         var RomData = File.ReadAllBytes (romPath);

         // Update the ROM based on the game mode
         int TheSeed = RandoGameMode.UpdateRom (SpecifiedSeed, RomData, generateSpoiler, false);

         // No seed generated?
         if (TheSeed < 0)
            {
            MessageBox.Show ("DASH", "Failed to generate seed.", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            return;
            }

         // Generate the path based on the game mode and seed
         string RomDir = Path.GetDirectoryName (romPath);
         string OutputPath = Path.Combine (RomDir, RandoGameMode.GetFileName (TheSeed));

         // Write the updated rom to the disk
         File.WriteAllBytes (OutputPath, RomData);

         // Show the New ROM form
         NewRomForm.ShowGeneratedRom (GetShortPath (Path.GetFullPath (OutputPath)), RandoGameMode, TheSeed);
         }

      private void btnBrowse_Click (object sender, EventArgs e)
         {
         OpenFileDialog BrowseForVanilla = new OpenFileDialog ();

         BrowseForVanilla.Title = "Select Vanilla Super Metroid ROM";
         BrowseForVanilla.Filter = "ROMs (*.SFC;*.SMC)|*.SFC;*.SMC|All Files (*.*)|*.*";

         if (DialogResult.OK == BrowseForVanilla.ShowDialog ())
            {
            romPath = Path.GetFileName (BrowseForVanilla.FileName);

            File.Copy (BrowseForVanilla.FileName,
               Path.Combine (Path.GetDirectoryName (Application.ExecutablePath), romPath), true);

            romVerified = VerifyRomChecksum (romPath);

            UpdateRomStatusUI ();
            }
         }

      public static string GetShortPath (string fullPath)
         {
         string RelativePath = GetRelativePath (fullPath, Directory.GetCurrentDirectory ());

         if (RelativePath.Length < fullPath.Length)
            return RelativePath;

         return fullPath;
         }

      public static string GetRelativePath (string fullPath, string basePath)
         {
         // Require trailing backslash for path
         if (!basePath.EndsWith ("\\"))
            basePath += "\\";

         Uri baseUri = new Uri (basePath);
         Uri fullUri = new Uri (fullPath);

         Uri relativeUri = baseUri.MakeRelativeUri (fullUri);

         // Uri's use forward slashes so convert back to backward slashes
         return relativeUri.ToString ().Replace ("/", "\\");
         }

      private void radioButtonManual_CheckedChanged (object sender, EventArgs e)
         {
         if (radioButtonManual.Checked)
            {
            numericUpDownSeed.Enabled = true;
            numericUpDownSeed.Controls[1].Text = numericUpDownSeed.Value.ToString ();
            }
         else
            {
            numericUpDownSeed.Enabled = false;
            numericUpDownSeed.Controls[1].Text = "";
            }
         }

      private void UpdateRomStatus ()
         {
         string ExecDir = Path.GetDirectoryName (Application.ExecutablePath);

         foreach (var RomFile in Directory.GetFiles (ExecDir).Where (p =>
              p.ToLower ().EndsWith (".sfc") || p.ToLower ().EndsWith (".smc")))
            {
            if (!VerifyRomChecksum (RomFile))
               continue;

            romPath = RomFile;
            romVerified = true;
            break;
            }

         UpdateRomStatusUI ();
         }

      private void UpdateRomStatusUI ()
         {
         if (String.IsNullOrEmpty (romPath))
            {
            labelRomStatus.BackColor = Color.Red;
            labelRomStatus.ForeColor = Color.White;
            labelRomStatus.Text = "Missing";
            btnRandomize.Enabled = false;
            toolTip1.SetToolTip (labelRomStatus,
               "Super Metroid unheadered ROM missing. Press Browse to find the ROM.");
            return;
            }

         btnRandomize.Enabled = true;

         if (romVerified)
            {
            labelRomStatus.BackColor = Color.Green;
            labelRomStatus.ForeColor = Color.White;
            labelRomStatus.Text = "Verified";
            toolTip1.SetToolTip (labelRomStatus,
               "Super Metroid unheadered ROM found. Ready to Randomize!");
            return;
            }

         labelRomStatus.BackColor = Color.Yellow;
         labelRomStatus.ForeColor = Color.Black;
         labelRomStatus.Text = "Unverified";
         toolTip1.SetToolTip (labelRomStatus,
            "Specified ROM does match known Super Metroid unheadered ROM checksum. Results may vary.");
         return;
         }

      private bool VerifyRomChecksum (string RomPath)
         {
         using (var stream = File.OpenRead (RomPath))
            {
            SHA256Managed Temp = new SHA256Managed ();
            byte[] checksum = Temp.ComputeHash (stream);
            string checksumString = BitConverter.ToString (checksum).Replace ("-", String.Empty);

            if (checksumString.ToLower () != "12b77c4bc9c1832cee8881244659065ee1d84c70c3d29e6eaf92e6798cc2ca72")
               return false;

            return true;
            }
         }

      private void MainForm_Load (object sender, EventArgs e)
         {
         UpdateRomStatus ();
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

      private void btnPrac_Click (object sender, EventArgs e)
         {
         var RandoGameMode = comboBoxRandomization.SelectedItem as GameMode;

         if (RandoGameMode == null)
            {
            MessageBox.Show ("DASH", "Invalid game mode selection!", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            return;
            }

         PracRomForm.ShowPracHackForm (RandoGameMode, romPath);
         }
      }
   }
