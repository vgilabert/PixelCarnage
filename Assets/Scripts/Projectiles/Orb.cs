using Player;
using UnityEngine;

public class Orb : PlayerProjectile
{
    private Vector3 _playerPosition;
    private float _distanceToPlayer;
    private float _rotationSpeed;

    public void Initialize(float speed, float distanceToPlayer, float rotationSpeed)
    {
        base.Initialize(speed);
        _distanceToPlayer = distanceToPlayer;
        _rotationSpeed = rotationSpeed;
    }

    protected override void Move()
    {
        base.Move();
        _playerPosition = SceneManager.Instance.PlayerPosition;
        transform.RotateAround(_playerPosition, Vector3.forward * _distanceToPlayer, _rotationSpeed * Time.deltaTime);
    }
}