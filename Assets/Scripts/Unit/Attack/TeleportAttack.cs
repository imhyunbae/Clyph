using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class TeleportAttack : BaseAttack
{
    Enemy Target;
    Vector3 StartPosition;

    public override void Setup(Unit InUnit)
    {
        Power = 5000;

        StartPosition = InUnit.transform.position;

        List<Enemy> Enemies = BattleManager.Instance.Enemies;

        Enemies.OrderBy(x => Vector3.Distance(x.transform.position, StartPosition));

        Target = Enemies.Last();

        if (Target == null)
            Destroy(gameObject);
        else
        {
            StopAllCoroutines();
            StartCoroutine(FadeInOut(InUnit.GetComponent<SpriteRenderer>(), 0.75f));
         
        }
    }

    void Start()
    {
    }
    void Update()
    {
        
    }
    IEnumerator FadeInOut(SpriteRenderer _Renderer , float _Duraction)
    {
        _Renderer.color = new Color(1, 1, 1, 0.25f);

        Quaternion UnitRotation = Target.transform.rotation;
        UnitRotation.x = 0f;

        Vector3 Forward = UnitRotation * Vector3.forward;

        _Renderer.transform.position = Target.transform.position - Forward;

        transform.position = Target.transform.position - Forward;


        yield return new WaitForSeconds(0.5f);
        _Renderer.color = new Color(1, 1, 1, 1f);

        Target.Damage(Power);

        yield return new WaitForSeconds(0.15f);
        Die();

        yield return null;
    }
}
