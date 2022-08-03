using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private float _speed = 3;
    [Space]
    [SerializeField] private Transform _turret;
    [SerializeField] private float _reloadingTime = 1;
    [Space]
    [SerializeField] private Transform _muzzleTransform;
    [SerializeField] private Transform _bulletsContainer;
    [SerializeField] private GameObject _bulletPrefab;
    [Space]
    [SerializeField] private Joystick _moveJoystick;
    [SerializeField] private Joystick _attackJoystick;

    private bool _isNeedToShoot;
    private float _currentReloadingTime;
    
    private void Start()
    {
        // Cursor.lockState = CursorLockMode.Locked;
        // Cursor.visible = false;

        StartCoroutine(ShootingCoroutine());
    }

    void Update()
    {
        Move();
        Attack();
    }

    private void Move()
    {
        float horizontalMove = _moveJoystick.Horizontal;
        float verticalMove = _moveJoystick.Vertical;
        
        if (horizontalMove * horizontalMove + verticalMove * verticalMove == 0)
            return;

        Quaternion rot = Quaternion.LookRotation(new Vector3(horizontalMove, 0, verticalMove), transform.up);
        transform.rotation = rot;

        _characterController.Move(_speed * Time.deltaTime * transform.forward);
    }

    private void Attack()
    {
        float horizontalAttack = _attackJoystick.Horizontal;
        float verticalAttack = _attackJoystick.Vertical;
        
        if (horizontalAttack * horizontalAttack + verticalAttack * verticalAttack == 0)
            return;
        
        Quaternion rot = Quaternion.LookRotation(new Vector3(horizontalAttack, 0, verticalAttack), _turret.up);
        _turret.rotation = rot;

        Shoot();
    }

    private void Shoot()
    {
        _isNeedToShoot = true;
    }

    private IEnumerator ShootingCoroutine()
    {
        while (true)
        {
            // wait for shoot command
            while (!_isNeedToShoot)
                yield return null;
            
            // shoot
            Instantiate(_bulletPrefab, _muzzleTransform.position, _muzzleTransform.rotation, _bulletsContainer);
            
            // reload
            _currentReloadingTime = _reloadingTime;
            while (_currentReloadingTime > 0)
            {
                _currentReloadingTime -= Time.deltaTime;
                yield return null;
            }

            // prepare for new shoot command
            _isNeedToShoot = false;
        }
    }
}