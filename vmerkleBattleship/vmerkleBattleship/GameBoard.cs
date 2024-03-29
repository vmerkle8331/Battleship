using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace vmerkleBattleship
{
    class GameBoard
    {
        enum status { water, ship, hit, miss};
        status[,] secretBoard = new status[10, 10];
        status[,] board = new status[10, 10];

        bool isGameBoard;


        /// <summary>
        /// Prints the board for the game
        /// </summary>
        public void print()
        {
            for (int x = 0; x < 10; x++)
            {
                if (x == 0)
                {
                    Console.Write("   ");
                    for (int i = 0; i < 10; i++)
                    {
                        Console.Write(Convert.ToChar(Convert.ToByte(65 + i)) + " ");
                    }
                    Console.WriteLine();

                }
                if (x < 9)
                    Console.Write(x + 1 + " |");
                else
                    Console.Write(x + 1 + "|");
                for (int y = 0; y < 10; y++)
                {
                    if (board[x, y] == status.water) Console.Write("~ ");
                    if (board[x, y] == status.hit) Console.Write("X ");
                    if (board[x, y] == status.miss) Console.Write("  ");
                }
                Console.Write("|\n");

                if (x == 9)
                {
                    Console.WriteLine();
                }
            }
        }


        /// <summary>
        /// makes a secret board for reference
        /// </summary>
        public void printSecret()
        {
            for (int x = 0; x < 10; x++)
            {
                if (x == 0)
                {
                    Console.Write("   ");
                    for (int i = 0; i < 10; i++)
                    {
                        Console.Write(Convert.ToChar(Convert.ToByte(65 + i)) + " ");
                    }
                    Console.WriteLine();

                }
                if (x < 9)
                    Console.Write(x + 1 + " |");
                else
                    Console.Write(x + 1 + "|");
                for (int y = 0; y < 10; y++)
                {
                    if (secretBoard[x, y] == status.water) Console.Write("~ ");
                    if (secretBoard[x, y] == status.hit) Console.Write("X ");
                    if (secretBoard[x, y] == status.miss) Console.Write("  ");
                }
                Console.Write("|\n");
                if (x == 9)
                {
                    Console.Write("   ");
                    for (int i = 0; i < 20; i++)
                    {
                        Console.Write("¯");
                    }
                    Console.Write(" ");
                    Console.WriteLine();
                }
            }
        }

        /// <summary>
        /// Helps keep count of the ships
        /// </summary>
        /// <returns></returns>

        public int shipCount()
        {
            int shipCount = 0;
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    if (secretBoard[x, y] == status.ship) shipCount++;
                }
            }
            return shipCount;
        }

        /// <summary>
        /// spawns the ships given the size of each ship
        /// </summary>
        /// <param name="size"></param>

        void spawnShip(int size)
        {
            Random rand = new Random();
            int x = -1;
            int y = -1;
            status[,] tempBoard = new status[10, 10];
            for (x = 0; x < 10; x++) //initialises temporary board so the og board can be restored in case of a mess up
            {
                for (y = 0; y < 10; y++)
                {
                    tempBoard[x, y] = secretBoard[x, y];
                }
            }
            int numShips = shipCount();
            while (true)
            {
                while (true) //finds random place on board without ship and starts building
                {
                    x = rand.Next(9);
                    y = rand.Next(9);
                    if (secretBoard[x, y] == status.water)
                    {
                        secretBoard[x, y] = status.ship;
                        break;
                    }
                }
                int i = 1;
                if (rand.Next() % 2 == 0) //horizontal ship
                {
                    for (i = 1; i < size; i++)
                    {
                        if (y - i < 0 || y - i > 9)
                        {
                            break;
                        }
                        else if (secretBoard[x, y - i] == status.ship) break;
                        else secretBoard[x, y - i] = status.ship;
                    }
                    if (i <= size + 1)
                    {
                        for (int t = 1; t < (size + 1) - i; t++)
                        {
                            if (y + t < 0 || y + t > 9)
                            {
                                break;
                            }
                            if (secretBoard[x, y + t] == status.ship) break;
                            secretBoard[x, y + t] = status.ship;
                        }
                    }
                }

                else //vertical ship
                {
                    for (i = 1; i < size; i++)
                    {
                        if (x - i < 0 || x - i > 9)
                        {
                            break;
                        }
                        else if (secretBoard[x - i, y] == status.ship) break;

                        else secretBoard[x - i, y] = status.ship;
                    }
                    if (i <= size + 1)
                    {
                        for (int t = 1; t < (size + 1) - i; t++)
                        {
                            if (x + t < 0 || x + t > 9)
                            {
                                break;
                            }
                            if (secretBoard[x + t, y] == status.ship) break;
                            secretBoard[x + t, y] = status.ship;
                        }
                    }

                }
                if (shipCount() - numShips == size) break; //if not enough ship spaces have been built it tries again
            }
        }

        void setSecretBoard()
        {
            spawnShip(5);
            spawnShip(4);
            spawnShip(3);
            spawnShip(3);
            spawnShip(2);
            spawnShip(2);

        }

        public GameBoard(bool GameBoardStatus)  //initialises boards and sets ships on secret board
        {
            isGameBoard = GameBoardStatus;
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    board[x, y] = status.water;
                }
            }
            setSecretBoard();
        }

        public void fire(int x, int y) //inputs counting first index as 1 w/ 1 taken away
        {
            int a = 0;
            switch (secretBoard[x - 1, y - 1])
            {
                case status.ship:
                    secretBoard[x - 1, y - 1] = status.hit;
                    board[x - 1, y - 1] = status.hit;
                    Console.WriteLine("Hit!");
                    Console.ReadLine();
                    break;

                case status.water:
                    secretBoard[x - 1, y - 1] = status.miss;
                    board[x - 1, y - 1] = status.miss;
                    Console.WriteLine("Miss!");
                    Console.ReadLine();
                    break;

                case status.hit:
                    a = 1 / a; //throws divid by zero error, which causes the user to get a message indicating the spot has already been fired upon
                    break;

                case status.miss:
                    a = 1 / a;
                    break;

                default:
                    Console.WriteLine("error firing missile");
                    break;
            }
        }

    }
}
