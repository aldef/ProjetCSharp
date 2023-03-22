using System;
namespace GameLogic.Models
{
    public class Player
    {
        public List<Boat> boats { get; set; }
        public Gameboard playerBoard { get; set; }
        public Gameboard noteBoard { get; set; }
        public string name { get; set; }

        public Player(List<Boat> boatListModel, Gameboard playerBoard,Gameboard noteBoard, string name)
        {
            boats = boatListModel;
            this.noteBoard = noteBoard;
            this.playerBoard= playerBoard;

            this.name = name;
        }
        
        public List<Boat> AvailableBoats() {

            List<Boat> availableBoats = new List<Boat>();
            int i = 1;
            foreach (Boat boat in boats) 
            {
                if (!boat.isPlaced) 
                {
                    availableBoats.Add(boat);
                }
                i++;
            }
            return availableBoats;
        }

        public bool isAlive() 
        {
            int allBoatSize = 0;

            foreach (Boat b in boats) 
            {
                allBoatSize += b.size;
            }

            if (allBoatSize == playerBoard.CountCharX()) 
            {
                return false;
            }

            return true;
        }
    }
}
