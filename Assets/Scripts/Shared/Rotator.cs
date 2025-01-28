using UnityEngine;

namespace Shared
{
    public class Rotator : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed = 10f;

        private void Update()
        {
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }
    }
}
