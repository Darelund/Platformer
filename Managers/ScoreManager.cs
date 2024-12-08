using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    public class ScoreManager
    {
        public static int PlayerScore { get; private set; } = 0;
        public static event Action OnScoreChanged;
        public static void UpdateScore(int points)
        {
            PlayerScore += points;
            Debug.WriteLine(points);
            OnScoreChanged?.Invoke();
        }
        public static void ResetScore()
        {
            PlayerScore = 0;
            OnScoreChanged?.Invoke();
        }
    }
}
