using System;

namespace GameLogic.Models
{
    public class Turn
    {
        public Player currentPlayer;
        public Player enemyPlayer;

        public Turn(Player currentPlayer, Player enemyPlayer) 
        {
            this.currentPlayer = currentPlayer;
            this.enemyPlayer = enemyPlayer;
        }
        
        // returns true if a player died during this turn.

        private bool isBoatHit(Coordinate strike)
        {
            // boat hit
            if (enemyPlayer.playerBoard.matrix[strike.x, strike.y] == '1')
            {
                return true;
            }
            // boat not hit
            return false;
        }

        // true if strike hit boat, false if it missed
        public bool DoStrike(Coordinate strike) 
        {
            if (isBoatHit(strike))
            {
                enemyPlayer.playerBoard.matrix[strike.x, strike.y] = 'X';
                currentPlayer.noteBoard.matrix[strike.x, strike.y] = 'X';
                return true;
            }
            else 
            {
                currentPlayer.noteBoard.matrix[strike.x, strike.y] = 'M';
                return false;
            }
        }

        // return true if the boat was killed
        public bool DmgBoat(Coordinate strike) 
        {           
            // Find the boat that was hit
            foreach (Boat boat in enemyPlayer.boats)
            {
                if (boat.coordinates.Contains(strike))
                {
                    boat.HP --;          
                    if (boat.isAlive()) 
                    {
                        return false;
                    }
                    return true;
                }           
            }
            return false;
        }


    }
}
