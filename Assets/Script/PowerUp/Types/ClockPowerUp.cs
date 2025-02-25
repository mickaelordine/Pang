using UnityEngine;

namespace Script.PowerUp.Types
{
    public class ClockPowerUp : PickupableObject<Collider>
    {
        [SerializeField]
        private AudioSource audioSource;
        protected override void ApplyEffect(Collider component)
        {
            audioSource.Play();
            GameManager.Instance.FreezeBubbles();
        }
    }
}