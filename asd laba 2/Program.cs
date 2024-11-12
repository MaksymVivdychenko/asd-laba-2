using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography;
using System.Xml;

namespace asd_laba_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            //byte[,] board = RandomBoardGenerating();
            //Board board1 = new(board);
            //board1.PrintBoard();
            //Console.WriteLine();
            //LDFS ldfs = new(board1);
            //Board result = ldfs.LDFS_Call();
            //result.PrintBoard();

            //byte[,] board = RandomBoardGenerating();
            //BoardRBFS boardRBFS = new(board);
            //boardRBFS.hCost = boardRBFS.CalculateHeuristic();
            //boardRBFS.PrintBoard();
            //Console.WriteLine();
            //RBFS solution = new RBFS(boardRBFS);
            //BoardRBFS result = solution.RBFSCall();
            //result.PrintBoard();


        }
        public static byte[,] RandomBoardGenerating()
        {
            Random random = new Random();
            byte[,] returnValue = new byte[8, 8];
            for (int i = 0; i < 8; i++)
            {
                returnValue[random.Next(8), i] = 1;
            }
            return returnValue;
        }
        public static void PrintBoard(byte[,] board)
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
        public static byte[,] CopyBoard(byte[,] board)
        {
            byte[,] copiedBoard = new byte[8, 8];
            for(int i = 0;i < 8;i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    copiedBoard[i,j] = board[i,j];
                }
            }
            return copiedBoard;
        }
        public static List<byte[,]> NeighboursFinder(byte[,] board)
        {
            List<byte[,]> neighbours = new List<byte[,]>();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (board[j,i] != 1)
                    {
                        byte[,] copiedBoard = CopyBoard(board);
                        copiedBoard[j, i] = 1;
                        for (int k = 0; k < 8; k++)
                        {
                            if (k != j)
                            {
                                copiedBoard[k, i] = 0;
                            }
                        }
                        //можлива модифікація коли стовпець повністю зчищається і на кожній ітерації додається 1 на новому місці
                        neighbours.Add(copiedBoard);
                    }
                }
            }
            return neighbours;
        }
        public static bool CheckCorrectnessOfBoard(byte[,]  board)
        {
            (int, int)[] queens = FindQueens(board);
            List<(int, int)> aab = CheckPossibleAttacks(4, 4);
            foreach ((int,int) pos in queens)
            {
                List<(int,int)> possibleAttacks = CheckPossibleAttacks(pos.Item1, pos.Item2);
                foreach((int, int) attackedFields in possibleAttacks)
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
                List<(int,int)> possibleAttacks = new List<(int,int)>();
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
            static (int, int)[] FindQueens(byte[,] board)
            {
                int k = 0;
                (int, int)[] queensPositions = new (int, int)[board.GetLength(0)];
                for (int i = 0; i < 8; i++)
                { 
                    for(int j = 0; j < 8; j++)
                    {
                        if (board[i, j] == 1)
                        {
                            queensPositions[k] = (i,j);
                            k++;
                        }
                    }
                }
                return queensPositions;
            }
        }

        public static byte[,] LDFS(byte[,] board, HashSet<byte[,]> visitedBoard, int counter)
        {
            if (CheckCorrectnessOfBoard(board) == true)
            {
                return board;
            }
            if (counter > 8)
            {
                return null;
            }
            visitedBoard.Add(board);
            List<byte[,]> neighbours = NeighboursFinder(board);
            foreach (byte[,] neighbour in neighbours)
            {
                if (!visitedBoard.Contains(neighbour))
                {
                    byte[,] result = LDFS(neighbour, visitedBoard, counter + 1);
                    if (result != null)
                    {
                        return result;
                    }
                }
            }
            return null;
        }
        public static int nodesToSolution = -1;
        public static byte[,] LDFS(
    byte[,] board,
    HashSet<byte[,]> visitedBoard,
    int counter,
    ref int iterationsCount,
    ref int deadEndsCount,
    ref int totalNodesCount)
        {
            // Збільшуємо лічильник ітерацій
            iterationsCount++;

            // Якщо дошка коректна, повертаємо її як рішення
            if (CheckCorrectnessOfBoard(board))
            {
                if (nodesToSolution == -1)
                {
                    nodesToSolution = counter - 1;
                }
                return board;
            }

            // Якщо лічильник перевищує 8, це глухий кут, повертаємо null
            if (counter > 8)
            {
                deadEndsCount++;
                return null;
            }

            // Додаємо дошку у відвідані вузли
            visitedBoard.Add(board);
            totalNodesCount++; // Загальна кількість вузлів

            // Отримуємо сусідів
            List<byte[,]> neighbours = NeighboursFinder(board);

            foreach (byte[,] neighbour in neighbours)
            {
                if (!visitedBoard.Contains(neighbour))
                {
                    byte[,] result = LDFS(neighbour, visitedBoard, counter + 1, ref iterationsCount, ref deadEndsCount, ref totalNodesCount);

                    if (result != null)
                    {
                        return result;
                    }
                }
            }
            return null;
        }
    }
}
//LDFS
// програмування дошки - 1 етап done
// програмування випадкового розташування 8 ферзів - 2 етап  done
// програмування функції знаходження нащадків - 3 етап done
// визначення максимальної глибини для ldfs - це буде 8 - 4 етап  done
// функція визначення правильності дошки - 5 етап done
// програмування ldfs - 6  етап
// // зробити ldfs і board окремим класом - 7 етап done
// євристична функція оцінки дошки - 8 етап done
// програмування rbfs
