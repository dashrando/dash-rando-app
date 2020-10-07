using System;
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

      public GameModeVanilla ()
         {
         difficulty = 0;
         randoText = "Vanilla";
         }

      public override string GetFileName (int Seed)
         {
         return string.Format ("DASH_v9_VN_{0}.sfc", Seed);
         }

      public override int UpdateRom (int Seed, byte[] RomData, bool GenerateSpoiler, bool Verify)
         {
         ApplyPatches (RomData);

         //TODO: Either Populate item locations in credits or remove custom credits
         //TODO: Remove seed info from loading screen

         return 0;
         }
      }
   }
