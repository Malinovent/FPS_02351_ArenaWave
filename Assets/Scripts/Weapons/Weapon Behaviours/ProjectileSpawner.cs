using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Raycaster raycaster;

    public void Fire()
    {
        raycaster.UpdateTargetFromMouse();

        Vector3 aimPoint = raycaster.GetAimPoint(firePoint.position, firePoint.forward);
        Vector3 direction = (aimPoint - firePoint.position).normalized;

        Instantiate(projectilePrefab, firePoint.position, Quaternion.LookRotation(direction));

    }
}
