using UnityEngine;
using System;
using UnityEngine.Events;

public abstract class WeaponBase : MonoBehaviour
{
    [SerializeField] protected string weaponName;

    [SerializeField] private UnityEvent onWeaponFired;
    

    public abstract void UpdateWeapon();
    public abstract void OnReload();
}
