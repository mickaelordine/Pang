using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script.PowerUp.Types
{
    public class ClockPowerUp : PickupableObject<Collider>
    {
        [SerializeField]
        private AudioSource audioSource;

        private List<GameObject> bubbles = null;
        protected override void ApplyEffect(Collider component)
        {
            audioSource.Play();
            GameManager.Instance.GetPowerUpManager().FreezeBubbles();
        }
    }
}