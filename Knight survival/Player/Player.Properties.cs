using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Knight_survival
{
    internal partial class Player : Sprite
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
    }
}