using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Knight_survival
{
    internal partial class Player : Sprite
    {
        private List<Rectangle> SliceSpriteSheet(Texture2D spritesheet, int frameWidth)
        {
            List<Rectangle> frames = new List<Rectangle>();
            int columns = spritesheet.Width / frameWidth;

            for (int x = 0; x < columns; x++)
            {
                int frameX = x * frameWidth;
                Rectangle frame = new Rectangle(frameX, 0, frameWidth, spritesheet.Height);
                frames.Add(frame);
            }

            return frames;
        }
    }
}