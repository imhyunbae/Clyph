using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tider : BaseAttack
{
    Vector3 StartPosition;
    public float Range;
    public float SpeedY;
    public override void Setup(Unit InUnit)
    {
        StartPosition = InUnit.Target.transform.position;
    }

    void Start()
    {
        transform.position = StartPosition;
        List<Enemy> ListTarget = new List<Enemy>();
        foreach (var each in Manager.Instance.Enemies)
        {

            if ((each.transform.position - StartPosition).magnitude < Range)
            {
                ListTarget.Add(each);

            }
        }

        foreach (var each in ListTarget)
        {
            each.SpeedY = SpeedY;
            each.Damage(Power);

        }
    }
}
