using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Knight_survival
{
    internal partial class Monster
    {
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!IsAlive && fadeEffect <= 0) return; // Do not draw if fully faded

            Color color = new Color(1f, 1f, 1f, fadeEffect); // Apply fading by adjusting alpha
            Rectangle sourceRectangle = frameRectangles[currentFrame];
            spriteBatch.Draw(texture, position, sourceRectangle, color, 0f, Vector2.Zero, 1f, spriteEffect, 0f);
        }
    }
}