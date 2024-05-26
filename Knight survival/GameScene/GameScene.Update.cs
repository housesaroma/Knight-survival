
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Knight_survival.GameScene
{
    internal partial class GameScene
    {
        private void UpdateGameTimers(GameTime gameTime)
        {
            totalTime += gameTime.ElapsedGameTime.TotalSeconds;
            spawnTimer += gameTime.ElapsedGameTime.TotalSeconds;

            // Уменьшаем spawnInterval каждые 30 секунд игрового времени, но не меньше minSpawnInterval
            if (totalTime % 10 == 0) // каждые 30 секунд
            {
                spawnInterval = Math.Max(minSpawnInterval, spawnInterval - 0.5); // уменьшаем на 0.5 секунды
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
                monster.AttackPlayer(player, gameTime); // Use the new AttackPlayer method
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
                    player.Health = Math.Min(player.Health + 3, 10);
                    break;
                case 1: // Increase Damage
                    player.Damage += 1;
                    break;
                case 2: // Increase Speed
                    player.Speed += 0.5f; // Adjust the value based on your game's balance
                    break;
            }
            upgradeMeter = 0; // Сброс шкалы прокачки
            upgradeThreshold++;
        }

        public void Update(GameTime gameTime)
        {
            if (isGameOver)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.R))
                {
                    RestartGame();
                    return;
                }
                gameOverFade += (float)gameTime.ElapsedGameTime.TotalSeconds * 0.2f; // Увеличиваем прозрачность
                gameOverFade = Math.Min(gameOverFade, 1); // Убедимся, что значение не превысит 1
                return;
            }

            if (isUpgradeMenuOpen)
            {
                UpdateUpgradeMenu(gameTime);
                return; // Возвращаемся, чтобы не обновлять другие элементы игры
            }

            if (Keyboard.GetState().IsKeyDown(Keys.P))
            {
                if (!lastPausePressed)
                {
                    isPaused = !isPaused;
                    lastPausePressed = true;
                }
            }
            else
            {
                lastPausePressed = false;
            }

            if (isPaused)
                return;

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && rageMeter >= 10)
            {
                RageAttack();
            }

            UpdateGameTimers(gameTime);
            if (spawnTimer >= spawnInterval)
            {
                spawnTimer = 0;
                SpawnMonsters();
            }
            if (player.isAttacking)
                TakeDamage();

            UpdateSprites(gameTime);
        }
    }
}