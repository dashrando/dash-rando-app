using System;
using System.IO;
using System.Windows.Forms;
using DashRandomizer;

namespace DASH
{
   static class Program
   {
      /// <summary>
      /// The main entry point for the application.
      /// </summary>
      [STAThread]
      static void Main(string[] args)
      {
         GameMode ConsoleGameMode = null;
         string VanillaRomPath = String.Empty;
         int SpecifiedSeed = 0;
         bool TestMode = false, Verify = false;
         Action<string> Log = p => MessageBox.Show (p);

         for (int i = 0; i < args.Length; i++)
            {
            if (args[i] == "-q")
               {
               Log = p => {};
               continue;
               }

            if (args[i] == "-v")
               {
               Verify = true;
               continue;
               }

            if (args[i] == "-t")
               {
               TestMode = true;
               continue;
               }

            if (args[i] == "-s")
               {
               if (i + 1 < args.Length)
                  SpecifiedSeed = Convert.ToInt32 (args[++i]);

               continue;
               }

            if (args[i] == "-p")
               {
               if (i + 1 < args.Length)
                  VanillaRomPath = args[++i];

               continue;
               }

            if (args[i] == "-m")
               {
               if (i + 1 >= args.Length)
                  break;

               switch (args[++i])
                  {
                  case "mm":
                     ConsoleGameMode = new GameModeMajorMinor ();
                     break;

                  case "full":
                     ConsoleGameMode = new GameModeFull ();
                     break;

                  case "sgl20":
                     ConsoleGameMode = new GameModeSGL20 ();
                     break;

                  case "vanilla":
                     ConsoleGameMode = new GameModeVanilla ();
                     break;

                  default:
                     break;
                  }
               }
            }

         if (ConsoleGameMode != null)
            {
            if (TestMode)
               {
               // Generate logs only
               ConsoleGameMode.TestRom (SpecifiedSeed);
               return;
               }

            if (!String.IsNullOrEmpty (VanillaRomPath))
               {
               // Read the vanilla ROM into memory
               var RomBytes = File.ReadAllBytes (VanillaRomPath);

               // Update the ROM based on the game mode
               int Seed = ConsoleGameMode.UpdateRom (SpecifiedSeed, RomBytes, false, Verify);

               if (Verify && Seed == -2)
                  {
                  Log ("Verification failed.");
                  Environment.ExitCode = 2;
                  }
               else if (Seed < 0)
                  {
                  Log ("Failed to generate seed.");
                  Environment.ExitCode = 3;
                  }
               else
                  {
                  File.WriteAllBytes (ConsoleGameMode.GetFileName (Seed), RomBytes);

                  if (Verify)
                     Log (String.Format ("Verified seed {0} generated.", Seed));
                  else
                     Log (String.Format ("Seed {0} generated.", Seed));
                  }

               return;
               }

            Log ("Invalid command line arguments.");
            Environment.ExitCode = 1;
            return;
            }

         Application.EnableVisualStyles();
         Application.SetCompatibleTextRenderingDefault(false);
         Application.Run(new MainForm(false));
      }
   }
}
