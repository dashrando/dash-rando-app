using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.FSharp.Collections;
using ItemRandomizer;

namespace DashRandomizer
{
    public abstract class GameMode
    {
      protected Types.Difficulty difficulty;

      protected virtual string PatchName
         {
         get { return "dash_mm.bps"; }
         }

      protected virtual string PracticePatchSaveStates
         {
         get { return "dash_v10_hack_savestates.bps"; }
         }

      protected virtual string PracticePatchNoSaveStates
         {
         get { return "dash_v10_hack_no_savestates.bps"; }
         }

      public abstract string Mode { get; }
      public abstract string Randomization { get; }

      internal void ApplyPatch (ref byte[] Rom, string PatchPath)
         {
         var CurrentDirectory = Directory.GetCurrentDirectory ();
         string assemblyPath = Path.GetDirectoryName (Assembly.GetExecutingAssembly ().Location);
         Directory.SetCurrentDirectory (Path.Combine (assemblyPath, "patches"));

         var ThePatch = BpsPatch.Load (PatchPath);
         ThePatch.Apply (ref Rom);

         Directory.SetCurrentDirectory (CurrentDirectory);
         }

      internal void ApplyPatches (ref byte[] RomData)
         {
         if (RomData == null)
            return;

         ApplyPatch (ref RomData, PatchName);
         }

      public abstract string GetFileName (int Seed);

      public abstract IEnumerable<Types.ItemLocation> GetItemLocations (int Seed);

      public abstract string GetPracticeName (bool SaveStates);

      internal string GetLocationString (Types.Location TheLocation)
         {
         // See if we can find a more common name for the location
         switch (TheLocation.Name)
            {
            // Crateria
            case "Power Bomb (Crateria surface)": return "Power Bomb (Landing Site)";
            case "Missile (Crateria bottom)": return "Missile (Old MB)";
            case "Missile (Crateria middle)": return "Missile (230 Missiles)";
            case "Missile (Crateria moat)": return "Missile (Moat)";

            // Green Brinstar
            case "Power Bomb (green Brinstar bottom)": return "Power Bomb (Etecoons)";
            case "Missile (green Brinstar below super missile)": return "Missile (Early Supers Bridge)";
            case "Super Missile (green Brinstar top)": return "Super Missile (Early Supers)";
            case "Missile (green Brinstar behind reserve tank)": return "Missile (Brinstar Reserve 1)";
            case "Missile (green Brinstar behind missile)": return "Missile (Brinstar Reserve 2)";
            case "Super Missile (green Brinstar bottom)": return "Super Missile (Etecoons)";

            // Pink Brinstar
            case "Super Missile (pink Brinstar)": return "Super Missile (Spore Spawn)";
            case "Missile (pink Brinstar bottom)": return "Missile (Charge Missiles)";
            case "Missile (pink Brinstar top)": return "Missile (Big Pink top)";

            // Blue Brinstar
            case "Missile (blue Brinstar top)": return "Missile (Billy Mays 1)";
            case "Missile (blue Brinstar behind missile)": return "Missile (Billy Mays 2)";
            case "Missile (blue Brinstar middle)": return "Missile (Beta Missiles)";
            case "Missile (blue Brinstar bottom)": return "Missile (Alpha Missiles)";

            // Red Brinstar
            case "Power Bomb (red Brinstar sidehopper room)": return "Power Bomb (Beta PBs)";
            case "Power Bomb (red Brinstar spike room)": return "Power Bomb (Alpha PBs)";
            case "Missile (red Brinstar spike room)": return "Missile (Alpha PBs)";

            // Others
            default: return TheLocation.Name;
            }
         }

      public override string ToString ()
         {
         return Mode + " - " + Randomization;
         }

      public string PatchForPractice (string VanillaRom, bool Emulator)
         {
         // *************************

         string OutputFileName = Path.Combine (Path.GetDirectoryName (VanillaRom),
            GetPracticeName (!Emulator));

         // *************************

         var RomBytes = File.ReadAllBytes (VanillaRom);

         if (Emulator)
            ApplyPatch (ref RomBytes, PracticePatchNoSaveStates);
         else
            ApplyPatch (ref RomBytes, PracticePatchSaveStates);

         File.WriteAllBytes (OutputFileName, RomBytes);
         return OutputFileName;
         }

      public void SetupSeed (ref int Seed, byte[] RomData, int Index = 0x2FFF00)
         {
         if (Seed == 0)
            Seed = new Random ().Next (1, 100000);

         if (RomData == null)
            return;

         var rnd = new Random (Seed);

         var seedInfo = rnd.Next (0xFFFF);
         var seedInfo2 = rnd.Next (0xFFFF);
         var seedInfoArr = Items.toByteArray (seedInfo);
         var seedInfoArr2 = Items.toByteArray (seedInfo2);

         RomData[Index] = seedInfoArr[0];
         RomData[Index + 1] = seedInfoArr[1];
         RomData[Index + 2] = seedInfoArr2[0];
         RomData[Index + 3] = seedInfoArr2[1];
         }

      public void TestRom (int Seed)
         {
         var ItemLocations = GetItemLocations (Seed);

         if (ItemLocations == null)
            return;

         WriteProgressionLog (Seed, ItemLocations);
         WriteSpoilerLog (Seed, ItemLocations);
         }

      public abstract int UpdateRom (int Seed, ref byte[] RomData, bool GenerateSpoiler);

      internal void WriteProgressionLog (int Seed, IEnumerable<Types.ItemLocation> ItemLocations)
         {
         if (!Directory.Exists ("logs"))
            Directory.CreateDirectory ("logs");

         string Output = String.Empty;

         var FirstPB = ItemLocations.LastOrDefault (p => p.Item.Type == Types.ItemType.PowerBomb);

         if (FirstPB != null)
            Output += String.Format ("Power Bomb -> {0}{1}", GetLocationString (FirstPB.Location), Environment.NewLine);

         var FirstSuper = ItemLocations.LastOrDefault (p => p.Item.Type == Types.ItemType.Super);

         if (FirstSuper != null)
            Output += String.Format ("Super Missile -> {0}{1}", GetLocationString (FirstSuper.Location), Environment.NewLine);

         File.WriteAllText (Path.Combine ("logs", String.Format ("{0}.progression.txt", Seed)), Output);
         }

      internal void WriteSpoilerLog (int Seed, IEnumerable<Types.ItemLocation> ItemLocations)
         {
         if (!Directory.Exists ("logs"))
            Directory.CreateDirectory ("logs");

         string Output = String.Empty;

         foreach (var ItemLoc in ItemLocations.OrderBy (p => p.Location.Address))
            Output += String.Format ("{0} -> {1}{2}", GetLocationString (ItemLoc.Location), ItemLoc.Item.Name, Environment.NewLine);

         File.WriteAllText (Path.Combine ("logs", String.Format ("{0}.dash.txt", Seed)), Output);
         }
      }
}
