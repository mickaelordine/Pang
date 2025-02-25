using Script.Character;
using UnityEngine;

namespace Script.PowerUp.Types
{
    public class WeaponPowerUp : PickupableObject<CharacterShoot>
    {
        [SerializeField] 
        private CharacterShoot.ShootType _shootType;
        protected override void ApplyEffect(CharacterShoot component)
        {
            component.SetShootType(_shootType);
        }
    }
}