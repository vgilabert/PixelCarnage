using System.Collections.Generic;
using Extensions;
using UnityEngine;
using Weapons;

namespace UpgradeCards.WeaponMods
{
    public enum WeaponModType
    {
        Ricochet,
        Piercing,
        Explosive,
        MultiShot,
        Homing,
        SplitShot,
    }
    
    public class WeaponModsManager : MonoSingleton<WeaponModsManager>
    {
        [SerializeField] private Weapon equippedWeapon;
        
        private Dictionary<WeaponModType, WeaponModBase> _modTemplates = new();

        public void ApplyMod(WeaponModType weaponModType)
        {
            if (_modTemplates.ContainsKey(weaponModType))
            {
                _modTemplates[weaponModType].Upgrade();
                return;
            }

            switch (weaponModType)
            {
                case WeaponModType.Ricochet:
                    _modTemplates[weaponModType] = new RicochetWeaponMod();
                    equippedWeapon.AddMod(_modTemplates[weaponModType]);
                    break;
                case WeaponModType.Piercing:
                    _modTemplates[weaponModType] = new PierceWeaponMod();
                    equippedWeapon.AddMod(_modTemplates[weaponModType]);
                    break;
                case WeaponModType.MultiShot:
                    _modTemplates[weaponModType] = new MultiShotWeaponMod();
                    equippedWeapon.AddMod(_modTemplates[weaponModType]);
                    break;
            }
        }

        /*public List<WeaponModBase> GetActiveMods()
        {
            List<WeaponModBase> activeMods = new();
            foreach (var mod in _modTemplates.Values)
            {
                // Copy to ensure unique instances for each bullet
                activeMods.Add(mod.Clone());
            }
            return activeMods;
        }*/
    }
}