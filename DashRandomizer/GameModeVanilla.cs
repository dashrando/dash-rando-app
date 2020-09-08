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

      protected override IEnumerable<Types.ItemLocation> RandomizeRom (ref int Seed, byte[] RomData)
         {
         var ItemLocations = new List<Types.ItemLocation> ();

         //TODO: Populate list with vanilla positions (I guess)

         return ItemLocations;
         }
      }
   }
