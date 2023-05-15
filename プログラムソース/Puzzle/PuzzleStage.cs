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
    class PuzzleStage
    {
        private Vector2 stagePosition;
        Blocks blocks;
        public PuzzleStage(Blocks blocks)
        {
            this.blocks = blocks;
        }

        public void Initialize()
        {
            stagePosition = Vector2.Zero;
        }
        public void Update(GameTime gameTime)
        {

        }
        public void Draw(Render render)
        {
            if (!blocks.GetGameEnding())
            {
                render.DrawTexture("PuzzleStageComplete", stagePosition);
            }
        }
    }
}
