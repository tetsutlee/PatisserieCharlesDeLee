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
    class Ending
    {
        Blocks blocks;
        SpecialPrize specialPrize;

        private Vector2 backgroundEffectPosition;
        private float backgroundEffectScale;
        private float backgroundEffectRotate;
        private Vector2 presentPosition;
        private float presentScale;
        private float presentRotate;
        private Vector2 endingTitlePosiotn;
        private float endingTitleScale;

        private Vector2 loadNextSignPosition;
        private float coverAlphaValue;

        private int time;
        private bool isEnded;
        private bool playSE;
        private bool showNextSign;

        private Sound sound;
        private GameDevice gameDevice;
        public Ending(Blocks blocks)
        {
            this.blocks = blocks;
        }
        public void Initialize()
        {
            gameDevice = GameDevice.Instance();
            sound = gameDevice.GetSound();

            backgroundEffectPosition = new Vector2(544, 640);            
            backgroundEffectScale = 0f;
            backgroundEffectRotate = 0f;

            presentPosition = new Vector2(544, 640);
            presentRotate = 0f;
            presentScale = 0f;

            endingTitlePosiotn = new Vector2(544, 780);
            endingTitleScale = 0f;

            loadNextSignPosition = Vector2.Zero;
            coverAlphaValue = 1f;
            time = 0;
            isEnded = false;
            playSE = false;
            showNextSign = false;
        }
        public void Update(GameTime gameTime)
        {
            if (blocks.GetGameEnding() && !isEnded && !blocks.GetSpecialEnding())
            {
                coverAlphaValue -= 0.005f;
                time++;
                if (time % 15 == 0)
                {
                    loadNextSignPosition.Y -= 15f;
                    if (endingTitleScale == 1.25f)
                    {
                        endingTitlePosiotn.Y -= 20f;
                    }
                }
                if (time % 30 == 0)
                {
                    loadNextSignPosition.Y += 30f;
                    if (endingTitleScale == 1.25f)
                    {
                        endingTitlePosiotn.Y += 40f;
                    }
                }

                if (endingTitleScale != 1.25f)
                {
                    endingTitleScale += 0.05f;
                    if (endingTitleScale >= 1.25)
                    {
                        endingTitleScale = 1.25f;
                    }
                }
                if(presentScale != 1.25f)
                {
                    presentScale += 0.05f;
                    if (presentScale >= 1.25)
                    {
                        presentScale = 1.25f;
                    }
                }
                else
                {
                    if (!playSE)
                    {
                        sound.PlaySE("Surprise");
                        playSE = true;
                    }
                }
                if (backgroundEffectScale != 1.5f)
                {
                    backgroundEffectScale += 0.05f;
                    if (backgroundEffectScale >= 1.5f)
                    {
                        backgroundEffectScale = 1.5f;
                    }
                }
                else
                {
                    backgroundEffectRotate += 2.5f;
                }
                if (time == 360)
                {
                    showNextSign = true;
                }
                if (showNextSign)
                {
                    if (Input.GetKeyTrigger(Keys.Enter))
                    {
                        isEnded = true;
                        sound.PlaySE("KawaiiSE2");
                    }
                }
            }

        }
        public void Draw(Render render)
        {
            if (blocks.GetGameEnding() && !isEnded && !blocks.GetSpecialEnding())
            {
                render.DrawTextureRotate("BackGroundEffect", backgroundEffectPosition, backgroundEffectRotate, 1088, 1280, backgroundEffectScale);
                render.DrawTextureRotate("BigPresentBasket", presentPosition, presentRotate, 700, 400, presentScale);
                render.DrawBrownRotate("EndingTitle", endingTitlePosiotn, 0f, 1088, 1280, endingTitleScale);
                if (showNextSign)
                {
                    render.DrawBlack("LoadingNext1", loadNextSignPosition);
                }
                render.DrawTexture("BrownCover", Vector2.Zero, coverAlphaValue);
            }
        }
        public bool GetEnding()
        {
            return isEnded;
        }
    }
}
