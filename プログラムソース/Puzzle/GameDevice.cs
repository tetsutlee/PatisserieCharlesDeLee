using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System.Windows.Input;

namespace Puzzle
{
    sealed class GameDevice
    {
        private static GameDevice instance;

        private Sound sound;
        private static Random random;
        private ContentManager content;
        private GraphicsDevice graphics;

        private GameDevice(ContentManager content, GraphicsDevice graphics)
        {
            sound = new Sound(content);
            random = new Random();
            this.content = content;
            this.graphics = graphics;
        }

        public static GameDevice Instance(ContentManager content, GraphicsDevice graphics)
        {
            if (instance == null)
            {
                instance = new GameDevice(content, graphics);
            }
            return instance;
        }

        public static GameDevice Instance()
        {
            return instance;
        }

        public void Initialize()
        {

        }

        public void Update(GameTime game)
        {
            //Input.update();


        }

        public Sound GetSound()
        {
            return sound;
        }

        public Random GetRandom()
        {
            return random;
        }

        public ContentManager GetContentManager()
        {
            return content;
        }

        public GraphicsDevice GetGraphicsDevice()
        {
            return graphics;
        }
    }
}
