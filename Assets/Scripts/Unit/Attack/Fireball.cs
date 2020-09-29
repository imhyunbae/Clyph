using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : BaseAttack
{
    Vector3 Velocity;
    Vector3 Target;
    public Explosion ExplosionClass;
    public float Speed;
    public float Gravity;
    public float KnockbackRadius;
    public float KnockbackDistance;
    public float KnockbackForce;
    
    public override void Setup(Unit InUnit)
    {
        Target = InUnit.Target.transform.position;
    }
    void Start()
    {
        Vector3 Distance = Target - transform.position;
        Vector2 DistanceH = new Vector2(Distance.x, Distance.z);
        float T = Speed == 0 ? 0 : Distance.magnitude / Speed;
        Vector2 VelocityH = DistanceH.normalized * Speed;
        float VelocityV = T * Gravity / 2;
        Velocity = new Vector3(VelocityH.x, VelocityV, VelocityH.y);
    }

    void FixedUpdate()
    {
        float DeltaTime = Time.fixedDeltaTime;
        Velocity.y -= Gravity * DeltaTime;
        Vector3 Displacement = Velocity * DeltaTime;
        transform.Translate(Displacement, Space.World);
    }

    void OnTriggerEnter(Collider Other)
    {
        var Enemy = Other.gameObject.GetComponent<Enemy>();
        if (Enemy != null)
        {
            Enemy.Damage(Power);
            Explode();
        }
    }

    void Update()
    {
        if (transform.position.y < -0.5f)
            Explode();

        Lifetime -= Time.deltaTime;
        if (Lifetime < 0)
            Destroy(gameObject);
    }

    void Explode()
    {
        List<Enemy> Enemies = Manager.Instance.Enemies;
        for (int i = 0; i < Enemies.Count; i++)
        {
            Enemy NearUnit = Enemies[i];
            Vector3 Direction = NearUnit.transform.position - transform.position;
            Direction.y = 0.0f;
            if (Direction.magnitude < KnockbackRadius)
            {
                KnockbackData knockbackData = new KnockbackData();
                knockbackData.Force = Direction.normalized * KnockbackForce;
                knockbackData.Distance = KnockbackDistance;
                NearUnit.knockbackData = knockbackData;
            }
        }

        if (ExplosionClass)
            Instantiate(ExplosionClass.gameObject, transform.position, Quaternion.identity, transform.parent);
        Destroy(gameObject);
    }
}
