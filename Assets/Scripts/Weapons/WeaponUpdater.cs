public static class WeaponUpdater
{
    public static event System.Action<WeaponInfo> onWeaponUpdated;

    public static void UpdateWeaponInformation(Ammo ammo, string weaponName)
    {
        string maxAmmo = $"___\n{ammo.MaxAmmo}";

        WeaponInfo info = new WeaponInfo(ammo.RemainingAmmo.ToString(), maxAmmo, ammo.RemainingMagazine.ToString(), weaponName);
        onWeaponUpdated(info);
    }

    public static void UpdateWeaponInformation(string ammoRemainingText, string ammoMaxText, string magazineRemaining, string weaponName)
    {
        WeaponInfo info = new WeaponInfo(ammoRemainingText, ammoMaxText, magazineRemaining, weaponName);
        onWeaponUpdated(info);
    }

}
