using Script.Character;
using UnityEngine;

namespace Script.PowerUp.Types
{
    public class BombPowerUp : PickupableObject<Collider>
    {
        protected override void ApplyEffect(Collider component)
        {
            GameManager.Instance.DestroyBubbles();
        }
    }
}
