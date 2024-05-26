
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Knight_survival.GameScene
{
    internal partial class GameScene
    {
        private ContentManager contentManager { get; set; }
        private SceneManager sceneManager { get; set; }
        private GraphicsDeviceManager graphics { get; set; }
        List<Sprite> sprites { get; set; } = new();
        internal Player player { get; set; }
        internal List<Monster> monsters { get; set; } = new();
        Texture2D whitePixelTexture { get; set; }
        Texture2D backgroundTexture { get; set; }
        Texture2D skeletonTexture { get; set; }
        Texture2D skeletonDeathTexture { get; set; }
        Texture2D eyeTexture { get; set; }
        Texture2D goblinTexture { get; set; }
        Texture2D mushroomTexture { get; set; }
        Texture2D idleSpritesheet { get; set; }
        Texture2D runSpritesheet { get; set; }
        Texture2D attackSpritesheet { get; set; }
        internal double spawnTimer { get; set; } = 0;
        private Random random { get; set; } = new Random();
        private SpriteFont font { get; set; }
        internal int killedEnemies { get; set; } = 0;
        private double totalTime { get; set; } = 0;
        bool isUpgradeMenuOpen { get; set; } = false;
        List<string> upgradeOptions = new List<string>() { "Increase HP", "Increase Damage", "Increase Speed" };
        int selectedOption { get; set; } = 0;
        private double lastInputTime { get; set; } = 0;
        private double inputDelay { get; set; } = 0.2;
        SoundEffect swordStrike { get; set; }
        internal bool isGameOver { get; set; } = false;
        private float gameOverFade { get; set; } = 0.0f; // Начальное значение полной непрозрачности
        private bool isPaused = false;
        private bool lastPausePressed = false;
        int rageMeter { get; set; } = 0;
        internal double spawnInterval = 5.0; // начальный интервал в секундах
        private double minSpawnInterval = 2.0; // минимальный интервал в секундах
        private int upgradeMeter = 0;
        private int upgradeThreshold = 1; // Количество убийств для активации прокачки
    }
}