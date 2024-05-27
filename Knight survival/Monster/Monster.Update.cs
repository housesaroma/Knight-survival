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
        fadeEffect -= (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (fadeEffect < 0) fadeEffect = 0;
        return;
    }

    base.Update(gameTime);
    Vector2 direction = playerPosition - position;
    direction.Normalize();
    float distance = Vector2.Distance(playerPosition, position);
    float accelerationThreshold = 300; // Set this to whatever makes sense for your game
    float speedMultiplier = 1.0f;

    if (distance > accelerationThreshold)
    {
        speedMultiplier = 1.0f + (distance - accelerationThreshold) / accelerationThreshold*0.3f; // Linear interpolation factor
    }

    float changeX = direction.X * Speed * speedMultiplier;
    float changeY = direction.Y * Speed * speedMultiplier;

    position.X += changeX;
    foreach (var sprite in collisionGroup)
        if (sprite != this && sprite.Rect.Intersects(Rect))
            position.X -= changeX;

    position.Y += changeY;
    foreach (var sprite in collisionGroup)
        if (sprite != this && sprite.Rect.Intersects(Rect))
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