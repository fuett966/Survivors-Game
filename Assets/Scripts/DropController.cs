using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class DropController : MonoBehaviour
{
    [SerializeField] private GameObject _coin;

    private void OnEnable()
    {
        GetComponent<IHealth>().Died += DropLoot;
    }

    private void OnDisable()
    {
        GetComponent<IHealth>().Died -= DropLoot;
    }

    private void DropLoot()
    {
        Instantiate(_coin,transform.position,quaternion.identity);
    }
}
