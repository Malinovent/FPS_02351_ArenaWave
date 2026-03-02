using UnityEngine;

public class WeaponRocketLauncher : WeaponBase, IFirePressed, IFireReleased
{
    [SerializeField] Ammo ammo;
    [SerializeField] ProjectileSpawner projectile;
    [SerializeField] RateOfFire RoF;

    public void OnFirePressed()
    {
        if (RoF.CanFire)
        {
            projectile.Fire();
            RoF.FireShot();
        }
    }

    public void OnFireReleased()
    {
        
    }

    public override void OnReload()
    {
        ammo.StartReload();
    }

    public override void UpdateWeapon()
    {
        RoF.UpdateFire(Time.deltaTime);
        ammo.UpdateReload(Time.deltaTime);
    }
}
