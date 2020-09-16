﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class BaseAttack : MonoBehaviour
{
    public float Power;
    public float Lifetime;
    public abstract void Setup(Unit InUnit);

    protected void Update()
    {
        Lifetime -= Time.deltaTime;
        if (Lifetime <= 0)
            Destroy(gameObject);
    }
}
