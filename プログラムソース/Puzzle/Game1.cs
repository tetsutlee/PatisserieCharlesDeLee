using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System;

namespace Puzzle
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Render render;
        private Random random;
        private Sound sound;
        private GameDevice gameDevice;

        private Blocks blocks;
        private PuzzleStage puzzleStage;
        private Background background;
        private TitleScreen title;
        private LoadingScreen loading;
        private Tutorial tutorial;
        private Ending ending;
        private Rank rank;
        private SpecialPrize specialPrize;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1088;
            graphics.PreferredBackBufferHeight = 1280;
            IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            random = new Random();
            gameDevice = GameDevice.Instance(Content, GraphicsDevice);
            sound = gameDevice.GetSound();
            Input.Initialize();

                    
            title = new TitleScreen();
            title.Initialize();

            loading = new LoadingScreen(title);
            loading.Initialize();

            tutorial = new Tutorial(loading);
            tutorial.Initialize();

            blocks = new Blocks(tutorial);
            blocks.Initialize();

            puzzleStage = new PuzzleStage(blocks);
            puzzleStage.Initialize();

            background = new Background(blocks);
            background.Initialize();

            ending = new Ending(blocks);
            ending.Initialize();

            specialPrize = new SpecialPrize(blocks);
            specialPrize.Initialize();

            rank = new Rank(ending, blocks, specialPrize);
            rank.Initialize();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            render = new Render(Content, GraphicsDevice);
            // TODO: use this.Content to load your game content here

            render.LoadContent("PuzzleStage");
            render.LoadContent("PuzzleStageComplete");
            render.LoadContent("BigAnpan");
            render.LoadContent("BigDonuts");
            render.LoadContent("BigToast");
            render.LoadContent("BigWaffle");
            render.LoadContent("BigCroissant");
            render.LoadContent("Press_Enter1");
            render.LoadContent("Press_Enter2");
            render.LoadContent("PatisserieChralesDeLee");
            render.LoadContent("BlackCover");
            render.LoadContent("LoadingConversation");
            render.LoadContent("NowLoading1");
            render.LoadContent("NowLoading2");
            render.LoadContent("LoadingNext1");
            render.LoadContent("BrownCover");
            render.LoadContent("RedBlocks");
            render.LoadContent("Number");
            render.LoadContent("BigPresentBasket");
            render.LoadContent("BackGroundEffect");
            render.LoadContent("EndingTitle");
            render.LoadContent("flower");
            render.LoadContent("flowerTutorial");
            render.LoadContent("ScoreTime");
            render.LoadContent("BackToTitle");
            render.LoadContent("Rank");
            render.LoadContent("BigCoupon");
            render.LoadContent("1millionPoint");
            render.LoadContent("GetSpecialitem");
            for (int i = 1; i <= 3; i++)
            {
                render.LoadContent("BackgroundCake" + i);
                render.LoadContent("KiraEffect" + i);
                render.LoadContent("LoadingCake" + i);
            }

            

            //Sound
            string filepath = "./Sound/";

            sound.LoadBGM("the_opening_of_a_book");
            sound.LoadBGM("MusicBandPassage");
            sound.LoadBGM("raspis");
            sound.LoadBGM("wooden_beats");

            sound.LoadSE("KawaiiSE2", filepath);
            sound.LoadSE("KawaiiSE3", filepath);
            sound.LoadSE("Sante", filepath);
            sound.LoadSE("StartSE", filepath);
            sound.LoadSE("SystemSE", filepath);
            sound.LoadSE("Gorgeous", filepath);
            sound.LoadSE("Surprise", filepath);

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            Input.Update();
            //System.Console.WriteLine("Mouse X Position :" + Input.GetMousePosition.X 
            //    + "Mouse Y Position" + Input.GetMousePosition.Y);

            blocks.Update(gameTime);
            puzzleStage.Update(gameTime);
            background.Update(gameTime);
            loading.Update(gameTime);
            title.Update(gameTime);
            tutorial.Update(gameTime);
            ending.Update(gameTime);
            specialPrize.Update(gameTime);
            rank.Update(gameTime);

            if (rank.GetRankEnd())
            {
                this.Initialize();
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Wheat);

            // TODO: Add your drawing code here
            render.Begin();

            background.Draw(render);
            puzzleStage.Draw(render);
            blocks.Draw(render);
            loading.Draw(render);
            title.Draw(render);
            tutorial.Draw(render);
            ending.Draw(render);
            specialPrize.Draw(render);
            rank.Draw(render);

            render.End();
           
            base.Draw(gameTime);
        }
    }
}
