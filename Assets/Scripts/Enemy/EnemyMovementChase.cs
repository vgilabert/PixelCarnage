using UnityEngine;

namespace Enemy
{
    public class EnemyMovementChase : EnemyMovementBase
    {
        protected override void PerformMovement()
        {
            base.PerformMovement();
            Vector2 targetPosition = SceneManager.Instance.PlayerPosition;
            
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, Speed * Time.deltaTime);
        }
    }
}