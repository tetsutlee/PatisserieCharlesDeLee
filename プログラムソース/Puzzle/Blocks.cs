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
    class Blocks
    { 
        private BlocksStruct[][] blocksArray;
        private int width;
        private int height;       
        private int blocksX;
        private int blocksY;
        private int blockWidth;
        private int blockHeight;
        private int animationTime;
        private int score;
        private int scoreNumber;
        private int gameSecond;
        private int gameMinute;
        private float textureScaleX;
        private float textureScaleY;
        private float originX;
        private float originY;
        private bool[][] arrayCheck;
        private bool MouseLock;
        private bool GameEnd;
        private bool SpecialEnding;
        private Vector2 mousePosition;
        private List<BlocksEffect> blocksEffects;
        Random random;
        Timer timer;
        Tutorial tutorial;

        private Sound sound;
        private GameDevice gameDevice;

        enum BlocksStatus
        {
            No,
            Anpan,
            Donuts,
            Toast,
            Waffle,
            Croissant,
            BlocksStatutsMax,
        }
        struct BlocksStruct
        {
            public BlocksStatus status;
            public bool isSelected;
            public string name;
        }

        
        public Blocks(Tutorial tutorial) 
        {
            this.tutorial = tutorial;
        }

        ~Blocks() 
        { 

        }
        public void Initialize()
        {
            gameDevice = GameDevice.Instance();
            gameSecond = 0;
            sound = gameDevice.GetSound();

            gameMinute = 120; 
            GameEnd = false;
            SpecialEnding = false;
            MouseLock = false;
            blocksX = 0;
            blocksY = 0;
            score = 0;
            scoreNumber = 0;
            random = new Random();
            timer = new Timer(0.1f);
            width = 12;
            height = 13;
            textureScaleX = 1f;
            textureScaleY = 1f;
            originX = 64f;
            originY = 64f;
            blocksEffects = new List<BlocksEffect>();

            //Struct Initialize
            arrayCheck = new bool[height][];
            for (int i = 0; i < height; i++)
            {
                arrayCheck[i] = new bool[width];
            }
            for (int i = 0; i < height; i++)//構造体の初期化と配列に入れる
            {
                if (i == height - 1) continue;
                for (int j = 0; j < width; j++)
                {
                    if (j == 0 || j == width - 1) continue;
                    arrayCheck[i][j] = false;
                }
            }


            blocksArray = new BlocksStruct[height][];
            for(int i = 0; i < height; i++)
            {
                blocksArray[i] = new BlocksStruct[width];
            }
            for(int i = 0; i < height; i++)//構造体の初期化と配列に入れる
            {
                if (i == height - 1) continue;
                for (int j = 0; j < width; j++)
                {
                    if (j == 0 || j == width - 1) continue;
                    BlocksStruct blocksStruct = new BlocksStruct();
                    blocksStruct.isSelected = false;
                    blocksStruct.status = (BlocksStatus)random.Next((int)BlocksStatus.Anpan, (int)BlocksStatus.BlocksStatutsMax);

                    switch (blocksStruct.status)
                    {
                        case BlocksStatus.Anpan:
                            blocksStruct.name = "BigAnpan";
                            break;
                        case BlocksStatus.Donuts:
                            blocksStruct.name = "BigDonuts";
                            break;
                        case BlocksStatus.Toast:
                            blocksStruct.name = "BigToast";
                            break;
                        case BlocksStatus.Waffle:
                            blocksStruct.name = "BigWaffle";
                            break;
                        case BlocksStatus.Croissant:
                            blocksStruct.name = "BigCroissant";
                            break;

                    }
                    blocksArray[i][j] = blocksStruct;
                }
            }
            blockWidth = 64;
            blockHeight = 64;

        }
        public void Update(GameTime gameTime)
        {
            if (tutorial.GetEndTutorial() && !GameEnd)
            {
                sound.PlayBGM("raspis");
                for (int i = 0; i < blocksEffects.Count; i++)
                {
                    blocksEffects[i].Update(gameTime);
                    if (!blocksEffects[i].effectIsAlive)
                    {
                        blocksEffects.Remove(blocksEffects[i]);
                    }
                }
                animationTime++;
                if (animationTime % 10 == 0)
                {

                    textureScaleX += 0.05f;
                    if (textureScaleX >= 1.1f)
                    {
                        textureScaleX -= 0.1f;
                    }
                    textureScaleY += 0.05f;
                    if (textureScaleY >= 1.1f)
                    {
                        textureScaleY -= 0.1f;
                    }
                }

                for (int i = 0; i < height; i++)//配列のチェック
                {
                    if (i == height - 1) continue;
                    for (int j = 0; j < width; j++)
                    {
                        if (j == 0 || j == width - 1) continue;
                        arrayCheck[i][j] = false;
                    }
                }

                bool isSwitchPos = false;
                int blockCheckNumber; //

                for (int i = 0; i < height; i++)//全オブジェクトチェック(Tate)
                {
                    if (i == height - 1) continue;//縦境界
                    for (int j = 0; j < width; j++)//全オブジェクトチェック(Yoko)
                    {
                        blockCheckNumber = BlocksCheck(blocksArray[i][j].status, j, i, 0);
                        if (blockCheckNumber == 3)
                        {
                            Disappear(blocksArray[i][j].status, j, i);
                            scoreNumber += 1000;
                        }
                        if (blockCheckNumber == 4)
                        {
                            Disappear(blocksArray[i][j].status, j, i);
                            scoreNumber += 1600;
                        }
                        if (blockCheckNumber >= 5)
                        {
                            Disappear(blocksArray[i][j].status, j, i);
                            scoreNumber += 2000;
                        }
                        if (j == 0 || j == width - 1) continue; //横境界
                        if (blocksArray[i][j].isSelected)
                        {
                            isSwitchPos = true;
                            blocksX = j;
                            blocksY = i;
                        }
                    }
                }
                if (scoreNumber > 0)
                {
                    scoreNumber -= 200;
                    score += 200;
                }

                if (!MouseLock)
                {
                    if (!isSwitchPos)
                    {
                        if (Input.IsMouseLeftButtonDown())//ブロック選択処理
                        {
                            sound.PlaySE("KawaiiSE3");
                            mousePosition = Input.GetMousePosition;
                            int x = (int)mousePosition.X / blockWidth;//
                            int y = ((int)mousePosition.Y - 7 * blockHeight) / blockHeight;
                            if (x < 1 || x >= width - 1 || y < 0 || y >= height - 1) { return; }
                            blocksArray[y][x].isSelected = true;
                        }
                    }
                    else
                    {
                        if (Input.IsMouseLeftButtonDown())//ブロック選択処理
                        {
                            sound.PlaySE("KawaiiSE3");
                            Vector2 mousePosition = Input.GetMousePosition;
                            int x = (int)mousePosition.X / blockWidth;//
                            int y = ((int)mousePosition.Y - 7 * blockHeight) / blockHeight;
                            if (y == blocksY - 1 && x == blocksX || y == blocksY + 1 && x == blocksX
                                || x == blocksX - 1 && y == blocksY || x == blocksX + 1 && y == blocksY)
                            {
                                if (x < 1 || x >= width - 1 || y < 0 || y >= height - 1) { return; }
                                blocksArray[blocksY][blocksX].isSelected = false;
                                BlocksStruct blocks = new BlocksStruct();
                                blocks = blocksArray[blocksY][blocksX];
                                blocksArray[blocksY][blocksX] = blocksArray[y][x];
                                blocksArray[y][x] = blocks;
                                MouseLock = true;
                            }
                        }
                    }
                }

                MouseLock = false;
                timer.CountDown();
                if (timer.isTime())
                {
                    for (int i = height - 2; i >= 0; i--)//落下処理
                    {
                        for (int j = 0; j < width; j++)
                        {
                            if (j == 0 || j == width - 1) continue;
                            if (blocksArray[i][j].status != BlocksStatus.No && blocksArray[i + 1][j].status == BlocksStatus.No)
                            {
                                blocksArray[i + 1][j].status = blocksArray[i][j].status;
                                blocksArray[i][j].status = BlocksStatus.No;
                                MouseLock = true;
                                timer.SetTime(0.3f);
                            }

                        }
                    }
                }
                //一番上の行をチェック(生成)
                for (int j = 0; j < width; j++)
                {
                    if (j == 0 || j == width - 1) continue;
                    if (blocksArray[0][j].status == BlocksStatus.No)
                    {
                        BlocksStruct blocksStruct = new BlocksStruct();
                        blocksStruct.isSelected = false;
                        blocksStruct.status = (BlocksStatus)random.Next((int)BlocksStatus.Anpan, (int)BlocksStatus.BlocksStatutsMax);

                        switch (blocksStruct.status)
                        {
                            case BlocksStatus.Anpan:
                                blocksStruct.name = "BigAnpan";
                                break;
                            case BlocksStatus.Donuts:
                                blocksStruct.name = "BigDonuts";
                                break;
                            case BlocksStatus.Toast:
                                blocksStruct.name = "BigToast";
                                break;
                            case BlocksStatus.Waffle:
                                blocksStruct.name = "BigWaffle";
                                break;
                            case BlocksStatus.Croissant:
                                blocksStruct.name = "BigCroissant";
                                break;

                        }
                        blocksArray[0][j] = blocksStruct;
                    }

                }

                for (int i = 0; i < height; i++)//構造体の初期化と配列に入れる
                {
                    if (i == height - 1) continue;
                    for (int j = 0; j < width; j++)
                    {
                        if (j == 0 || j == width - 1) continue;
                        switch (blocksArray[i][j].status)
                        {
                            case BlocksStatus.Anpan:
                                blocksArray[i][j].name = "BigAnpan";
                                break;
                            case BlocksStatus.Donuts:
                                blocksArray[i][j].name = "BigDonuts";
                                break;
                            case BlocksStatus.Toast:
                                blocksArray[i][j].name = "BigToast";
                                break;
                            case BlocksStatus.Waffle:
                                blocksArray[i][j].name = "BigWaffle";
                                break;
                            case BlocksStatus.Croissant:
                                blocksArray[i][j].name = "BigCroissant";
                                break;
                        }
                        Console.Write((int)blocksArray[i][j].status);
                    }
                    Console.WriteLine();
                }

                gameSecond++;
                if (gameSecond % 60 == 0)
                {
                    gameMinute--;
                }
                if (score >= 1300000 || gameMinute <= 0)
                {
                    GameEnd = true;
                    sound.StopBGM();
                }
                if (score >= 1300000)
                {
                    SpecialEnding = true;
                }
            }


        }
        public void Draw(Render render)
        {
            if (tutorial.GetEndTutorial() && !GameEnd)
            {
                mousePosition = Input.GetMousePosition;
                int x = (int)mousePosition.X / blockWidth;//
                int y = (int)mousePosition.Y / blockHeight;
                if (!(x < 1 || x >= width - 1 || y < 7 || y >= height + 6)) //配列の範囲外チェック
                {
                    render.DrawTexture("RedBlocks", new Vector2(x * blockWidth, y * blockHeight));
                }

                for (int i = 0; i < height; i++)
                {
                    if (i == height - 1) continue;
                    for (int j = 0; j < width; j++)
                    {
                        if (j == 0 || j == width - 1) continue;

                        if (blocksArray[i][j].status == BlocksStatus.No) continue;
                        if (blocksArray[i][j].isSelected)
                        {
                            render.DrawTexture(blocksArray[i][j].name, Input.GetMousePosition - new Vector2(32, 32));
                        }
                        else
                        {
                            render.DrawTextureScale(blocksArray[i][j].name, new Vector2(j * blockWidth, i * blockHeight + 7 * blockHeight) + new Vector2(32, 32),
                               originX / 2, originY / 2, textureScaleX, textureScaleY);
                        }
                    }
                }
                foreach (var effect in blocksEffects)
                {
                    effect.Draw(render);
                }
                render.DrawNumber("Number", new Vector2(0, 30), score); //score
                render.DrawNumber("Number", new Vector2(896, 30), gameMinute); //time
                render.DrawTexture("ScoreTime", Vector2.Zero);
            }
        }
        private int BlocksCheck(BlocksStatus blocksStatus, int X, int Y, int Counter)
        {
            if(X < 0 || width <= X || Y < 0 || height <= Y)
            {
                return Counter;
            }
            if(arrayCheck[Y][X] || blocksArray[Y][X].status == BlocksStatus.No || blocksArray[Y][X].status != blocksStatus)
            {
                return Counter;
            }

            Counter++;
            arrayCheck[Y][X] = true;
            Counter = BlocksCheck(blocksStatus, X, Y - 1, Counter);
            Counter = BlocksCheck(blocksStatus, X, Y + 1, Counter);
            Counter = BlocksCheck(blocksStatus, X - 1, Y, Counter);
            Counter = BlocksCheck(blocksStatus, X + 1, Y, Counter);

            return Counter;
        }
        private void Disappear(BlocksStatus blocksStatus, int X, int Y)
        {          
            if (X < 0 || width <= X || Y < 0 || height <= Y)
            {
                return;
            }
            if(blocksArray[Y][X].status == BlocksStatus.No || blocksArray[Y][X].status != blocksStatus)
            {
                return;
            }
            blocksArray[Y][X].status = BlocksStatus.No;
            Disappear(blocksStatus, X, Y - 1);
            Disappear(blocksStatus, X, Y + 1);
            Disappear(blocksStatus, X - 1, Y);
            Disappear(blocksStatus, X + 1, Y);
            blocksEffects.Add(new BlocksEffect(new Vector2(X * blockWidth, (Y + 7) * blockHeight)));
        }
        public int GetScore()
        {
            return score;
        }
        public bool GetGameEnding()
        {
            return GameEnd;
        }
        public bool GetSpecialEnding()
        {
            return SpecialEnding;
        }

    }
}
