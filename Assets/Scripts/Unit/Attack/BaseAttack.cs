using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class BaseAttack : MonoBehaviour
{
    public float Power;
    public float Interval;
    public float Range;
    public float Lifetime;
    public abstract void Setup(Unit InUnit);

    protected void Update()
    {
        Lifetime -= Time.deltaTime;
        if (Lifetime <= 0)
            Die();
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }
}
