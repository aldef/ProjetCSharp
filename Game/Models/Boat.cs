using System.Text;

namespace GameLogic.Models
{
    public class Boat
    {
        public int size { get; set; }
        public string name { get; set; }
        public bool isPlaced { get; set; }
        public int HP { get; set; }
        public List<Coordinate> coordinates { get; set; }


        public Boat(int taille, string nom)
        {

            this.size = taille;
            this.name = nom;
            HP = size; 
            isPlaced = false;
            coordinates = new List<Coordinate>();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Boat: {");
            sb.AppendFormat("size={0}, ", size);
            sb.AppendFormat("name='{0}' ", name);
            sb.AppendFormat("placed='{0}'", isPlaced);
            sb.Append("}");
            return sb.ToString();
        }

        public void FillCoordinates(Coordinate start, Coordinate end)
        {
            // Determine the direction and distance of the line
            int dx = end.x - start.x;
            int dy = end.y - start.y;
            int distance = Math.Max(Math.Abs(dx), Math.Abs(dy)) + 1;

            // Calculate the positions of the boat
            float step_x = (float)dx / (distance - 1);
            float step_y = (float)dy / (distance - 1);
            for (int i = 0; i < distance; i++)
            {
                int x = (int)Math.Round(start.x + i * step_x);
                int y = (int)Math.Round(start.y + i * step_y);

                Coordinate coordinate= new Coordinate(x, y);
                coordinates.Add(coordinate);
            }
        }

        public bool isAlive() 
        {
            if (HP == 0) 
            {
                return false;
            }
            return true;
        }

    }
}
