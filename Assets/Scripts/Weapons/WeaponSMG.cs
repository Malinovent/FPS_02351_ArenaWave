using UnityEngine;

public class WeaponSMG : WeaponBase
{
    [Header("Ammo Parameters")]
    [SerializeField] private int maxAmmo = 10;
    [SerializeField] private int maxMagazine = 3;
    [SerializeField] private float reloadTime = 2f;

    private float reloadTimer = 0f;

    private bool isReloading = false;
    public bool IsReloading => isReloading;

    private int remainingAmmo = 10;
    private int remainingMagazines = 3;

    [Header("Rate of Fire")]
    [SerializeField] private float roundsPerSecond = 1f;

    private float timeBetweenShots;
    private float fireTimer;
    private bool canFire = true;
    private bool isFiring = false;

    public bool CanFire => canFire;

    [Header("Raycast Parameters")]
    [SerializeField] private LayerMask validLayers;
    [HideInInspector] public Camera mainCamera;

    #region OVERRIDES
    private void Awake()
    {
        timeBetweenShots = 1f / roundsPerSecond;

        remainingAmmo = maxAmmo;
        remainingMagazines = maxMagazine;

        mainCamera = Camera.main;
    }

    public override void UpdateWeapon()
    {
        UpdateReload(Time.deltaTime);
        UpdateFire(Time.deltaTime);

        if(CanFire && isFiring && !isReloading)
        {
            FireShot();
        }
    }

    public override void OnFirePressed()
    {
        isFiring = true;
    }

    public override void OnFireReleased()
    {
        isFiring = false;
    }

    public override void OnReload()
    {
        canFire = false;
        StartReload();
    }
    #endregion

    #region Raycasting
    public RaycastHit GetRaycastTarget(Ray ray, float distance)
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, distance, validLayers))
        {
            return hit;
        }

        return hit;
    }

    private Vector3 GetMouseWorldPosition()
    {
        if (!mainCamera)
            return this.transform.position;

        return mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    public void FireShot()
    {
        remainingAmmo = Mathf.Max(0, remainingAmmo - 1);
        fireTimer = 0;
        canFire = false;

        Vector3 startingPosition = GetMouseWorldPosition();
        Ray ray = new Ray(startingPosition, transform.forward);
        RaycastHit hit = GetRaycastTarget(ray, 100f);

        if (hit.collider != null)
        {
            Debug.Log("hit: " + hit.collider.name);
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 startingPosition = GetMouseWorldPosition();
        Ray ray = new Ray(startingPosition, transform.forward);
        RaycastHit hit = GetRaycastTarget(ray, 100f);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(startingPosition, transform.forward * 100f);
        Gizmos.DrawSphere(hit.point, 0.25f);
    }

    #endregion

    #region RATE OF FIRE

    public void UpdateFire(float deltaTime)
    {
        if (CanFire)
            return;

        fireTimer += deltaTime;
        if (fireTimer >= timeBetweenShots)
        {
            canFire = true;
        }
    }

    #endregion

    #region AMMO PARAMETERS
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

    #endregion
}
