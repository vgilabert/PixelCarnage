using Extensions;
using UnityEngine;

namespace Abilities
{
    public class OrbsAbility : MonoBehaviour
    {
        [SerializeField] private GameObject orbPrefab;
        [SerializeField] private float orbDistance;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private int baseOrbCount = 2;
        private int _currentOrbCount = 2;

        private void OnEnable()
        {
            for (int i = 0; i < baseOrbCount; i++)
            {
                Vector2 orbPosition = (Vector2)transform.position + MathHelper.PointOnCircle(i, baseOrbCount, orbDistance);
                Orb orb = Instantiate(orbPrefab, orbPosition, Quaternion.identity, transform).GetComponent<Orb>();
                orb.Initialize(rotationSpeed, orbDistance, rotationSpeed);
            }
        }

        private void OnDisable()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }

        
    }
}