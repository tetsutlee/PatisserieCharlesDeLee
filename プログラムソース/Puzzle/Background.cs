using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Puzzle
{
    class Background
    {
        Blocks blocks;
        private int animationTime;
        private short animationNumber;
        private Vector2 backgroundPosition;
        private Vector2 backgroundPosition2;
        public Background(Blocks blocks)
        {
            this.blocks = blocks;
        }
        public void Initialize()
        {
            animationTime = 0;
            animationNumber = 1;
            backgroundPosition = Vector2.Zero;
            backgroundPosition2 = new Vector2(-1088, -1280);
        }
        public void Update(GameTime gameTime)
        {
            if (!blocks.GetGameEnding())
            {
                animationTime++;
                if (animationTime % 20 == 0)
                {
                    animationNumber++;
                    if (animationNumber >= 4)
                    {
                        animationNumber = 1;
                    }
                }

                backgroundPosition.X += 3f;
                backgroundPosition.Y += 3f;
                backgroundPosition2.X += 3f;
                backgroundPosition2.Y += 3f;
                if (backgroundPosition.X >= 1088)
                {
                    backgroundPosition = new Vector2(-1088, -1280);
                }
                if (backgroundPosition2.X >= 1088)
                {
                    backgroundPosition2 = new Vector2(-1088, -1280);
                }
            }
        }
        public void Draw(Render render)
        {
            if (!blocks.GetGameEnding())
            {
                render.DrawTexture("BackgroundCake" + animationNumber, backgroundPosition);
                render.DrawTexture("BackgroundCake" + animationNumber, backgroundPosition2);
            }
        }
    }
}
