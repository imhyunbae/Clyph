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
        List<Enemy> Enemies = Manager.Instance.Enemies;
        for (int i = 0; i < Enemies.Count; i++)
        {
            Enemy each = Enemies[i];
            if (each == null)
                continue;
            if ((each.transform.position - StartPosition).magnitude < Range)
            {
                each.SpeedY = SpeedY;
                each.Damage(Power);
            }
        }
    }
}
