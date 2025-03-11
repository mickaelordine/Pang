using System.Collections;
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
    private float speed = 5f; // Velocità iniziale
    [SerializeField]
    private float gravity = -9.8f; // Gravità simulata
    private Vector2 position;
    private bool shouldFall = true;
    // reference to the PooledObject component so we can return to the pool
    private PooledObject pooledObject = null;

    private void Awake()
    {
        //position = gameObject.transform.position;
        pooledObject = gameObject.GetComponentInParent<PooledObject>(); //setting up the pooledObj reference
    }
    private void Start() => StartCoroutine(DestroyingTimer(lifeTime));
    


    void Update()
    {
        Movement();
    }

    IEnumerator DestroyingTimer(float time)
    {
        yield return new WaitForSeconds(time);
        pooledObject.Release();
        pooledObject.gameObject.SetActive(false);
    }
    
    private void Movement()
    {
        if (shouldFall)
        {
            transform.position += Vector3.down * (speed * Time.deltaTime);
        }
        // else
        // {
        //     transform.position = Vector3.down * (0 * Time.deltaTime);
        // }
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
                StartCoroutine(DestroyingTimer(0.001f));;
            }
        }else if ((groundMask.value & objLayerMask) != 0 || (cielingMask.value & objLayerMask) != 0)
        { 
            shouldFall = false;
            Debug.Log(gameObject.name + " " + shouldFall);
        }
        
    }

    protected abstract void ApplyEffect(T component);
}
