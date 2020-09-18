using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChainLightning : BaseAttack
{
    public int Count;
    public float Interval;
    public GameObject Prefab;
    List<Unit> Targets;
    Vector3 StartPos;
    Vector3 TargetPos;
    float Timer;
    public override void Setup(Unit InUnit)
    {
        StartPos = InUnit.transform.position;
    }
    void Start()
    {
        Lifetime = Interval * Count;
        Timer = Interval;
        if (Targets.Count == 0)
        {
            Destroy(gameObject);
        }
        else
        {   
            Targets = FindObjectsOfType<Unit>().Where(x => x.Team == ETeam.Enemy && x != null).ToList();
            Targets.Sort((Unit A, Unit B) => Vector3.Distance(transform.position, A.transform.position).CompareTo(Vector3.Distance(transform.position, B.transform.position)));
            foreach (Unit Target in Targets)
            {
                


                if (Target != null)
                {
                    TargetPos = Target.transform.position;
                    AdjustTransform(StartPos, TargetPos);
                    Target.Damage(Power);
                    Targets.Remove(Target);
                    return;
                }
            }
        }
       
    }

    void Update()
    {
        Timer -= Time.deltaTime;
        if (Timer <= 0.0f && Count > 0)
            Chain();

        base.Update();
    }

    void AdjustTransform(Vector3 StartPosition, Vector3 EndPosition)
    {
        transform.position = (StartPosition + EndPosition) * 0.5f;
        Vector3 Direction = EndPosition - StartPosition;
        float Angle = Mathf.Atan(Direction.z / Direction.x) * Mathf.Rad2Deg;
        if (Direction.x < 0)
            Angle += 180;
        transform.eulerAngles = new Vector3(90.0f, -Angle, 90.0f);
        transform.localScale = new Vector3(1, Direction.magnitude / 5.0f, 1);
    }

    void Chain()
    {
        GameObject NextChainLightning = Instantiate(Prefab, transform.position, transform.rotation, null);
        ChainLightning Child = NextChainLightning.GetComponent<ChainLightning>();
        Child.Targets = Targets;
        Child.StartPos = TargetPos;
        Child.Interval = Interval;
        Child.Count = Count - 1;
        Count = 0;
    }
}
