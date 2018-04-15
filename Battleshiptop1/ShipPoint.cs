using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleshiptop1
{
    public enum PartType
    {
        ShipPart,
        Aura
    }

    public class ShipPoint
    {
        public int X { get; set; }
        public int Y { get; set; }
        public PartType PType { get; set; }
    }
}
