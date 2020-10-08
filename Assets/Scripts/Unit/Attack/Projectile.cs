using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 Target;
    public float Speed;
    public float Gravity;
    public Vector3 Velocity;
    public GameObject Explosion;
    public float Lifetime;
    public float Power;
    public bool IsLightning;

    public float KnockbackForce;
    public float KnockbackRadius;
    public float KnockbackDistance;

    Vector3 StartPosition;
    void Start()
    {
        Vector3 Distance = Target - transform.position;
        Vector2 DistanceH = new Vector2(Distance.x, Distance.z);
        float T = Speed == 0 ? 0 : Distance.magnitude / Speed;
        Vector2 VelocityH = DistanceH.normalized * Speed;
        float VelocityV = T * Gravity / 2;
        Velocity = new Vector3(VelocityH.x, VelocityV, VelocityH.y);
        StartPosition = transform.position;
        if (IsLightning)
            transform.position = (StartPosition + Target) * 0.5f;
    }

    void FixedUpdate()
    {
        float DeltaTime = Time.fixedDeltaTime;
        Velocity.y -= Gravity * DeltaTime;
        Vector3 Displacement = Velocity * DeltaTime;
        transform.Translate(Displacement, Space.World);
        if (IsLightning && Target != null)
        {
            Vector3 Direction = Target - StartPosition;
            transform.localScale = new Vector3(1, Direction.magnitude / 2.5f, 1);
        }
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
        List<Enemy> Enemies = BattleManager.Instance.Enemies;
        for (int i = 0; i < Enemies.Count; i++)
        {
            Enemy NearUnit = Enemies[i];
            Vector3 Direction = NearUnit.transform.position - transform.position;
            if (Direction.magnitude < KnockbackRadius)
            {
                KnockbackData knockbackData = new KnockbackData();
                knockbackData.Force = Direction.normalized * KnockbackForce;
                knockbackData.Distance = KnockbackDistance;
                NearUnit.knockbackData = knockbackData;
            }
        }

        if (Explosion)
            Instantiate(Explosion, transform.position, Quaternion.identity, transform.parent);
        Destroy(gameObject);
    }
}
