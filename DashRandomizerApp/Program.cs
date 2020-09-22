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
         bool Quiet = false, TestMode = false, Verify = false;

         for (int i = 0; i < args.Length; i++)
            {
            if (args[i] == "-q")
               {
               Quiet = true;
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
               _ = ConsoleGameMode.UpdateRom (SpecifiedSeed, null, true, false);
               return;
               }

            if (!String.IsNullOrEmpty (VanillaRomPath))
               {
               byte[] RomBytes = File.ReadAllBytes (VanillaRomPath);
               int Seed = ConsoleGameMode.UpdateRom (SpecifiedSeed, RomBytes, false, Verify);

               if (Seed < 0)
                  {
                  if (!Quiet)
                     MessageBox.Show ("error");
                  }
               else
                  {
                  File.WriteAllBytes (ConsoleGameMode.GetFileName (Seed), RomBytes);

                  if (!Quiet)
                     MessageBox.Show (String.Format ("Seed {0} generated", Seed));
                  }

               return;
               }

            MessageBox.Show ("invalid console arguments");
            }

         Application.EnableVisualStyles();
         Application.SetCompatibleTextRenderingDefault(false);
         Application.Run(new MainForm(false));
      }
   }
}
