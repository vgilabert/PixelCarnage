using UnityEngine;

namespace Enemy
{
    public class EnemyMovementWander : EnemyMovementBase
    {
        private const float IDLE_DURATION_MIN = 1.5f;
        private const float IDLE_DURATION_MAX = 2f;
        private const float WANDER_RADIUS = 4f;
        
        private float _currentIdleDuration;
        private float _idleTimer;
        private Vector2 _wanderTarget;
        
        protected override void Start()
        {
            base.Start();
            _currentIdleDuration = Random.Range(IDLE_DURATION_MIN, IDLE_DURATION_MAX);
            _wanderTarget = transform.position;
        }
        
        protected override void PerformMovement()
        {
            if (_idleTimer < _currentIdleDuration)
            {
                _idleTimer += Time.deltaTime;
            }
            else
            {
                if (Vector2.Distance(transform.position, _wanderTarget) < 0.1f)
                {
                    _wanderTarget = transform.position + (Vector3)Random.insideUnitCircle * WANDER_RADIUS;
                    _idleTimer = 0;
                    _currentIdleDuration = Random.Range(IDLE_DURATION_MIN, IDLE_DURATION_MAX);
                }
                else
                {
                    transform.position = Vector2.MoveTowards(transform.position, _wanderTarget, Speed * Time.deltaTime);
                }
            }
        }
    }
}