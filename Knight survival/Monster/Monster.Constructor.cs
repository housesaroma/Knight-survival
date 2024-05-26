using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Knight_survival
{
    internal partial class Monster : Sprite
    {
        public Monster(Texture2D runSpritesheet, List<Sprite> collisionGroup, Vector2 position, Vector2 playerPosition,
                int totalFrames, float frameTime, int health, int maxHealth, int attack) : base(runSpritesheet, position, totalFrames)
        {
            this.runSpritesheet = runSpritesheet;
            this.playerPosition = playerPosition;
            this.totalFrames = totalFrames;
            this.frameTime = frameTime;
            this.collisionGroup = collisionGroup;
            Health = health;
            MaxHealth = maxHealth;
            Attack = attack;
            frameRectangles = SliceSpriteSheet(runSpritesheet, 150, 150, 150, 150);
            timeSinceLastFrame = 0;
            currentFrame = 0;
        }
    }
}