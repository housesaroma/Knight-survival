using NUnit.Framework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Moq;
using NUnit.Framework.Legacy;
using Microsoft.Xna.Framework.Graphics; // Assuming you are using Moq for mocking

namespace Knight_survival.GameScene
{
    [TestFixture]
    public class GameSceneTests
    {

        [Test]
        public void GameScene_Initialization_PlayerAndMonstersAreNotNull()
        {
            var contentManager = new Mock<ContentManager>(); 
            var sceneManager = new Mock<SceneManager>(); 
            var graphics = new Mock<GraphicsDeviceManager>();

            var gameScene = new GameScene(contentManager.Object, sceneManager.Object, graphics.Object);

            gameScene.Load();

            ClassicAssert.IsNotNull(gameScene.player);
            ClassicAssert.IsNotNull(gameScene.monsters);
        }
        [Test]
        public void GameScene_CheckGameOver_GameOverIsTrueWhenPlayerHealthIsZero()
        {
            var contentManager = new Mock<ContentManager>(); 
            var sceneManager = new Mock<SceneManager>(); 
            var graphics = new Mock<GraphicsDeviceManager>();

            var gameScene = new GameScene(contentManager.Object, sceneManager.Object, graphics.Object);

            gameScene.Load();
            gameScene.player.Health = 0;
            gameScene.Update(new GameTime());
            gameScene.CheckGameOver(Mock.Of<SpriteBatch>());

            ClassicAssert.IsTrue(gameScene.isGameOver);
        }
        [Test]
        public void GameScene_SpawnMonsters_IncreasesMonsterCount()
        {
            var contentManager = new Mock<ContentManager>();
            var sceneManager = new Mock<SceneManager>();
            var graphics = new Mock<GraphicsDeviceManager>();
            var gameScene = new GameScene(contentManager.Object, sceneManager.Object, graphics.Object);
            gameScene.Load();

            int initialCount = gameScene.monsters.Count;
            gameScene.spawnTimer = gameScene.spawnInterval; // Set timer to trigger spawn
            gameScene.Update(new GameTime());

            ClassicAssert.IsTrue(gameScene.monsters.Count > initialCount);
        }
    }
}