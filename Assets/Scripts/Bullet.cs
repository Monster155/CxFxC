using System;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _lifeTime = 5;

    private void Start()
    {
        _rigidbody.AddForce(transform.forward * _speed * 100);
        StartCoroutine(LifetimeCoroutine());
    }

    private IEnumerator LifetimeCoroutine()
    {
        yield return new WaitForSeconds(_lifeTime);
        Destroy(gameObject);
    }
}