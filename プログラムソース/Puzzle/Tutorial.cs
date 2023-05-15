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
    class Tutorial
    {
        LoadingScreen loading;
        private short loadingAnimation;
        private int animationTime;
        private bool showNextSign;
        private bool endTutorial;
        private Vector2 nextSignPosition;

        private Sound sound;
        private GameDevice gameDevice;

        public Tutorial(LoadingScreen loading)
        {
            this.loading = loading;
        }
        public void Initialize()
        {
            endTutorial = false;
            showNextSign = false;
            loadingAnimation = 1;
            animationTime = 0;
            nextSignPosition = Vector2.Zero;

            gameDevice = GameDevice.Instance();
            sound = gameDevice.GetSound();
        }
        public void Update(GameTime gameTime)
        {
            if (loading.GetLoadingScreenEnd())
            {
                animationTime++;
                if (animationTime % 15 == 0)
                {
                    nextSignPosition.Y -= 20f;
                }
                if (animationTime % 30 == 0)
                {
                    loadingAnimation++;
                    if (loadingAnimation >= 3)
                    {
                        loadingAnimation = 1;
                    }
                    nextSignPosition.Y += 40f;
                }
                if(animationTime >= 240)
                {
                    showNextSign = true;
                }
                if(showNextSign)
                {
                    if(Input.GetKeyTrigger(Keys.Enter))
                    {
                        endTutorial = true;
                        sound.PlaySE("KawaiiSE2");
                        sound.StopBGM();
                    }
                }
            }
        }
        public void Draw(Render render)
        {
            if (loading.GetLoadingScreenEnd() && !endTutorial)
            {
                render.DrawWheat("flower", Vector2.Zero);
                render.DrawTexture("flowerTutorial", Vector2.Zero);
                if (!showNextSign)
                {
                    render.DrawBlack("NowLoading" + loadingAnimation, Vector2.Zero);
                }
                else
                {
                    render.DrawBlack("LoadingNext1", nextSignPosition);
                }
            }
        }
        public bool GetEndTutorial()
        {
            return endTutorial;
        }
    }
}
