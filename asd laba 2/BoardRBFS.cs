using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asd_laba_2
{
    public class BoardRBFS
    {
        public byte[,] board;
        List<(int, int)> correctBoard;
        public int gCost; 
        public int hCost;

        public BoardRBFS(byte[,] board)
        {
            this.board = board;
            correctBoard =
            [
                (0, 5),
                (1, 3),
                (2, 6),
                (3, 0),
                (4, 7),
                (5, 1),
                (6, 4),
                (7, 2),
             ];
            hCost = CalculateHeuristic();

        }

        public int FCost => gCost + hCost;
        public bool IsGoal() => hCost == 0;
        protected BoardRBFS CopyBoard()
        {
            byte[,] copiedBoard = new byte[8, 8];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    copiedBoard[i, j] = board[i, j];
                }
            }
            BoardRBFS returnValue = new(copiedBoard);
            return returnValue;
        }
        public List<BoardRBFS> NeighboursFinder()
        {
            List<BoardRBFS> neighbours = new List<BoardRBFS>();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (board[j, i] != 1)
                    {
                        BoardRBFS copiedBoard = CopyBoard();
                        copiedBoard.board[j, i] = 1;
                        for (int k = 0; k < 8; k++)
                        {
                            if (k != j)
                            {
                                copiedBoard.board[k, i] = 0;
                            }
                        }
                        neighbours.Add(copiedBoard);
                    }
                }
            }
            return neighbours;
        }
        public int CalculateHeuristic()
        {
            int returnValue = 0;
            (int, int)[] queensPos = FindQueens();
            for (int i = 0; i < queensPos.Length; i++)
            {
                if (queensPos[i] != correctBoard[i])
                {
                    returnValue++;
                }
            }
            return returnValue;
        }
        protected (int, int)[] FindQueens()
        {
            int k = 0;
            (int, int)[] queensPositions = new (int, int)[board.GetLength(0)];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (board[i, j] == 1)
                    {
                        queensPositions[k] = (i, j);
                        k++;
                    }
                }
            }
            return queensPositions;
        }
        public void PrintBoard()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Console.Write(board[i, j]);
                    Console.Write(' ');
                }
                Console.WriteLine();
            }
        }

    }
}
