using UnityEngine;

public class WeaponPistol : WeaponBase
{
    //Ammo
    [SerializeField] private Ammo ammo;
    //Raycasting
    [SerializeField] private Raycaster raycaster;

    public override void OnFirePressed()
    {
        //0 - Condition (Un a du ammo, pas reload)
        //1 - Enlever un ammo
        //2 - Fait raycast (et faire du degat)
        if(ammo.HasAmmo() && !ammo.IsReloading)
        {
            ammo.FireShot();
            //raycaster.FireShot();
        }
      
    }

    public override void OnFireReleased()
    {
        
    }

    public override void OnReload()
    {
        //0 - Leftover magazine, ammo n'est pas au max, pas reload
        //1 - reload
        if(ammo.CanReload())
        {
            ammo.StartReload();
        }
    }

    public override void UpdateWeapon()
    {
        ammo.UpdateReload(Time.deltaTime);
    }
}
