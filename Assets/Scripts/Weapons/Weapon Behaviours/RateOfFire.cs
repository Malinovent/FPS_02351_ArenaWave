using UnityEngine;

public class RateOfFire : MonoBehaviour
{
    [Header("Rate of Fire")]
    [SerializeField] private float roundsPerSecond = 1f;

    private float timeBetweenShots;
    private float fireTimer;
    private bool canFire = true;

    public bool CanFire => canFire;

    private void Awake()
    {
        timeBetweenShots = 1f / roundsPerSecond;
    }

    public void FireShot()
    {
        canFire = false;
        fireTimer = 0;
    }

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
}
