using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Knight_survival;

internal partial class Monster : Sprite
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
    public int MaxHealth { get; set; }
    public int Attack { get; set; } = 1;
    private float fadeEffect = 1.0f;
    private double lastAttackTime = 0;
    private double attackCooldown = 1.0; // Cooldown in seconds
}
