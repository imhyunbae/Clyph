﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackData
{
    public Vector3 Force;
    public float Distance;
}

public enum ETeam
{
    Module, Enemy
}

abstract public class Unit : MonoBehaviour
{
    public Vector3 StartPos;

    public bool Battle;
    public float Range;
    public float Interval;
    public float MaxHP;
    public float HP;
    public float Speed;
    public float SpeedMultiplier;
    public ETeam Team;
    public GameObject Target;
    public float SpeedY = 0.0f;
    public KnockbackData knockbackData;

    [HideInInspector]
    public BoxCollider Collider;
    float AttackTimer = 0.0f;

    private void Awake()
    {
        Battle = false;
    }

    protected void Start()
    {
        StartPos = transform.position;
        SpeedMultiplier = 1.0f;
        Collider = GetComponent<BoxCollider>();
        AttackTimer = Interval;
        GetComponent<SpriteRenderer>().flipX = true;

        Camera camera = Manager.Instance.Camera;
        transform.rotation = Quaternion.LookRotation(-camera.transform.forward);

        float Distance = (camera.transform.position - transform.position).magnitude;
        float Ratio = Distance / camera.transform.position.magnitude * 0.5f;
        transform.localScale = new Vector3(Ratio, Ratio, Ratio);
    }

    protected void Update()
    {

        Camera camera = Manager.Instance.Camera;
        transform.rotation = Quaternion.LookRotation(-camera.transform.forward);

        float Distance = (camera.transform.position - transform.position).magnitude;
        float Ratio = Distance / camera.transform.position.magnitude * 0.5f;
        transform.localScale = new Vector3(Ratio, Ratio, Ratio);

        if (Target != null)
            GetComponent<SpriteRenderer>().flipX = Target.transform.position.x > transform.position.x;
    }

    protected void FixedUpdate()
    {
        if (Battle == true)
        {
            SetTarget();
            if (Target == null)
                return;

            if (Target.transform.position.magnitude > 8)
                knockbackData = null;

            if (knockbackData != null)
            {
                Knockback();
                return;
            }

            SpeedY = Mathf.Max(SpeedY - 9.8f * Time.deltaTime, -10.0f);
            float Y = Mathf.Max(transform.position.y + SpeedY * Time.deltaTime, 0.0f);
            transform.position = new Vector3(transform.position.x, Y, transform.position.z);

            Vector3 Distance = Target.transform.position - transform.position;
            if (Distance.magnitude > Range)
            {
                Vector3 Displacement = Distance.normalized * Speed * SpeedMultiplier * Time.fixedDeltaTime;
                transform.position += Displacement;
                AttackTimer = 0;
            }
            else
            {
                AttackTimer += Time.fixedDeltaTime;
                if (AttackTimer >= Interval)
                {
                    Attack();
                    AttackTimer = 0.0f;
                }
            }
        }
    }

    public void Damage(float Power)
    {
        HP -= Power;
        if (HP <= 0)
            Die();
    }

    public void Knockback()
    {
        Collider.enabled = false;
        transform.Translate(knockbackData.Force * Time.deltaTime, Space.World);
        knockbackData.Distance -= knockbackData.Force.magnitude * Time.deltaTime;
        if (knockbackData.Distance < 0)
        {
            Collider.enabled = true;
            knockbackData = null;
        }
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }

    public abstract void Attack();
    public abstract void SetTarget();


}
