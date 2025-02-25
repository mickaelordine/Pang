using UnityEngine;

namespace Script.PowerUp.Types
{
    public class FruitPowerUp : PickupableObject<Collider>
    {
        [SerializeField] 
        private int howMuchPoint;
        protected override void ApplyEffect(Collider component)
        {
            GameManager.Instance.AddPoints(howMuchPoint);
        }
    }
}