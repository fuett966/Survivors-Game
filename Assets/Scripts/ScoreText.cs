using System;
using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    private void OnEnable()
    {
        PlayerScore.ScoreChanged += UpdateText;
    }

    private void OnDisable()
    {
        PlayerScore.ScoreChanged -= UpdateText;
    }

    private void UpdateText(int newScore, bool newRecord)
    {
        if (newRecord)
        {
            text.text = "New record: " + Convert.ToString(newScore);
        }
        else
        {
            text.text = "Score: " + Convert.ToString(newScore);
        }
    }

}
