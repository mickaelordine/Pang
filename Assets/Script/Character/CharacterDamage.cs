using System;
using System.Collections;
using UnityEngine;

public class CharacterDamage : MonoBehaviour
{
    [SerializeField]
    LayerMask bubbleLayer;

    private int lives = 2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void addOneLive()
    {
        lives++;
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        int objLayer = collision.gameObject.layer; // Ottieni il numero del layer dell'oggetto
        int objLayerMask = 1 << objLayer; // Crea la bitmask per il layer dell'oggetto
        if ((bubbleLayer.value & objLayerMask) != 0)
        {
            Debug.Log(collision.gameObject.name);
            lives--;
            if (lives == 0)
            {
                GameManager.Instance.EndGameBad();
            }
        }
    }
}
