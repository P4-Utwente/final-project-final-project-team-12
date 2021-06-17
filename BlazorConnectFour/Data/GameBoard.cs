using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorConnectFour.Data
{
    public class GameBoard
    {
        public  GamePiece[,] Board { get; set; }


        public GameBoard()
        {
            Board = new GamePiece[6, 7];
           
            //Populate the Board with blank pieces
            for(int i = 0; i <= 5; i++)
            {
                for(int j = 0; j <= 6; j++)
                {
                    Board[i, j] = new GamePiece(PieceColor.Blank);
                }
            }
        }
    }
}
