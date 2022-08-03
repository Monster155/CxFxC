using UnityEngine;

public class PCameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    void Update()
    {
        transform.position = _playerTransform.position;
    }
}
