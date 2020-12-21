using System.IO;
using System.Linq;

namespace DashRandomizer
   {
   public abstract class Patch
      {
      public abstract bool Apply (ref byte[] FileBuffer);

      public byte[] Apply (string FileName)
         {
         // Specified file does not exist?
         if (!File.Exists (FileName))
            return null;

         // Apply the patch to the specified file.
         var FileBytes = File.ReadAllBytes (FileName);
         if (Apply (ref FileBytes))
            return FileBytes;

         return null;
         }

      protected void ResizeBuffer (ref byte[] Buffer, long MinSize)
         {
         if (Buffer.LongLength >= MinSize)
            return;

         var Temp = Buffer;
         Buffer = new byte[MinSize];
         Temp.CopyTo (Buffer, 0);
         }

      public string Test (string CleanFile, string PatchedFile)
         {
         var PatchedBytes = Apply (CleanFile);

         if (PatchedBytes == null)
            return "Failed to apply patch";

         if (!File.Exists (CleanFile))
            return "Clean file does not exist";

         if (!Enumerable.SequenceEqual (PatchedBytes, File.ReadAllBytes (PatchedFile)))
            return "Patch applied but does not match";

         // Patch applied and matches :)
         return null;
         }
      }
   }
