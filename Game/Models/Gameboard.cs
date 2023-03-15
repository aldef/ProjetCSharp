using System.Text;

namespace GameLogic.Models
{
    public class Gameboard
    {
        public int lines { get; }
        public int columns { get; }
        public char[,] matrix { get; set; }

        public Gameboard(int nbLignes, int nbColonnes)
        {
            this.lines = nbLignes;
            this.columns = nbColonnes;

            matrix = FillMatrix();
        }

        private char[,] FillMatrix()
        {
            char[,] result = new char[lines, columns];

            for (int i = 0; i < lines; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    result[i, j] = '0';
                }
            }

            return result;
        }

        public int CountCharX()
        {
            int count = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == 'X')
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        public bool PlaceBoat(Boat boat, Coordinate startPosition, Coordinate endPosition)
        {
            // Check that the start and end positions are on the same row or column to avoid diagonals
            if (startPosition.x != endPosition.x && startPosition.y != endPosition.y)
            {
                return false;
            }

            // Determine the direction and distance of the line
            int dx = endPosition.x - startPosition.x;
            int dy = endPosition.y - startPosition.y;
            int distance = Math.Max(Math.Abs(dx), Math.Abs(dy)) + 1;

            if (distance != boat.size)
            {
                return false;
            }

            // Check if the boat overlaps with an existing boat
            if (BoatOverlaps(boat, startPosition, endPosition))
            {
                return false;
            }

            // Calculate the positions of the boat
            Coordinate[] positions = CalculateBoatPositions(startPosition, dx, dy, distance);

            // Place the boat on the player board
            PlaceBoatOnBoard(positions);
            boat.FillCoordinates(startPosition, endPosition);
            return true;
        }


        private bool BoatOverlaps(Boat boat, Coordinate startPosition, Coordinate endPosition)
        {
            // Calculate the positions of the boat
            Coordinate[] positions = CalculateBoatPositions(startPosition, endPosition.x - startPosition.x, endPosition.y - startPosition.y, boat.size);

            // Check if any of the positions overlap with a boat that has already been placed
            foreach (Coordinate position in positions)
            {
                if (matrix[position.x, position.y] == '1')
                {
                    return true;
                }
            }

            return false;
        }

        private Coordinate[] CalculateBoatPositions(Coordinate startPosition, int dx, int dy, int distance)
        {
            // Determine the step size for each coordinate
            float step_x = (float)dx / (distance - 1);
            float step_y = (float)dy / (distance - 1);

            // Calculate the positions of the boat
            Coordinate[] positions = new Coordinate[distance];
            for (int i = 0; i < distance; i++)
            {
                int x = (int)Math.Round(startPosition.x + i * step_x);
                int y = (int)Math.Round(startPosition.y + i * step_y);
                positions[i] = new Coordinate(x, y);
            }

            return positions;
        }

        private void PlaceBoatOnBoard(Coordinate[] positions)
        {
            // Place the boat on the player board
            foreach (Coordinate position in positions)
            {
                matrix[position.x, position.y] = '1';
            }
        }

        // A strike is allowed if the gameboard's value is 1 or 0 at this index
        public bool isStrikeAllowed(Coordinate strike)
        {
            // Strike allowed
            if (matrix[strike.x, strike.y] == '1' || matrix[strike.x, strike.y] == '0')
            {
                return true;
            }
            // Strike not allowed
            return false;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            
            // Determine the maximum width of the row numbers
            int rowNumberWidth = (lines >= 10) ? 3 : 2;

            // Print column headers
            sb.Append(new string(' ', rowNumberWidth + 1));
            for (int j = 0; j < columns; j++)
            {
                char colLetter = (char)('A' + j);
                sb.AppendFormat("{0,2} ", colLetter);
            }
            sb.AppendLine();

            // Print rows with row number and cell values
            for (int i = 0; i < lines; i++)
            {
                sb.AppendFormat("{0," + rowNumberWidth + "} ", (i + 1));
                for (int j = 0; j < columns; j++)
                {
                    sb.AppendFormat("{0,2} ", matrix[i, j]);
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}
