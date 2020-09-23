using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Enemy : Unit
{
    public float Power;
    public override void Attack()
    {
        SetTarget();
        if (Target != null)
        {
            Unit UnitComponent = Target.GetComponent<Unit>();
            if (UnitComponent != null)
                UnitComponent.Damage(Power);

            IceWall IceWallComponent = Target.GetComponent<IceWall>();
            if (IceWallComponent != null)
                IceWallComponent.Damage(Power);
        }
    }

    public override void Die()
    {
        Manager.Instance.OnEnemyDie(this);
        base.Die();
    }

    public override void SetTarget()
    {
        if (Manager.Instance == null)
            return;
        List<Module> Modules = Manager.Instance.Modules;
        List<IceWall> IceWalls = FindObjectsOfType<IceWall>().Where(x => x != null).ToList();
        List<GameObject> Objects = new List<GameObject>();
        foreach (Module each in Modules)
            Objects.Add(each.gameObject);
        foreach(IceWall each in IceWalls)
            Objects.Add(each.gameObject);

        Objects.Sort((GameObject A, GameObject B) => Vector3.Distance(transform.position, A.transform.position).CompareTo(Vector3.Distance(transform.position, B.transform.position)));
        Target = Objects[0];
    }
}
