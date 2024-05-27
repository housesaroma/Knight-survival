using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Knight_survival.GameScene
{
    internal partial class GameScene : IScene
    {
        private async void RageAttack()
        {
            var sortedMonsters = monsters.OrderBy(m => Vector2.Distance(player.position, m.position)).ToList();
            foreach (var monster in sortedMonsters)
            {
                if (!monster.IsAlive) continue;
                monster.IsAlive = false;
                await Task.Delay(500); // Задержка перед уничтожением следующего монстра
                sprites.Remove(monster);
                monsters.Remove(monster);
                killedEnemies++;
                upgradeMeter++;
                if (upgradeMeter >= upgradeThreshold)
                {
                    isUpgradeMenuOpen = true;
                    upgradeMeter = 0;
                }
            }
            rageMeter = 0;
        }
        private void TakeDamage()
        {
            if (player.currentFrame == 2 && !player.hasAttacked)
            {
                swordStrike.Play();
                foreach (var monster in monsters.ToList())
                {
                    if (!monster.IsAlive) continue;
                    Rectangle playerHitbox = new Rectangle((int)player.position.X - 10, (int)player.position.Y - 10, player.Rect.Width + 20, player.Rect.Height + 15);
                    Rectangle monsterHitbox = new Rectangle((int)monster.position.X - 10, (int)monster.position.Y - 10, monster.Rect.Width + 20, monster.Rect.Height + 15);
                    if (playerHitbox.Intersects(monsterHitbox))
                    {
                        monster.Health -= player.Damage;
                        player.hasAttacked = true;
                        if (monster.Health <= 0)
                        {
                            upgradeMeter++;
                            rageMeter = Math.Min(5, rageMeter+1);
                            monster.IsAlive = false;
                            Task.Delay(1000).ContinueWith(t =>
                            {
                                sprites.Remove(monster);
                                monsters.Remove(monster);
                            });
                            killedEnemies++;
                            if (upgradeMeter >= upgradeThreshold)
                            {
                                isUpgradeMenuOpen = true;
                            }
                        }
                    }
                }
            }
        }

        private void RestartGame()
        {
            player.Health = 10; // или другое начальное значение
            player.Damage = 1;
            player.Speed = 1f;
            killedEnemies = 0;
            totalTime = 0;
            spawnInterval = 5.0; // Сброс интервала спавна к начальному значению
            minSpawnInterval = 2.0; // Можно также сбросить, если это значение может изменяться в игре
            rageMeter = 0; // Сброс рейд шкалы
            isGameOver = false;
            gameOverFade = 0;
            upgradeMeter = 0; 
            upgradeThreshold = 1;
            monsters.Clear();
            sprites.Clear();
            sprites.Add(player);
            InitializeMonsters();
        }

        public void CheckGameOver(SpriteBatch spriteBatch)
        {
            if (player.Health <= 0)
            {
                isGameOver = true;
                string gameOverText = "Game Over";
                string restartText = "Press R to Restart";
                string enemiesText = $"Killed Enemies: {killedEnemies}";
                string timeText = $"Survived Time: {totalTime:F2} sec";
                Vector2 centerScreen = new Vector2(graphics.PreferredBackBufferWidth / 2 - 150, graphics.PreferredBackBufferHeight / 2 - 100);

                spriteBatch.Draw(backgroundTexture, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), new Color(0, 0, 0, gameOverFade));
                spriteBatch.DrawString(font, gameOverText, centerScreen, Color.Red * gameOverFade);
                spriteBatch.DrawString(font, enemiesText, centerScreen + new Vector2(0, 30), Color.White * gameOverFade);
                spriteBatch.DrawString(font, timeText, centerScreen + new Vector2(0, 60), Color.White * gameOverFade);
                spriteBatch.DrawString(font, restartText, centerScreen + new Vector2(0, 90), Color.Yellow * gameOverFade);
            }
        }
    }
}