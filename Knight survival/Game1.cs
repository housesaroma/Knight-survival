using Knight_survival.GameScenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Knight_survival;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics { get; set; }
    private SpriteBatch _spriteBatch { get; set; }
    private SceneManager _sceneManager { get; set; }
    Song song;

    public Game1()
    {
        _sceneManager = new();
        _graphics = new GraphicsDeviceManager(this)
        {
            PreferredBackBufferWidth = 850,
            PreferredBackBufferHeight = 850
        };
        //_graphics.IsFullScreen = true;
        _graphics.ApplyChanges();
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _sceneManager.AddScene(new GameScene(Content));
        song = Content.Load<Song>("Audio/mainMusic");
        MediaPlayer.Play(song);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        _sceneManager.GetCurrentScene().Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

        _sceneManager.GetCurrentScene().Draw(_spriteBatch);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}

