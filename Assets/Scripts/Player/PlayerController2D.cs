using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerController2D : MonoBehaviour
    {
        [Header("Settings"), Space(5)]
        [SerializeField] private float baseSpeed = 10f;
        [SerializeField] private float smoothTime = 0.1f;
        [SerializeField] private bool useRigidbody = false;
    
        // Component references
        private Rigidbody2D _rigidbody2D;
    
        // private fields
        private Vector2 _rawInputVector2D;
        private Vector2 _currentInputVector2D;
        private Vector2 _smoothInputVelocity;

        private float _maxSpeed;
    
        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            _maxSpeed = baseSpeed;
        }

        public void Move(InputAction.CallbackContext context)
        {
            _rawInputVector2D = context.ReadValue<Vector2>();
        }
    
        private void Update()
        {
            if (useRigidbody)
            {
                UpdateVelocity();
            }
            else
            {
                UpdatePosition();
            }
        }

        private void UpdatePosition()
        {
            _currentInputVector2D = Vector2.SmoothDamp(_currentInputVector2D, _rawInputVector2D, ref _smoothInputVelocity, smoothTime);
            transform.position += (Vector3)_currentInputVector2D * (_maxSpeed * Time.deltaTime);
        }
    
        private void UpdateVelocity()
        {
            if (!_rigidbody2D) return;
        
            _currentInputVector2D = Vector2.SmoothDamp(_currentInputVector2D, _rawInputVector2D, ref _smoothInputVelocity, smoothTime);
            _rigidbody2D.linearVelocity = _currentInputVector2D * _maxSpeed;
        }
    
        public float SetMaxSpeed(float value)
        {
            return _maxSpeed = value;
        }
    }
}
