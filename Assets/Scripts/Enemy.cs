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
        //  List<Module> Modules = Manager.Instance.Modules;
        //  List<IceWall> IceWalls = FindObjectsOfType<IceWall>().Where(x => x != null).ToList();
        //List<GameObject> Objects = new List<GameObject>();

        //foreach (Module each in anager.Instance.Modules)
        //    Objects.Add(each.gameObject);
        //foreach (IceWall each in IceWalls)
        //    Objects.Add(each.gameObject);
        var List = Manager.Instance.Modules;
        if (List.Count == 0)
            return;

        var Module = List[0]; // 첫번째를 먼저 
        float shortDis = Vector3.Distance(gameObject.transform.position, List[0].transform.position); // 첫번째를 기준으로 잡아주기 

        foreach (Module found in List)
        {
            float Distance = Vector3.Distance(gameObject.transform.position, found.transform.position);

            if (Distance < shortDis) // 위에서 잡은 기준으로 거리 재기
            {
                Module = found;
                shortDis = Distance;
            }
        }
        //Manager.Instance.Modules.Sort((Module A, Module B) => Vector3.Distance(transform.position, A.transform.position).CompareTo(Vector3.Distance(transform.position, B.transform.position)));
        //STarget = Manager.Instance.Modules[0].gameObject;
        Target = Module.gameObject;
    }
}
