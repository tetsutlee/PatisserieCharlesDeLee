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
    class LoadingScreen
    {
        private int animationTime;
        private int loadingWaitTime;
        private short nextSignWaitTime;
        private short loadingAnimation;
        private short loadingCakeAnimation;
        private float alphaValue;
        private bool showNextSign;
        private bool showLoadingScreen;
        private bool endLoadingScreen;
        private TitleScreen title;
        private Vector2 loadNextSignPosition;

        private Sound sound;
        private GameDevice gameDevice;
        public LoadingScreen(TitleScreen title)
        {
            this.title = title;
        }
        public void Initialize()
        {
            animationTime = 0;
            loadingWaitTime = 0;
            loadingAnimation = 1;
            nextSignWaitTime = 0;
            loadingCakeAnimation = 1;
            alphaValue = 0f;
            showNextSign = false;
            endLoadingScreen = false;
            loadNextSignPosition = Vector2.Zero;

            gameDevice = GameDevice.Instance();
            sound = gameDevice.GetSound();
        }
        public void Update(GameTime gameTime)
        {
            if (title.GetTitleEnd() && !endLoadingScreen)
            {
                sound.PlayBGM("MusicBandPassage");
                animationTime++;
                if (animationTime % 15 == 0)
                {
                    loadingCakeAnimation++;
                    if (loadingCakeAnimation >= 4)
                    {
                        loadingCakeAnimation = 1;
                    }
                    loadNextSignPosition.Y -= 20f;
                }
                if (animationTime % 30 == 0)
                {
                    loadingAnimation++;
                    if (loadingAnimation >= 3)
                    {
                        loadingAnimation = 1;
                    }
                    loadNextSignPosition.Y += 40f;
                }
                if (animationTime >= 120)
                {
                    alphaValue += 0.05f;
                    if (alphaValue >= 1f)
                    {
                        alphaValue = 1f;
                        if (animationTime == 300)
                        {
                            showNextSign = true;
                        }
                    }
                }
                if (showNextSign)
                {
                    if (Input.GetKeyTrigger(Keys.Enter))
                    {
                        showNextSign = false;
                        showLoadingScreen = true;
                        sound.PlaySE("KawaiiSE2");
                    }
                }
                if (showLoadingScreen)
                {
                    loadingWaitTime++;
                    if (loadingWaitTime >= 300)
                    {
                        endLoadingScreen = true;
                    }
                }
            }

        }
        public void Draw(Render render)
        {
            if (title.GetTitleEnd() && !endLoadingScreen)
            {
                render.DrawTexture("BrownCover", Vector2.Zero);
                if (!showLoadingScreen)
                {
                    render.DrawTexture("LoadingConversation", Vector2.Zero, alphaValue);
                }
                if (showNextSign && !showLoadingScreen)
                {
                    render.DrawTexture("LoadingNext1", loadNextSignPosition);  
                }
                if (showLoadingScreen)
                {
                    render.DrawTexture("LoadingCake" + loadingCakeAnimation, Vector2.Zero);
                    render.DrawTexture("NowLoading" + loadingAnimation, Vector2.Zero);
                }
            }
        }
        public bool GetLoadingScreenEnd()
        {
            return endLoadingScreen;
        }
    }
}
