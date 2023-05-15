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
    class BlocksEffect
    {
        private Vector2 effectPosition;
        private short effectAnimation;
        public bool effectIsAlive { set; get; }

        private Sound sound;
        private GameDevice gameDevice;
        Timer timer;
        public BlocksEffect(Vector2 effectPosition)
        {
            this.effectPosition = effectPosition;
            effectAnimation = 1;
            effectIsAlive = true;
            timer = new Timer(0.1f);

            gameDevice = GameDevice.Instance();
            sound = gameDevice.GetSound();
        }

        public void Update(GameTime gameTime)
        {
            timer.CountDown();
            if(timer.isTime())
            {
                effectAnimation++;
                if(effectAnimation >= 4)
                {
                    effectIsAlive = false;
                    sound.PlaySE("Gorgeous");
                }
                timer.SetTime(0.1f);
            }
        }
        public void Draw(Render render)
        {
            render.DrawTexture("KiraEffect" + effectAnimation, effectPosition);
        }
    }
}
