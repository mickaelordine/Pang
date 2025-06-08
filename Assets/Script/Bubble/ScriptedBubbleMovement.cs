using System;
using UnityEngine;

public class ScriptedBubbleMovement : MonoBehaviour
{
    [SerializeField]
    public Vector2 velocity = new Vector2(2f, 5f); // Velocità iniziale
    [SerializeField]
    private float gravity = -9.8f; // Gravità simulata
    [SerializeField]
    public float minBounceForce = 2f; // Forza minima per evitare che si fermi
    [SerializeField]
    LayerMask groundLayer;
    [SerializeField]
    LayerMask bubbleLayer;
    private Vector2 position;
    [SerializeField]
    AudioSource audioBounce;
    [HideInInspector]
    public bool shouldFreeze = false;

    void Start()
    {
        position = transform.position;
    }

    void FixedUpdate()
    {
        if (!shouldFreeze)
        {
            velocity.y += gravity * Time.deltaTime;
        
            if (velocity.y > minBounceForce)
            {
                velocity.y = minBounceForce;
            }else if (velocity.y < gravity)
            {
                velocity.y = gravity;
            }
            position += velocity * Time.deltaTime;
        
            transform.position = position;
        }
        else
        {
            transform.position = position;
        }
    }

    void OnCollisionEnter(Collision collision) {
        
        int objLayer = collision.gameObject.layer; // Ottieni il numero del layer dell'oggetto
        int objLayerMask = 1 << objLayer; // Crea la bitmask per il layer dell'oggetto
        
        ContactPoint contact = collision.contacts[0];
        
        Vector2 hitDirection = (contact.point - transform.position).normalized;
        
        
        if ((bubbleLayer.value & objLayerMask) == 0)
        {
            audioBounce.Play();
            if (hitDirection.y > 0.5f) // Colpito dall'alto
            {
                velocity.y = -Mathf.Abs(velocity.y);
            }
            else if (hitDirection.y < -0.5f && (groundLayer.value & objLayerMask) != 0) // Colpito dal basso da un ground
            {
                velocity.y = Mathf.Max(minBounceForce); 
            }else if (hitDirection.y < -0.5f && (groundLayer.value & objLayerMask) == 0) // Colpito dal basso da un platform
            {
                velocity.y = Mathf.Max(minBounceForce/1.5f); 
            }
    
            if (hitDirection.x > 0.5f) // Colpito da destra
            {
                velocity.x = -Mathf.Abs(velocity.x); 
            }
            else if (hitDirection.x < -0.5f) // Colpito da sinistra
            {
                velocity.x = Mathf.Abs(velocity.x); 
            }
        }
        
    }
    
}