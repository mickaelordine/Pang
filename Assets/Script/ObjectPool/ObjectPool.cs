using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] 
    private uint maxSize = 15; //max amount of pooledObj
    [SerializeField]
    private PooledObject pooledObject; //reference of the polledobj prefab, we can also use gameobj, but the we should acces to the component, in this way is much better
    
    private Stack<PooledObject> stack; //stack implementation for the poolledObjs


    private void Start()
    {
        SetUpPoll();
    }

    /*
        The SetupPool method populates the object pool . Create a new stack of
        PooledObjects and then instantiate copies of the objectToPool to fill it with
        initPoolSize elements . Invoke SetupPool in Start to make sure that it runs
        once during gameplay
     */
    private void SetUpPoll()
    {
        stack = new Stack<PooledObject>();
        PooledObject instance = null;
        for (int i = 0; i < maxSize; i++)
        {
            instance = Instantiate(pooledObject);
            instance.Pool = this;
            instance.gameObject.SetActive(false);
            stack.Push(instance);
        }
    }

    //return the firstactive obj in the pool
    public PooledObject GetPooledObject()
    {
        //if stack doesn't cointain enough objs, then instantiate new one in this case
        if (stack.Count == 0)
        {
            PooledObject instance = Instantiate(pooledObject);
            instance.Pool = this;
            stack.Push(instance);
        }
        //otherwise pick a already present one in the pool
        PooledObject newInstance = stack.Pop();
        newInstance.gameObject.SetActive(true);
        return newInstance;
    }
    
    //replace the polledObj in the stack for a future re-use
    public void ReturnToPool(PooledObject pooledObject)
    {
        stack.Push(pooledObject);
        pooledObject.gameObject.SetActive(false);
    }
}