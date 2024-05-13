using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Knight_survival
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics { get; set; }
        private SpriteBatch _spriteBatch { get; set; }
        List<Sprite> sprites { get; set; }
        Player player { get; set; }
        List<Monster> monsters { get; set; }
        Texture2D backgroundTexture { get; set; }
        Texture2D monsterTexture { get; set; }
        Texture2D idleSpritesheet { get; set; }
        Texture2D runSpritesheet { get; set; }
        Texture2D attackSpritesheet { get; set; }
        private double spawnTimer { get; set; } = 0;
        private Random random { get; set; } = new Random();
        private SpriteFont font { get; set; }
        int killedEnemies { get; set; } = 0;
        private double totalTime { get; set; } = 0;

        bool isUpgradeMenuOpen = false;
        List<string> upgradeOptions = new List<string>() { "Increase HP", "Increase Damage" };
        int selectedOption = 0;

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
            sprites = new();
            monsters = new();

            base.Initialize();
        }

        private void LoadTextures()
        {
            idleSpritesheet = Content.Load<Texture2D>("_Idle");
            runSpritesheet = Content.Load<Texture2D>("_Run");
            attackSpritesheet = Content.Load<Texture2D>("_Attack2");
            monsterTexture = Content.Load<Texture2D>("skeleton");
            backgroundTexture = Content.Load<Texture2D>("background");
        }

        private void LoadFonts()
        {
            font = Content.Load<SpriteFont>("Fonts/textfont");
        }

        private void InitializeGameObjects()
        {
            player = new Player(idleSpritesheet, runSpritesheet, attackSpritesheet, new Vector2(500, 500), sprites, 0.1f, 6);
            InitializeMonsters();
            sprites.Add(player);
        }

        private void InitializeMonsters()
        {
            monsters.Add(new Monster(monsterTexture, monsterTexture, sprites, new Vector2(100, 100), player.position, 6, 0.1f));
            monsters.Add(new Monster(monsterTexture, monsterTexture, sprites, new Vector2(200, 500), player.position, 6, 0.1f));
            foreach (var monster in monsters)
                sprites.Add(monster);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            LoadFonts();
            LoadTextures();
            InitializeGameObjects();
        }

        private void UpdateGameTimers(GameTime gameTime)
        {
            spawnTimer += gameTime.ElapsedGameTime.TotalSeconds;
            totalTime += gameTime.ElapsedGameTime.TotalSeconds;
        }

        private void SpawnMonsters()
        {
            if (spawnTimer >= 10)
            {
                spawnTimer = 0;
                Vector2 randomSpawnPoint = GetRandomSpawnPoint();
                monsters.Add(new Monster(monsterTexture, monsterTexture, sprites, randomSpawnPoint, player.position, 6, 0.1f));
                sprites.Add(monsters.Last());
            }
        }

        private Vector2 GetRandomSpawnPoint()
        {
            Vector2 randomSpawnPoint = new Vector2(random.Next(0, _graphics.PreferredBackBufferWidth), random.Next(0, _graphics.PreferredBackBufferHeight));
            foreach (var sprite in sprites)
            {
                while (Math.Abs(randomSpawnPoint.X - sprite.position.X) < 200 || Math.Abs(randomSpawnPoint.Y - sprite.position.Y) < 200)
                    randomSpawnPoint = new Vector2(random.Next(0, _graphics.PreferredBackBufferWidth), random.Next(0, _graphics.PreferredBackBufferHeight));
            }
            return randomSpawnPoint;
        }

        private void TakeDamage()
        {
            if (player.currentFrame == 2 && !player.hasAttacked)
            {
                foreach (var monster in monsters.ToList())
                {
                    Console.WriteLine(monster.Health);
                    if (!monster.IsAlive) continue;
                    Rectangle monsterHitbox = new Rectangle((int)monster.position.X - 10, (int)monster.position.Y - 10, monster.Rect.Width + 20, monster.Rect.Height + 15);
                    if (player.Rect.Intersects(monsterHitbox))
                    {
                        monster.Health -= player.Damage;
                        player.hasAttacked = true;
                        if (monster.Health <= 0)
                        {
                            monster.IsAlive = false;
                            sprites.Remove(monster);
                            monsters.Remove(monster);
                            killedEnemies++;
                            isUpgradeMenuOpen = true;
                        }
                    }
                }
            }
        }

        private void UpdateSprites(GameTime gameTime)
        {
            UpdateUpgradeMenu();

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
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            UpdateGameTimers(gameTime);
            SpawnMonsters();

            if (player.isAttacking)
                TakeDamage();

            UpdateSprites(gameTime);

            base.Update(gameTime);
        }

        private void DrawUpgradeMenu()
        {
            if (!isUpgradeMenuOpen) return;

            Vector2 menuPosition = new Vector2(300, 300);
            _spriteBatch.DrawString(font, "Choose an upgrade:", menuPosition, Color.Yellow);
            for (int i = 0; i < upgradeOptions.Count; i++)
            {
                Color textColor = i == selectedOption ? Color.Red : Color.White;
                _spriteBatch.DrawString(font, upgradeOptions[i], menuPosition + new Vector2(0, 20 * (i + 1)), textColor);
            }
        }

        private void UpdateUpgradeMenu()
        {
            if (!isUpgradeMenuOpen) return;

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                selectedOption = Math.Max(0, selectedOption - 1);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                selectedOption = Math.Min(upgradeOptions.Count - 1, selectedOption + 1);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                ApplyUpgrade(selectedOption);
                isUpgradeMenuOpen = false;
            }
        }

        private void ApplyUpgrade(int option)
        {
            switch (option)
            {
                case 0: // Increase HP
                    player.Health += 10;
                    break;
                case 1: // Increase Damage
                    player.Damage += 1;
                    break;
            }
        }

        private void DrawBackground()
        {
            _spriteBatch.Draw(backgroundTexture, Vector2.Zero, Color.White);
        }

        private void DrawUI()
        {
            _spriteBatch.DrawString(font, "Killed Enemies: " + killedEnemies, Vector2.Zero, Color.White);

            string timeText = $"Time: {totalTime:F2} sec";
            Vector2 timePosition = new Vector2(_graphics.PreferredBackBufferWidth - 200, 0);
            _spriteBatch.DrawString(font, timeText, timePosition, Color.White);
        }

        private void DrawSprites()
        {
            foreach (var sprite in sprites)
            {
                sprite.Draw(_spriteBatch);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            DrawBackground();
            DrawUI();
            DrawSprites();
            DrawUpgradeMenu();

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
