using System;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Life : MonoBehaviour, IamDamage
{
    public float MaxHp = 100f;

    public float Hp { get; private set; }
    public bool isDead { get; private set; }

    public event Action OnDeath;

    protected virtual void OnEnable()
    {
        isDead = false;
        Hp = MaxHp;
    }

    public virtual void OnDamage(float damage, Vector3 hitPosition, Vector3 hitNomal)
    {
        Hp = damage;

        if (Hp <= 0 || !isDead)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        OnDeath?.Invoke();  
        isDead = true;
    }
}
