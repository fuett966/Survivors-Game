using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    private void OnEnable()
    {
        CoinManager.OnCoinChanged += UpdateText;
    }

    private void OnDisable()
    {
        CoinManager.OnCoinChanged -= UpdateText;
    }

    private void UpdateText(int coins)
    {
        _text.text = Convert.ToString(coins);
    }
}