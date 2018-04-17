using BattleShip.BLL.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.UI
{
    public class GameView //displays state of game
    {
        public void DisplayWelcomeToGame()
        {
            Console.WriteLine("Welcome to Battleship!");
        }

        public void DisplayBoardState(ShotHistory[,] boardState)
        {
            Console.Clear();
            Console.WriteLine();
            int maxRow = boardState.GetUpperBound(0) + 2;
            int maxCol = boardState.GetUpperBound(1) + 2;
            for (int row = 0; row < maxRow; row++)
            {
                for (int col = 0; col < maxCol; col++)
                {
                    if(row == 0 && col == 0)
                    {
                        Console.Write("   ");
                    }
                    else if (row == 0 && col > 0)
                    {
                        Console.Write(Char.ConvertFromUtf32(col + 64)+"  ");
                    }
                    else if (col == 0 && row > 0)
                    {
                        Console.Write(row.ToString().PadLeft(2));
                    }
                    else
                    {
                        switch(boardState[row-1,col-1])
                        {
                            case ShotHistory.Hit:
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write(" H ");
                                Console.ResetColor();
                                break;
                            case ShotHistory.Miss:
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.Write(" M ");
                                Console.ResetColor();
                                break;
                            case ShotHistory.Unknown:
                                Console.Write(" ~ ");
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                }
                Console.WriteLine();
            }

        }

    }
}
