using BeatTheStormSystem;

namespace BeatTheStormTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestStartGame()
        {
            Game game = new();
            game.AddPlayer(new() { PlayerName = "John", PlayingPiece = "I" });
            game.AddPlayer(new() { PlayerName = "Mike", PlayingPiece = "J" });
            game.StartGame();
            string msg = $"game status = {game.GameStatus} num spots = {game.Spots.Count}";
            Assert.IsTrue(game.GameStatus == Game.GameStatusEnum.Playing && game.Spots.Count == 101, msg);
            TestContext.WriteLine(msg);
        }
        [Test]
        public void TestRollDice()
        {
            Game game = new();
            game.AddPlayer(new() { PlayerName = "John", PlayingPiece = "I" });
            game.AddPlayer(new() { PlayerName = "Mike", PlayingPiece = "J" });
            game.StartGame();
            int dicevalue = game.RollDice();
            string msg = $"dice value = {dicevalue} between 1 and 6";
            Assert.IsTrue(dicevalue > 0 && dicevalue < 7, msg);
            TestContext.WriteLine(msg);
        }
        [Test]
        public void TestGetCard()
        {
            Game game = new();
            game.AddPlayer(new() { PlayerName = "John", PlayingPiece = "I" });
            game.AddPlayer(new() { PlayerName = "Mike", PlayingPiece = "J" });
            game.StartGame();
            var card = game.GetRandomCard();
            string msg = $"card = {card["Name"]} value = {card["Value"]}";
            Assert.IsTrue(card != null, msg);
            TestContext.WriteLine(msg);
        }
        [Test]
        public void TestSpotChange()
        {
            Game game = new();
            game.AddPlayer(new() { PlayerName = "John", PlayingPiece = "I" });
            game.AddPlayer(new() { PlayerName = "Mike", PlayingPiece = "J" });
            game.StartGame();
            game.TakeSpot(20);
            string msg = $"spot players = {game.Spots[20].SpotPlayers.Count}, current player is in correct spot = {game.Spots[20].SpotPlayers.Contains(game.CurrentPlayer)}";
            Assert.IsTrue(game.Spots[20].SpotPlayers.Contains(game.CurrentPlayer) && game.Spots[20].SpotPlayers.Count > 0, msg);
            TestContext.WriteLine(msg);
        }
        [Test]
        public void TestTakeTurn()
        {
            Game game = new();
            game.AddPlayer(new() { PlayerName = "John", PlayingPiece = "I" });
            game.AddPlayer(new() { PlayerName = "Mike", PlayingPiece = "J" });
            game.StartGame(false, Game.GameModeEnum.DiceWithRandomCard);
            int rnd = 20;
            int currentplayer = game.Players.IndexOf(game.CurrentPlayer);
            game.TakeTurn(game.Spots[rnd]);
            string msg = $"current player = {game.CurrentPlayer.PlayerName}, previous player {game.Players[currentplayer].PlayerName}, players in new spot = {game.Players[currentplayer].SpotValue.AllPlayersInSpot()}";
            Assert.IsTrue(game.Players[currentplayer] != game.CurrentPlayer && game.Players[currentplayer].SpotValue != game.Spots[rnd] && game.Players[currentplayer].SpotValue.SpotPlayers.Count > 0, msg);
            TestContext.WriteLine(msg);
        }
        [Test]
        public void TestWinner()
        {
            Game game = new();
            game.AddPlayer(new() { PlayerName = "John", PlayingPiece = "I" });
            game.AddPlayer(new() { PlayerName = "Mike", PlayingPiece = "J" });
            game.StartGame();
            while (!game.PlayingCard.Contains("Hashomrim") || game.CurrentPlayer.SpotValue != game.Spots[100])
            {
                game.TakeTurn(game.Spots[100]);
                TestContext.WriteLine(game.CurrentPlayer.PlayerName);
                TestContext.WriteLine("----------------");
            }
            string msg = $"current player = {game.CurrentPlayer.PlayerName}, winner = {game.Winner.PlayerName}, game status = {game.GameStatus}";
            Assert.IsTrue(game.GameStatus == Game.GameStatusEnum.Winner && game.Winner == game.CurrentPlayer && game.CurrentPlayer.SpotValue == game.Spots[100], msg);
            TestContext.WriteLine(msg);
        }
        [Test]
        public void TestLoser()
        {
            Game game = new();
            game.AddPlayer(new() { PlayerName = "John", PlayingPiece = "I" });
            game.AddPlayer(new() { PlayerName = "Mike", PlayingPiece = "J" });
            game.StartGame();
            while (game.PlayingCard.Contains("Hashomrim") || game.CurrentPlayer.SpotValue != game.Spots[0])
            {
                game.TakeTurn(game.Spots[0]);
                TestContext.WriteLine(game.CurrentPlayer.PlayerName);
                TestContext.WriteLine("----------------");
            }
            string msg = $"current player = {game.CurrentPlayer.PlayerName}, loser = {game.Loser.PlayerName}, game status = {game.GameStatus}";
            Assert.IsTrue(game.GameStatus == Game.GameStatusEnum.Loser && game.Loser == game.CurrentPlayer && game.CurrentPlayer.SpotValue == game.Spots[0], msg);
            TestContext.WriteLine(msg);
        }
        [Test]
        public void TestUndoRedo()
        {
            Game game = new();
            game.AddPlayer(new() { PlayerName = "John", PlayingPiece = "I" });
            game.AddPlayer(new() { PlayerName = "Mike", PlayingPiece = "J" });
            game.StartGame();
            TestContext.WriteLine($"current player is at spot {game.Spots.IndexOf(game.CurrentPlayer.SpotValue)}, current player is {game.CurrentPlayer.PlayerName}");
            game.DoTurn();
            TestContext.WriteLine($"current player is at spot {game.Spots.IndexOf(game.CurrentPlayer.SpotValue)}, current player is {game.CurrentPlayer.PlayerName}");
            game.DoTurn();
            TestContext.WriteLine($"current player is at spot {game.Spots.IndexOf(game.CurrentPlayer.SpotValue)}, current player is {game.CurrentPlayer.PlayerName}");
            game.DoTurn();
            TestContext.WriteLine($"current player is at spot {game.Spots.IndexOf(game.CurrentPlayer.SpotValue)}, current player is {game.CurrentPlayer.PlayerName}");
            game.DoTurn();
            TestContext.WriteLine($"current player is at spot {game.Spots.IndexOf(game.CurrentPlayer.SpotValue)}, current player is {game.CurrentPlayer.PlayerName}");
            game.Undo();
            TestContext.WriteLine($"current player is at spot {game.Spots.IndexOf(game.CurrentPlayer.SpotValue)}, current player is {game.CurrentPlayer.PlayerName}");
            game.Redo();
            TestContext.WriteLine($"current player is at spot {game.Spots.IndexOf(game.CurrentPlayer.SpotValue)}, current player is {game.CurrentPlayer.PlayerName}");
        }
        [Test]
        public void TestPlayComputer()
        {
            //SM This test will only pass if you remove if statement in undo procedure.
            Game game = new();
            game.AddPlayer(new() { PlayerName = "John", PlayingPiece = "I" });
            game.StartGame(true);
            game.DoTurn();
            game.Undo();
            string msg = $"current player = {game.CurrentPlayer.PlayerName}";
            Assert.IsTrue(game.CurrentPlayer.PlayerName == "Computer", msg);
            TestContext.WriteLine(msg);
        }
    }
}