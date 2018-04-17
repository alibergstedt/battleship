using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.BLL.Requests;
using BattleShip.BLL.Ships;

namespace BattleShip.UI
{
    public class GameInput //prompts for user input
    {
        public string promptForPlayerName(string playerNamePrompt)
        {
            string rv;
            do
            {
                Console.Write("{0} - Please enter your name: ", playerNamePrompt);
                rv = Console.ReadLine();
                if (rv.Length > 2) break;
                Console.WriteLine("Please enter at least two characters. Press any key to continue.");
                Console.ReadKey();
                Console.Clear();
            } while (true);
            return rv;
        }

        public Coordinate promptForCoordinate(string message, string playerName)
        {
            int x = 0;
            int y = 0;
            Coordinate result;
            do
            {
                Console.WriteLine();
                Console.Write(message, playerName);
                string shipCoordinate = Console.ReadLine().ToUpper();
                string XCoordinate = shipCoordinate.Substring(0, 1);
                string YCoordinate = shipCoordinate.Substring(1);
                x = 0;
                y = 0;
                int.TryParse(YCoordinate, out y);
                switch (XCoordinate)
                {
                    case "A":
                        x = 1;
                        break;
                    case "B":
                        x = 2;
                        break;
                    case "C":
                        x = 3;
                        break;
                    case "D":
                        x = 4;
                        break;
                    case "E":
                        x = 5;
                        break;
                    case "F":
                        x = 6;
                        break;
                    case "G":
                        x = 7;
                        break;
                    case "H":
                        x = 8;
                        break;
                    case "I":
                        x = 9;
                        break;
                    case "J":
                        x = 10;
                        break;
                }
                if (y >= 1 && y <= 10 && x >= 1 && x <= 10)
                {
                    break;
                }
                Console.WriteLine("That was not a valid choice, you must choose a column A-J and a row 1-10 (e.g. B6). Press any key to try again: ");
                Console.ReadKey();
            } while (true);
            result = new Coordinate(x, y);
            return result;
        }

        public ShipDirection shipDirection()
        {
            Console.Write("Please select the direction for your ship (up, down, left, right): ");
            string input = Console.ReadLine();
            ShipDirection shipDirection = new ShipDirection();
            switch(input.ToUpper())
            {
                case "UP":
                    shipDirection = ShipDirection.Up;
                    return shipDirection;
                case "DOWN":
                    shipDirection = ShipDirection.Down;
                    return shipDirection;
                case "LEFT":
                    shipDirection = ShipDirection.Left;
                    return shipDirection;
                case "RIGHT":
                    shipDirection = ShipDirection.Right;
                    return shipDirection;
                default:
                    Console.WriteLine("Invalid Input");
                    break;
            }
            Console.ReadLine();
            return shipDirection;
        }

        public ShipType shipType()
        {
            ShipType shipType = new ShipType();
            for (int i = 0; i < 5; i++)
            {
                switch (i)
                {
                    case 1:
                    default:
                        shipType = ShipType.Battleship;
                        return shipType;
                    case 2:
                        shipType = ShipType.Carrier;
                        return shipType;
                    case 3:
                        shipType = ShipType.Cruiser;
                        return shipType;
                    case 4:
                        shipType = ShipType.Destroyer;
                        return shipType;
                    case 5:
                        shipType = ShipType.Submarine;
                        return shipType;
                }
            }

            Console.ReadLine();
            return shipType;
        }

        /*TicTacToe Code public string promptForBoardSetup(int playerNumber, string playerName)
        {
            string rv; // return a value that is valid, int the form column is A tru J and row is 1 tru 10
            do
            {
                Console.Write("Player {0}, {1}, please place your first ship (Column & Row & Direction(up,down,left,right)): ", playerNumber, playerName);
                rv = Console.ReadLine().ToUpper();
                if (rv.Length != 2 || rv[0] >= 'A' || rv[0] <= 'J' || rv[1] >= 1 || rv[1] <= 10) break;
                Console.WriteLine("Invalid Choice..You must choose column A-J and row 1-10. Press any key to continue.");
                Console.ReadKey();
                Console.Clear();
            } while (true);
            return rv;
        }*/

    }
}
