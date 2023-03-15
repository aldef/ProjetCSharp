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
    }

}

