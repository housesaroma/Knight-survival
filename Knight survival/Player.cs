using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Knight_survival
{
    internal class Player : Sprite
    {
        List<Sprite> collisionGroup;
        private readonly int totalFrames;
        private readonly int attackFrames;
        private SpriteEffects spriteEffect = SpriteEffects.None;
        private float frameTime;
        private float timeSinceLastFrame;
        private int currentFrame;
        private Texture2D idleSpritesheet;
        private Texture2D runSpritesheet;
        private Texture2D attackSpritesheet;
        private List<Rectangle> frameRectangles;

        public Player(Texture2D idleSpritesheet, Texture2D runSpritesheet, Texture2D attackSpritesheet, Vector2 position, List<Sprite> collisionGroup, int totalFrames, float frameTime, int attackFrames) : base(idleSpritesheet, position, 10)
        {
            this.idleSpritesheet = idleSpritesheet;
            this.runSpritesheet = runSpritesheet;
            this.attackSpritesheet = attackSpritesheet;
            this.collisionGroup = collisionGroup;
            this.totalFrames = totalFrames;
            this.frameTime = frameTime;
            this.attackFrames = attackFrames;
            frameRectangles = SliceSpriteSheet(idleSpritesheet, 120);
            timeSinceLastFrame = 0;
            currentFrame = 0;
        }

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

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            float speed = 2;
            float changeX = 0;
            float changeY = 0;
            bool isMoving = false;
            bool isAttacking = false;

            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                isAttacking = true;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
            {
                speed = 3;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                changeX += speed;
                isMoving = true;
                isAttacking = false;
                spriteEffect = SpriteEffects.None;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                changeX -= speed;
                isMoving = true;
                isAttacking = false;
                spriteEffect = SpriteEffects.FlipHorizontally;
            }

            position.X += changeX;

            foreach (var sprite in collisionGroup)
            {
                if (sprite != this)
                {
                    //Rectangle partOfSprite = new Rectangle(sprite.Rect.X, sprite.Rect.Y, 10, 10);

                    if (sprite.Rect.Intersects(Rect))
                    {
                        position.X -= changeX;
                    }
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                changeY -= speed;
                isMoving = true;
                isAttacking = false;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                changeY += speed;
                isMoving = true;
                isAttacking = false;
            }

            position.Y += changeY;

            foreach (var sprite in collisionGroup)
            {
                if (sprite != this)
                {
                    //Rectangle partOfSprite = new Rectangle(sprite.Rect.X, sprite.Rect.Y, 10, 10);

                    if (sprite.Rect.Intersects(Rect))
                    {
                        position.Y -= changeY;
                    }
                }
            }

            if (isMoving)
            {
                texture = runSpritesheet;
            }
            else if (isAttacking)
            {
                UpdateFrame(gameTime, attackFrames);
                texture = attackSpritesheet;

            }
            else { texture = idleSpritesheet; }
        }

        public void UpdateFrame(GameTime gameTime, int framesCount)
        {
            timeSinceLastFrame += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timeSinceLastFrame >= frameTime)
            {
                currentFrame = (currentFrame + 1) % framesCount;
                timeSinceLastFrame = 0;
            }
        }
        

        public override void Draw(SpriteBatch spriteBatch)
        {
            Rectangle sourceRectangle = frameRectangles[currentFrame];
            spriteBatch.Draw(texture, position, sourceRectangle, Color.White, 0f, Vector2.Zero, 1f, spriteEffect, 0f);

        }
    }
}