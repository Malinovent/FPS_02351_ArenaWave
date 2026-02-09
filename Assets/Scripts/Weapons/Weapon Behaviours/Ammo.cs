using UnityEngine;

public class Ammo : MonoBehaviour
{
    [Header("Ammo Parameters")]
    [SerializeField] private int maxAmmo;

    private int remainingAmmo;

    [Header("Reload Parameters")]
    [SerializeField] private int maxMagazines = 3;
    [SerializeField] private float reloadTime = 1;

    private int remainingMagazines;
    private float reloadTimer = 0;
    private bool isReloading = false;

    public bool IsReloading => isReloading;

    private void Awake()
    {
        remainingAmmo = maxAmmo;
        remainingMagazines = maxMagazines;
    }
    
    public void FireShot()
    {
        remainingAmmo = Mathf.Max(0, remainingAmmo - 1);
    }

    public void UpdateReload(float deltaTime)
    {
        if (isReloading)
        {
            reloadTimer += deltaTime;
            if (reloadTimer >= reloadTime)
            {
                Reload();
            }
        }
    }


    public bool HasAmmo()
    {
        return remainingAmmo > 0;
    }

    public bool CanReload()
    {
        return remainingMagazines > 0 && !isReloading && remainingAmmo < maxAmmo;
    }

    public void StartReload()
    {
        if (!CanReload())
            return;

        isReloading = true;
    }

    private void Reload()
    {
        isReloading = false;
        remainingAmmo = maxAmmo;
        reloadTimer = 0f;
        remainingMagazines = Mathf.Max(0, remainingMagazines - 1);
        Debug.Log("Reloaded. Current Magazine: " + remainingMagazines);
    }
}
