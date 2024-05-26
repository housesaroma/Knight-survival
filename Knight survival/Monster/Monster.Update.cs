using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Knight_survival
{
    internal partial class Monster
    {
        public override void Update(GameTime gameTime)
        {

            if (!IsAlive)
            {
                fadeEffect -= (float)gameTime.ElapsedGameTime.TotalSeconds; // Decrease opacity over 1 second
                if (fadeEffect < 0) fadeEffect = 0; // Ensure it does not go below 0
                return;
            }

            base.Update(gameTime);
            float speed = 1;
            Vector2 direction = playerPosition - position;
            direction.Normalize();
            float changeX = 0;
            float changeY = 0;

            changeX += direction.X * speed;

            position.X += changeX;
            foreach (var sprite in collisionGroup)
                if (sprite != this)
                    if (sprite.Rect.Intersects(Rect))
                        position.X -= changeX;

            changeY += direction.Y * speed;
            position.Y += changeY;
            foreach (var sprite in collisionGroup)
                if (sprite != this)
                    if (sprite.Rect.Intersects(Rect))
                        position.Y -= changeY;

            if (direction.X > 0)
                spriteEffect = SpriteEffects.None;
            else if (direction.X < 0)
                spriteEffect = SpriteEffects.FlipHorizontally;

            texture = runSpritesheet;
        }

        public void UpdateFrame(GameTime gameTime)
        {
            timeSinceLastFrame += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timeSinceLastFrame >= frameTime)
            {
                currentFrame = (currentFrame + 1) % totalFrames;
                timeSinceLastFrame = 0;
            }
        }

        public void UpdatePlayerPosition(Vector2 newPosition)
        {
            playerPosition = newPosition;
        }
    }
}