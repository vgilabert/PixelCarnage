using System.Collections.Generic;
using Projectiles;
using WeaponMods;

namespace Weapons
{
    public class Shooter : Weapon
    {
        protected override void SetUpProjectile(Projectile projectile)
        {
            base.SetUpProjectile(projectile);
            PlayerBullet playerBullet = projectile as PlayerBullet;
            
            if (playerBullet != null)
            {
                    // Pass independent mod instances to the bullet
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