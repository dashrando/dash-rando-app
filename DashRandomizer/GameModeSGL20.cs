﻿using System;
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
      protected override string PatchName
         {
         get { return "dash_sgl20.bps"; }
         }

      protected override string PracticePatchNoSaveStates
         {
         get { return "dash_SGL20_hack_no_savestates.bps"; }
         }

      protected override string PracticePatchSaveStates
         {
         get { return "dash_SGL20_hack_savestates.bps"; }
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
         return string.Format ("DASH_SGL20_{0:D5}.sfc", Seed);
         }

      public override IEnumerable<Types.ItemLocation> GetItemLocations (int Seed)
         {
         var rnd = new Random (Seed);

         var ItemPool = Items.addReserves (3, Items.Items);
         ItemPool = Items.addETanks (13, ItemPool);
         ItemPool = Items.addMissiles (33, ItemPool);
         ItemPool = Items.addSupers (15, ItemPool);
         ItemPool = Items.addPowerBombs (15, ItemPool);

         var NewItems = ListModule.Empty<Types.Item> ();
         var ItemLocations = ListModule.Empty<Types.ItemLocation> ();

         // Place Morph at one of the earliest locations so that it's always accessible
         NewRandomizer.prefill (rnd, Types.ItemType.Morph, ref NewItems, ref ItemLocations,
            ref ItemPool, TournamentLocations.AllLocations);

         // Place either a super or a missile to open up BT's location
         var BT_Access = rnd.Next (2) == 0 ? Types.ItemType.Missile : Types.ItemType.Super;
         NewRandomizer.prefill (rnd, BT_Access, ref NewItems, ref ItemLocations,
            ref ItemPool, TournamentLocations.AllLocations);

         // Next step is to place items that opens up access to breaking bomb blocks
         // by placing either Screw/Speed/Bomb or just a PB pack early.
         // One PB pack will be placed after filling with other items so that there's at least one accessible
         Types.ItemType BombBreaker;
         switch (rnd.Next (13))
            {
            case 0:
               BombBreaker = Types.ItemType.SpeedBooster;
               break;
            case 1:
            case 2:
               BombBreaker = Types.ItemType.ScrewAttack;
               break;
            case 3:
            case 4:
            case 5:
            case 6:
               BombBreaker = Types.ItemType.Bomb;
               break;
            default:
               BombBreaker = Types.ItemType.PowerBomb;
               break;
            }
         NewRandomizer.prefill (rnd, BombBreaker, ref NewItems, ref ItemLocations,
            ref ItemPool, TournamentLocations.AllLocations);

         // Place a super if it's not already placed
         if (NewItems.Count (p => p.Type == Types.ItemType.Super) < 1)
            {
            NewRandomizer.prefill (rnd, Types.ItemType.Super, ref NewItems, ref ItemLocations,
               ref ItemPool, TournamentLocations.AllLocations);
            }

         // Place a power bomb if it's not already placed
         if (NewItems.Count (p => p.Type == Types.ItemType.PowerBomb) < 1)
            {
            var ModifiedLocations = TournamentLocations.AllLocations.Where (p =>
               p.Name != "Missile (Crateria bottom)");
            NewRandomizer.prefill (rnd, Types.ItemType.PowerBomb, ref NewItems, ref ItemLocations,
               ref ItemPool, ListModule.OfSeq (ModifiedLocations));
            }

         return NewRandomizer.generateItemsWithoutPrefill (rnd, NewItems,
            ItemLocations, ItemPool, TournamentLocations.AllLocations);
         }

      public override string GetPracticeName (bool SaveStates)
         {
         if (SaveStates)
            return "DASH_SGL20_Practice_SaveStates.sfc";

         return "DASH_SGL20_Practice_NoSaveStates.sfc";
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
