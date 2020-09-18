using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class IceWall : BaseAttack
{
    public int Count;
    public float Health;
    public float Radius;
    public GameObject AttackPrefab;

    public override void Setup(Unit InUnit)
    {
    }

    int SortUnit(Unit A, Unit B)
    {
        Vector3 DirectionA = A.transform.position - transform.position;
        Vector3 DirectionB = B.transform.position - transform.position;
        float AngleA = Mathf.Atan(DirectionA.z / DirectionA.x) * Mathf.Rad2Deg;
        if (AngleA < 0)
            AngleA += 180.0f;
        if (DirectionA.z < 0)
            AngleA += 180.0f;
        float AngleB = Mathf.Atan(DirectionB.z / DirectionB.x) * Mathf.Rad2Deg;
        if (AngleB < 0)
            AngleB += 180.0f;
        if (DirectionB.z < 0)
            AngleB += 180.0f;
        return AngleA.CompareTo(AngleB);
    }

    void Start()
    {
        if (AttackPrefab == null || Count == 0)
            return;

        List<Unit> Targets = FindObjectsOfType<Unit>().Where(x => x.Team == ETeam.Enemy && x != null).ToList();
        if (Targets.Count > 2)
            Targets.Sort(SortUnit);

        Unit TargetA = Targets.First();
        Unit TargetB = Targets.Last();

        Vector3 DirectionA = TargetA.transform.position - transform.position;
        Vector3 DirectionB = TargetB.transform.position - transform.position;
        float AngleA = Mathf.Atan(DirectionA.z / DirectionA.x) * Mathf.Rad2Deg;
        if (AngleA < 0)
            AngleA += 180.0f;
        if (DirectionA.z < 0)
            AngleA += 180.0f;
        float AngleB = Mathf.Atan(DirectionB.z / DirectionB.x) * Mathf.Rad2Deg;
        if (AngleB < 0)
            AngleB += 180.0f;
        if (DirectionB.z < 0)
            AngleB += 180.0f;
        float MaxAngle = Mathf.Max(AngleA, AngleB);
        float MinAngle = Mathf.Min(AngleA, AngleB);
        List<GameObject> Children = new List<GameObject>();
        for (int i = 0; i < Count; i++)
        {
            float Angle = MinAngle + (MaxAngle - MinAngle) * (0.5f + (float)i) / Count;
            float Radius = DirectionA.magnitude * 0.5f;
            Vector3 Offset = new Vector3(Mathf.Cos(Angle * Mathf.Deg2Rad) * Radius, 0, Mathf.Sin(Angle * Mathf.Deg2Rad) * Radius);
            Vector3 Position = transform.position + Offset;
            GameObject AttackInstance = Instantiate(AttackPrefab, Position, transform.rotation, null);
            Children.Add(AttackInstance);
            if (AttackInstance != null)
            {
                IceWall IceWallInstance = AttackInstance.GetComponent<IceWall>();
                IceWallInstance.Health = Health;
                IceWallInstance.Power = Power;
                IceWallInstance.Lifetime = Lifetime;
                IceWallInstance.Count = 0;
            }
        }

        Destroy(gameObject);
    }

    public void Damage(float Power)
    {
        Health -= Power;
        if (Health < 0)
            Die();    
    }

    public override void Die()
    {
        List<Unit> Targets = FindObjectsOfType<Unit>().Where(x => x.Team == ETeam.Enemy && x != null).ToList();
        Targets = Targets.Where(x => Vector3.Distance(transform.position, x.transform.position) < Radius).ToList();
        foreach (Unit Target in Targets)
            Target.Damage(Power);
        
        base.Die();
    }
    
}
