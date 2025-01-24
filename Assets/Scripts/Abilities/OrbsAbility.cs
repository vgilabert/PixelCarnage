using Extensions;
using StatSystem;
using UnityEngine;

namespace Abilities
{
    public class OrbsAbility : MonoBehaviour
    {
        [SerializeField] private GameObject orbPrefab;
        [SerializeField] private float orbDistance;
        [SerializeField] private float rotationSpeed;
        private int _orbCount = 8;
        
        private void Start()
        {
            Player.Player player = SceneManager.Instance.PlayerReference;
            for (int i = 0; i < _orbCount; i++)
            {
                Vector2 orbPosition = MathHelper.PointOnCircle(i, _orbCount, orbDistance);
                Orb orb = Instantiate(orbPrefab, orbPosition, Quaternion.identity, transform).GetComponent<Orb>();
                Debug.Log("damage: " + player.Stats[StatType.Attack].Value);
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