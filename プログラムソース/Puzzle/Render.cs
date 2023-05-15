using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace Puzzle

{
    class Render
    {
        private ContentManager contentManager;
        private GraphicsDevice graphicsDevice;
        private SpriteBatch spriteBatch;

        private Dictionary<string, Texture2D> textures
            = new Dictionary<string, Texture2D>();

        public Render(ContentManager content, GraphicsDevice graphics)
        {
            contentManager = content;
            graphicsDevice = graphics;
            spriteBatch = new SpriteBatch(graphicsDevice);

        }

        public void LoadContent(string assetName, string filepath = "./")
        {

            if (textures.ContainsKey(assetName))
            {
                Console.WriteLine(assetName + "はすでに読み込まれています");

                return;
            }

            textures.Add(assetName, contentManager.Load<Texture2D>(filepath + assetName));

        }

        public void Unload()
        {
            textures.Clear();
        }

        public void Begin()
        {
            spriteBatch.Begin();
        }

        public void End()
        {
            spriteBatch.End();
        }

        public void DrawTexture(string assetName, Vector2 position, float
        alpha = 1.0f)
        {
            spriteBatch.Draw(textures[assetName], position, Color.White * alpha);
        }

        public void DrawTexture(string assetName, Vector2 position,
            Rectangle rect, float alpha = 1f)
        {
            spriteBatch.Draw(textures[assetName], position, rect,
             Color.White * alpha);
        }
        public void DrawBrown(string assetName, Vector2 position,float alpha = 1f)
        {
            spriteBatch.Draw(textures[assetName], position, Color.Brown * alpha);
        }
        public void DrawWheat(string assetName, Vector2 position, float alpha = 1f)
        {
            spriteBatch.Draw(textures[assetName], position, Color.Wheat * alpha);
        }
        public void DrawBlack(string assetName, Vector2 position, float alpha = 1f)
        {
            spriteBatch.Draw(textures[assetName], position, Color.Black * alpha);
        }

        public void DrawTextureCustom(
            string assetName,
            Vector2 position,
            float rotate,
            Vector2 centerPosition,
            float scale)
        {
            spriteBatch.Draw(
                textures[assetName],
                position, 
                null,
                Color.White,
                MathHelper.ToRadians(rotate),
                centerPosition,
                scale,
                SpriteEffects.None,
                1f );
        }

        public void DrawTextureScale(
            string assetName,
            Vector2 position,
            float scale)
        {
            spriteBatch.Draw(
                textures[assetName],
                position,
                null,
                Color.White,
                0f,
                Vector2.Zero,
                scale,
                SpriteEffects.None,
                1f);
        }

        public void DrawTextureRotate(
            string assetName,
            Vector2 position,
            float rotate,
            int width,
            int height,
            float scale)
        {
            spriteBatch.Draw(
                textures[assetName],
                position,
                null,
                Color.White,
                MathHelper.ToRadians(rotate),
                new Vector2(width / 2, height / 2),
                scale,
                SpriteEffects.None,
                1f);
        }
        public void DrawBrownRotate(
            string assetName,
            Vector2 position,
            float rotate,
            int width,
            int height,
            float scale)
        {
            spriteBatch.Draw(
                textures[assetName],
                position,
                null,
                Color.Brown,
                MathHelper.ToRadians(rotate),
                new Vector2(width / 2, height / 2),
                scale,
                SpriteEffects.None,
                1f);
        }

        public void DrawString(
            SpriteFont spriteFont,
            string text,
            Vector2 position,
            Color color
            )
        {
            spriteBatch.DrawString(
                spriteFont,
                text,
                position,
                color);
        }

   
        public void DrawTextureSwing(
            string assetName,
            Vector2 position,
            Vector2 origin,
            float rotate,
            float scale,
            int width,
            int height)
        {
            spriteBatch.Draw(
                textures[assetName],
                position,
                null,
                Color.White,
                MathHelper.ToRadians(rotate),
                origin, scale,
                SpriteEffects.None,
                1f);
        }

        public void DrawTextureScale(
            string assetName,
            Vector2 position,
            float originX,
            float originY,
            float width,
            float height
            )
        {
            spriteBatch.Draw(textures[assetName], position, null,
    Color.White, 0.0f, new Vector2(originX, originY), new Vector2(width, height),
    SpriteEffects.None, 0.0f);
        }

        public void DrawMovie(Texture2D videoTexture)
        {
            spriteBatch.Draw(videoTexture, graphicsDevice.Viewport.Bounds, Color.White);
        }
        public void DrawNumber(
            string assetName,
            Vector2 position,
            int number)
        {
            if(number < 0)
            {
                number = 0;
            }

            int width = 64;

            foreach (var n in number.ToString())
            {
                spriteBatch.Draw(
                    textures[assetName],
                    position,
                    new Rectangle((n - '0') * width, 0, width, 64),
                    Color.Brown);

                position.X += width;
            }
        }

    }
}
