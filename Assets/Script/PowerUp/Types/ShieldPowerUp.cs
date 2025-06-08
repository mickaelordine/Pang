using UnityEngine;

public class ShieldPowerUp : PickupableObject<Collider>
{
    protected override void ApplyEffect(Collider component)
    {
        component.gameObject.GetComponent<CharacterDamage>().ActivateShield();   
    }
    
}
