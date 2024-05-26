
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Knight_survival.GameScene
{

    internal partial class GameScene
    {
        public GameScene(ContentManager contentManager, SceneManager sceneManager, GraphicsDeviceManager graphics)
        {
            this.contentManager = contentManager;
            this.sceneManager = sceneManager;
            this.graphics = graphics;
        }
    }
}