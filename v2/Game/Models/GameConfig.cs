using System.Drawing;
using System.Text;
using System.Xml.Linq;

namespace GameLogic.Models
{
    public class GameConfig
    {
        public int Lines { get; set; }
        public int Columns { get; set; }
        public List<Boat> Boats { get; set; }

        public GameConfig(int nbLignes, int nbColonnes, List<Boat> bateaux)
        {
            Lines = nbLignes;
            Columns = nbColonnes;
            Boats = bateaux;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Lines: " + Lines);
            sb.AppendLine("Columns: " + Columns);
            sb.AppendLine("Boats:");
            foreach (var boat in Boats)
            {
                sb.AppendLine(boat.ToString());
            }
            return sb.ToString();
        }

    }

}

