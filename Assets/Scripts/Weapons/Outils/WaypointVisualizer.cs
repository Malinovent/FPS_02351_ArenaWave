using System;
using Unity.VisualScripting;
using UnityEngine;

public class WaypointVisualizer : MonoBehaviour
{
    [SerializeField][Range(0,1)] private float maxDistance = 1;

    [Header("Gizmo Parameters")]
    [Tooltip("The radius of the gizmo")]
    [SerializeField] private float radius = 0.25f;

    [TextArea(1, 4)][SerializeField] private string textBox;
    [SerializeField] private AnimationCurve curve;

    private void OnDrawGizmos()
    {
     
        if(Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, maxDistance))
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.position, radius);
            Gizmos.DrawLine(transform.position, transform.position + (Vector3.down * maxDistance));

            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(hit.point, 0.25f);
        }
        else
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, radius);
            Gizmos.DrawLine(transform.position, transform.position + (Vector3.down * maxDistance));
        }

       

    }
}
