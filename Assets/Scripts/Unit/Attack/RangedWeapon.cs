using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RangedWeapon : MonoBehaviour
{
    public float Lifetime;
    public float Radius;
    public float Angle;
    public float Damage;

    void Update()
    {
        Lifetime -= Time.deltaTime;
        if (Lifetime < 0)
            Destroy(gameObject);
    }

    void FixedUpdate()
    {
        List<Enemy> Enemies = Manager.Instance.Enemies;
        int Count = Enemies.Count;
        for (int i = 0; i < Count; i++)
        {
            Enemy enemy = Enemies[i];
            if (enemy == null)
                continue;

            Vector3 Direction = enemy.transform.position - transform.position;
            float Theta = Vector3.Angle(Direction.normalized, transform.forward);
            if (Mathf.Abs(Theta) < Mathf.Abs(Angle) && Direction.magnitude < Radius)
            {
                enemy.SpeedMultiplier = 0.5f;
            }
            else
            {
                enemy.SpeedMultiplier = 1.0f;
            }
        }
    }
}
