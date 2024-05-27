using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Knight_survival
{
    internal class Eye : Monster
    {
        public Eye(Texture2D idleSpritesheet, List<Sprite> collisionGroup, Vector2 position, Vector2 playerPosition)
            : base(idleSpritesheet, collisionGroup, position, playerPosition, totalFrames: 6, frameTime: 0.1f, health: 5,maxHealth: 5, attack: 2, speed: 1.5f)
        {
        }
    }
}
