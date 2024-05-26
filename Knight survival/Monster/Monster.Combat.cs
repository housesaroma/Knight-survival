using Microsoft.Xna.Framework;

namespace Knight_survival
{
    
    internal partial class Monster
    {
        public void AttackPlayer(Player player, GameTime gameTime)
        {
            if (!IsAlive) return;

            double currentTime = gameTime.TotalGameTime.TotalSeconds;
            if (currentTime - lastAttackTime > attackCooldown)
            {
                Rectangle monsterHitbox = new Rectangle((int)position.X - 10, (int)position.Y - 10, Rect.Width + 20, Rect.Height + 15);
                if (monsterHitbox.Intersects(player.Rect))
                {
                    player.Health -= Attack;
                    lastAttackTime = currentTime;
                }
            }
        }
    }
}