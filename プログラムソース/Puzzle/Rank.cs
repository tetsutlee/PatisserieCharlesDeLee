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
    class Rank
    {
        Ending ending;
        Blocks blocks;
        SpecialPrize specialPrize;
        private Vector2 rankTitlePosition;
        private Vector2 backToTitleSignPosition;
        private int RankTime;
        private short showScoreTime;
        private float rankTitleScale;
        private bool RankOver;
        private bool showScoreSign;
        private bool showBackToTitle;
        private bool playSE;
        private bool playSE2;
        private bool playSE3;

        private short animationNumber;
        private Vector2 backgroundPosition;
        private Vector2 backgroundPosition2;

        private Sound sound;
        private GameDevice gameDevice;
        public Rank(Ending ending, Blocks blocks, SpecialPrize specialPrize)
        {
            this.ending = ending;
            this.blocks = blocks;
            this.specialPrize = specialPrize;
        }
        public void Initialize()
        {
            rankTitlePosition = new Vector2(544, 730);
            backToTitleSignPosition = Vector2.Zero;
            rankTitleScale = 0f;
            RankTime = 0;
            showScoreTime = 0;
            RankOver = false;
            showScoreSign = false;
            showBackToTitle = false;
            playSE = false;
            playSE2 = false;
            playSE3 = false;
            

            animationNumber = 1;
            backgroundPosition = Vector2.Zero;
            backgroundPosition2 = new Vector2(-1088, -1280);

            gameDevice = GameDevice.Instance();
            sound = gameDevice.GetSound();
        }
        public void Update(GameTime gameTime)
        {
            if(ending.GetEnding() && !RankOver || specialPrize.GetSpecialPrizeEnd() && !RankOver)
            {
                sound.PlayBGM("wooden_beats");
                if (!playSE2)
                {
                    sound.PlaySE("Gorgeous");
                    playSE2 = true;
                }
                RankTime++;
                if(RankTime % 15 == 0)
                {
                    backToTitleSignPosition.Y -= 15f;
                }
                if (RankTime % 30 == 0)
                {
                    backToTitleSignPosition.Y += 30f;
                }
                if (RankTime % 20 == 0)
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

                if (rankTitleScale != 1.25f)
                {
                    rankTitleScale += 0.05f;
                    if (rankTitleScale >= 1.25)
                    {
                        rankTitleScale = 1.25f;
                    }
                }
                else
                {
                    showScoreTime++;
                    if(showScoreTime >= 240)
                    {
                        showScoreSign = true;
                        if(!playSE)
                        {
                            sound.PlaySE("StartSE");
                            playSE = true;
                        }
                    }
                    if(showScoreTime == 480)
                    {
                        showBackToTitle = true;
                    }
                }
                
                if(showBackToTitle)
                {
                    if(Input.GetKeyTrigger(Keys.Enter))
                    {
                        RankOver = true;
                        sound.PlaySE("SystemSE");
                        sound.StopBGM();
                    }
                }
            }
        }
        public void Draw(Render render)
        {
            if (ending.GetEnding() && !RankOver || specialPrize.GetSpecialPrizeEnd() && !RankOver)
            {
                render.DrawTexture("BackgroundCake" + animationNumber, backgroundPosition);
                render.DrawTexture("BackgroundCake" + animationNumber, backgroundPosition2);
                render.DrawBrownRotate("Rank", rankTitlePosition, 0f, 1088, 1280, rankTitleScale);
                if (showScoreSign)
                {
                    render.DrawNumber("Number", new Vector2(390, 500), blocks.GetScore());
                }
                if (showBackToTitle)
                {
                    render.DrawBrown("BackToTitle", backToTitleSignPosition);
                }
            }
        }
        public bool GetRankEnd()
        {
            return RankOver;
        }
    }
}
