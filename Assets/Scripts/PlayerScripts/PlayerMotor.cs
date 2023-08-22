using UnityEngine;

namespace PlayerScripts
{
    public class PlayerMotor : MonoBehaviour
    {
        [Header("References")]
        private CharacterController _controller;
        private Vector3 _playerVelocity;

        [Header("Player Values")] 
        private bool _isGrounded;
        private bool _lerpCrouch;
        private bool _crouching;
        private bool _sprinting;
        public float crouchTimer;
    
        public float gravity = -9.8f;
        private float _currentSpeed;
        public float normalSpeed = 5f;
        public float sprintSpeed = 8f;
        public float jumpHeight = 3f;

    
        // Start is called before the first frame update
        void Start()
        {
            _controller = GetComponent<CharacterController>();
            _currentSpeed = normalSpeed;
        }

        // Update is called once per frame
        void Update()
        {
            _isGrounded = _controller.isGrounded;
            if (_lerpCrouch)
            {
                crouchTimer += Time.deltaTime;
                float p = crouchTimer / 1;
                p *= p;
                if (_crouching)
                {
                    _controller.height = Mathf.Lerp(_controller.height, 1, p);
                }
                else
                {
                    _controller.height = Mathf.Lerp(_controller.height, 2, p);
                }

                if (p >1)
                {
                    _lerpCrouch = false;
                    crouchTimer = 0f;
                }
            }
        }
    
        //receives inputs from InputManager script
        public void ProcessMove(Vector2 input)
        {
            //movement
            Vector3 moveDirection = Vector3.zero;
            moveDirection.x = input.x;
            moveDirection.z = input.y;
            _controller.Move(transform.TransformDirection(moveDirection) * _currentSpeed * Time.deltaTime);

            //Gravity shit
            _playerVelocity.y += gravity * Time.deltaTime;
            if (_isGrounded && _playerVelocity.y <0)
            {
                _playerVelocity.y = -2f;
            }
            _controller.Move(_playerVelocity * Time.deltaTime);
        }

        public void Jump()
        {
            if (_isGrounded)
            {
                _playerVelocity.y = Mathf.Sqrt(jumpHeight * -3f * gravity);
            }
        }

        public void Crouch()
        {
            _crouching = !_crouching;
            crouchTimer = 0;
            _lerpCrouch = true;
        }

        public void Sprint()
        {
            _sprinting = !_sprinting;
            if (_sprinting)
            {
                _currentSpeed = sprintSpeed;
            }
            else
            {
                _currentSpeed = normalSpeed;
            }
        }
    }
}
