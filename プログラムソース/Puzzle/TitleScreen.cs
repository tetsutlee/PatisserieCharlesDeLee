using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;


namespace Puzzle
{
    class TitleScreen
    {
        private Vector2 enterIconPosition;
        private short enterIconAnimation;
        private short enterGameTime;
        private int TitleTime;
        private bool isPressEnter;
        private bool titleEnd;
        private bool playSE;

        private Sound sound;
        private GameDevice gameDevice;
        public void Initialize()
        {
            enterIconPosition = new Vector2(380, 1130);
            TitleTime = 0;
            enterGameTime = 0;
            enterIconAnimation = 1;
            isPressEnter = false;
            titleEnd = false;
            playSE = false;

            gameDevice = GameDevice.Instance();
            sound = gameDevice.GetSound();
        }
        public void Update(GameTime gameTime)
        {
            if (!titleEnd)
            {
                sound.PlayBGM("the_opening_of_a_book");
                TitleTime++;
                if (!isPressEnter)
                {
                    if (TitleTime % 40 == 0)
                    {
                        enterIconAnimation++;
                        if (enterIconAnimation >= 3)
                        {
                            enterIconAnimation = 1;
                        }
                    }
                }
                else
                {
                    if (TitleTime % 10 == 0)
                    {
                        enterIconAnimation++;
                        if (enterIconAnimation >= 3)
                        {
                            enterIconAnimation = 1;
                        }
                    }
                }

                if (Input.GetKeyTrigger(Keys.Enter))
                {
                    isPressEnter = true;
                }
                if (isPressEnter)
                {
                    if (!playSE)
                    {
                        playSE = true;
                        sound.PlaySE("Sante");
                    }
                    enterGameTime++;
                }
                if (enterGameTime >= 150)
                {
                    titleEnd = true;
                    sound.StopBGM();
                }
            }

        }
        public void Draw(Render render)
        {
            if (!titleEnd)
            {
                render.DrawTexture("PatisserieChralesDeLee", Vector2.Zero);
                render.DrawTexture("Press_Enter" + enterIconAnimation, enterIconPosition);
            }
        }
        public bool GetTitleEnd()
        {
            return titleEnd;
        }
    }
}
