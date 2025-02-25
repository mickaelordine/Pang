using UnityEngine;
using UnityEngine.UIElements;

public class DamageBubble : MonoBehaviour
{
    [SerializeField] 
    private GameObject childBubble;
    [SerializeField] 
    private LayerMask projectileLayer;
    [SerializeField] 
    private LayerMask playerLayer;


    private void SpawnChildBubble()
    {
        //spawn 2 child prefab
        Vector3 bubble2Pos = new Vector3(gameObject.transform.position.x + 0.5f, gameObject.transform.position.y, 0);
        Vector3 bubble1Pos = new Vector3(gameObject.transform.position.x - 0.5f, gameObject.transform.position.y, 0);
        GameObject c_bubble1 = Instantiate(childBubble, bubble1Pos, Quaternion.identity);
        GameObject c_bubble2 = Instantiate(childBubble, bubble2Pos, Quaternion.identity);
        c_bubble2.GetComponent<ScriptedBubbleMovement>().velocity = new Vector2(2,5);
        c_bubble1.GetComponent<ScriptedBubbleMovement>().velocity = new Vector2(-2,5);
    }
    public void DestroyBubble()
    {
        if (childBubble != null)
        {
            SpawnChildBubble();
        }
        //destroy
        Destroy(gameObject);
    }
    
    void OnCollisionEnter(Collision other)
    {
        
        int objLayer = other.gameObject.layer; // Ottieni il numero del layer dell'oggetto
        int objLayerMask = 1 << objLayer; // Crea la bitmask per il layer dell'oggetto
        
        
        if ((projectileLayer.value & objLayerMask) != 0 || (playerLayer.value & objLayerMask) != 0 )
        {
            DestroyBubble();
        }
        
    }
}
