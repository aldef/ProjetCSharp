using GameLogic.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public abstract class UserInputs
    {
        public static string AskPlayerName(string player) 
        {
            bool loopCond;
            string playerName; 
            do
            {
                Console.WriteLine($"Enter the name of the {player}");
                playerName = Console.ReadLine();

                if (playerName != null && playerName.Length > 3)
                {
                    loopCond = true;
                }
                else 
                {
                    Console.WriteLine($"The player name must be 3 chars long or more.");
                    loopCond = false;
                }
            } while (!loopCond);

            return playerName;
        }

        public static Boat AskBoat(Player player)
        {
            Boat boatSelected = null;
            bool loopCond = false;
            do
            {
                Console.WriteLine("Please select a boat from this list");
                string userInput = Console.ReadLine();

                // Check if the user input is a valid integer index
                if (int.TryParse(userInput, out int selectedIndex))
                {
                    // Check if the selected index is within the bounds of the available boats
                    List<Boat> availableBoats = player.AvailableBoats();
                    if (selectedIndex >= 0 && selectedIndex - 1 < availableBoats.Count)
                    {
                        boatSelected = availableBoats[selectedIndex - 1];
                        loopCond = true;
                    }
                }
            } while (!loopCond);

            return boatSelected;
        }
        
        public static Coordinate[] AskBoatCoordinates(Player player, Boat boatSelected)
        {
            Console.WriteLine($"{player.name}'s Gameboard");
            Extras.ShowGameboardColored(player.playerBoard);

            Coordinate startBoat = AskCoordinate($"Enter the start coordinates of the boat, separate with a space " +
                        $"(Lines = 1 to {player.playerBoard.lines}, " +
                        $"Columns = A to {(char)('A' + player.playerBoard.columns - 1)})", 
                        player.playerBoard.lines, player.playerBoard.columns);
            Coordinate endBoat = AskCoordinate($"Enter the end coordinates of the boat, separate with a space " +
            $"(Lines = 1 to {player.playerBoard.lines}, " +
            $"Columns = A to {(char)('A' + player.playerBoard.columns - 1)})",
            player.playerBoard.lines, player.playerBoard.columns);

            return new Coordinate[] { startBoat, endBoat }; 
        }

        public static Coordinate AskCoordinate(string message, int lines, int columns) 
        {
            bool loopCondition = false;
            int[] lineNumber = new int[2];
            int x = 0, y = 0; ;
            do
            {
                Console.WriteLine(message);
                string? userInput = Console.ReadLine();
                
                if (userInput != null && userInput.Length > 2)
                {
                    string[] strArr = userInput.Split(' ');
                    if (int.TryParse(strArr[0], out int line) &&
                        char.TryParse(strArr[1].ToUpper(), out char col))
                    {
                        x = line;
                        y = (int)col - (int)'A' + 1;

                        if ((x > 0) && (x <= lines) && (y <= columns))
                        {
                            loopCondition = true;
                        }
                        else
                        {
                            Console.WriteLine("Invalid input, please enter a valid line number and column letter.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input, please enter a valid line number and column letter.");
                    }
                }
            } while (!loopCondition);

            // -1 to convert from user interface 1 to 10 to machine index 0 to 9
            x -= 1;
            y -= 1;

            return new Coordinate(x, y);
        }

    }
}
