using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace DashRandomizer
   {
   internal class Patch
      {
      internal static bool Apply (string PatchFileName, string RomFilePath)
         {
         string FlipsPath = "patches\\flips.exe";

         if (!File.Exists (FlipsPath))
            {
            FlipsPath = "flips.exe";

            if (!File.Exists (FlipsPath))
               return false;

            return false;
            }

         string PatchPath = PatchFileName;

         if (!File.Exists (PatchPath))
            {
            PatchPath = "patches\\" + PatchFileName;

            if (!File.Exists (PatchPath))
               return false;
            }

         var Patcher = Process.Start (FlipsPath, String.Format ("--ignore-checksum --apply \"{0}\" \"{1}\"",
            PatchPath, RomFilePath));
         Patcher.WaitForExit ();

         return true;
         }
      }
   }
