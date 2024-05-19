using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Knight_survival
{
    internal class Player : Sprite
    {
        List<Sprite> collisionGroup { get; set; }
        private int attackFrames { get; set; }
        private SpriteEffects spriteEffect { get; set; } = SpriteEffects.None;
        private float frameTime { get; set; }
        private float timeSinceLastFrame { get; set; }
        public int currentFrame { get; set; }
        private Texture2D idleSpritesheet { get; set; }
        private Texture2D runSpritesheet { get; set; }
        private Texture2D attackSpritesheet { get; set; }
        private List<Rectangle> frameRectangles { get; set; }
        public int Health { get; set; } = 10;
        public int Damage { get; set; } = 1;
        public float Speed { get; set; } = 1f;
        public bool IsShiftPressed { get; set; } = false;
        public bool hasAttacked { get; set; } = false;
        public bool isAttacking { get; set; }

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
                    changeX += Speed * 1.2f;
                changeX += Speed;
                isMoving = true;
                isAttacking = false;
                spriteEffect = SpriteEffects.None;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                if (IsShiftPressed)
                    changeX -= Speed * 1.2f;
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
                    changeY -= Speed * 1.2f;
                changeY -= Speed;
                isMoving = true;
                isAttacking = false;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                if (IsShiftPressed)
                    changeY += Speed * 1.2f;
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

        public override void Draw(SpriteBatch spriteBatch)
        {
            Rectangle sourceRectangle = frameRectangles[currentFrame];
            spriteBatch.Draw(texture, position, sourceRectangle, Color.White, 0f, Vector2.Zero, 1f, spriteEffect, 0f);
        }
    }
}