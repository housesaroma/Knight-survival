using Knight_survival.GameScenes;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;

public class MainMenuScene : IScene
{
    private Texture2D _backgroundTexture;
    private Rectangle _newGameButton;
    private Rectangle _volumeSlider;
    private float _sliderValue;
    private ContentManager contentManager;
    private SceneManager sceneManager;

    public MainMenuScene(ContentManager contentManager, SceneManager sceneManager)
    {
        this.contentManager = contentManager;
        this.sceneManager = sceneManager;
    }

    public void Load()
    {
        _backgroundTexture = contentManager.Load<Texture2D>("background");
        _newGameButton = new Rectangle(400, 300, 100, 50);
        _volumeSlider = new Rectangle(400, 400, 200, 20);
        _sliderValue = 0.5f;
    }

    public void Update(GameTime gameTime)
    {
        MouseState mouseState = Mouse.GetState();
        if (_newGameButton.Contains(mouseState.X, mouseState.Y) && mouseState.LeftButton == ButtonState.Pressed)
        {
            sceneManager.AddScene(new GameScene(contentManager, sceneManager));
        }

        if (_volumeSlider.Contains(mouseState.X, mouseState.Y) && mouseState.LeftButton == ButtonState.Pressed)
        {
            _sliderValue = (mouseState.X - _volumeSlider.X) / (float)_volumeSlider.Width;
        }

        MediaPlayer.Volume = _sliderValue;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_backgroundTexture, Vector2.Zero, Color.White);
        spriteBatch.Draw(_newGameButton, Color.White);
        spriteBatch.Draw(_volumeSlider, Color.White);
    }
}

public static class SpriteBatchExtensions
{
    public static void Draw(this SpriteBatch spriteBatch, Rectangle rectangle, Color color)
    {
        spriteBatch.Draw(new Texture2D(spriteBatch.GraphicsDevice, 1, 1), rectangle, color);
    }
}