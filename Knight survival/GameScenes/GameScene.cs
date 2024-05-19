using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Knight_survival.GameScenes;

public class GameScene : IScene
{
    private ContentManager contentManager;
    List<Sprite> sprites { get; set; } = new();
    Player player { get; set; }
    List<Monster> monsters { get; set; } = new();
    Texture2D backgroundTexture { get; set; }
    Texture2D skeletonTexture { get; set; }
    Texture2D skeletonDeathTexture { get; set; }
    Texture2D eyeTexture { get; set; }
    Texture2D eyeDeathTexture { get; set; }
    Texture2D goblinTexture { get; set; }
    Texture2D goblinDeathTexture { get; set; }
    Texture2D mushroomTexture { get; set; }
    Texture2D mushroomDeathTexture { get; set; }
    Texture2D idleSpritesheet { get; set; }
    Texture2D runSpritesheet { get; set; }
    Texture2D attackSpritesheet { get; set; }
    private double spawnTimer { get; set; } = 0;
    private Random random { get; set; } = new Random();
    private SpriteFont font { get; set; }
    int killedEnemies { get; set; } = 0;
    private double totalTime { get; set; } = 0;

    bool isUpgradeMenuOpen = false;
    List<string> upgradeOptions = new List<string>() { "Increase HP", "Increase Damage", "Increase Speed" };
    int selectedOption = 0;
    private double lastInputTime = 0;
    private double inputDelay = 0.2;
    SoundEffect swordStrike;

    public GameScene(ContentManager contentManager)
    {
        this.contentManager = contentManager;
    }

    private void LoadTextures()
    {
        idleSpritesheet = contentManager.Load<Texture2D>("_Idle");
        runSpritesheet = contentManager.Load<Texture2D>("_Run");
        attackSpritesheet = contentManager.Load<Texture2D>("_Attack2");

        skeletonTexture = contentManager.Load<Texture2D>("skeleton");
        skeletonDeathTexture = contentManager.Load<Texture2D>("skeleton_death");

        eyeTexture = contentManager.Load<Texture2D>("eye");
        eyeDeathTexture = contentManager.Load<Texture2D>("eye_death");

        goblinTexture = contentManager.Load<Texture2D>("goblin");
        goblinDeathTexture = contentManager.Load<Texture2D>("goblin_death");

        mushroomTexture = contentManager.Load<Texture2D>("mushroom");
        mushroomDeathTexture = contentManager.Load<Texture2D>("mushroom_death");

        backgroundTexture = contentManager.Load<Texture2D>("background");
    }

    private void LoadFonts()
    {
        font = contentManager.Load<SpriteFont>("Fonts/textfont");
    }

    private void InitializeGameObjects()
    {
        player = new Player(idleSpritesheet, runSpritesheet, attackSpritesheet, new Vector2(500, 500), sprites, 0.1f, 6);
        InitializeMonsters();
        sprites.Add(player);
    }

    private void InitializeMonsters()
    {
        foreach (var monster in monsters)
            sprites.Add(monster);
    }

    private void LoadAudio()
    {
        swordStrike = contentManager.Load<SoundEffect>("Audio/swordStrike");
    }

    public void Load()
    {
        LoadFonts();
        LoadTextures();
        LoadAudio();
        InitializeGameObjects();
    }

    private void UpdateGameTimers(GameTime gameTime)
    {
        spawnTimer += gameTime.ElapsedGameTime.TotalSeconds;
        totalTime += gameTime.ElapsedGameTime.TotalSeconds;
    }

    private void SpawnMonsters()
    {
        if (spawnTimer >= 5)
        {
            spawnTimer = 0;
            int monsterType = random.Next(4); // Generates a random number between 0 and 3
            Vector2 randomSpawnPoint = GetRandomSpawnPoint();

            switch (monsterType)
            {
                case 0:
                    monsters.Add(new Skeleton(skeletonTexture, skeletonDeathTexture, sprites, randomSpawnPoint, player.position));
                    break;
                case 1:
                    monsters.Add(new Goblin(goblinTexture, goblinDeathTexture, sprites, randomSpawnPoint, player.position));
                    break;
                case 2:
                    monsters.Add(new Eye(eyeTexture, eyeDeathTexture, sprites, randomSpawnPoint, player.position));
                    break;
                case 3:
                    monsters.Add(new Mushroom(mushroomTexture, mushroomDeathTexture, sprites, randomSpawnPoint, player.position));
                    break;
            }
            sprites.Add(monsters.Last());
        }
    }

    private Vector2 GetRandomSpawnPoint()
    {
        Vector2 randomSpawnPoint = new Vector2(random.Next(0, 850), random.Next(0, 850));
        foreach (var sprite in sprites)
        {
            while (Math.Abs(randomSpawnPoint.X - sprite.position.X) < 200 || Math.Abs(randomSpawnPoint.Y - sprite.position.Y) < 200)
                randomSpawnPoint = new Vector2(random.Next(0, 850), random.Next(0, 850));
        }
        return randomSpawnPoint;
    }

    private void TakeDamage()
    {
        if (player.currentFrame == 2 && !player.hasAttacked)
        {
            swordStrike.Play();
            foreach (var monster in monsters.ToList())
            {
                Console.WriteLine(monster.Health);
                if (!monster.IsAlive) continue;
                Rectangle playerHitbox = new Rectangle((int)player.position.X - 10, (int)player.position.Y - 10, player.Rect.Width + 20, player.Rect.Height + 15);
                Rectangle monsterHitbox = new Rectangle((int)monster.position.X - 10, (int)monster.position.Y - 10, monster.Rect.Width + 20, monster.Rect.Height + 15);
                if (playerHitbox.Intersects(monsterHitbox))
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
        UpdateUpgradeMenu(gameTime);

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

    private void UpdateUpgradeMenu(GameTime gameTime)
    {
        if (!isUpgradeMenuOpen) return;

        double currentTime = gameTime.TotalGameTime.TotalSeconds;
        if (Keyboard.GetState().IsKeyDown(Keys.Up) && currentTime - lastInputTime > inputDelay)
        {
            selectedOption = Math.Max(0, selectedOption - 1);
            lastInputTime = currentTime;
        }
        if (Keyboard.GetState().IsKeyDown(Keys.Down) && currentTime - lastInputTime > inputDelay)
        {
            selectedOption = Math.Min(upgradeOptions.Count - 1, selectedOption + 1);
            lastInputTime = currentTime;
        }
        if (Keyboard.GetState().IsKeyDown(Keys.Enter) && currentTime - lastInputTime > inputDelay)
        {
            ApplyUpgrade(selectedOption);
            isUpgradeMenuOpen = false;
            lastInputTime = currentTime;
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
            case 2: // Increase Speed
                player.Speed += 0.5f; // Adjust the value based on your game's balance
                break;
        }
    }

    public void Update(GameTime gameTime)
    {
        UpdateGameTimers(gameTime);
        SpawnMonsters();

        if (player.isAttacking)
            TakeDamage();

        UpdateSprites(gameTime);
    }

    private void DrawUpgradeMenu(SpriteBatch spriteBatch)
    {
        if (!isUpgradeMenuOpen) return;

        Vector2 menuPosition = new Vector2(300, 300);
        spriteBatch.DrawString(font, "Choose an upgrade:", menuPosition, Color.Yellow);
        for (int i = 0; i < upgradeOptions.Count; i++)
        {
            Color textColor = i == selectedOption ? Color.Red : Color.White;
            spriteBatch.DrawString(font, upgradeOptions[i], menuPosition + new Vector2(0, 30 * (i + 1)), textColor);
        }
    }

    private void DrawBackground(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(backgroundTexture, Vector2.Zero, Color.White);
    }

    private void DrawUI(SpriteBatch spriteBatch)
    {
        spriteBatch.DrawString(font, "Killed Enemies: " + killedEnemies, Vector2.Zero, Color.White);

        string timeText = $"Time: {totalTime:F2} sec";
        Vector2 timePosition = new Vector2(850 - 200, 0);
        spriteBatch.DrawString(font, timeText, timePosition, Color.White);
    }

    private void DrawSprites(SpriteBatch spriteBatch)
    {
        foreach (var sprite in sprites)
        {
            sprite.Draw(spriteBatch);
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        DrawBackground(spriteBatch);
        DrawUI(spriteBatch);
        DrawSprites(spriteBatch);
        DrawUpgradeMenu(spriteBatch);
    }
}
