using System;
using UnityEngine;

public class HearingBehaviour : MonoBehaviour
{
    [SerializeField] private float hearingRange = 5;

    private bool canHearPlayer = false;

    public bool CanHearPlayer => canHearPlayer;
    public event Action onHeardPlayer;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            canHearPlayer = true;
            onHeardPlayer?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canHearPlayer = false;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, hearingRange);
    }

}