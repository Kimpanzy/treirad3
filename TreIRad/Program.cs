using TreIRad;
GameBoard test = new GameBoard(3);
Checker test2 = new Checker();
Player player = new Player("Kim");
player.move(test,test2);

//string display = " ";

//for (int row = 0; row < test.getBoard().GetLength(0); row++)
//{
//    for (int column = 0; column < test.getBoard()[row].Length; column++)
//    {
//        display += test.getBoard()[row][column].ToString() + " ";
//    }
//    display += "\n";
//}
//Console.WriteLine(display);
public class BoardCell
{
    char checker;
    
    
    public BoardCell() { }
    public BoardCell(Checker checker)
    {
        this.checker = checker.CheckSign;
    }
    public char getChecker()
    {
        return checker;
    }
}
public class Checker
    
{
    public Checker() { }
    public Checker(char checkSign)
    {
        CheckSign = checkSign;
    }

    public char CheckSign { get; private set; }
        
}
public class GameBoard
{
    public int boardSize { get; private set; }
    BoardCell[][] board;

    public GameBoard(int boardSize)
    {
        this.boardSize = boardSize;
        this.SetBoard(boardSize);
    }
    public void SetBoard(int boardSize)
    {
        board = new BoardCell[boardSize][];



        for (int i = 0; i < boardSize; i++)
        {
            BoardCell[] row = new BoardCell[boardSize];
            for (int j = 0; j < row.Length; j++)
            {
                row[j] = new BoardCell(new Checker(' '));
            }
            board[i] = row;
        }

    }
    public BoardCell[][] getBoard()
    {
        return this.board;
    }
    public void PrintBoard()
    {
        Console.WriteLine($"+___+___+___+");
        Console.WriteLine($"| {board[0][0].getChecker()} | {board[0][0].getChecker()} | {board[0][0].getChecker()} |");
        Console.WriteLine($"+___+___+___+");
        Console.WriteLine($"| {board[0][0].getChecker()} | {board[0][0].getChecker()} | {board[0][0].getChecker()} |");
        Console.WriteLine($"+___+___+___+");
        Console.WriteLine($"| {board[0][0].getChecker()} | {board[0][0].getChecker()} | {board[0][0].getChecker()} |");
        Console.WriteLine($"+___+___+___+");

    }
    public bool isValidPosition(int position)
    {
        if (position < 1 || position > (this.boardSize * this.boardSize)) { return false; }

        int count = 1, i = 0, j = 0;
        bool flag = true;

        for (i = 0; i < this.boardSize; ++i)
        {
            for (j = 0; j < this.board.Length; ++j)
            {
                if (count == position)
                {
                    flag = false; break;
                }
                ++count;
            }

            if (!flag) { break; }
        }

        if (board[i][j].getChecker() != ' ') { return false; }

        return true;

    }
}
public class Player
{
    public string name { get; private set; }
    public int rowIndex { get; private set; } // Row index of a single placement
    public int colIndex { get; private set; } // Column index of a single placement
    public string input = " ";
    private Player() { }

    public Player(string name)
    {
        this.name = name;
        rowIndex = -1;
        colIndex = -1;
        
    }
    /*
        A player can move a checker to place it on the board
     */
    public void move(GameBoard board, Checker checker)
    {
        int movePos;
        while (true)
        {
            NotificationCenter.boardPlacement(1, this.name, board.boardSize);
            board.PrintBoard();

            if (int.TryParse(Console.ReadLine(), out int input))
            {
                movePos = input;

                if (!board.isValidPosition(movePos))
                {
                    // Input is an integer, but it leads to an illegal position on the board
                    NotificationCenter.boardPlacement(3, this.name, board.boardSize);
                    continue;
                }
                else
                {
                    // Input is a valid integer which points to an open vacancy on the board
                    break;
                }
            }
            else
            {
                // Input is not an integer
                
                NotificationCenter.boardPlacement(2, this.name, board.boardSize);
            }
        }

        // Place the move to the board
        //placeTheMove(board, checker, movePos);
    }

    /*
        Place the move that a player made to the game board
     */
    //public void placeTheMove(GameBoard gameBoard, Checker checker, int movePosition)
    //{
    //    int count = 1, i = 0, j = 0;
    //    bool flag = true;

    //    for (i = 0; i < gameBoard.getBoard().length; ++i)
    //    {
    //        for (j = 0; j < gameBoard.getBoard()[i].length; ++j)
    //        {
    //            if (count == movePosition)
    //            {
    //                gameBoard.getBoard()[i][j].setChecker(checker);
    //                flag = false; break;
    //            }
    //            ++count;
    //        }

    //        if (!flag) { break; }
    //    }

    //    this.rowIndex = i;
    //    this.colIndex = j;
    //}
}
public class NotificationCenter
{
    private NotificationCenter() { }

    public static void welcome()
    {
        Console.WriteLine("Welcome to Tic Tac Toe! Please be noticed of the followings before our game starts:");

        Console.WriteLine();
        Console.WriteLine("    1. Make sure you are playing this game with one and only one of your friends.");
        Console.WriteLine("    2. The system will randomly decide which one of you two to begin first.");
        Console.WriteLine("    3. You can choose the size of the board from 3x3 to 10x10.");
        Console.WriteLine();

        Console.WriteLine("Hit \"y/Y\" to start the game. Or hit any other key to exit.");
    }

    //public static int startOrExit(string message)
    //{
    //    if (message.equalsIgnoreCase("Y"))
    //    {
    //        Console.WriteLine("**************************************************");
    //        Console.WriteLine("Game starts! May the odds be ever in your favor :)");
    //        Console.WriteLine("**************************************************");
    //        Console.WriteLine();
    //        return 1;
    //    }
    //    else
    //    {
    //        Console.WriteLine("**********************************************");
    //        Console.WriteLine("Game exits:( Take care and come back anytime!");
    //        Console.WriteLine("**********************************************");
    //        return 0;
    //    }
    //}

    public static void namesAndSize(int index)
    {
        switch (index)
        {
            case 1:
                Console.Write("Please enter YOUR name: ");
                break;
            case 2:
                Console.Write("Please enter YOUR FRIEND'S name: ");
                break;
            case 3:
                Console.Write("Please enter your preferred SIZE of the board");
                Console.WriteLine(" (from 3 to 10. 3 -> 3x3; 4 -> 4x4; 10 -> 10x10, etc): ");
                break;
            case 4:
                Console.Write("------------------------------------------------------");
                Console.WriteLine("------------------------------------------");
                Console.Write("Invalid input! Please enter a valid SIZE from 3 to 10! ");
                Console.WriteLine("(3 -> 3x3; 4 -> 4x4; 10 -> 10x10, etc)");
                Console.Write("------------------------------------------------------");
                Console.WriteLine("------------------------------------------");
                Console.WriteLine();
                break;
            case 5:
                Console.WriteLine("----------------------------------");
                Console.WriteLine("Invalid input! Must be an INTEGER!");
                Console.WriteLine("----------------------------------");
                Console.WriteLine();
                break;
            default:
                Console.WriteLine("Index can only be 1/2/3/4/5!");
                break;
        }
    }

    public static void boardPlacement(int index, String playerName, int boardSize)
    {
        switch (index)
        {
            case 1:
                Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
                Console.Write("Player " + playerName + ", ");
                Console.WriteLine("please enter your move. (enter a value from 1 - " + boardSize * boardSize + ")");
                Console.WriteLine("Example: 1 (means: cell[1, 1]); 3 (means: cell[1, 3])");
                Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
                Console.WriteLine();
                break;
            case 2:
                Console.WriteLine("----------------------------------");
                Console.WriteLine("Invalid input! Must be an INTEGER!");
                Console.WriteLine("----------------------------------");
                break;
            case 3:
                Console.WriteLine();
                Console.WriteLine("-------------------------------------------------------");
                Console.WriteLine("Input out of range or position taken! Please try again.");
                Console.WriteLine("-------------------------------------------------------");
                break;
            default:
                Console.WriteLine("Index for boardPlacement() must be 1/2/3!");
                break;
        }
    }

    public static void winnerCongratulations(String winnerName)
    {
        Console.WriteLine();
        Console.WriteLine("***********************************************");
        Console.WriteLine("Congratulations " + winnerName + "! You have won the game!");
        Console.WriteLine("***********************************************");
        Console.WriteLine();
    }

    public static void stalemateAnnouncement()
    {
        Console.WriteLine("*********************************");
        Console.WriteLine("Ops! We have reached a stalemate~");
        Console.WriteLine("*********************************");
    }

    public static void newGamePrompt()
    {
        Console.WriteLine("************************************************************");
        Console.WriteLine("Do you guys want to enjoy another round?");
        Console.WriteLine("Hit \"y/Y\" to kick off again! Or hit any other key to exit.");
        Console.WriteLine("************************************************************");
        Console.WriteLine();
    }

    public static void printSummaryResults(int wins1, String name1, int wins2, String name2)
    {
        String finalChampion;

        Console.WriteLine("√√√√√√√√√√√√√√√√√√√√√√√√√√√√√√√√√√√√√√√√√√√√√√√√√√√√√√√√√√√√√√");
        Console.WriteLine("Fantastic performance guys! Superb!");
        Console.WriteLine("Player " + name1 + " has won: " + wins1 + " time(s).");
        Console.WriteLine("Player " + name2 + " has won: " + wins2 + " time(s).");

        if (wins1 == wins2)
        {
            Console.WriteLine("Therefore, the final champion is: BOTH YOU GUYS!!!");
        }
        else
        {
            finalChampion = wins1 > wins2 ? name1 : name2;
            Console.WriteLine("Therefore, the final winner is: " + finalChampion + "!!!");
        }

        Console.WriteLine("√√√√√√√√√√√√√√√√√√√√√√√√√√√√√√√√√√√√√√√√√√√√√√√√√√√√√√√√√√√√√√");
        Console.WriteLine();
    }
}