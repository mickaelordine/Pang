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
        
        public void HandleInput()
        {
            // Reset input
            xInput = 0;
            yInput = 0;

            if (Input.GetKey(upward))
            {
                yInput++;
            }

            if (Input.GetKey(backward))
            {
                yInput--;
            }

            if (Input.GetKey(left))
            {
                xInput--;
            }

            if (Input.GetKey(right))
            {
                xInput++;
            }

            inputVector = new Vector3(xInput, yInput, 0.0f);

            //isJumping = Input.GetKeyDown(jump); find how to manage the isclimbing bool variable
        }

        private void Update()
        {
            HandleInput();
        }
        
    }
}