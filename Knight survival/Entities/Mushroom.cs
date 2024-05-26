using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Knight_survival
{
    internal class Mushroom : Monster
    {
        public Mushroom(Texture2D idleSpritesheet, List<Sprite> collisionGroup, Vector2 position, Vector2 playerPosition)
            : base(idleSpritesheet, collisionGroup, position, playerPosition, totalFrames: 11, frameTime: 0.1f, health: 3,maxHealth: 3, attack: 1)
        {
        }
    }
}
