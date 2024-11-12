using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asd_laba_2
{
    internal class Board
    {
        public byte[,] board { get;protected set; }
        public Board(byte[,] board)
        {
            this.board = board;
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
        protected Board CopyBoard()
        {
            byte[,] copiedBoard = new byte[8, 8];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    copiedBoard[i, j] = board[i, j];
                }
            }
            Board returnValue = new(copiedBoard);
            return returnValue;
        }
        public List<Board> NeighboursFinder()
        {
            List<Board> neighbours = new List<Board>();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (board[j, i] != 1)
                    {
                        Board copiedBoard = CopyBoard();
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
        public bool CheckCorrectnessOfBoard()
        {
            (int, int)[] queens = FindQueens();
            foreach ((int, int) pos in queens)
            {
                List<(int, int)> possibleAttacks = CheckPossibleAttacks(pos.Item1, pos.Item2);
                foreach ((int, int) attackedFields in possibleAttacks)
                {
                    if (board[attackedFields.Item1, attackedFields.Item2] == 1)
                    {
                        return false;
                    }
                }
            }
            return true;


            static List<(int, int)> CheckPossibleAttacks(int queenPosX, int queenPosY)
            {
                List<(int, int)> possibleAttacks = new List<(int, int)>();
                int queenPosXCopy = queenPosX;
                int queenPosYCopy = queenPosY;
                queenPosXCopy--;
                while (queenPosXCopy >= 0)
                {
                    possibleAttacks.Add((queenPosXCopy, queenPosYCopy));
                    queenPosXCopy--;
                }
                queenPosXCopy = queenPosX;
                queenPosYCopy = queenPosY;
                queenPosXCopy++;
                while (queenPosXCopy < 8)
                {
                    possibleAttacks.Add((queenPosXCopy, queenPosYCopy));
                    queenPosXCopy++;
                }
                queenPosXCopy = queenPosX;
                queenPosYCopy = queenPosY;
                queenPosYCopy--;
                while (queenPosYCopy >= 0)
                {
                    possibleAttacks.Add((queenPosXCopy, queenPosYCopy));
                    queenPosYCopy--;
                }
                queenPosXCopy = queenPosX;
                queenPosYCopy = queenPosY;
                queenPosYCopy++;
                while (queenPosYCopy < 8)
                {
                    possibleAttacks.Add((queenPosXCopy, queenPosYCopy));
                    queenPosYCopy++;
                }
                queenPosXCopy = queenPosX;
                queenPosYCopy = queenPosY;
                queenPosXCopy++; queenPosYCopy++;
                while (queenPosYCopy < 8 && queenPosXCopy < 8) // права нижня діагональ
                {
                    possibleAttacks.Add((queenPosXCopy, queenPosYCopy));
                    queenPosXCopy++; queenPosYCopy++;
                }
                queenPosXCopy = queenPosX;
                queenPosYCopy = queenPosY;
                queenPosXCopy--; queenPosYCopy--;
                while (queenPosYCopy >= 0 && queenPosXCopy >= 0) // ліва верхня діагональ
                {
                    possibleAttacks.Add((queenPosXCopy, queenPosYCopy));
                    queenPosXCopy--; queenPosYCopy--;
                }
                queenPosXCopy = queenPosX;
                queenPosYCopy = queenPosY;
                queenPosXCopy++; queenPosYCopy--;
                while (queenPosXCopy < 8 && queenPosYCopy >= 0) // права верхня діагональ
                {
                    possibleAttacks.Add((queenPosXCopy, queenPosYCopy));
                    queenPosXCopy++; queenPosYCopy--;
                }
                queenPosXCopy = queenPosX;
                queenPosYCopy = queenPosY;
                queenPosXCopy--; queenPosYCopy++;
                while (queenPosXCopy >= 0 && queenPosYCopy < 8) // ліва нижня діагональ
                {
                    possibleAttacks.Add((queenPosXCopy, queenPosYCopy));
                    queenPosXCopy--; queenPosYCopy++;
                }
                return possibleAttacks;
            }
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
        public override int GetHashCode()
        {
            return board.GetHashCode();
        }

    }
}
