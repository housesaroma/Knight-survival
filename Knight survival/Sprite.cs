using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Knight_survival
{
    internal class Sprite
    {
        private static readonly float SCALE = 1f;

        public Texture2D texture;
        public Vector2 position;
        public int frameRate;

        public Rectangle Rect
        {
            get
            {
                return new Rectangle(
                    (int)position.X,
                    (int)position.Y,
                    texture.Width * (int)SCALE/frameRate-100,
                    texture.Height * (int)SCALE-70
                    );
            }
        }

        public Sprite(Texture2D texture, Vector2 position, int frameRate)
        {
            this.texture = texture;
            this.position = position;
            this.frameRate = frameRate;
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Rect, Color.White);
        }
    }
}
