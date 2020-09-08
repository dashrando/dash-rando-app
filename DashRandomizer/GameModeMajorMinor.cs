using System;
using System.Collections.Generic;
using System.Linq;
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

      protected override IEnumerable<Types.ItemLocation> RandomizeRom (ref int Seed, byte[] RomData)
         {
         var rnd = SetupSeed (ref Seed, RomData);

         var TheItems = Items.addReserves (3, Items.Items);
         TheItems = Items.addETanks (13, TheItems);
         TheItems = Items.addMissiles (33, TheItems);
         TheItems = Items.addSupers (13, TheItems);
         TheItems = Items.addPowerBombs (17, TheItems);

         var ItemList = new List<Types.Item> ();
         var ItemLocationList = new List<Types.ItemLocation> ();

         return NewRandomizer.generateItems (rnd, ListModule.OfSeq (ItemList),
            ListModule.OfSeq (ItemLocationList), TheItems, TournamentLocations.AllLocations);
         }
      }
   }
