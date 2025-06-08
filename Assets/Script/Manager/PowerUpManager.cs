using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class PowerUpManager : MonoBehaviour
{
    
    [SerializeField]
    private ObjectPool powerUpPool;
    [SerializeField]
    private List<PooledObject> powerUpPoolled;
    private List<GameObject> bubbles = null;
    
    //SPAWN POWERUPS FUNCTIONS
    public void SpawnPowerUp(GameObject bubble) 
    {
        Random random = new Random();
        GameObject pulledObject = powerUpPool.GetPooledObject(powerUpPoolled[random.Next(0,powerUpPoolled.Count)]).gameObject;
        if (pulledObject == null)
            return;
        pulledObject.transform.position = bubble.transform.position;
    }
    
    //FREEZE POWER UP FUNCTION
    IEnumerator RemoveFreezing()
    {
        yield return new WaitForSeconds(3f);
        bubbles = GameManager.Instance.GetBubbles();
        foreach (var elem in bubbles)
        {
            elem.GetComponent<ScriptedBubbleMovement>().shouldFreeze = false;
        }
        GameManager.Instance.SetIsFreezingActive(false);
    }
    public void FreezeBubbles()
    {
        bubbles = GameManager.Instance.GetBubbles();
        foreach (var elem in bubbles)
        {
            elem.GetComponent<ScriptedBubbleMovement>().shouldFreeze = true;
        }
        GameManager.Instance.SetIsFreezingActive(true);
        StartCoroutine(RemoveFreezing());
    }
    
    //BOMB POWERUP FUNCTION
    public void DestroyBubbles() //called by bomb powerUp
    {
        bubbles = GameManager.Instance.GetBubbles();
        foreach (var elem in bubbles)
        {
            elem.GetComponent<DamageBubble>().DestroyBubble();
        }
    }
}
