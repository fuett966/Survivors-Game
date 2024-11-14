using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CoinMagnet : MonoBehaviour
{
    [SerializeField] private SphereCollider _collider;
    [SerializeField] private float _magnetRadius = 2f;
    [SerializeField] private float _initialSpeed = 0.1f;
    [SerializeField] private float _acceleration = 0.1f;
    [SerializeField] private float _distanceThreshold = 0.1f;

    public float MagnetRadius
    {
        get
        {
            return _magnetRadius;
        }
        set
        {
            _collider.radius = value;
            _magnetRadius = value;
        }
    }

    private void Start()
    {
        if (!_collider)
        {
            _collider = GetComponent<SphereCollider>();
        }
        
        _collider.radius = _magnetRadius;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        if (other.CompareTag("Coin"))
        {
            StartCoroutine(MoveToTargetCoroutine(other.transform, transform));
        }
    }

    private IEnumerator MoveToTargetCoroutine(Transform movingTransform,Transform target)
    {
        float currentSpeed = _initialSpeed;

        while (Vector3.Distance(movingTransform.position, target.position) > _distanceThreshold)
        {
            currentSpeed += _acceleration * Time.deltaTime;

            movingTransform.position = Vector3.MoveTowards(movingTransform.position, target.position, currentSpeed * Time.deltaTime);

            yield return null;
        }
        CoinManager.instance.AddCoin(1);
        Destroy( movingTransform.gameObject, 0.1f);
    }
}
