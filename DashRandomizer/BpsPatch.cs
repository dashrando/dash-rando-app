using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DashRandomizer
   {
   class BpsPatch : Patch
      {
      byte[] patchBuffer;
      ulong patchOffset;
      static uint[] crcTable = null;

      public override bool Apply (ref byte[] buffer)
         {
         ulong outputOffset = 0;
         ulong sourceRelativeOffset = 0;
         ulong targetRelativeOffset = 0;

         var SourceSize = Decode ();
         var TargetSize = Decode ();
         var MetaDataSize = Decode ();

         if (SourceSize != (ulong) buffer.Length)
            return false;

         var source = buffer;
         var target = new byte[TargetSize];

         if (MetaDataSize > 0)
            patchOffset += MetaDataSize;

         ulong EOF = (ulong)patchBuffer.Length - 12;

         if (BitConverter.ToUInt32 (patchBuffer, (int)EOF) != CRC32 (source))
            return false;

         if (BitConverter.ToUInt32 (patchBuffer, (int)EOF + 8) != CRC32 (patchBuffer, EOF + 8))
            return false;

         while (patchOffset < EOF)
            {
            ulong data = Decode ();
            ulong command = data & 3;
            ulong length = (data >> 2) + 1;

            switch (command)
               {
               case 0:
                  while (0 < length--)
                     {
                     target[outputOffset] = source[outputOffset];
                     outputOffset++;
                     }
                  break;

               case 1:
                  while (0 < length--)
                     target[outputOffset++] = Read ();
                  break;

               case 2:
                  ulong src_copy = Decode ();
                  ulong src_temp = (ulong)((src_copy & 1) != 0 ? -1 : +1);
                  sourceRelativeOffset += src_temp * (src_copy >> 1);
                  while (0 < length--)
                     target[outputOffset++] = source[sourceRelativeOffset++];
                  break;

               case 3:
                  ulong tgt_copy = Decode ();
                  ulong tgt_temp = (ulong)((tgt_copy & 1) != 0 ? -1 : +1);
                  targetRelativeOffset += tgt_temp * (tgt_copy >> 1);
                  while (0 < length--)
                     target[outputOffset++] = target[targetRelativeOffset++];
                  break;
               }
            }

         if (BitConverter.ToUInt32 (patchBuffer, (int)EOF + 4) != CRC32 (target))
            return false;

         buffer = target;
         return true;
         }

      static internal uint CRC32 (byte[] buffer, ulong length = 0)
         {
         if (crcTable == null)
            {
            uint c;
            crcTable = new uint[256];

            for (uint n = 0; n < 256; n++)
               {
               c = n;
               for (var k = 0; k < 8; k++)
                  c = ((c & 1) != 0 ? (0xEDB88320 ^ (c >> 1)) : (c >> 1));
               crcTable[n] = c;
               }
            }

         uint crc = 0xffffffff;
         ulong num = length > 0 ? length : (ulong) buffer.Length;

         for (ulong i = 0; i < num; i++)
            crc = (crc >> 8) ^ crcTable[(crc ^ buffer[i]) & 0xff];

         return (uint) (crc ^ (-1)) >> 0;
         }

      ulong Decode ()
         {
         ulong data = 0, shift = 1;
         while (true)
            {
            byte x = Read ();
            ulong temp = (ulong)(x & (byte)0x7f);
            data += temp * shift;
            if ((x & 0x80) != 0) break;
            shift <<= 7;
            data += shift;
            }
         return data;
         }

      static public bool IsBpsPatch (byte[] PatchBuffer)
         {
         // Unable to read patch bytes or not enough bytes?
         if (PatchBuffer == null || PatchBuffer.Length < 4)
            return false;

         // Does the patch data not start with BPS1?
         if (Encoding.ASCII.GetString (PatchBuffer, 0, 4) != "BPS1")
            return false;

         return true;
         }

      public static BpsPatch Load (string FileName)
         {
         if (!File.Exists (FileName))
            return null;

         var ThePatch = new BpsPatch ();

         if (ThePatch.Process (File.ReadAllBytes (FileName)))
            return ThePatch;

         return null;
         }

      bool Process (byte[] PatchBuffer)
         {
         if (!IsBpsPatch (PatchBuffer))
            return false;

         patchBuffer = PatchBuffer;
         patchOffset = 4;

         return true;
         }

      byte Read ()
         {
         return patchBuffer[patchOffset++];
         }
      }
   }
