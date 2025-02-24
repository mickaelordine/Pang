using UnityEngine;
using UnityEngine.InputSystem;

namespace Script.Character
{
    public class CharacterShoot : MonoBehaviour
    {
        [SerializeField]
        GameObject projectile;
        [SerializeField]
        ObjectPool projectilePool = null;
        private InputSystem_Actions input = null;
        public bool canShoot = true;
        [SerializeField]
        private ShootType shooterType = ShootType.hook;


        public enum ShootType
        {
            hook,
            heavyMachineGun,
        }
        
        private void Awake()
        {
            input = new InputSystem_Actions();
        }

        
        private void OnEnable()
        {
            input.Enable();
            input.Player.Attack.performed += OnAttackPerformed;
        }

        private void OnDisable()
        {
            input.Disable();
            input.Player.Attack.performed -= OnAttackPerformed;
        }
        
        private void OnAttackPerformed(InputAction.CallbackContext value)
        {
            if (canShoot)
            {
                GameObject pulledObject = projectilePool.GetPooledObject().gameObject;
                if (pulledObject == null)
                    return;
                //GameObject i_projectile = Instantiate(projectile, gameObject.transform.position, Quaternion.identity);
                pulledObject.transform.position = transform.position; //set the position of the current pulledObj
                switch (shooterType)
                {
                    case ShootType.hook:
                        canShoot = !canShoot;
                        pulledObject.GetComponent<ProjectileScript>().SetCreator(gameObject);
                        break;
                    
                    case ShootType.heavyMachineGun:
                        //canShoot = !canShoot; // we can shoot repeteatly, at max use a courutine
                        pulledObject.GetComponent<ProjectileScript>().SetCreator(gameObject);
                        break;
                    
                    default:
                        break;
                }
                
            }
            
        }

        public void SetType(ShootType type)
        {
            shooterType = type;
        }
    }
}