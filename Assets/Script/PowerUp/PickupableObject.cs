using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class PickupableObject<T> : MonoBehaviour where T : Component
{
    [SerializeField]
    private float lifeTime = 5f;
    [Header("Object to interact with")]
    [SerializeField]
    private LayerMask playerMask;
    [SerializeField]
    private LayerMask groundMask;
    [SerializeField]
    private LayerMask cielingMask;
    
    [Header("movement info")]
    [SerializeField]
    public Vector2 velocity = new Vector2(0f, 0f); // Velocità iniziale
    [SerializeField]
    private float gravity = -9.8f; // Gravità simulata
    private Vector2 position;
    private bool shouldFall = true;

    private void Awake()
    {
        position = gameObject.transform.position;
    }
    private void Start() => Destroy(gameObject, lifeTime);


    void Update()
    {
        Movement();
    }
    
    private void Movement()
    {
        if (shouldFall)
        {
            velocity.y += gravity * Time.deltaTime;
            position += velocity * Time.deltaTime;
            transform.position = position;
        }
        else
        {
            velocity.y = 0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        int objLayer = other.gameObject.layer; // Ottieni il numero del layer dell'oggetto
        int objLayerMask = 1 << objLayer; // Crea la bitmask per il layer dell'oggetto
        if ((playerMask.value & objLayerMask) != 0)
        {
            T target = other.GetComponent<T>();
            if (target != null)
            {
                ApplyEffect(target);
                Destroy(gameObject);
            }
        }else if ((groundMask.value & objLayerMask) != 0 || (cielingMask.value & objLayerMask) != 0)
        { 
            shouldFall = false;
        }
        
    }

    protected abstract void ApplyEffect(T component);
}
