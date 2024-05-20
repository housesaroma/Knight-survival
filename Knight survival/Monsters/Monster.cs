using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Knight_survival;

internal class Monster : Sprite
{
    List<Sprite> collisionGroup { get; set; }
    private int totalFrames { get; set; }
    private SpriteEffects spriteEffect { get; set; } = SpriteEffects.None;
    private float frameTime { get; set; }
    private float timeSinceLastFrame { get; set; }
    private int currentFrame { get; set; }
    private Vector2 playerPosition { get; set; }
    private Texture2D runSpritesheet { get; set; }
    private List<Rectangle> frameRectangles { get; set; }
    public bool IsAlive { get; set; } = true;
    public int Health { get; set; }
    public int Attack { get; set; } = 1;
    private float fadeEffect = 1.0f;

    public Monster(Texture2D runSpritesheet, Texture2D deathSpritesheet, List<Sprite> collisionGroup, Vector2 position, Vector2 playerPosition, int totalFrames, float frameTime, int health, int attack) : base(runSpritesheet, position, 6)
    {
        this.runSpritesheet = runSpritesheet;
        this.playerPosition = playerPosition;
        this.totalFrames = totalFrames;
        this.frameTime = frameTime;
        this.collisionGroup = collisionGroup;
        Health = health;
        Attack = attack;
        frameRectangles = SliceSpriteSheet(runSpritesheet, 150, 150, 150, 150);
        timeSinceLastFrame = 0;
        currentFrame = 0;
    }

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

public override void Draw(SpriteBatch spriteBatch)
    {
        if (!IsAlive && fadeEffect <= 0) return; // Do not draw if fully faded

        Color color = new Color(1f, 1f, 1f, fadeEffect); // Apply fading by adjusting alpha
        Rectangle sourceRectangle = frameRectangles[currentFrame];
        spriteBatch.Draw(texture, position, sourceRectangle, color, 0f, Vector2.Zero, 1f, spriteEffect, 0f);
    }
}
