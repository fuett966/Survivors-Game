using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;

    public static Action<int> OnCoinChanged;
    private int _coin = 0;

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
    
    public void AddCoin(int value)
    {
        _coin += value;
        OnCoinChanged?.Invoke(_coin);
    }
}
