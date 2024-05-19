using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Knight_survival
{
    internal class Eye : Monster
    {
        public Eye(Texture2D idleSpritesheet, Texture2D runSpritesheet, List<Sprite> collisionGroup, Vector2 position, Vector2 playerPosition)
            : base(idleSpritesheet, runSpritesheet, collisionGroup, position, playerPosition, 6, 0.1f, 5)
        {
        }
    }
}
