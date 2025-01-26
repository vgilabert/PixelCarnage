using UnityEngine;

namespace Enemy
{
    public class EnemyMovementWander : EnemyMovementBase
    {
        private const float IDLE_DURATION = 2f;
        private const float WANDER_RADIUS = 3f;
        private float _idleTimer;
        private Vector2 _wanderTarget;
        
        protected override void PerformMovement()
        {
            if (_idleTimer < IDLE_DURATION)
            {
                _idleTimer += Time.deltaTime;
            }
            else
            {
                if (Vector2.Distance(transform.position, _wanderTarget) < 0.1f)
                {
                    _wanderTarget = transform.position + (Vector3)Random.insideUnitCircle * WANDER_RADIUS;
                    _idleTimer = 0;
                }
                else
                {
                    transform.position = Vector2.MoveTowards(transform.position, _wanderTarget, Speed * Time.deltaTime);
                }
            }
        }
    }
}