using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenUO.MapMaker.MapMaking
{
    public class Globals
    {
        public static List<string> names = new List<string>()
                                               {
                                                   @"FeluccaML[7168,4096]",
                                                   @"TrammelML[7168,4096]",
                                                   @"FeluccaOld[6144,4096]",
                                                   @"TrammelOld[6144,4096]",
                                                   @"Ilshenar[2304,1600]",
                                                   @"Malas[2560,2048]",
                                                   @"Tokuno[1448,1448]",
                                                   @"TerMur[1280,4096]"
                                               };

        public static List<int> Indexes = new List<int>() {0, 1, 0, 1, 2, 3, 4, 5};

        public static List<int[]> Dimentions = new List<int[]>()
                                                   {
                                                       new[] {7168, 4096},
                                                       new[] {7168, 4096},
                                                       new[] {6144, 4096},
                                                       new[] {6144, 4096},
                                                       new[] {2304, 1600},
                                                       new[] {2560, 2048},
                                                       new[] {1448, 1448},
                                                       new[] {1280, 4096},
                                                   };
    }
}
