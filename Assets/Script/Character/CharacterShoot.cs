using System.Collections;
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
        public int ammoAmount = 1;
        public int maxAmmo = 1;
        [SerializeField]
        public ShootType shooterType = ShootType.hook;


        public enum ShootType
        {
            hook,
            heavyMachineGun,
            doubleBarrel,
            grapplingHook,
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

        private void HookShoot()
        {
            GameObject pulledObject = projectilePool.GetPooledObject().gameObject;
            if (pulledObject == null)
                return;
            pulledObject.transform.position = transform.position; //set the position of the current pulledObj
            pulledObject.GetComponent<ProjectileScript>().SetCreator(gameObject, shooterType);
            ammoAmount -= 1;
        }

        private void DoubleBarrelShoot()
        {
            GameObject pulledObject1 = projectilePool.GetPooledObject().gameObject;
            GameObject pulledObject2 = projectilePool.GetPooledObject().gameObject;
            if (pulledObject1 == null)
                return;
            if (pulledObject2 == null)
                return;
            pulledObject1.transform.position = new Vector3(transform.position.x + 0.25f, transform.position.y); //set the position of the current pulledObj
            pulledObject2.transform.position = new Vector3(transform.position.x - 0.25f, transform.position.y); //set the position of the current pulledObj
            pulledObject1.GetComponent<ProjectileScript>().SetCreator(gameObject, shooterType);
            pulledObject2.GetComponent<ProjectileScript>().SetCreator(gameObject, shooterType);
            ammoAmount -= 2;
        }

        private void MachineGunShoot()
        {
            GameObject pulledObject = projectilePool.GetPooledObject().gameObject;
            if (pulledObject == null)
                return;
            pulledObject.transform.position = transform.position; //set the position of the current pulledObj
            pulledObject.GetComponent<ProjectileScript>().SetCreator(gameObject, shooterType);
            ammoAmount -= 1;
        }

        private void GrapplingHookShoot()
        {
            GameObject pulledObject = projectilePool.GetPooledObject().gameObject;
            if (pulledObject == null)
                return;
            pulledObject.transform.position = transform.position; //set the position of the current pulledObj
            pulledObject.GetComponent<ProjectileScript>().SetCreator(gameObject, shooterType);
            ammoAmount -= 1;
        }
        
        private void OnAttackPerformed(InputAction.CallbackContext value)
        {
            if (ammoAmount > 0)
            {
                switch (shooterType)
                {
                    case ShootType.hook:
                        HookShoot();
                        break;
                    
                    case ShootType.doubleBarrel:
                        DoubleBarrelShoot();
                        break;
                    
                    case ShootType.grapplingHook:
                        GrapplingHookShoot();
                        break;
                    
                    case ShootType.heavyMachineGun:
                        MachineGunShoot();
                        break;
                }
            }
            
        }
        
        

        //reset the hooktype after a tot time
        IEnumerator ResetHook()
        {
            yield return new WaitForSeconds(10f);
            shooterType = ShootType.hook;
            maxAmmo = 1;
            ammoAmount = maxAmmo;
        }

        public void SetShootType(ShootType type)
        {
            shooterType = type;
            switch (shooterType)
            {
                case ShootType.hook:
                    maxAmmo = 1;
                    break;
                case ShootType.heavyMachineGun:
                    maxAmmo = 15;
                    break;
                case ShootType.doubleBarrel: //shoot two projectile istead of one
                    maxAmmo = 4;
                    break;
                case ShootType.grapplingHook:
                    maxAmmo = 1;
                    break;
            }
            ammoAmount = maxAmmo;
            StartCoroutine(ResetHook());
        }
        
        
        
    }
}