﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.FSharp.Collections;
using ItemRandomizer;

namespace DashRandomizer
{
    public abstract class GameMode
    {
      protected Types.Difficulty difficulty;

      protected virtual string DashPatchName
         {
         get { return "dash_v9.ips"; }
         }

      public abstract string Mode { get; }
      public abstract string Randomization { get; }

      internal void ApplyPatches (byte[] RomData)
         {
         if (RomData == null)
            return;

         var CurrentDirectory = Directory.GetCurrentDirectory ();
         string assemblyPath = Path.GetDirectoryName (Assembly.GetExecutingAssembly ().Location);
         Directory.SetCurrentDirectory (assemblyPath);

         var IpsPatchesToApply = Patches.IpsPatches.Where (p =>
             (p.Difficulty == this.difficulty || p.Difficulty == Types.Difficulty.Any) &&
             p.Default).Concat (new[] { GetDashPatch () });
         var RomPatchesToApply = Patches.RomPatches.Where (p =>
             (p.Difficulty == this.difficulty || p.Difficulty == Types.Difficulty.Any) && p.Default);

         _ = Patches.ApplyPatches (ListModule.OfSeq (IpsPatchesToApply),
            ListModule.OfSeq (RomPatchesToApply), RomData);

         Directory.SetCurrentDirectory (CurrentDirectory);
         }

      internal Types.IpsPatch GetDashPatch ()
         {
         return new Types.IpsPatch ("DASH", DashPatchName, Types.Difficulty.Any,
            Types.PatchType.Standard, true);
         }

      public abstract string GetFileName (int Seed);

      public override string ToString ()
         {
         return Mode + " - " + Randomization;
         }

      protected Random SetupSeed (ref int Seed, byte[] RomData)
         {
         if (Seed == 0)
            Seed = new Random ().Next (1000000, 9999999);

         if (RomData != null)
            {
            var rnd = new Random (Seed);

            var seedInfo = rnd.Next (0xFFFF);
            var seedInfo2 = rnd.Next (0xFFFF);
            var seedInfoArr = Items.toByteArray (seedInfo);
            var seedInfoArr2 = Items.toByteArray (seedInfo2);

            RomData[0x2FFF00] = seedInfoArr[0];
            RomData[0x2FFF01] = seedInfoArr[1];
            RomData[0x2FFF02] = seedInfoArr2[0];
            RomData[0x2FFF03] = seedInfoArr2[1];
            }

         return new Random (Seed);
         }

      public abstract int UpdateRom (int Seed, byte[] RomData, bool GenerateSpoiler, bool Verify);

      internal void WriteProgressionLog (int Seed, IEnumerable<Types.ItemLocation> ItemLocations)
         {
         if (!Directory.Exists ("logs"))
            Directory.CreateDirectory ("logs");

         string Output = String.Empty;

         var FirstPB = ItemLocations.LastOrDefault (p => p.Item.Type == Types.ItemType.PowerBomb);

         if (FirstPB != null)
            Output += String.Format ("Power Bomb -> {0}{1}", FirstPB.Location.Name, Environment.NewLine);

         var FirstSuper = ItemLocations.LastOrDefault (p => p.Item.Type == Types.ItemType.Super);

         if (FirstSuper != null)
            Output += String.Format ("Super Missile -> {0}{1}", FirstSuper.Location.Name, Environment.NewLine);

         File.WriteAllText (Path.Combine ("logs", String.Format ("{0}.progression.txt", Seed)), Output);
         }

      internal void WriteSpoilerLog (int Seed, IEnumerable<Types.ItemLocation> ItemLocations)
         {
         if (!Directory.Exists ("logs"))
            Directory.CreateDirectory ("logs");

         string Output = String.Empty;

         foreach (var ItemLoc in ItemLocations.OrderBy (p => p.Location.Address))
            Output += String.Format ("{0} -> {1}{2}", ItemLoc.Location.Name, ItemLoc.Item.Name, Environment.NewLine);

         File.WriteAllText (Path.Combine ("logs", String.Format ("{0}.dash.txt", Seed)), Output);
         }
      }
}
