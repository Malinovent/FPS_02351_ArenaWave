using UnityEngine;

public class WeaponPistol : WeaponBase, IFirePressed
{
    //Ammo
    [SerializeField] private Ammo ammo;
    //Raycasting
    [SerializeField] private Raycaster raycaster;

    public void OnFirePressed()
    {
        //0 - Condition (Un a du ammo, pas reload)
        //1 - Enlever un ammo
        //2 - Fait raycast (et faire du degat)
        if(ammo.HasAmmo() && !ammo.IsReloading)
        {
            ammo.FireShot();

            WeaponUpdater.UpdateWeaponInformation(ammo, weaponName);
            //raycaster.FireShot();
        }
      
    }

    public override void OnReload()
    {
        //0 - Leftover magazine, ammo n'est pas au max, pas reload
        //1 - reload
        if(ammo.CanReload())
        {
            ammo.StartReload();
            WeaponUpdater.UpdateWeaponInformation(ammo, weaponName);
        }
    }

    public override void UpdateWeapon()
    {
        ammo.UpdateReload(Time.deltaTime);
    }

}
