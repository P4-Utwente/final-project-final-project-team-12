using BlazorConnectFour.Data;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorConnectFour.Pages
{
    public partial class ConnectFour : ComponentBase
    {
        GameBoard board = new GameBoard();
        PieceColor currentTurn = PieceColor.Red;
        WinningPlay winningPlay;

        public ref GameBoard gameBoard()
        {
            return  ref board;
        }
        private void PieceClicked(int x, int y)
        {
            if (winningPlay != null) { return; }

            GamePiece clickedSpace = board.Board[x, y];

            //The piece must "fall" to the lowest unoccupied space in the clicked column
            if (clickedSpace.Color == PieceColor.Blank)
            {
                while (x < 5)
                {
                    GamePiece nextSpace = board.Board[x + 1, y];

                    x = x + 1;
                    if (nextSpace.Color == PieceColor.Blank)
                    {
                        clickedSpace = nextSpace;
                    }
                }
                clickedSpace.Color = currentTurn;

            }

            winningPlay = GetWinner();
            if (winningPlay == null)
            {
                SwitchTurns();
            }

        }
        private void SwitchTurns()
        {
            if (currentTurn == PieceColor.Red)
            {
                currentTurn = PieceColor.Yellow;
            }
            else
            {
                currentTurn = PieceColor.Red;
            }
        }

        public WinningPlay GetWinner()
        {
            WinningPlay winningPlay = null;

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    winningPlay = EvaluatePieceForWinner(i, j, EvaluationDirection.Up);
                    if (winningPlay != null) { return winningPlay; }

                    winningPlay = EvaluatePieceForWinner(i, j, EvaluationDirection.UpRight);
                    if (winningPlay != null) { return winningPlay; }

                    winningPlay = EvaluatePieceForWinner(i, j, EvaluationDirection.Right);
                    if (winningPlay != null) { return winningPlay; }

                    winningPlay = EvaluatePieceForWinner(i, j, EvaluationDirection.DownRight);
                    if (winningPlay != null) { return winningPlay; }
                }
            }

            return winningPlay;

        }
        public WinningPlay EvaluatePieceForWinner(int i, int j, EvaluationDirection dir)
        {
            ref GamePiece currentPiece = ref board.Board[i, j];
            if (currentPiece.Color == PieceColor.Blank)              
            {
                return null;
            }

            int inARow = 1;
            int iNext = i;
            int jNext = j;

            var winningMoves = new List<string>();
            //Console.WriteLine(inARow);
            while (inARow < 4)
            {
                switch (dir)
                {
                    case EvaluationDirection.Up:
                        iNext = iNext - 1;
                        break;
                    case EvaluationDirection.UpRight:
                        jNext = jNext + 1;
                        iNext = iNext - 1;
                        break;
                    case EvaluationDirection.Right:
                        jNext = jNext + 1;
                        break;
                    case EvaluationDirection.DownRight:
                        jNext = jNext + 1;
                        iNext = iNext + 1;
                        break;
                }
                if (iNext < 0 || iNext >= 6 || jNext < 0 || jNext >= 7) { break; }
                if (board.Board[iNext, jNext].Color == currentPiece.Color)
                {
                    winningMoves.Add($"{iNext},{jNext}");
                    inARow++;
                }
                else
                {
                    return null;
                }
            }

            if (inARow >= 4)
            {
                winningMoves.Add($"{i},{j}");
                Console.WriteLine(currentPiece.Color);
                return new WinningPlay()
                {
                    WinningMoves = winningMoves,
                    WinningColor = currentPiece.Color,
                    WinningDirection = dir,
                };
            }

            return null;
        }
        private void Reset()
        {
            board = new GameBoard();
            currentTurn = PieceColor.Red;
            winningPlay = null;
        }

        private bool IsGamePieceAWinningPiece(WinningPlay winningPlay, int i, int j)
        {
            return winningPlay?.WinningMoves?.Contains($"{i},{j}") ?? false;
        }
    }
}
