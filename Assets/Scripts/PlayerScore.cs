using System;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    public static PlayerScore instance;

    public static Action<int, bool> ScoreChanged;
    private int score = 0;
    [SerializeField] private GameObject deathPanel;
    [SerializeField] private PauseManager pauseManager;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int value)
    {
        score += value;
        ScoreChanged.Invoke(score, false);
    }
    public void FinishGame()
    {
        deathPanel.SetActive(true);

        if (score > PlayerDataManager.instance.playerData.score)
        {
            PlayerDataManager.instance.playerData.score = score;
            PlayerDataManager.instance.SavePlayerData(PlayerDataManager.instance.playerData);

            ScoreChanged.Invoke(score, true);
        }
        else
        {
            ScoreChanged.Invoke(score, false);
        }
        pauseManager.PauseGame();
    }
}
