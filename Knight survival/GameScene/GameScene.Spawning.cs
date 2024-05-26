
using System;
using System.Linq;
using Microsoft.Xna.Framework;

namespace Knight_survival.GameScene
{
    internal partial class GameScene
    {
        private void SpawnMonsters()
        {
            Monster monsterToSpawn = ChooseMonsterToSpawn();
            if (monsterToSpawn != null)
            {
                monsters.Add(monsterToSpawn);
                sprites.Add(monsters.Last());
            }
        }

        private Monster ChooseMonsterToSpawn()
        {
            double timeFactor = totalTime / 60; // Делим общее время на 60, чтобы получить фактор изменения в зависимости от времени игры
            int mushroomWeight = Math.Max(0, 100 - (int)(timeFactor * 50)); // Вес для грибов уменьшается со временем
            int eyeWeight = (int)Math.Max(0, Math.Min(50, timeFactor * 25)); // Вес для глаз начинает увеличиваться и достигает максимума в 50
            int goblinWeight = (int)Math.Max(0, Math.Min(50, (timeFactor - 0.5) * 25)); // Вес для гоблинов начинает увеличиваться после 30 сек
            int skeletonWeight = (int)Math.Max(0, Math.Min(50, (timeFactor - 1) * 25)); // Вес для скелетов начинает увеличиваться после первой минуты
            Vector2 randomSpawnPoint = GetRandomSpawnPoint();
            int totalWeight = mushroomWeight + eyeWeight + goblinWeight + skeletonWeight;
            if (totalWeight == 0) return null;

            int randomNumber = random.Next(totalWeight);
            if (randomNumber < mushroomWeight)
                return new Mushroom(mushroomTexture, sprites, randomSpawnPoint, player.position);
            else if (randomNumber < mushroomWeight + eyeWeight)
                return new Eye(eyeTexture, sprites, randomSpawnPoint, player.position);
            else if (randomNumber < mushroomWeight + eyeWeight + goblinWeight)
                return new Goblin(goblinTexture, sprites, randomSpawnPoint, player.position);
            else
                return new Skeleton(skeletonTexture, sprites, randomSpawnPoint, player.position);
        }

        private Vector2 GetRandomSpawnPoint()
        {
            Vector2 randomSpawnPoint = new Vector2(random.Next(0, graphics.PreferredBackBufferWidth), random.Next(0, graphics.PreferredBackBufferHeight));
            foreach (var sprite in sprites)
            {
                while (Math.Abs(randomSpawnPoint.X - sprite.position.X) < 200 || Math.Abs(randomSpawnPoint.Y - sprite.position.Y) < 200)
                    randomSpawnPoint = new Vector2(random.Next(0, graphics.PreferredBackBufferWidth), random.Next(0, graphics.PreferredBackBufferHeight));
            }
            return randomSpawnPoint;
        }
    }
}