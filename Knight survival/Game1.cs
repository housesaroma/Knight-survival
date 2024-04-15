using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Knight_survival
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        List<Sprite> sprites;
        Player player;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            sprites = new();

            // TODO: use this.Content to load your game content here
            Texture2D playerTexture = Content.Load<Texture2D>("player");
            Texture2D monsterTexture = Content.Load<Texture2D>("monster");

            sprites.Add(new Sprite(monsterTexture, new Vector2(100, 100)));
            sprites.Add(new Sprite(monsterTexture, new Vector2(400, 200)));
            sprites.Add(new Sprite(monsterTexture, new Vector2(700, 300)));

            player = new Player(playerTexture, Vector2.Zero);

            sprites.Add(player);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
            }

            if (Keyboard.GetState().IsKeyUp(Keys.Space))
            {
            }

            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
            }

            foreach (var sprite in sprites)
            {
                sprite.Update(gameTime);
            }

            player.Update(gameTime, sprites);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            foreach (var sprite in sprites)
            {
                sprite.Draw(_spriteBatch);
            }

            player.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
