
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Knight_survival.GameScene
{
    internal partial class GameScene
    {
        private void DrawMonsterHealthBar(SpriteBatch spriteBatch, Monster monster)
        {
            Vector2 healthPosition = new Vector2(monster.position.X + 53, monster.position.Y + 30);
            int healthBarWidth = 30;
            int healthBarHeight = 5;
            int segmentWidth = healthBarWidth / monster.MaxHealth;

            // Draw the background of the health bar
            spriteBatch.Draw(whitePixelTexture, new Rectangle((int)healthPosition.X, (int)healthPosition.Y, healthBarWidth, healthBarHeight), Color.Gray);

            // Draw the filled part of the health bar based on the monster's health
            for (int i = 0; i < monster.Health; i++)
            {
                spriteBatch.Draw(whitePixelTexture, new Rectangle((int)healthPosition.X + i * segmentWidth, (int)healthPosition.Y, segmentWidth, healthBarHeight), Color.Red);
            }
        }

        private void DrawHealthBar(SpriteBatch spriteBatch)
        {
            // Получаем позицию игрока и устанавливаем health bar немного выше его головы
            Vector2 healthPosition = new Vector2(player.position.X + 43, player.position.Y + 30);
            int healthBarWidth = 30;
            int healthBarHeight = 5;
            int segmentWidth = healthBarWidth / 10;

            // Рисуем фон health bar
            spriteBatch.Draw(whitePixelTexture, new Rectangle((int)healthPosition.X, (int)healthPosition.Y, healthBarWidth, healthBarHeight), Color.Gray);

            // Рисуем заполнение health bar в зависимости от текущего здоровья игрока
            for (int i = 0; i < player.Health; i++)
            {
                spriteBatch.Draw(whitePixelTexture, new Rectangle((int)healthPosition.X + i * segmentWidth, (int)healthPosition.Y, segmentWidth, healthBarHeight), Color.Red);
            }
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

        private void DrawRageMeter(SpriteBatch spriteBatch)
        {
            Vector2 ragePosition = new Vector2(140, 80); // Position below the health bar
            int rageBarWidth = 150; // Total width of the rage bar
            int rageBarHeight = 30; // Height of the rage bar
            int segmentWidth = rageBarWidth / 5; // Each segment represents 1 rage point

            Vector2 labelPosition = new Vector2(0, ragePosition.Y - 10); // Adjust X position as needed
            spriteBatch.DrawString(font, "Rage:", labelPosition, Color.White);

            // Draw the background of the rage bar
            spriteBatch.Draw(whitePixelTexture, new Rectangle((int)ragePosition.X, (int)ragePosition.Y, rageBarWidth, rageBarHeight), Color.Gray);

            // Draw the filled part of the rage bar based on the current rage
            for (int i = 0; i < rageMeter; i++)
            {
                spriteBatch.Draw(whitePixelTexture, new Rectangle((int)ragePosition.X + i * segmentWidth, (int)ragePosition.Y, segmentWidth, rageBarHeight), Color.Yellow);
            }
        }

        private void DrawUpgradeMeter(SpriteBatch spriteBatch)
        {
            Vector2 upgradePosition = new Vector2(140, 120); // Позиция ниже шкалы рейджа
            int upgradeBarWidth = 150;
            int upgradeBarHeight = 30;
            int segmentWidth = upgradeBarWidth / upgradeThreshold;

            Vector2 labelPosition = new Vector2(0, upgradePosition.Y - 10); // Adjust X position as needed
            spriteBatch.DrawString(font, "Update:", labelPosition, Color.White);

            spriteBatch.Draw(whitePixelTexture, new Rectangle((int)upgradePosition.X, (int)upgradePosition.Y, upgradeBarWidth, upgradeBarHeight), Color.Gray);
            for (int i = 0; i < upgradeMeter; i++)
            {
                spriteBatch.Draw(whitePixelTexture, new Rectangle((int)upgradePosition.X + i * segmentWidth, (int)upgradePosition.Y, segmentWidth, upgradeBarHeight), Color.Orange);
            }
        }

        private void DrawUI(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, "Killed Enemies: " + killedEnemies, Vector2.Zero, Color.White);
            DrawHealthBar(spriteBatch);
            DrawRageMeter(spriteBatch);
            DrawUpgradeMeter(spriteBatch);

            string timeText = $"Time: {totalTime:F2} sec";
            Vector2 timePosition = new Vector2(graphics.PreferredBackBufferWidth - 300, 0);
            spriteBatch.DrawString(font, timeText, timePosition, Color.White);

            if (isPaused && !isUpgradeMenuOpen) // Проверка, что игра на паузе и окно прокачки не открыто
            {
                Vector2 pausePosition = new Vector2(graphics.PreferredBackBufferWidth / 2-50, graphics.PreferredBackBufferHeight / 2-50);
                spriteBatch.DrawString(font, "Paused", pausePosition, Color.Yellow);
            }
        }

        private void DrawSprites(SpriteBatch spriteBatch)
        {
            foreach (var sprite in sprites)
            {
                sprite.Draw(spriteBatch);
                if (sprite is Monster monster && monster.IsAlive)
                {
                    DrawMonsterHealthBar(spriteBatch, monster);
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            DrawBackground(spriteBatch);
            DrawUI(spriteBatch);
            DrawSprites(spriteBatch);
            DrawUpgradeMenu(spriteBatch);
            CheckGameOver(spriteBatch); // Проверка и отображение состояния Game Over
        }
    }
}