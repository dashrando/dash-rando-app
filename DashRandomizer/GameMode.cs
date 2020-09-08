using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.FSharp.Collections;
using ItemRandomizer;

namespace DashRandomizer
{
    public abstract class GameMode
    {
      protected string randoText;
      protected Types.Difficulty difficulty;

      public abstract string Mode { get; }

      internal void ApplyPatches (byte[] RomData)
         {
         var IpsPatchesToApply = Patches.IpsPatches.Where (p =>
             (p.Difficulty == this.difficulty || p.Difficulty == Types.Difficulty.Any) && p.Default);
         var RomPatchesToApply = Patches.RomPatches.Where (p =>
             (p.Difficulty == this.difficulty || p.Difficulty == Types.Difficulty.Any) && p.Default);

         _ = Patches.ApplyPatches (ListModule.OfSeq (IpsPatchesToApply),
            ListModule.OfSeq (RomPatchesToApply), RomData);
         }

      public abstract string GetFileName (int Seed);

      public override string ToString ()
         {
         return randoText;
         }

      internal int Randomize (int Seed, byte[] RomData, bool GenerateSpoiler)
         {
         var RandomizedItems = RandomizeRom (ref Seed, RomData);

         var itemLocations = Randomizer.writeSpoiler (Seed, GenerateSpoiler, "", ListModule.OfSeq (RandomizedItems));
         var sortedItems = itemLocations.Where (p => p.Item.Class == Types.ItemClass.Major &&
            p.Item.Type != Types.ItemType.ETank && p.Item.Type != Types.ItemType.Reserve).OrderBy (p => p.Item.Type);

         _ = Randomizer.writeRomSpoiler (RomData, ListModule.OfSeq (sortedItems), 0x2f5240);
         _ = Randomizer.writeLocations (RomData, itemLocations);
         return Seed;
         }

      protected abstract IEnumerable<Types.ItemLocation> RandomizeRom (ref int Seed, byte[] RomData);

      protected Random SetupSeed (ref int Seed, byte[] RomData)
         {
         if (Seed == 0)
            Seed = new Random ().Next (1000000, 9999999);

         var rnd = new Random (Seed);

         var seedInfo = rnd.Next (0xFFFF);
         var seedInfo2 = rnd.Next (0xFFFF);
         var seedInfoArr = Items.toByteArray (seedInfo);
         var seedInfoArr2 = Items.toByteArray (seedInfo2);

         RomData[0x2FFF00] = seedInfoArr[0];
         RomData[0x2FFF01] = seedInfoArr[1];
         RomData[0x2FFF02] = seedInfoArr2[0];
         RomData[0x2FFF03] = seedInfoArr2[1];

         return new Random (Seed);
         }

      public int UpdateRom (int Seed, byte[] RomData, bool GenerateSpoiler)
         {
         //TODO: run legacy rando code and compare outputs

         ApplyPatches (RomData);

         return Randomize (Seed, RomData, GenerateSpoiler);
         }
      }
}
