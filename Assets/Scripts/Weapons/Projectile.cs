using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float vitesse = 4;
    [SerializeField] private float explosionRadius = 4;
    //[SerializeField] private LayerMask ignoreLayer;
    //[SerializeField] private string[] allowedTags;
    //degat

    private void Update()
    {
        //Move forward
        transform.Translate(transform.forward * vitesse * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Explode();
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach(Collider collider in colliders)
        {
            //Do damage
        }

        Dispose();
    }

    private void Dispose()
    {
        Destroy(this.gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}