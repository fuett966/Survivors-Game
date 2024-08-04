using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static Action OnGamePaused;
    public static Action OnGameResumed;

    private bool isPaused = false;

    //� ������ ��������� ������ ������� ���������� �� ����� ����� ��������� ������ ��������� � ��������� ������ �������
    public void PauseGame()
    {
        isPaused = true;
        OnGamePaused?.Invoke();
    }

    public void ResumeGame()
    {
        isPaused = false;
        OnGameResumed?.Invoke();
    }
}
