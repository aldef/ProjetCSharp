using API;
using ConsoleApp;
using GameLogic.Models;
using Newtonsoft.Json;

namespace ConsoleApp
{
    class Program
    {
        public static void Main(string[] args)
        {

            // 1- Get the game config from the API
            GameConfig gameConfig = null;

            try
            {
                string configString = ApiRepo.GetDataAsync().GetAwaiter().GetResult();
                gameConfig = JsonConvert.DeserializeObject<GameConfig>(configString);

                if (gameConfig == null) 
                {
                    Console.Error.WriteLine($"gameConfig is Null");
                    Environment.Exit(1); // No config no game 
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An error occurred: {ex.Message}");
                Environment.Exit(1); // No config no game 
            }

            // For tests, delete later
            gameConfig.Boats.RemoveRange(0, 3);

            // 2- Create Players
            //string playerOneName = UserInputs.AskPlayerName("player one");
            //string playerTwoName = UserInputs.AskPlayerName("player two");

            Player playerOne = new Player(Helper.DuplicateBoatList(gameConfig.Boats), 
                new Gameboard(gameConfig.Lines, gameConfig.Columns),
                new Gameboard(gameConfig.Lines, gameConfig.Columns), "player1");

            Player playerTwo = new Player(Helper.DuplicateBoatList(gameConfig.Boats),
                new Gameboard(gameConfig.Lines, gameConfig.Columns),
                new Gameboard(gameConfig.Lines, gameConfig.Columns), "player2");

           
            // 3- Place boats on the gameboards
            Extras.TurnTransition($"{playerOne.name}'s turn to place boats");
            Console.WriteLine($"{playerOne.name}'s turn to place boats");
            Extras.PlaceBoats(playerOne);

            Extras.TurnTransition($"{playerTwo.name}'s turn to place boats");
            Console.WriteLine($"{playerTwo.name}'s turn to place boats");
            Extras.PlaceBoats(playerTwo);

            // 4- FIGHT

            Extras.TurnTransition($"Battle time !");

            var random = new Random();
            bool isPlayerOneTurn = random.Next(2) == 0; // randomly decide which player goes first
            bool playerDied;
            do
            {
                if (isPlayerOneTurn)
                {
                    Extras.TurnTransition($"{playerOne.name}'s turn");
                    playerDied = Extras.DoTurn(playerOne, playerTwo);
                    isPlayerOneTurn = false;
                }
                else
                {
                    Extras.TurnTransition($"{playerTwo.name}'s turn");
                    playerDied = Extras.DoTurn(playerTwo, playerOne);
                    isPlayerOneTurn = true;
                }

                if (playerDied) 
                {
                    Extras.ShowWinner(playerOne, playerTwo);
                }

            } while (!playerDied);

        }
        
    }
}
