using System;
using TMPro;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    private void Start()
    {
        PlayerDataManager.instance.LoadPlayerData();
        scoreText.text = "Score: " + Convert.ToString(PlayerDataManager.instance.playerData.score);
    }
}
