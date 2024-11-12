using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asd_laba_2
{
    internal class LDFS
    {
        public int iterationsCount { get;protected set; }
        public int deadEndsCount { get; protected set; }
        public long totalNodesCount { get; protected set; }
        public int nodesInMemory { get; protected set; }
        Board board;
        public LDFS(Board board)
        {
            this.board = board;
            iterationsCount = 0;
            deadEndsCount = 0;
            totalNodesCount = 0;
            nodesInMemory = 0;
        }
        public Board LDFS_Call()
        {
            HashSet<Board> visitedBoards = new HashSet<Board>();
            Board result = LDFSFunc(board,visitedBoards, 0);
            if(result == null)
            {
                return null;
            }
            else
            {
                return result;
            }
        }
        public void PrintCounters()
        {
            Console.WriteLine($"{iterationsCount}      {deadEndsCount}   {totalNodesCount}    {nodesInMemory}");
        }
        private Board LDFSFunc(Board boardParam,HashSet<Board> visitedBoards, int counter)
        {
            iterationsCount++;

            if (boardParam.CheckCorrectnessOfBoard())
            {
                return boardParam;
            }
            if (counter > 8)
            {
                deadEndsCount++;
                return null;
            }
            visitedBoards.Add(boardParam);
            nodesInMemory++; 

            List<Board> neighbours = boardParam.NeighboursFinder();
            totalNodesCount += 56;

            foreach (Board neighbour in neighbours)
            {
                if (!visitedBoards.Contains(neighbour))
                {
                    Board result = LDFSFunc(neighbour, visitedBoards ,counter+1);

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
