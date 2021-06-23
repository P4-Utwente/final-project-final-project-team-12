using BlazorConnectFour.Pages;
using BlazorConnectFour.Shared;
using Bunit;
using Microsoft.JSInterop;
using Moq;
using NUnit.Framework;
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

            var navBarButtons = navBar.FindAll("a");
            var ConnectFourButton = navBarButtons.FirstOrDefault(b => b.GetAttribute("href").Contains("ConnectFour"));
            Assert.IsNotNull(ConnectFourButton);
        }

        [Test]
        public void InsertDishTest()
        {
            IRenderedComponent<ConnectFour> gamePage = testContext.RenderComponent<ConnectFour>();


            //get two dimensional array

            //check if the size is right

            for (int x = 0; x < 10; x++)
            {
                for (int y = 10; y > 0; y--)
                {
                    //click on cell


                    //check if dish is in row

                    if(true) //this cell is empty
                    {
                        //get current player

                        //check if this cell is not current players color color
                    }
                }
            }
        }

        [Test]
        public void ClickOnOccupiedCellTest()
        { 
        
        }

        [Test]
        public void CanPlayFullGame()
        {
            //play game and win

            IRenderedComponent<ConnectFour> gamePage = testContext.RenderComponent<ConnectFour>();

            var GamePageButtons = gamePage.FindAll("button");
            var resetButton = GamePageButtons.FirstOrDefault(b => b.OuterHtml.Contains("Reset"));
            Assert.IsNotNull(resetButton);

            resetButton.Click();

            var RecheckedGamePageButtons = gamePage.FindAll("button");
            var RecheckedResetButton = GamePageButtons.FirstOrDefault(b => b.OuterHtml.Contains("Reset"));
            Assert.IsNull(RecheckedResetButton);
        }

        [Test]
        public void CorrectWinnerIsDisplayed()
        {
            IRenderedComponent<ConnectFour> gamePage = testContext.RenderComponent<ConnectFour>();

            PlayGame(gamePage, true);

            PlayGame(gamePage, false);
        }

        

        //-----------------------------------
        // helper methods
        //-----------------------------------
        
        private void PlayGame(IRenderedComponent<ConnectFour> gamePage)
        {
            PlayGame(gamePage, true);
        }

        private void PlayGame(IRenderedComponent<ConnectFour> gamePage, bool playerOneWins)
        {

        }
    }
}