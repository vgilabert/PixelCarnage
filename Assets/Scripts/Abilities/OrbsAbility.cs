using Extensions;
using UnityEngine;

namespace WeaponMods
{
    public class OrbsWeaponMod : MonoBehaviour
    {
        [SerializeField] private GameObject orbPrefab;
        [SerializeField] private float orbDistance;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private int baseOrbCount = 2;

        private void Start()
        {
            SetUpOrbs(baseOrbCount);
        }

        protected void OnEnable()
        {
            SetUpOrbs(baseOrbCount);
        }

        protected void OnDisable()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }
        
        private void SetUpOrbs(int orbCount)
        {
            // Destroy all orbs
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            // Create new orbs
            for (int i = 0; i < orbCount; i++)
            {
                Vector2 orbPosition = (Vector2)transform.position + MathHelper.PointOnCircle(i, orbCount, orbDistance);
                Orb orb = Instantiate(orbPrefab, orbPosition, Quaternion.identity, transform).GetComponent<Orb>();
                orb.Initialize(rotationSpeed, orbDistance, rotationSpeed);
            }
        }

        protected void OnAbilityUpgrade()
        {
            SetUpOrbs(baseOrbCount);
        }
    }
}