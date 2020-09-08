using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.FSharp.Collections;
using ItemRandomizer;

namespace DashRandomizer
{
   public enum RandoLogic
      {
      MajorMinor,
      Full,
      Vanilla
      };

    public class GameMode
    {
      private string nameCode;
      private string randoText;
      private string modeText;
      private RandoLogic logic;
      private Types.Difficulty difficulty;

      public Types.Difficulty Difficulty { get { return difficulty; } }
      public RandoLogic Logic { get { return logic; } }
      public string Mode { get { return modeText; } }

      public GameMode (string ModeText, RandoLogic Logic, string NameCode)
         {
         switch (Logic)
            {
            case RandoLogic.MajorMinor:
               difficulty = Types.Difficulty.Tournament;
               randoText = "Major / Minor";
               break;

            case RandoLogic.Full:
               difficulty = Types.Difficulty.Full;
               randoText = "Full";
               break;

            case RandoLogic.Vanilla:
               difficulty = 0;
               randoText = "Vanilla";
               break;

            default:
               throw new Exception ("Unknown logic specified.");
            }

         logic = Logic;
         nameCode = NameCode;
         modeText = ModeText;
         }

      internal void ApplyPatches (byte[] RomData)
         {
         var IpsPatchesToApply = Patches.IpsPatches.Where (p =>
             (p.Difficulty == this.difficulty || p.Difficulty == Types.Difficulty.Any) && p.Default);
         var RomPatchesToApply = Patches.RomPatches.Where (p =>
             (p.Difficulty == this.difficulty || p.Difficulty == Types.Difficulty.Any) && p.Default);

         Patches.ApplyPatches (ListModule.OfSeq (IpsPatchesToApply),
            ListModule.OfSeq (RomPatchesToApply), RomData);
         }

      public string GetFileName (int Seed)
         {
         return string.Format ("DASH_v9_{0}_{1}.sfc", nameCode, Seed);
         }

      public override string ToString ()
         {
         return randoText;
         }

      internal byte[] Randomize (int Seed, byte[] RomData, bool GenerateSpoiler)
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

         rnd = new Random (Seed);
         var ItemList = new List<Types.Item> ();
         var ItemLocationList = new List<Types.ItemLocation> ();
         FSharpList<Types.ItemLocation> RandomizedItems;

         if (logic == RandoLogic.MajorMinor)
            {
            RandomizedItems = NewRandomizer.generateItems (rnd, ListModule.OfSeq (ItemList),
               ListModule.OfSeq (ItemLocationList), Items.getItemPool<Random> (rnd),
               TournamentLocations.AllLocations);
            }
         else if (logic == RandoLogic.Full)
            {
            RandomizedItems = FullRandomizer.generateItems (rnd, ListModule.OfSeq (ItemList),
               ListModule.OfSeq (ItemLocationList), Items.getItemPool<Random> (rnd),
               TournamentLocations.AllLocations);
            }
         else
            return RomData;

         var itemLocations = Randomizer.writeSpoiler (Seed, GenerateSpoiler, "", RandomizedItems);
         var sortedItems = itemLocations.Where (p => p.Item.Class == Types.ItemClass.Major &&
            p.Item.Type != Types.ItemType.ETank && p.Item.Type != Types.ItemType.Reserve).OrderBy (p => p.Item.Type);

         RomData = Randomizer.writeRomSpoiler (RomData, ListModule.OfSeq (sortedItems), 0x2f5240);
         return Randomizer.writeLocations (RomData, itemLocations);
         }

      public int UpdateRom (int Seed, byte[] RomData, bool GenerateSpoiler)
         {
         //TODO: run legacy rando code and compare outputs

         ApplyPatches (RomData);

         if (logic == RandoLogic.Vanilla)
            return Seed;

         if (Seed == 0)
            Seed = new Random ().Next (1000000, 9999999);

         RomData = Randomize (Seed, RomData, GenerateSpoiler);
         return Seed;
         }
      }
}
