using BattleShip.BLL.GameLogic;
using BattleShip.BLL.Requests;
using BattleShip.BLL.Responses;
using BattleShip.BLL.Ships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.UI
{
    public class GameManager //coordinates everything
    {
        private GameInput _gameInput;
        private GameView _gameView;
        private Player _p1;
        private Player _p2;
        private bool _gameAbandoned = false;
        private PlayerTurn _playerTurn = PlayerTurn.ONE;
        

        public GameManager(GameInput gameInput, GameView gameView, Player p1, Player p2)
        {
            this._gameInput = gameInput;
            this._gameView = gameView;
            _p1 = p1;
            _p2 = p2;
        }

        public void Start()
        {
            Console.Clear();
            displayWelcome();
            promptForPlayerNames();

            // Start turns (gameInput)
            while(!_gameAbandoned)
            {
                promptForShipPlacement(_p1);
                promptForShipPlacement(_p2);
                attemptPlayerForShot();
                playAgain();
                Console.ReadKey();
            }
      
        }

        private void displayWelcome()
        {
            _gameView.DisplayWelcomeToGame();
        }

        private void promptForPlayerNames()
        {
            _p1.Name = _gameInput.promptForPlayerName("Player 1");
            _p2.Name = _gameInput.promptForPlayerName("Player 2");
        }

        private void promptForShipPlacement(Player p)
        {
            // Prompt each player to set up boards for 5 ships
            // with a starting Coordinate and direction and clear screen afterwards

            ShipType shipType = new ShipType();
            for (int i = 1; i < 6; i++)
            {
                switch (i)
                {
                    case 1:
                    default:
                        shipType = ShipType.Battleship;
                        break;
                    case 2:
                        shipType = ShipType.Carrier;
                        break;
                    case 3:
                        shipType = ShipType.Cruiser;
                        break;
                    case 4:
                        shipType = ShipType.Destroyer;
                        break;
                    case 5:
                        shipType = ShipType.Submarine;
                        break;
                }
                displayCurrentBoardState(p.PlayerBoard);
                Coordinate shipCoordinate = _gameInput.promptForCoordinate("{0}, Please place your " + shipType + " (Ex/ A6, B8) (Column(A-J) & Row(1-10)): ", p.Name);
                PlaceShipRequest placeShip = new PlaceShipRequest()
                {
                    Coordinate = shipCoordinate,
                    ShipType = shipType,
                    Direction = _gameInput.shipDirection()
                };
                var response = p.PlayerBoard.PlaceShip(placeShip);
                switch (response)
                {
                    case ShipPlacement.Ok:
                        Console.WriteLine("Ship placed. Press any key to continue.");
                        break;
                    case ShipPlacement.NotEnoughSpace:
                        Console.WriteLine("There is not enough space. Press Any Key and Try again.");
                        i--;
                        break;
                    case ShipPlacement.Overlap:
                        Console.WriteLine("The ships are overlapping. Press Any Keey and Try a different coordinate.");
                        i--;
                        break;
                    default:
                        break;
                }
                Console.ReadLine();
                Console.Clear();
            }       

        }

        private void displayCurrentBoardState(Board board)
        {
            Console.Clear();
            ShotHistory[,] boardState = new ShotHistory[10, 10];
            for( int row=0; row < 10; row++)
            {
                for(int col=0; col<10; col++)
                {
                    Coordinate coord = new Coordinate(row + 1, col + 1);

                    if (board.ShotHistory.ContainsKey(coord))
                    {
                        boardState[col, row] = board.ShotHistory[coord];
                    }
                    else
                    {
                        boardState[col, row] = ShotHistory.Unknown;
                    }
                }
                _gameView.DisplayBoardState(boardState);
            }
        }

        private void attemptPlayerForShot()
        {
            // Prompt user for Shot (coordinate entry)
            // validate and call FireShot() method  
            FireShotResponse response;
            do
            {
                Player player;
                switch (_playerTurn)
                {
                    case PlayerTurn.ONE:
                        player = _p1;
                        break;
                    case PlayerTurn.TWO:
                    default:
                        player = _p2;
                        break;
                }
                do
                {
                    displayCurrentBoardState(player.EnemyPlayerBoard);
                    response = player.EnemyPlayerBoard.FireShot(_gameInput.promptForCoordinate("{0} Please enter coordinate: ", player.Name));

                    switch (response.ShotStatus)
                    {
                        case ShotStatus.Invalid:
                            Console.WriteLine("Invalid coordinate - Press any key and repeat turn");
                            break;
                        case ShotStatus.Duplicate:
                            Console.WriteLine("Duplicate shot - Press any key and repeat turn");
                            break;
                        case ShotStatus.Miss:
                            Console.WriteLine("You missed. Next player's turn. Press any key to continue.");
                            break;
                        case ShotStatus.Hit:
                            Console.WriteLine("A Hit! Next Player's Turn. Press any key to continue.");
                            break;
                        case ShotStatus.HitAndSunk:
                            Console.WriteLine("You sank your opponent's ship. Next players turn. Press any key to continue.");
                            // How do I call oppenents specific ship type here.  Do I call ShipImpacted?
                            break;
                        case ShotStatus.Victory:
                            Console.WriteLine("You have sunk all your opponent's ships, you win! Press any key to continue.");
                            break;
                        default:
                            break;
                    }
                    Console.ReadLine();
                } while (response.ShotStatus == ShotStatus.Invalid || response.ShotStatus == ShotStatus.Duplicate);
                if (_playerTurn == PlayerTurn.ONE)
                {
                    _playerTurn = PlayerTurn.TWO;
                }
                else
                {
                    _playerTurn = PlayerTurn.ONE;
                }
            } while (response.ShotStatus != ShotStatus.Victory);
        }
        
        private void playAgain()
        {
            //Not sure why this isn't working.  It won't clear the game.
            Console.Clear();
            Console.WriteLine("Game over! Thanks for playing! Do you want to play again (Y/N)?: ");
            string playAgain = Console.ReadLine().ToUpper();
            if (playAgain != "Y")
            {
                _gameAbandoned = true;
            }
            else
            {
                _p1.PlayerBoard = new Board();
                _p2.PlayerBoard = new Board();
                _p2.EnemyPlayerBoard = _p1.PlayerBoard;
                _p1.EnemyPlayerBoard = _p2.PlayerBoard;
            }
        }

    }

    public enum PlayerTurn
    {
        ONE = 0,
        TWO = 1
    }
}
