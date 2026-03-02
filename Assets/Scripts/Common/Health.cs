using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    private int currentHealth = 0;

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }
}

public class Enemy : MonoBehaviour, IDamageable
{
    Health health;

    public void TakeDamage(int amount)
    {
        health.TakeDamage(amount);

        if(amount > 50)
        {
            Stun();
        }
    }

    private void Stun()
    {
        //Arret pour quelque second
    }
}

//The moment it takes ANY damage it is destroyed
public class BreakableObject : MonoBehaviour, IDamageable
{
    public void TakeDamage(int amount)
    {
        if (amount > 0)
        {
            DestroyObject();
        }
    }

    private void DestroyObject()
    {

    }
}

//NEVER dies
public class Dummy : MonoBehaviour, IDamageable
{
    public void TakeDamage(int amount)
    {
        //Lance animation
    }
}

public class Damager : MonoBehaviour
{
    [SerializeField] private int damageAmount = 4;

    IDamageable currentTarget;

    public void DoDamage()
    {
        currentTarget?.TakeDamage(damageAmount);
    }

    public void DoDamage(IDamageable damageable)
    {
        damageable?.TakeDamage(damageAmount);
    }
}

public interface IDamageable
{
    public void TakeDamage(int amount);
}