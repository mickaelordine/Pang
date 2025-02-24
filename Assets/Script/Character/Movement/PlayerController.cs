using System;
using Script.Character.Movement.StateMachine;
using UnityEngine;

namespace Script.Character.Movement
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerInput playerInput;
        private PlayerStateMachine playerStateMachine;

        [Header("Movement")]
        [Tooltip("Horizontal speed")]
        [SerializeField] private float moveSpeed = 5f;
        [Tooltip("Rate of change for move speed")]
        [SerializeField] private float acceleration = 10f;

        [Tooltip("Custom gravity for player")]
        [SerializeField] private float gravity = -15f;
        
        [Header("Colliders")]
        [Tooltip("Collider info for player")]
        [SerializeField] private LayerMask groundLayers;
        [SerializeField] private LayerMask ladderLayers;
        [SerializeField] private GameObject raycastPos;
        
        [Header("Animation")]
        [SerializeField]
        Animator animator;
        
        public CharacterController CharController => charController;
        //public bool IsGrounded => isGrounded;
        public PlayerStateMachine PlayerStateMachine => playerStateMachine;


        private CharacterController charController;
        private float targetSpeed;
        private float verticalVelocity;
        
        private void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
            charController = GetComponent<CharacterController>();

            // initialize state machine
            playerStateMachine = new PlayerStateMachine(this, animator);
        }
        
        private void Start()
        {
            playerStateMachine.Initialize(playerStateMachine.idleState);
        }

        private void Update()
        {
            // update the current State
            playerStateMachine.Execute();
        }
        
        private void LateUpdate()
        {
            Move();
        }

        private void Move()
        {
            Vector3 inputVector = playerInput.InputVector;

            // If we are not providing movement input, set target speed to 0
            if (inputVector == Vector3.zero)
            {
                targetSpeed = 0;
            }
            else
            {
                targetSpeed = moveSpeed;
            }
            
            checkMovingAnim(inputVector);
            
            CalculateVertical(inputVector);
            
            
            // Move the player
            charController.Move(new Vector3(inputVector.x * targetSpeed, verticalVelocity, 0) * Time.deltaTime);
            
        }

        private void checkMovingAnim(Vector3 inputVector)
        {
            if (inputVector.x > 0)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
            else if (inputVector.x < 0)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
                
            }
            else
            {
                animator.SetBool("IsMoving", false);
            }
        }

        private void CalculateVertical(Vector3 inputVector)
        {
            //climbing check
            if (IsClimbing() && inputVector.y > Mathf.Epsilon)
            {
                verticalVelocity = moveSpeed;
            } //upward
                
            else if (IsClimbing() && inputVector.y < -Mathf.Epsilon)
            {
                verticalVelocity = -moveSpeed;
            } //backwards
                
            else if (IsClimbing() && inputVector.y < Mathf.Epsilon && inputVector.y > -Mathf.Epsilon)
            {
                verticalVelocity = 0;
            } //stay on ladder
            else if (!IsClimbing() && !IsGrounded()) //also check ground
            {
                verticalVelocity = gravity;
            } //is falling
            else if (IsGrounded() && !IsClimbing())
            {
                verticalVelocity = 0;
            } //is grounded
        }

        private bool IsClimbing()
        {
            // check if grounded
            return  Physics.CheckSphere(raycastPos.transform.position, 0.1f, ladderLayers);
        }
        
        private bool IsGrounded()
        {
            return Physics.CheckSphere(raycastPos.transform.position, 0.1f, groundLayers);
        }
    }
}