using UnityEngine;

[RequireComponent(typeof(Ammo), typeof(Raycaster), typeof(RateOfFire))]
public class WeaponSniper : WeaponBase
{
    private Ammo ammo;
    private Raycaster raycaster;
    private RateOfFire RoF;

    private void Awake()
    {
        ammo = GetComponent<Ammo>();
        raycaster = GetComponent<Raycaster>();
        RoF = GetComponent<RateOfFire>();
    }

    public override void OnFirePressed()
    {
        //0 - Condition (Un a du ammo, pas reload)
        //1 - Enlever un ammo
        //2 - Fait raycast (et faire du degat)
        if (ammo.HasAmmo() && !ammo.IsReloading && RoF.CanFire)
        {
            ammo.FireShot();
            raycaster.FireShot();
        }

    }

    public override void OnFireReleased()
    {

    }

    public override void OnReload()
    {
        //0 - Leftover magazine, ammo n'est pas au max, pas reload
        //1 - reload
        if (ammo.CanReload())
        {
            ammo.StartReload();
        }
    }

    public override void UpdateWeapon()
    {
        ammo.UpdateReload(Time.deltaTime);
        RoF.UpdateFire(Time.deltaTime);
    }
}
