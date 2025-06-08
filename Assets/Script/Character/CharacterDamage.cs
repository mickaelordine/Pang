using System;
using System.Collections;
using UnityEngine;

public class CharacterDamage : MonoBehaviour
{
    [SerializeField]
    LayerMask bubbleLayer;
    [SerializeField] 
    private GameObject m_ShieldEffect;
    private int lives = 1;
    private bool m_isShielded = false;

    public void DeactivateShield()
    {
        m_isShielded = false;
        m_ShieldEffect.SetActive(false);
    }
    public void ActivateShield()
    {
        m_isShielded = true;
        m_ShieldEffect.SetActive(true);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (m_isShielded)
        {
            DeactivateShield();
            return;
        }
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
