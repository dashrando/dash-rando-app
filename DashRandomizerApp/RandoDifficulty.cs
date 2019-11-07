﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItemRandomizer;

namespace SuperMetroidRandoApp
{
   internal class RandoDifficulty
   {
      public Types.Difficulty Difficulty { get; set; }
      public string Text { get; set; }
      public string Mode { get; set; }
      public string Code { get; set; }

      public RandoDifficulty(Types.Difficulty DifficultyType, string ModeText, string DifficultyText, string DifficultyCode)
      {
         Difficulty = DifficultyType;
         Text = DifficultyText;
         Code = DifficultyCode;
         Mode = ModeText;
      }

      public string GetFileName(int Seed)
      {
         return string.Format("DASH_v6_{0}_{1}.sfc", Code, Seed);
      }

      public override string ToString()
      {
         return Text;
      }
   }
}
