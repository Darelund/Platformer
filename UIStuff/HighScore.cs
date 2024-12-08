using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    public class HighScore
    {
        private static List<Score> _highScores = new List<Score>();
        private const string ScoreFilePath = "Content/HighScore.txt";
        private const string _nameText = "Name";
        private const string _pointsText = "Points";
        private const string _levelsText = "Level";
        private static readonly Vector2 _nameColumnPos = new Vector2(GameManager.Window.ClientBounds.Width / 2 - 150, 250);
        private static readonly Vector2 _pointsColumnPos = new Vector2(GameManager.Window.ClientBounds.Width / 2 + 50, 250);
        private static readonly Vector2 _levelsColumnPos = new Vector2(GameManager.Window.ClientBounds.Width / 2 + 200, 250);

        private static UIText _highScoretext = new UIText(ResourceManager.GetSpriteFont("GameText"), "HighScore", new Vector2(GameManager.Window.ClientBounds.Width / 2 - 50, 100), Color.White, 0.8f, Vector2.Zero, 0.9f);
        private static UIText _Toptext = new UIText(ResourceManager.GetSpriteFont("GameText"), $"{_nameText,2}{_pointsText,2}{_levelsText,0}", new Vector2(GameManager.Window.ClientBounds.Width / 2, 250), Color.White, 0.8f, Vector2.Zero, 0.9f);
        private static UIText _scoreText = new UIText(ResourceManager.GetSpriteFont("GameText"), "empty", new Vector2(GameManager.Window.ClientBounds.Width / 2 - 120, 300), Color.White, 0.8f, Vector2.Zero, 0.9f);
        //Read high score list from file
        //public static void LoadScores()
        //{
        //    //Todo Load scores from saved file

        //    _highScores.Add(new Score("Daniel", 44, 1));
        //    _highScores.Add(new Score("Harry", 84, 2));
        //    _highScores.Add(new Score("Diane", 176, 4));
        //    _highScores.Add(new Score("Moira", 29, 1));
        //}
        public static void LoadScores()
        {
            List<string> fileLines = FileManager.ReadFromFile(ScoreFilePath);
            foreach (var line in fileLines)
            {
                List<string> scoreLine = line.Split(' ').ToList();
                string name = scoreLine[0];
                int points = int.Parse(scoreLine[1]);
                int level = int.Parse(scoreLine[2]);
                _highScores.Add(new Score(name, points, level));
            }
            Debug.Write("");
        }
        private static void SaveScores()
        {
            using (StreamWriter writer = new StreamWriter(ScoreFilePath))
            {
                foreach (var score in _highScores)
                {
                    writer.WriteLine($"{score.Name} {score.Points} {score.Levels}");
                }
            }
        }
        public static void DisplayScores(SpriteBatch spriteBatch)
        {
            var sortedScore = (from score in _highScores orderby score.Points descending select score).ToList();

            _highScoretext.Draw(spriteBatch);
            for (int i = 0; i < sortedScore.Count; i++)
            {
                var yOffset = _nameColumnPos + new Vector2(0, (100 * i));
                _scoreText._text = $"{sortedScore[i].Name}";
                _scoreText.Draw(spriteBatch, yOffset);

                yOffset = _pointsColumnPos + new Vector2(0, (100 * i));
                _scoreText._text = $"{sortedScore[i].Points}";
                _scoreText.Draw(spriteBatch, yOffset);

                yOffset = _levelsColumnPos + new Vector2(0, (100 * i));
                _scoreText._text = $"{sortedScore[i].Levels}";
                _scoreText.Draw(spriteBatch, yOffset);
            }
        }
        public static void UpdateScore(string name, int points, int level)
        {
            var existingScore = _highScores.FirstOrDefault(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (existingScore != null)
            {
                existingScore.UpdatePoints(points);
                existingScore.UpdateLevel(level);
            }
            else
            {
                if (_highScores.Count >= 5)
                {
                    int lowestPointCount = 999999;
                    int lowestHighScore = 0;
                    for (int i = 0; i < _highScores.Count; i++)
                    {
                        if (_highScores[i].Points < lowestPointCount)
                        {
                            lowestPointCount = _highScores[i].Points;
                            lowestHighScore = i;
                        }
                    }
                    _highScores.RemoveAt(lowestHighScore);
                    _highScores.Add(new Score(name, points, level));
                }
                else
                {
                    _highScores.Add(new Score(name, points, level));
                }
            }

            SaveScores();
        }
        public class Score
        {
            public readonly string Name;
            public int Points { get; private set; }
            public int Levels { get; private set; }
            public Score(string name, int points, int levels)
            {
                Name = name;
                Points = points;
                Levels = levels;
            }
            public void UpdatePoints(int points)
            {
                if (Points < points)
                {
                    Points = points;
                }
            }
            public void UpdateLevel(int level)
            {
                if (Levels < level)
                {
                    Levels = level;
                }
            }
        }
    }
}
