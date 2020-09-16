using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackData
{
    public Vector3 Force;
    public float Distance;
}

abstract public class Unit : MonoBehaviour
{
    public float Range;
    public float Interval;
    public float MaxHP;
    public float HP;
    public float Speed;
    public float SpeedMultiplier;
    public GameObject Target;
    bool IsAttacking = false;
    public KnockbackData knockbackData;

    [HideInInspector]
    public BoxCollider Collider;

    protected void Start()
    {
        SpeedMultiplier = 1.0f;
        Collider = GetComponent<BoxCollider>();
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
        if (Target == null)
            SetTarget();

        if (Target.transform.position.magnitude > 8)
            knockbackData = null;

        if (knockbackData != null)
        {
            Knockback();
            return;
        }

        Vector3 Distance = Target.transform.position - transform.position;
        if (Distance.magnitude > Range)
        {
            if (IsAttacking)
            {
                StopCoroutine(Attack());
                IsAttacking = false;
            }

            Vector3 Displacement = Distance.normalized * Speed * SpeedMultiplier * Time.fixedDeltaTime;
            transform.Translate(Displacement, Space.World);
        }
        else
        {
            if (!IsAttacking)
            {
                StartCoroutine(Attack());
                IsAttacking = true;
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

    public abstract IEnumerator Attack();
    public abstract void SetTarget();
}
