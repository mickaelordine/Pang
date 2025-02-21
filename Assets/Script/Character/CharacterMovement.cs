using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


/*
 *  TODO
 *  IMPLEMENT THE POSSIBILITY TO USE Y CONTROL WHEN ON LADDER
 */

public class CharacterMovement : MonoBehaviour
{
    [SerializeField]
    float speed = 15f;
    [SerializeField]
    LayerMask ladderLayer;
    [SerializeField]
    LayerMask cielingLayer;
    [SerializeField]
    LayerMask groundLayer;
    [SerializeField]
    GameObject projectile;
    [SerializeField]
    Animator animator;
    
    
    private InputSystem_Actions input = null;
    Rigidbody rb = null;
    Vector2 moveVector = Vector2.zero;
    bool onLadder = false;
    public bool canShoot = true;
    private float verticalVelocity = 0;
    

    private void Awake()
    {
        input = new InputSystem_Actions();
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        input.Enable();
        input.Player.Move.performed += OnMovementPerformed;
        input.Player.Move.canceled += OnMovementCanceled;
        input.Player.Attack.performed += OnAttackPerformed;
    }

    private void OnDisable()
    {
        input.Disable();
        input.Player.Move.performed -= OnMovementPerformed;
        input.Player.Move.canceled -= OnMovementCanceled;
        input.Player.Attack.performed -= OnAttackPerformed;
    }

    private void OnMovementPerformed(InputAction.CallbackContext value)
    {
        animator.SetBool("IsMoving", true);
        moveVector = value.ReadValue<Vector2>();
        if (moveVector.x > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
    }
    
    private void OnMovementCanceled(InputAction.CallbackContext value)
    {
        animator.SetBool("IsMoving", false);
        moveVector = new Vector2(0, verticalVelocity);
    }
    
    private void OnAttackPerformed(InputAction.CallbackContext value)
    {
        if (canShoot)
        {
            GameObject i_projectile = Instantiate(projectile, gameObject.transform.position, Quaternion.identity);
            canShoot = !canShoot;
            i_projectile.GetComponent<ProjectileScript>().SetCreator(gameObject);
        }
            
    }
    
    private bool isFalling()
    {
        return Physics.OverlapSphere(transform.position, 0.5f, groundLayer).Length <= 0;
    }

    private void FixedUpdate()
    {
        if (onLadder && moveVector.y > 0)
        {
            verticalVelocity = 1;
        }else if (onLadder && moveVector.y < 0)
        {
            verticalVelocity = -1;
        }
        else if (isFalling())
        {
            verticalVelocity = -1;
        }
        else if(!isFalling())
        {
            verticalVelocity = 0;
        }
        rb.linearVelocity = new Vector2(moveVector.x * (speed * 1.5f), verticalVelocity * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        int objLayer = other.gameObject.layer; // Ottieni il numero del layer dell'oggetto
        int objLayerMask = 1 << objLayer; // Crea la bitmask per il layer dell'oggetto
        if ((objLayerMask & (1 << ladderLayer)) == 0) 
        {
            onLadder = true;
            verticalVelocity = 0;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        int objLayer = other.gameObject.layer; // Ottieni il numero del layer dell'oggetto
        int objLayerMask = 1 << objLayer; // Crea la bitmask per il layer dell'oggetto
        if ((objLayerMask & (1 << ladderLayer)) == 0) 
        {
            onLadder = false;
            verticalVelocity = -1;
        }
    }
}
