using Extensions;
using UnityEngine;

namespace Shared
{
    public class TargetMarker : MonoSingleton<TargetMarker>
    {
        [SerializeField] private float rotationSpeed = 10f;

        private void Update()
        {
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }
    }
}
