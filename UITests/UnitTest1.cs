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
        public void CanInsertDishInRow()
        {
            
        }

        [Test]
        public void CanClickResetGameButton()
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