using System;
using UnityEngine;

public class PooledObject : MonoBehaviour
{
    private ObjectPool pool; // Each pooled element will have a small PooledObject component, just to reference the ObjectPool
    public ObjectPool Pool {get => pool; set => pool = value;} //getter and setter of the pool
    
    private PooledObject prefabReference;
    public PooledObject PrefabReference { get => prefabReference; set => prefabReference = value; }
    
    /*
     * Calling Release disables the GameObject and returns it to the pool queue
     */
    public void Release()
    {
        pool.ReturnToPool(this);
    }
}