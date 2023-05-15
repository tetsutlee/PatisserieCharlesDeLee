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
    class SpecialPrize
    {
        Blocks blocks;
        private Sound sound;
        private GameDevice gameDevice;

        private Vector2 backgroundEffectPosition;
        private int time;
        private float backgroundEffectScale;
        private float backgroundEffectRotate;
        private bool showNextSign;
        private bool playSE;
        private bool SpecialPrizeOver;

        private Vector2 loadNextSignPosition;

        private Vector2 couponPosition; 
        private float couponScale;

        private Vector2 specialPrizeTitlePosiotn;
        private float specialPrizeTitleScale;

        private Vector2 getItemSignPosition;
        private float getItemSignScale;

        public SpecialPrize(Blocks blocks)
        {
            this.blocks = blocks;
        }
        public void Initialize()
        {
            gameDevice = GameDevice.Instance();
            sound = gameDevice.GetSound();

            couponPosition = new Vector2(544, 640);
            couponScale = 0f;
            specialPrizeTitlePosiotn = new Vector2(544, 730);
            specialPrizeTitleScale = 0f;
            getItemSignPosition = new Vector2(544, 590);
            getItemSignScale = 0f;
            loadNextSignPosition = Vector2.Zero;
            backgroundEffectPosition = new Vector2(544, 640);
            time = 0;
            backgroundEffectScale = 0f;
            backgroundEffectRotate = 0f;
            showNextSign = false;
            playSE = false;
            SpecialPrizeOver = false;

        }
        public void Update(GameTime gameTime)
        {
            if(blocks.GetSpecialEnding() && !SpecialPrizeOver)
            {
                time++;
                if (time % 15 == 0)
                {
                    loadNextSignPosition.Y -= 15f;
                }
                if (time % 30 == 0)
                {
                    loadNextSignPosition.Y += 30f;
                }

                if (specialPrizeTitleScale != 1.25f)
                {
                    specialPrizeTitleScale += 0.025f;
                    if (specialPrizeTitleScale >= 1.25f)
                    {
                        specialPrizeTitleScale = 1.25f;
                    }
                }
                if (getItemSignScale != 1.25f)
                {
                    getItemSignScale += 0.025f;
                    if (getItemSignScale >= 1.25f)
                    {
                        getItemSignScale = 1.25f;
                    }
                }

                if (couponScale != 1f)
                {
                    couponScale += 0.025f;
                    if (couponScale >= 1f)
                    {
                        couponScale = 1f;
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

                if(time == 300)
                {
                    showNextSign = true;                   
                }
                if(showNextSign)
                {
                    if (Input.GetKeyTrigger(Keys.Enter))
                    {
                        SpecialPrizeOver = true;
                        sound.PlaySE("KawaiiSE2");
                    }
                }
            }
        }
        public void Draw(Render render)
        {
            if (blocks.GetSpecialEnding() && !SpecialPrizeOver)
            {
                render.DrawTextureRotate("BackGroundEffect", backgroundEffectPosition, backgroundEffectRotate, 1088, 1280, backgroundEffectScale);
                render.DrawTextureRotate("BigCoupon", couponPosition, 0f, 1088, 1280, couponScale);
                render.DrawBrownRotate("1millionPoint", specialPrizeTitlePosiotn, 0f, 1088, 1280, specialPrizeTitleScale);
                render.DrawBrownRotate("GetSpecialitem", getItemSignPosition, 0f, 1088, 1280, getItemSignScale);
                if (showNextSign)
                {
                    render.DrawBlack("LoadingNext1", loadNextSignPosition);
                }
            }
        }
        public bool GetSpecialPrizeEnd()
        {
            return SpecialPrizeOver;
        }
    }
}
