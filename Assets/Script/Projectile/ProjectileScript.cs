using System;
using System.Collections;
using Script.Character;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    [SerializeField]
    private float speed = 0.01f;
    [SerializeField]
    private Animator animator;
    
    // reference to the PooledObject component so we can return to the pool
    private PooledObject pooledObject;
    
    private GameObject creator = null;
    
    private void Awake()
    {
        pooledObject = GetComponent<PooledObject>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!animator.GetBool("IsTouchingSomething"))
        {
            transform.position += Vector3.up * (speed * Time.fixedDeltaTime);
        }
            
    }

    IEnumerator DestroyingTimer()
    {
        yield return new WaitForSeconds(0.15f);
        pooledObject.Release();
        pooledObject.gameObject.SetActive(false);
    }
    
    public void Deactivate()
    {
        StartCoroutine(DestroyingTimer());
    }

    private void OnEnable()
    {
        gameObject.GetComponent<CapsuleCollider>().enabled = true;
    }

    private void OnCollisionEnter(Collision other)
    {
        //tell the player that can shoot again
        if (creator != null)
            creator.GetComponent<CharacterShoot>().canShoot = true;
        animator.SetBool("IsTouchingSomething", true); // istouchingsomething id
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        Deactivate();
    }

    public void SetCreator(GameObject p_creator)
    {
        creator = p_creator;
    }
}
