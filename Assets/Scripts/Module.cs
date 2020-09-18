using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Module : Unit
{
    public GameObject AttackPrefab;

    public override void Attack()
    {
        SetTarget();
        if (Target != null && AttackPrefab != null)
        {
            Quaternion Rotation = AttackPrefab.transform.rotation;
            GameObject AttackInstance = Instantiate(AttackPrefab, transform.position, Rotation, null);
            if (AttackInstance != null)
                AttackInstance.GetComponent<BaseAttack>().Setup(this);
        }
    }

    public override void SetTarget()
    {
        if (Manager.Instance == null)
            return;
        List<Enemy> Enemies = Manager.Instance.Enemies;
        Enemies.Sort((Enemy A, Enemy B) => Vector3.Distance(transform.position, A.transform.position).CompareTo(Vector3.Distance(transform.position, B.transform.position)));
        Target = Enemies.Count > 0 ? Enemies[0].gameObject : null;
    }
}
