using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Knight_survival
{
    internal partial class Monster
    {
        private List<Rectangle> SliceSpriteSheet(Texture2D spritesheet, int frameWidth, int frameHeight, int sliceWidth, int sliceHeight)
        {
            List<Rectangle> frames = new List<Rectangle>();
            int rows = spritesheet.Height / frameHeight;
            int columns = spritesheet.Width / frameWidth;

            for (int y = 0; y < rows; y++)
                for (int x = 0; x < columns; x++)
                {
                    int frameX = x * frameWidth + (frameWidth - sliceWidth) / 2;
                    int frameY = y * frameHeight + frameHeight - sliceHeight;
                    Rectangle frame = new Rectangle(frameX, frameY, sliceWidth, sliceHeight);
                    frames.Add(frame);
                }

            return frames;
        }

    }
}