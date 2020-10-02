using UnityEngine;


namespace learn3d
{
    public class PlayerGunOnGround : MonoBehaviour
    {
        #region Fields

        [SerializeField] private GameObject _weaponPrefab;
        private bool _hasWeaponDrag = false;
        #endregion


        #region UnityMethods

        private void OnTriggerEnter(Collider activator)
        {
            if (activator.gameObject.CompareTag("Player"))
            {
                if (!_hasWeaponDrag)
                {
                    _hasWeaponDrag = true;
                    Instantiate(_weaponPrefab, activator.gameObject.GetComponent<PlayerAnimationMovement>().Weapon.transform);
                    activator.gameObject.GetComponent<PlayerAnimationMovement>().HasWeapon = true;
                    Destroy(gameObject);
                }
            }
        }

        #endregion
    }
}
