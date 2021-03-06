﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ItemRandomizer;

namespace DashRandomizer
   {
   public class GameModeVanilla : GameMode
      {
      public override string Mode
         {
         get { return "Standard"; }
         }

      public override string Randomization
         {
         get { return "Vanilla"; }
         }

      public GameModeVanilla ()
         {
         difficulty = 0;
         }

      public override string GetFileName (int Seed)
         {
         return string.Format ("DASH_v10_VN_{0}.sfc", Seed);
         }

      public override IEnumerable<Types.ItemLocation> GetItemLocations (int Seed)
         {
         return null;
         }

      public override string GetPracticeName (bool SaveStates)
         {
         if (SaveStates)
            return "DASH_v10_VN_Practice_SaveStates.sfc";

         return "DASH_v10_VN_Practice_NoSaveStates.sfc";
         }

      public override int UpdateRom (int Seed, ref byte[] RomData, bool GenerateSpoiler)
         {
         ApplyPatches (ref RomData);

         //TODO: Either Populate item locations in credits or remove custom credits
         //TODO: Remove seed info from loading screen

         return 0;
         }
      }
   }
