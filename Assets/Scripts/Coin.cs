using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //anim + add coin + disable
            /*transform.DOMove(other.transform.position, 0.4f).OnComplete(() =>
            {
                CoinManager.instance.AddCoin(1);
                Destroy(this.gameObject, 0.1f);
            });*/
        }
    }
}