using System;
using System.Collections;
using Script.Character;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class ProjectileScript : MonoBehaviour
{
    [SerializeField]
    private float speed = 0.01f;
    [SerializeField]
    private Animator animator;
    private CharacterShoot.ShootType shooterType;
    
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
        {
            CharacterShoot shootCompRef = creator.gameObject.GetComponent<CharacterShoot>();
            if(shootCompRef.ammoAmount < shootCompRef.maxAmmo && shootCompRef.shooterType == this.shooterType)
                shootCompRef.ammoAmount += 1;
        }
            
        animator.SetBool("IsTouchingSomething", true); // istouchingsomething id
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        Deactivate();
    }

    public void SetCreator(GameObject p_creator, CharacterShoot.ShootType shooterType)
    {
        creator = p_creator;
        this.shooterType = shooterType;
    }
}
