using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private LayerMask _wallMask;
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _lifeTime = 5;

    private void Start()
    {
        StartCoroutine(LifetimeCoroutine());
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * _speed);
        
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.SphereCast(ray, .5f, out RaycastHit hit, Time.deltaTime * _speed + .01f, _wallMask))
        {
            Vector3 reflectDir = Vector3.Reflect(ray.direction, hit.normal);
            float rot = 90 - Mathf.Atan2(reflectDir.z, reflectDir.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, rot, 0);
        }
    }

    private IEnumerator LifetimeCoroutine()
    {
        yield return new WaitForSeconds(_lifeTime);

        Destroy(gameObject);
    }
}