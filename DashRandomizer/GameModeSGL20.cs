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
   public class GameModeSGL20 : GameMode
      {
      protected override string DashPatchName
         {
         get { return "dash_SGL2020.ips"; }
         }

      public override string Mode
         {
         get { return "SG Live 2020"; }
         }

      public override string Randomization
         {
         get { return "Major / Minor"; }
         }

      public GameModeSGL20 ()
         {
         difficulty = Types.Difficulty.Tournament;
         }

      public override string GetFileName (int Seed)
         {
         return string.Format ("DASH_SGL20_{0}.sfc", Seed);
         }

      public override int UpdateRom (int Seed, byte[] RomData, bool GenerateSpoiler, bool Verify)
         {
         var rnd = SetupSeed (ref Seed, RomData);

         ApplyPatches (RomData);

         var TheItems = Items.addReserves (3, Items.Items);
         TheItems = Items.addETanks (13, TheItems);
         TheItems = Items.addMissiles (33, TheItems);
         TheItems = Items.addSupers (15, TheItems);
         TheItems = Items.addPowerBombs (15, TheItems);

         var ItemLocationList = NewRandomizer.generateItems (rnd, ListModule.Empty<Types.Item> (),
            ListModule.Empty<Types.ItemLocation> (), TheItems, TournamentLocations.AllLocations);

         if (GenerateSpoiler)
            WriteSpoilerLog (Seed, ItemLocationList);

         if (RomData != null)
            {
            var sortedItems = ItemLocationList.Where (p => p.Item.Class == Types.ItemClass.Major &&
               p.Item.Type != Types.ItemType.ETank && p.Item.Type != Types.ItemType.Reserve).OrderBy (p => p.Item.Type);

            _ = Randomizer.writeRomSpoiler (RomData, ListModule.OfSeq (sortedItems), 0x2f5240);
            _ = Randomizer.writeLocations (RomData, ItemLocationList);
            }

         return Seed;
         }
      }
   }
