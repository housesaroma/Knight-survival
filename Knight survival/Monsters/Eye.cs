using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Knight_survival
{
    internal class Eye : Monster
    {
        public Eye(Texture2D idleSpritesheet, Texture2D runSpritesheet, List<Sprite> collisionGroup, Vector2 position, Vector2 playerPosition)
            : base(idleSpritesheet, runSpritesheet, collisionGroup, position, playerPosition, totalFrames: 6, frameTime: 0.1f, health: 5, attack: 1)
        {
        }
    }
}
