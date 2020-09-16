using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Module : Unit
{
    // public GameObject ProjectileObject;
    public GameObject AttackPrefab;

    public override IEnumerator Attack()
    {
        while (true)
        {
            SetTarget();
            if (Target != null && AttackPrefab != null)
            {
                Quaternion Rotation = AttackPrefab.transform.rotation;
                GameObject AttackInstance = Instantiate(AttackPrefab, transform.position, Rotation, transform);
                if (AttackInstance != null)
                    AttackInstance.GetComponent<BaseAttack>().Setup(this);
                // Projectile ProjectileComponent = ProjectileInstance.GetComponent<Projectile>();
                // if (ProjectileComponent != null)
                //     ProjectileComponent.Target = Target.transform.position;
            }
            yield return new WaitForSeconds(Interval);
        }
    }

    public override void SetTarget()
    {
        if (Manager.Instance == null)
            return;
        List<Enemy> Enemies = Manager.Instance.Enemies;
        Enemies.Sort((Enemy A, Enemy B) => Vector3.Distance(transform.position, A.transform.position).CompareTo(Vector3.Distance(transform.position, B.transform.position)));
        Target = Enemies[0].gameObject;
    }
}
