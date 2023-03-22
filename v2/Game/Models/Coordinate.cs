using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic.Models
{
    public class Coordinate
   {
        public int x { get; }
        public int y { get; }

        public Coordinate(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public override string ToString()
        {
            return $"({x}, {y})";
        }

        // Makes the List.Contains property work properly on
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            
            Coordinate other = (Coordinate)obj;
            return (x == other.x) && (y == other.y);
        }


    }
}
