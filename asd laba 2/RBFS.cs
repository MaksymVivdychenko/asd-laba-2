using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asd_laba_2
{
    internal class RBFS
    {
        public int iterationsCount { get; protected set; }
        public int deadEndsCount { get; protected set; }
        public long totalNodesCount { get; protected set; }
        public int nodesInMemory { get; protected set; }
        private BoardRBFS board;
        public RBFS(BoardRBFS boardRBFS)
        {
            this.board = boardRBFS;
            iterationsCount = 0;
            deadEndsCount = 0;
            totalNodesCount = 0;
            nodesInMemory = 0;
        }
        public BoardRBFS RBFSCall()
        {
            BoardRBFS result = RBFSFunc(board, int.MaxValue);
            if (result == null)
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
        private BoardRBFS RBFSFunc(BoardRBFS currentBoard, int fLimit)
        {
            iterationsCount++;
            if (currentBoard.IsGoal())
                return currentBoard;

            List<BoardRBFS> neighbors = currentBoard.NeighboursFinder();
            totalNodesCount += 56;

            if (neighbors.Count == 0)
            {
                currentBoard.hCost = int.MaxValue;
                return null;
            }

            foreach (var neighbor in neighbors)
            {
                neighbor.gCost = currentBoard.gCost + 1;
                neighbor.hCost = neighbor.CalculateHeuristic();
            }

            neighbors = neighbors.OrderBy(n => n.FCost).ToList();

            while (neighbors.Count > 0)
            {
                BoardRBFS best = neighbors[0];
                if (best.FCost > fLimit)
                    return null;

                int alternativeLimit = (neighbors.Count > 1) ? neighbors[1].FCost : int.MaxValue;

                BoardRBFS result = RBFSFunc(best, Math.Min(fLimit, alternativeLimit));
                if (result != null)
                    return result;

                best.hCost = alternativeLimit;
                neighbors = neighbors.OrderBy(n => n.FCost).ToList();
            }
            deadEndsCount++;
            return null;
        }
    }
}
