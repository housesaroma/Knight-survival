using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Knight_survival
{

    internal class Skeleton : Monster
    {
        public Skeleton(Texture2D idleSpritesheet, List<Sprite> collisionGroup, Vector2 position, Vector2 playerPosition)
            : base(idleSpritesheet, collisionGroup, position, playerPosition, totalFrames: 6, frameTime: 0.1f, health: 10,maxHealth: 10, attack: 5, speed: 0.8f)
        {
        }
    }
}
