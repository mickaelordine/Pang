using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    [SerializeField]
    private float speed = 0.01f;
    [SerializeField]
    private Animator animator;
    
    GameObject creator = null;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Update()
    {
        if(!animator.GetBool("IsTouchingSomething"))
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + speed, 0);
    }

    IEnumerator DestroyingTimer()
    {
        yield return new WaitForSeconds(0.15f);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.name);
        //tell the player that can shoot again
        if (creator != null)
            creator.GetComponent<CharacterMovement>().canShoot = true;
        animator.SetBool("IsTouchingSomething", true); // istouchingsomething id
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        StartCoroutine(DestroyingTimer()); //calling a timer to make the projectile death anim visibile
    }

    public void SetCreator(GameObject p_creator)
    {
        creator = p_creator;
    }
}
