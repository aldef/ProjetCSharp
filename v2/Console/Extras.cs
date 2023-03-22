using GameLogic.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public static class Extras
    {
        public static void PlaceBoats(Player player) 
        {     
            do 
            {
                ShowAvailableBoats(player);
                Boat selectedBoat = UserInputs.AskBoat(player);
                Coordinate[] coords = UserInputs.AskBoatCoordinates(player, selectedBoat);
                bool isBoatPlaced = player.playerBoard.PlaceBoat(selectedBoat, coords[0], coords[1]);

                if (!isBoatPlaced)
                {
                    Console.WriteLine("This boat doesn't fit here");
                }
                else
                {
                    selectedBoat.isPlaced = true;
                }

            } 
            while (!player.boats.All(b => b.isPlaced)); // LINQ to check if all if boats have been placed

            Console.WriteLine($"All the boats have been placed.");
            ShowGameboardColored(player.playerBoard);
        }

        public static void ShowAvailableBoats(Player player)
        {
            List<Boat> AvailableBoats = player.AvailableBoats();
            int i = 1;
            Console.WriteLine($"\nAvailable boats for {player.name}: ");
            foreach (Boat boat in AvailableBoats)
            {
                Console.WriteLine($"{i}: {boat}");
                i++;
            }
        }

        // true if a player  died
        public static bool DoTurn(Player currentPlayer, Player enemyPlayer) 
        {
            Turn turn = new Turn(currentPlayer, enemyPlayer);
            Console.WriteLine($"{currentPlayer.name}'s turn !\n" +
                $" here's your note board :");
            ShowGameboardColored(currentPlayer.noteBoard);
            bool isStrikeAllowed = false;
            bool didStrikeHit = false;
            bool didBoatDie = false;

            do
            {
                Coordinate strike = UserInputs.AskCoordinate($"Enter the start coordinates of the strike, separate with a space " +
                            $"(Lines = 1 to {enemyPlayer.playerBoard.lines}, " +
                            $"Columns = A to {(char)('A' + enemyPlayer.playerBoard.columns - 1)})",
                            enemyPlayer.playerBoard.lines, enemyPlayer.playerBoard.columns);

                isStrikeAllowed = enemyPlayer.playerBoard.isStrikeAllowed(strike);
                if (isStrikeAllowed)
                {
                    didStrikeHit = turn.DoStrike(strike);
                    if (didStrikeHit) 
                    {
                        didBoatDie = turn.DmgBoat(strike);
                    }
                }
            } while (!isStrikeAllowed);
            
            Console.Clear();
            ShowStrikeResult(didStrikeHit, didBoatDie);
            Console.WriteLine($"{currentPlayer.name}'s note board :");
            ShowGameboardColored(currentPlayer.noteBoard);

            if (!enemyPlayer.isAlive()) 
            {
                return true;
            }

            return false;
        }

        public static void ShowWinner(Player playerOne, Player playerTwo) 
        {
            if (!playerOne.isAlive())
            {
                Console.WriteLine($"{playerOne.name} has died, {playerTwo.name} is victorious !");
            }
            else
            {
                Console.WriteLine($"{playerTwo.name} has died, {playerOne.name} is victorious !");
            }
        }

        public static void TurnTransition(string text)
        {
            Console.WriteLine($"{text}");
            Console.WriteLine("Press a key to continue..");
            Console.ReadKey(true);
            Console.Clear();
        }

        public static void ShowStrikeResult(bool didStrikeHit, bool didBoatDie) 
        {
            if (didStrikeHit) 
            {
                Console.WriteLine("Hit !");
            }
            else 
            { 
                Console.WriteLine("Missed !"); 
            }

            if (didBoatDie) 
            {
                Console.WriteLine("Boat sunk !");
            }
        }

        public static void ShowGameboardColored(Gameboard gameboard)
        {
            int lines = gameboard.lines;
            int columns = gameboard.columns;
            int rowNumberWidth = (lines >= 10) ? 3 : 2;

            // Print column headers
            Console.Write(new string(' ', rowNumberWidth + 1));
            for (int j = 0; j < columns; j++)
            {
                char colLetter = (char)('A' + j);
                Console.Write("{0,2} ", colLetter);
            }
            Console.WriteLine();

            // Print rows with row number and cell values
            for (int i = 0; i < lines; i++)
            {
                Console.Write("{0," + rowNumberWidth + "} ", (i + 1));
                for (int j = 0; j < columns; j++)
                {
                    // Assign color
                    ConsoleColor color = ConsoleColor.White;
                    if (gameboard.matrix[i,j] == '0')
                    {
                        color = ConsoleColor.Blue;
                    }
                    else if (gameboard.matrix[i,j] == '1')
                    {
                        color = ConsoleColor.Green;
                    }
                    else if (gameboard.matrix[i,j] == 'X')
                    {
                        color = ConsoleColor.Red;
                    }
                    else if (gameboard.matrix[i,j] == 'M')
                    {
                        color = ConsoleColor.Yellow;
                    }

                    Console.ForegroundColor = color;
                    Console.Write("{0,2} ", gameboard.matrix[i, j]);
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
        }               
    }
}
