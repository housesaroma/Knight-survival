using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Knight_survival
{
    internal partial class Player : Sprite
    {
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            float changeX = 0;
            float changeY = 0;
            bool isMoving = false;
            isAttacking = false;

            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
            {
                IsShiftPressed = true;
            }
            else { IsShiftPressed = false; }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                if (IsShiftPressed)
                    changeX += Speed + 0.5f;
                changeX += Speed;
                isMoving = true;
                isAttacking = false;
                spriteEffect = SpriteEffects.None;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                if (IsShiftPressed)
                    changeX -= Speed + 0.5f;
                changeX -= Speed;
                isMoving = true;
                isAttacking = false;
                spriteEffect = SpriteEffects.FlipHorizontally;
            }

            position.X += changeX;

            foreach (var sprite in collisionGroup)
                if (sprite != this)
                    if (sprite.Rect.Intersects(Rect))
                        position.X -= changeX;

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                if (IsShiftPressed)
                    changeY -= Speed + 0.5f;
                changeY -= Speed;
                isMoving = true;
                isAttacking = false;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                if (IsShiftPressed)
                    changeY += Speed + 0.5f;
                changeY += Speed;
                isMoving = true;
                isAttacking = false;
            }

            position.Y += changeY;

            foreach (var sprite in collisionGroup)
                if (sprite != this)
                    if (sprite.Rect.Intersects(Rect))
                        position.Y -= changeY;

            if (Mouse.GetState().LeftButton == ButtonState.Pressed && !isMoving)
                isAttacking = true;

            if (isMoving)
                texture = runSpritesheet;
            else if (isAttacking)
            {
                UpdateFrame(gameTime, attackFrames);
                texture = attackSpritesheet;
            }
            else { texture = idleSpritesheet; }
        }

        public void UpdateFrame(GameTime gameTime, int framesCount)
        {
            if (currentFrame == 0) // Предполагается, что 0 - это начало цикла анимации
            {
                hasAttacked = false;
            }
            timeSinceLastFrame += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timeSinceLastFrame >= frameTime)
            {
                currentFrame = (currentFrame + 1) % framesCount;
                timeSinceLastFrame = 0;
            }
        }
    }
}