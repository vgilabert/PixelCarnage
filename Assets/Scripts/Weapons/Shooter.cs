using System.Collections.Generic;
using Projectiles;
using UpgradeCards.WeaponMods;

namespace Weapons
{
    public class Shooter : Weapon
    {
        private int _spreadAngle;
        
        protected override void SetUpProjectile(Projectile projectile)
        {
            base.SetUpProjectile(projectile);
            PlayerBullet playerBullet = projectile as PlayerBullet;
            
            if (playerBullet != null)
            {
                    List<WeaponModBase> bulletMods = new();
                    foreach (var mod in Mods)
                    {
                        bulletMods.Add(mod.Clone());
                    }
                    playerBullet.ApplyMods(bulletMods);
            }
        }
    }
}