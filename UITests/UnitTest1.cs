using AngleSharp.Dom;
using BlazorConnectFour.Pages;
using BlazorConnectFour.Shared;
using Bunit;
using Microsoft.JSInterop;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;

namespace UITests
{
    public class Tests
    {
        private Bunit.TestContext testContext;
        private Mock<IJSRuntime> jsMock;
        
        [SetUp]
        public void Setup()
        {
            testContext = new Bunit.TestContext();
            jsMock = new Mock<IJSRuntime>();
        }

        [TearDown]
        public void TearDown()
        {
            testContext.Dispose();
        }

        [Test]
        public void CanReachGamePage()
        {
            var navBar = testContext.RenderComponent<NavMenu>();

            var ConnectFourButton = navBar.Find("a[href='connectfour']");
            Assert.IsNotNull(ConnectFourButton);
        }

        [Test]
        public void InsertDishTest()
        {
            IRenderedComponent<ConnectFour> gamePage = testContext.RenderComponent<ConnectFour>();

            IElement[,] cellMatrix = GetMatrix(gamePage);

            Assert.IsTrue(cellMatrix.GetLength(0) == 7);
            Assert.IsTrue(cellMatrix.GetLength(1) == 6);

            for (int x = 0; x < cellMatrix.GetLength(0); x++)
            {
                for (int y = cellMatrix.GetLength(1) - 1; y > 5; y--)
                {
                    cellMatrix[x, y].Click();

                    IElement[,] updatedMatrix = GetMatrix(gamePage);


                    var dish = updatedMatrix[x, y].ClassList[1];

                    //check if dish was inserted
                    Assert.IsTrue(!dish.Equals("blank"));

                    var currentPlayer = gamePage.Find("h2").InnerHtml.ToLower();
                    if (dish.Equals("red"))
                    {
                        Assert.IsTrue(currentPlayer.Contains("yellow"));
                    }
                    else if(dish.Equals("yellow"))
                    {
                        Assert.IsTrue(currentPlayer.Contains("red"));
                    }

                    cellMatrix = updatedMatrix;
                }
            }
        }

        [Test]
        public void ClickOnOccupiedCellTest()
        {
            IRenderedComponent<ConnectFour> gamePage = testContext.RenderComponent<ConnectFour>();

            IElement[,] cellMatrix = GetMatrix(gamePage);

            GetMatrix(gamePage)[0, cellMatrix.GetLength(1) - 1].Click();

            //now it should be yellows turn
            GetMatrix(gamePage)[0, cellMatrix.GetLength(1) - 1].Click();

            //clicked on occupied cell, so it should remain yellows turn
            Assert.IsTrue(gamePage.Find("h2").InnerHtml.ToLower().Contains("yellow"));
        }

        [Test]
        public void RedCanWinTest()
        {
            IRenderedComponent<ConnectFour> gamePage = testContext.RenderComponent<ConnectFour>();

            GetMatrix(gamePage)[0, 0].Click();
            GetMatrix(gamePage)[1, 0].Click();
            GetMatrix(gamePage)[0, 0].Click();
            GetMatrix(gamePage)[1, 0].Click();
            GetMatrix(gamePage)[0, 0].Click();
            GetMatrix(gamePage)[1, 0].Click();
            GetMatrix(gamePage)[0, 0].Click();
            Assert.IsTrue(gamePage.Find("h2").InnerHtml.ToLower().Contains("red wins"));
        }

        [Test]
        public void YellowCanWinTest()
        {
            IRenderedComponent<ConnectFour> gamePage = testContext.RenderComponent<ConnectFour>();

            GetMatrix(gamePage)[0, 0].Click();
            GetMatrix(gamePage)[1, 0].Click();
            GetMatrix(gamePage)[0, 0].Click();
            GetMatrix(gamePage)[1, 0].Click();
            GetMatrix(gamePage)[0, 0].Click();
            GetMatrix(gamePage)[1, 0].Click();
            GetMatrix(gamePage)[3, 0].Click();
            GetMatrix(gamePage)[1, 0].Click();
            Assert.IsTrue(gamePage.Find("h2").InnerHtml.ToLower().Contains("yellow wins"));
        }

        [Test]
        public void CanPlayMutipleGamesTest()
        {
            IRenderedComponent<ConnectFour> gamePage = testContext.RenderComponent<ConnectFour>();

            GetMatrix(gamePage)[0, 0].Click();
            GetMatrix(gamePage)[1, 0].Click();
            GetMatrix(gamePage)[0, 0].Click();
            GetMatrix(gamePage)[1, 0].Click();
            GetMatrix(gamePage)[0, 0].Click();
            GetMatrix(gamePage)[1, 0].Click();
            GetMatrix(gamePage)[0, 0].Click();
            Assert.IsTrue(gamePage.Find("h2").InnerHtml.ToLower().Contains("red wins"));

            var GamePageButtons = gamePage.FindAll("button");
            var resetButton = GamePageButtons.FirstOrDefault(b => b.OuterHtml.Contains("Reset"));
            Assert.IsNotNull(resetButton);

            resetButton.Click();

            GetMatrix(gamePage)[0, 0].Click();
            GetMatrix(gamePage)[1, 0].Click();
            GetMatrix(gamePage)[0, 0].Click();
            GetMatrix(gamePage)[1, 0].Click();
            GetMatrix(gamePage)[0, 0].Click();
            GetMatrix(gamePage)[1, 0].Click();
            GetMatrix(gamePage)[3, 0].Click();
            GetMatrix(gamePage)[1, 0].Click();
            Assert.IsTrue(gamePage.Find("h2").InnerHtml.ToLower().Contains("yellow wins"));
        }

        //-----------------------------------
        // helper methods
        //-----------------------------------


        private IElement[,] GetMatrix(IRenderedComponent<ConnectFour> gamePage)
        {
            var boards = gamePage.FindAll(".board");

            IElement[,] cellMatrix = new IElement[boards[0].Children.Count(), boards.Count];

            for(int y = 0; y < boards.Count; y++)
            {
                for (int x = 0; x < boards[y].Children.Count(); x++)
                {
                    cellMatrix[x, y] = boards[y].Children[x];
                }
            }

            return cellMatrix;
        }
    }
}