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
   public class GameModeMajorMinor : GameMode
      {
      public override string Mode
         {
         get { return "Standard"; }
         }

      public GameModeMajorMinor ()
         {
         difficulty = Types.Difficulty.Tournament;
         randoText = "Major / Minor";
         }

      public override string GetFileName (int Seed)
         {
         return string.Format ("DASH_v9_SM_{0}.sfc", Seed);
         }

      public override int UpdateRom (int Seed, byte[] RomData, bool GenerateSpoiler, bool Verify)
         {
         byte[] CloneData = null;

         if (Verify && RomData != null)
            CloneData = RomData.ToArray ();

         var rnd = SetupSeed (ref Seed, RomData);

         //********* Legacy Randomizer Code ***************

         if (Verify)
            {
            var CurrentDirectory = Directory.GetCurrentDirectory ();
            string assemblyPath = Path.GetDirectoryName (Assembly.GetExecutingAssembly ().Location);
            Directory.SetCurrentDirectory (assemblyPath);

            var IpsPatchesToApply = ListModule.OfSeq (Patches.IpsPatches.Where (p =>
                (p.Difficulty == this.difficulty || p.Difficulty == Types.Difficulty.Any) && p.Default));
            var RomPatchesToApply = ListModule.OfSeq (Patches.RomPatches.Where (p =>
                (p.Difficulty == this.difficulty || p.Difficulty == Types.Difficulty.Any) && p.Default));
            var Results = Randomizer.Randomize (Seed, Types.Difficulty.Tournament, false,
               "", CloneData, IpsPatchesToApply, RomPatchesToApply);

            Directory.SetCurrentDirectory (CurrentDirectory);
            }

         //********* Updated Randomizer Code ***************

         ApplyPatches (RomData);

         var TheItems = Items.addReserves (3, Items.Items);
         TheItems = Items.addETanks (13, TheItems);
         TheItems = Items.addMissiles (33, TheItems);
         TheItems = Items.addSupers (13, TheItems);
         TheItems = Items.addPowerBombs (17, TheItems);

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

            //********* Compare Results ***************

            if (CloneData != null && !CloneData.SequenceEqual (RomData))
               throw new Exception ("not equal");
            }

         return Seed;
         }
      }
   }
