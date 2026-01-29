using UnityEngine;

public class WeaponPistol : WeaponBase
{

    public override void OnFirePressed()
    {
        if (Camera.main)
        {
            Vector3 rayOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if( Physics.Raycast(rayOrigin, transform.forward, out RaycastHit hit))
            {
                Debug.Log($"Hit {hit.collider.gameObject.name}");
            }
        }

        Debug.Log("I Fired the pistol!");
    }

    public override void OnFireReleased()
    {
        throw new System.NotImplementedException();
    }

    public override void OnReload()
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateWeapon()
    {
        throw new System.NotImplementedException();
    }

    private void OnDrawGizmos()
    {
        //Ray ray = new Ray(transform.position, transform.forward * 1000);
        if(Camera.main)
        {
            Vector3 rayOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Gizmos.color = Color.red;
            Gizmos.DrawRay(rayOrigin, transform.forward * 1000);


            if (Physics.Raycast(rayOrigin, transform.forward, out RaycastHit hit))
            {
                Gizmos.DrawSphere(hit.point, 0.25f);
            }
        }

        
    }
}
