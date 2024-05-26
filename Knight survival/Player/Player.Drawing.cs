using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Knight_survival
{
    internal partial class Player : Sprite
    {
        public override void Draw(SpriteBatch spriteBatch)
        {
            Rectangle sourceRectangle = frameRectangles[currentFrame];
            spriteBatch.Draw(texture, position, sourceRectangle, Color.White, 0f, Vector2.Zero, 1f, spriteEffect, 0f);
        }
    }
}