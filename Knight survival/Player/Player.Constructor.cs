using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Knight_survival
{
    internal partial class Player : Sprite
    {
        public Player(Texture2D idleSpritesheet, Texture2D runSpritesheet, Texture2D attackSpritesheet, Vector2 position, List<Sprite> collisionGroup, float frameTime, int attackFrames) : base(idleSpritesheet, position, 10)
        {
            this.idleSpritesheet = idleSpritesheet;
            this.runSpritesheet = runSpritesheet;
            this.attackSpritesheet = attackSpritesheet;
            this.collisionGroup = collisionGroup;
            this.frameTime = frameTime;
            this.attackFrames = attackFrames;
            frameRectangles = SliceSpriteSheet(idleSpritesheet, 120);
            timeSinceLastFrame = 0;
            currentFrame = 0;
        }
    }
}