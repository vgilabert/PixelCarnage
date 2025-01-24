using UnityEngine;

namespace Enemy
{
    public abstract class EnemyMovementBase : MonoBehaviour
    {
        protected float Speed;
        protected bool IsPushed;
        protected Vector3 PushDirection;
        protected float PushForce;
        protected float PushDuration;
        private float _pushTimer;
        
        protected virtual void Update()
        {
            if (!IsPushed)
            {
                PerformMovement();
            }
        }
        
        protected virtual void PerformMovement()
        {
            
        }
        
        public void SetSpeed(float speed)
        {
            Speed = speed;
        }
        
        public void Push(Vector3 direction, float force)
        {
            IsPushed = true;
            PushDirection = direction;
            PushForce = force;
            PushDuration = 0.3f;
            _pushTimer = 0;
        }
    }
}