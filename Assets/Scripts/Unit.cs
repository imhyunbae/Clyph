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
    public Vector3 StartScale;



    [HideInInspector]
    public BoxCollider Collider;
    float AttackTimer = 0.0f;

    private void Awake()
    {
        Battle = false;
    }

    protected void Start()
    {
        StartScale = transform.localScale;
        StartPos = transform.position;
        SpeedMultiplier = 1.0f;
        Collider = GetComponent<BoxCollider>();
        AttackTimer = Interval;
        GetComponent<SpriteRenderer>().flipX = true;

        Camera camera = Manager.Instance.Camera;
        transform.rotation = Quaternion.LookRotation(-camera.transform.forward);

        float Distance = (camera.transform.position - transform.position).magnitude;
        float Ratio = Distance / camera.transform.position.magnitude * 0.5f;
        StartScale.z = 1f;
        transform.localScale = StartScale * Ratio ;

    }

    protected void Update()
    {

        Camera camera = Manager.Instance.Camera;
        transform.rotation = Quaternion.LookRotation(-camera.transform.forward);

        float Distance = (camera.transform.position - transform.position).magnitude;
        float Ratio = Distance / camera.transform.position.magnitude * 1f;
        transform.localScale = StartScale * Ratio;
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 1f);
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

            Vector3 MinY = new Vector3(0, -(Collider.size.y * transform.localScale.y) / 2f, -Collider.size.z / 2f);
            
            

            Matrix4x4 matTrans = Matrix4x4.Translate(new Vector3(0f, 0,0));
            Matrix4x4 matRot = Matrix4x4.Rotate(transform.rotation);
            Matrix4x4 mat = matRot * matTrans;


            Vector3  A = matRot * MinY;
            float Y = Mathf.Max(transform.position.y + SpeedY * Time.deltaTime, -A.y-0.5f);
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
