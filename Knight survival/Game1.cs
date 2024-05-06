using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Knight_survival
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        List<Sprite> sprites;
        Player player;
        List<Monster> monsters;
        Texture2D backgroundTexture;
        Texture2D monsterTexture;
        private double spawnTimer = 0;
        private Random random = new Random();
        int killedEnemies = 0;

        public Game1()
        {
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
            // TODO: Add your initialization logic here
            sprites = new();
            monsters = new();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            Texture2D idleSpritesheet = Content.Load<Texture2D>("_Idle");
            Texture2D runSpritesheet = Content.Load<Texture2D>("_Run");
            Texture2D attackSpritesheet = Content.Load<Texture2D>("_Attack2");
            monsterTexture = Content.Load<Texture2D>("skeleton");
            backgroundTexture = Content.Load<Texture2D>("background");

            player = new Player(idleSpritesheet, runSpritesheet, attackSpritesheet, new Vector2(500, 500), sprites, 10, 0.1f, 6);
            monsters.Add(new Monster(monsterTexture, monsterTexture, sprites, new Vector2(100, 100), player.position, 6, 0.1f));
            monsters.Add(new Monster(monsterTexture, monsterTexture, sprites, new Vector2(200, 500), player.position, 6, 0.1f));

            foreach (var monster in monsters)
            {
                sprites.Add(monster);
            }
            sprites.Add(player);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            spawnTimer += gameTime.ElapsedGameTime.TotalSeconds;

            if (spawnTimer >= 2)
            {
                spawnTimer = 0;
                Vector2 randomSpawnPoint = new Vector2(random.Next(0, _graphics.PreferredBackBufferWidth), random.Next(0, _graphics.PreferredBackBufferHeight));

                foreach (var sprite in sprites)
                {
                    while (Math.Abs(randomSpawnPoint.X - sprite.position.X) < 200 || Math.Abs(randomSpawnPoint.Y - sprite.position.X) < 200)
                        randomSpawnPoint = new Vector2(random.Next(0, _graphics.PreferredBackBufferWidth), random.Next(0, _graphics.PreferredBackBufferHeight));
                }

                monsters.Add(new Monster(monsterTexture, monsterTexture, sprites, randomSpawnPoint, player.position, 6, 0.1f));
                sprites.Add(monsters[monsters.Count - 1]);
            }

            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                Rectangle playerHitbox = new Rectangle((int)player.position.X-10, (int)player.position.Y-10, player.Rect.Width + 10, player.Rect.Height + 5);

                foreach (var monster in monsters)
                {
                    Rectangle monsterHitbox = new Rectangle((int)monster.position.X-10, (int)monster.position.Y-10, monster.Rect.Width + 10, monster.Rect.Height + 5);

                    if (playerHitbox.Intersects(monsterHitbox))
                    {
                        sprites.Remove(monster);
                        killedEnemies++;
                    }
                }
            }

            if (sprites.Count > 0)
                foreach (var sprite in sprites)
                {
                    sprite.Update(gameTime);
                }

            foreach (var monster in monsters)
            {
                monster.UpdatePlayerPosition(new Vector2(player.position.X - 15, player.position.Y - 10));
                monster.UpdateFrame(gameTime);
            }

            player.UpdateFrame(gameTime, 6);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            _spriteBatch.Draw(backgroundTexture, Vector2.Zero, Color.White);

            _spriteBatch.DrawString(Content.Load<SpriteFont>("Font"), "Killed Enemies: " + killedEnemies, new Vector2(_graphics.PreferredBackBufferWidth - 200, 10), Color.White);

            foreach (var sprite in sprites)
            {
                sprite.Draw(_spriteBatch);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
