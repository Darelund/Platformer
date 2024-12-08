using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    public class LevelManager
    {
        public static List<Level> Levels = new List<Level>();
        public static Level GetCurrentLevel => Levels[LevelIndex];
        public static int LevelIndex { get; private set; } = - 1;

        public static void CreateLevels()
        {
            Levels.Add(new Level());
            Levels.Add(new Level());
            Levels.Add(new Level());
            Levels.Add(new Level());
        }

        public static void Update(GameTime gameTime)
        {
            if (GetCurrentLevel.IsLevelCompleted())
            {
                GetCurrentLevel.LevelCompleted = true;

            }
            GetCurrentLevel.Update(gameTime);
        }
        public static void Draw(SpriteBatch spriteBatch)
        {
            GetCurrentLevel.Draw(spriteBatch);
        }
        private static void ActivateLevel(int levelIndex, LevelConfig levelConfig, bool runLevel)
        {
            //Do I even need this check? It's not like I am gonna give it the wrong number.
            if (levelIndex > Levels.Count) return;

            int displayLevel = 0;
            ////Shouldn't do this for the display level
            //if (LevelIndex > displayLevel)
            //{
            //}
            if (LevelIndex >= displayLevel && GetCurrentLevel != null)
            {
                HighScore.UpdateScore(GameManager.Name, ScoreManager.PlayerScore, LevelIndex);
                GetCurrentLevel.UnloadLevel();
            }

            LevelIndex = levelIndex;

            if (LevelIndex < Levels.Count)
            {
                GetCurrentLevel.CreateLevel(levelConfig.LevelFile, levelConfig.LevelStartPosition, levelConfig.TileData, levelConfig.GameObjectData);

                if (runLevel)
                {
                    GameManager.ChangeGameState(GameManager.GameState.Playing);
                    Debug.WriteLine("Activated a level and now running it");
                }
            }
        }
        public static void SpecificLevel(int newLevel, bool runLevel)
        {
            GetLevel(newLevel, runLevel);
        }
        public static void NextLevel(bool runLevel)
        {
            int newLevel = LevelIndex + 1;
            if(newLevel > Levels.Count)
            {
                Debug.WriteLine("No more levels");
                return;
            }
            GetLevel(newLevel, runLevel);
        }
        private static void GetLevel(int newLevel, bool runLevel)
        {
            //Temporary solution
            //Should remove zero indexing
            //And what is this switch mess? Should fix when I care
            switch (newLevel)
            {
                case 0: ActivateLevel(newLevel, GameFiles.Levels.LevelData0, runLevel); break;
                case 1: ActivateLevel(newLevel, GameFiles.Levels.LevelData1, runLevel); break;
                case 2: ActivateLevel(newLevel, GameFiles.Levels.LevelData2, runLevel); break;
                case 3: ActivateLevel(newLevel, GameFiles.Levels.LevelData3, runLevel); break;
            }
        }
        public static void Restart()
        {
            ScoreManager.ResetScore();
            ActivateLevel(1, GameFiles.Levels.LevelData1, true);
        }
    }
}
