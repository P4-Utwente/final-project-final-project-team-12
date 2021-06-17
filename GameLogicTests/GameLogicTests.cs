using BlazorConnectFour.Data;
using NUnit.Framework;
using System;
using System.IO;
using BlazorConnectFour.Pages;

namespace GameLogicTests
{
    public class Tests
    {
        ConnectFour connectFour = new ConnectFour();

        string path;
        public void DataStoreMatrix(string path)
        {
            using (var reader = new StreamReader(path))
            {
                string[,] matrixTest = new string[6, 7];

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    int i = 0;
                    int j = 0;

                    foreach (string value in values)
                    {
                        while (i < 6 && j <= 7)
                        {
                            if (j == 7)
                            {
                                i++;
                                j = 0;
                                //Console.WriteLine();

                            }
                            matrixTest[i, j] = value;
                            //Console.WriteLine(matrixTest[i, j]);
                            if (i == 5 && j == 6)
                            {
                                break;
                            }
                            j++;
                            break;

                        }
                    }

                }
                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        if (matrixTest[i, j] == "1")
                        {
                            connectFour.gameBoard().Board[i, j].Color = PieceColor.Red;
                        }
                        if (matrixTest[i, j] == "-1")
                        {
                            connectFour.gameBoard().Board[i, j].Color = PieceColor.Yellow;
                        }
                        if (matrixTest[i, j] == "0")
                        {
                            connectFour.gameBoard().Board[i, j].Color = PieceColor.Blank;
                        }
                        //Console.WriteLine(connectFour.gameBoard().Board[i, j].Color);
                    }
                   // Console.WriteLine();
                }
            }
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Logic_Test_Red_Wins()
        {

            path = @"D:/Studies/2B 2021/P4/Final Project/new git/GameLogicTests/Data/RedWinsVertical.csv";
            DataStoreMatrix(path);

            //ACT

            WinningPlay winningPlay;
            winningPlay = connectFour.GetWinner();

            //ASSERT

            Assert.AreEqual(PieceColor.Red, winningPlay.WinningColor);
        }

        [Test]
        public void VerticalFour()
        {
            //ARRANGE           

            path = @"D:/Studies/2B 2021/P4/Final Project/new git/GameLogicTests/Data/RedWinsVertical.csv";
            DataStoreMatrix(path);


            //ACT

            WinningPlay winningPlay;
            winningPlay = connectFour.GetWinner();

            //ASSERT

            Assert.AreEqual(EvaluationDirection.Up, winningPlay.WinningDirection);
        }
        [Test]
        public void Logic_Test_Yellow_Wins()
        {

            path = @"D:/Studies/2B 2021/P4/Final Project/new git/GameLogicTests/Data/YellowWinsHorizontal.csv";
            DataStoreMatrix(path);


            //ACT

            WinningPlay winningPlay;
            winningPlay = connectFour.GetWinner();

            //ASSERT

            Assert.AreEqual(PieceColor.Yellow, winningPlay.WinningColor);
        }
        [Test]
        public void Logic_Test_Horizontal_Four()
        {

            path = @"D:/Studies/2B 2021/P4/Final Project/new git/GameLogicTests/Data/YellowWinsHorizontal.csv";
            DataStoreMatrix(path);


            //ACT

            WinningPlay winningPlay;
            winningPlay = connectFour.GetWinner();

            //ASSERT

            Assert.AreEqual(EvaluationDirection.Right, winningPlay.WinningDirection);
        }
        [Test]
        public void Logic_Test_Diagonal_Four()
        {

            path = @"D:/Studies/2B 2021/P4/Final Project/new git/GameLogicTests/Data/DiagonalWin.csv";
            DataStoreMatrix(path);


            //ACT

            WinningPlay winningPlay;
            winningPlay = connectFour.GetWinner();

            //ASSERT

            Assert.AreEqual(EvaluationDirection.DownRight, winningPlay.WinningDirection);
            //Assert.AreEqual(EvaluationDirection.UpRight, winningPlay.WinningDirection);
        }
    }
}