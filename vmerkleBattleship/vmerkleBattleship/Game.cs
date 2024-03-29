using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vmerkleBattleship
{
    class Game
    {

        public void Thing()
        {
            char play = 'y';

            GameBoard game = new GameBoard(true); // initialises board with ships 

            while (play == 'y') //game loop
            {
                game.print();
                Console.WriteLine("Attack!");
                while (true)  //get coodinates and verifies
                {
                    bool success = true;
                    Console.Write("Coordinates: ");
                    char[] input = Console.ReadLine().ToCharArray();
                    bool single = true; //single or double digit code

                    try
                    {
                        char temp = input[2];
                    }
                    catch (System.IndexOutOfRangeException)
                    {
                        single = false;
                        try
                        {
                            game.fire(Convert.ToInt32(input[1]) - 48, (Convert.ToInt32(input[0]) - 64));
                        }
                        catch (System.IndexOutOfRangeException)
                        {
                            Console.WriteLine("    invaild coordinate");
                            success = false;
                        }
                        catch (System.DivideByZeroException)
                        {
                            Console.WriteLine("    already fired there");
                            success = false;
                        }
                    }
                    if (single == true)
                    {
                        try
                        {
                            game.fire(Convert.ToInt32(input[1] + input[2]) - 87, (Convert.ToInt32(input[0]) - 64));
                        }
                        catch (System.IndexOutOfRangeException)
                        {
                            Console.WriteLine("    invaild coordinate");
                            success = false;
                        }
                        catch (System.DivideByZeroException)
                        {
                            Console.WriteLine("    already fired there");
                            success = false;
                        }
                    }
                    try
                    {
                        char temp = input[3];
                        Console.WriteLine("    too many inputs");
                        success = false;
                    }
                    catch (System.IndexOutOfRangeException) { }
                    if (success == true) break;
                }

                if (game.shipCount() == 0)
                {
                    Console.WriteLine("You sunk all of the ships");
                    Console.WriteLine("Play again? (y or n): ");
                    //play = Console.ReadLine();
                    play = 'n';
                    break;
                }
                Console.Clear();

            }
        }
    }
}

