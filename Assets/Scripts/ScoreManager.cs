using TMPro;
using TMPro.Examples;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    

    int score = 0;

    public void AddScore(int additionalScore)
    {
        score += additionalScore;
        scoreText.text = "Score: " + score;
    }
}
