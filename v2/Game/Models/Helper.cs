using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic.Models
{
    public static class Helper
    {

        // Without this, both players will share the same boat list
        public static List<Boat> DuplicateBoatList(List<Boat> oldBoats)
        {
            List<Boat> newBoats = new List<Boat>();

            foreach (Boat b in oldBoats)
            {
                Boat newboat = new Boat(b.size, b.name);
                newBoats.Add(newboat);
            }

            return newBoats;
        }
    }
}
