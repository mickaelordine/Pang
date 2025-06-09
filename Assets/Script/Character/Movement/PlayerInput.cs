using UnityEngine;

namespace Script.Character.Movement
{
    public class PlayerInput : MonoBehaviour
    {
        [Header("Controls")]
        [SerializeField] private KeyCode upward = KeyCode.W;
        [SerializeField] private KeyCode backward = KeyCode.S;
        [SerializeField] private KeyCode left = KeyCode.A;
        [SerializeField] private KeyCode right = KeyCode.D;
        
        public Vector3 InputVector => inputVector;
        public bool IsClimbing { get => isClimbing; set => isClimbing = value; }
        
        private Vector3 inputVector;
        private bool isClimbing;
        private float xInput;
        private float yInput;
        private bool isMovingLeft = false, isMovingRight = false, isMovingUp = false, isMovingDown = false;
        
        public void HandleInput()
        {
            // Reset input
            xInput = 0;
            yInput = 0;

            if (Input.GetKey(upward) || isMovingUp)
            {
                yInput++;
            }

            if (Input.GetKey(backward) || isMovingDown)
            {
                yInput--;
            }

            if (Input.GetKey(left) || isMovingLeft)
            {
                xInput--;
            }

            if (Input.GetKey(right) || isMovingRight)
            {
                xInput++;
            }

            inputVector = new Vector3(xInput, yInput, 0.0f);
        }

        private void Update()
        {
            HandleInput();
        }


        public void MoveLeft()
        {
            isMovingLeft = true;
        }
        public void MoveRight()
        {
            isMovingRight = true;
        }
        public void MoveUp()
        {
            isMovingUp = true;
        }
        public void MoveDown()
        {
            isMovingDown = true;
        }
        
        public void StopMoveLeft()
        {
            isMovingLeft = false;
        }
        public void StopMoveRight()
        {
            isMovingRight = false;
        }
        public void StopMoveUp()
        {
            isMovingUp = false;
        }
        public void StopMoveDown()
        {
            isMovingDown = false;
        }

        private void Reset()
        {
            isMovingDown = isMovingLeft = isMovingRight = isMovingUp = isMovingDown = false;
        }
        
    }
}