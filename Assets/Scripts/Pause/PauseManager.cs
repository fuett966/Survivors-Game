using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static Action OnGamePaused;
    public static Action OnGameResumed;

    private bool _isPaused = false;

    private void OnEnable()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<IHealth>().Died += PauseGame;
    }

    public void PauseGame()
    {
        _isPaused = true;
        OnGamePaused?.Invoke();
    }

    public void ResumeGame()
    {
        _isPaused = false;
        OnGameResumed?.Invoke();
    }
}
