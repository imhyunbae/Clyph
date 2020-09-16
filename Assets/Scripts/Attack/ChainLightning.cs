using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainLightning : BaseAttack
{
    public int Count;
    public float Interval;
    public GameObject Prefab;
    GameObject Target;
    Vector3 StartPos;
    Vector3 TargetPos;
    float Timer;
    public override void Setup(Unit InUnit)
    {
        Target = InUnit.Target;
        StartPos = InUnit.transform.position;
        TargetPos = InUnit.Target.transform.position;
        AdjustTransform(StartPos, TargetPos);
        
    }
    void Start()
    {
        Lifetime = Interval * Count;
        Timer = Interval;
        if (Target == null)
        {
            Destroy(gameObject);
            return;
        }
        Unit TargetUnit = Target.GetComponent<Unit>();
        if (TargetUnit != null)
            TargetUnit.Damage(Power);
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
        transform.eulerAngles = new Vector3(90.0f, -Angle, 90.0f);
        transform.localScale = new Vector3(1, Direction.magnitude / 5.0f, 1);
    }

    Enemy FindEnemy()
    {
        List<Enemy> Enemies = Manager.Instance.Enemies;
        foreach (Enemy Each in Enemies)
        {
            if (Each.gameObject != Target)
                return Each;
        }
        return null;
    }

    void Chain()
    {
        GameObject NextChainLightning = Instantiate(Prefab, transform.position, transform.rotation, null);
        Enemy NewTarget = FindEnemy();
        ChainLightning Child = NextChainLightning.GetComponent<ChainLightning>();
        Child.Target = NewTarget.gameObject;
        Child.StartPos = TargetPos;
        Child.TargetPos = NewTarget.transform.position;
        Child.AdjustTransform(Child.StartPos, Child.TargetPos);
        Child.Interval = Interval;
        Child.Count = Count - 1;
        Count = 0;
    }
}
