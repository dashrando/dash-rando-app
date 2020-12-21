using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.FSharp.Collections;
using ItemRandomizer;

namespace DashRandomizer
   {
   public class GameModeFull : GameMode
      {
      public override string Mode
         {
         get { return "Standard"; }
         }

      public override string Randomization
         {
         get { return "Full"; }
         }

      public GameModeFull ()
         {
         difficulty = Types.Difficulty.Full;
         }

      public override string GetFileName (int Seed)
         {
         return string.Format ("DASH_v10_SF_{0:D5}.sfc", Seed);
         }

      public override IEnumerable<Types.ItemLocation> GetItemLocations (int Seed)
         {
         var rnd = new Random (Seed);

         var TheItems = Items.addReserves (3, Items.Items);
         TheItems = Items.addETanks (13, TheItems);
         TheItems = Items.addMissiles (33, TheItems);
         TheItems = Items.addSupers (13, TheItems);
         TheItems = Items.addPowerBombs (17, TheItems);

         var NewItems = ListModule.Empty<Types.Item> ();
         var ItemLocations = ListModule.Empty<Types.ItemLocation> ();

         return FullRandomizer.generateItems (rnd, ListModule.Empty<Types.Item> (),
            ListModule.Empty<Types.ItemLocation> (), TheItems, TournamentLocations.AllLocations);
         }

      public override string GetPracticeName (bool SaveStates)
         {
         if (SaveStates)
            return "DASH_v10_SF_Practice_SaveStates.sfc";

         return "DASH_v10_SF_Practice_NoSaveStates.sfc";
         }

      public override int UpdateRom (int Seed, ref byte[] RomData, bool GenerateSpoiler)
         {
         ApplyPatches (ref RomData);

         SetupSeed (ref Seed, RomData);

         var ItemLocationList = GetItemLocations (Seed);

         if (GenerateSpoiler)
            {
            WriteProgressionLog (Seed, ItemLocationList);
            WriteSpoilerLog (Seed, ItemLocationList);
            }

         if (RomData != null)
            {
            var sortedItems = ItemLocationList.Where (p => p.Item.Class == Types.ItemClass.Major &&
               p.Item.Type != Types.ItemType.ETank && p.Item.Type != Types.ItemType.Reserve).OrderBy (p => p.Item.Type);

            _ = Randomizer.writeRomSpoiler (RomData, ListModule.OfSeq (sortedItems), 0x2f5240);
            _ = Randomizer.writeLocations (RomData, ListModule.OfSeq (ItemLocationList));
            }

         return Seed;
         }
      }
   }
