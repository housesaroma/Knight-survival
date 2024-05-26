
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace Knight_survival.GameScene
{
    internal partial class GameScene
    {
        private void LoadTextures()
        {
            idleSpritesheet = contentManager.Load<Texture2D>("_Idle");
            runSpritesheet = contentManager.Load<Texture2D>("_Run");
            attackSpritesheet = contentManager.Load<Texture2D>("_Attack2");

            skeletonTexture = contentManager.Load<Texture2D>("skeleton");
            eyeTexture = contentManager.Load<Texture2D>("eye");
            goblinTexture = contentManager.Load<Texture2D>("goblin");
            mushroomTexture = contentManager.Load<Texture2D>("mushroom");

            backgroundTexture = contentManager.Load<Texture2D>("background");
            whitePixelTexture = contentManager.Load<Texture2D>("whitePixel");
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
    }
}