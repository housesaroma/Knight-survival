using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knight_survival
{
    internal class Player : Sprite
    {
        public Player(Texture2D texture, Vector2 position) : base(texture, position)
        {
        }
        public void Update(GameTime gameTime, List<Sprite> collisionGroup)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                position.X++;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                position.X--;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                position.Y--;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                position.Y++;
            }
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
