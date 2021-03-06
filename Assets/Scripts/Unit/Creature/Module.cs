﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Module : Unit
{
    public ModuleKind Kind;
    public void SetData(ModuleData _Data)
    {
        this.HP = this.MaxHP = _Data.MaxHp;

        this.AttackPrefab = _Data.AttackPrefab;

        this.MoveSpeedMultiplier = _Data.MoveSpeedMultipier;
        this.AttackSpeedMultiplier = _Data.AttackSpeedMultiplier;
        this.DamageMultiplier = _Data.DamageMultipler;

        this.StartScale = _Data.Scale;

        this.Kind = _Data.Kind;
    }

    public override void Die()
    {
        BattleManager.Instance.OnModuleDie(this);
        base.Die();
    }


    public override void Attack()
    {
        SetTarget();
        if (Target != null && AttackPrefab != null)
        {
            Quaternion Rotation = AttackPrefab.transform.rotation;
            GameObject AttackInstance = Instantiate(AttackPrefab.gameObject, transform.position, Rotation, null);
            if (AttackInstance != null)
                AttackInstance.GetComponent<BaseAttack>().Setup(this);
        }
    }

    public override void SetTarget()
    {
        if (BattleManager.Instance == null)
            return;
        if (BattleManager.Instance.Enemies.Count == 0)
        {
            Target = null;
            return;
        }

        ///  Manager.Instance.Enemies.Sort((Enemy A, Enemy B) => Vector3.Distance(transform.position, A.transform.position).CompareTo(Vector3.Distance(transform.position, B.transform.position)));
        ///  Target = Manager.Instance.Enemies.Count > 0 ? Manager.Instance.Enemies[0].gameObject : null;

        var List = BattleManager.Instance.Enemies;
        if (List.Count == 0)
            return;

        var Enemy = List[0]; // 첫번째를 먼저 
        float shortDis = Vector3.Distance(gameObject.transform.position, List[0].transform.position); // 첫번째를 기준으로 잡아주기 

        foreach (Enemy found in List)
        {
            float Distance = Vector3.Distance(gameObject.transform.position, found.transform.position);

            if (Distance < shortDis) // 위에서 잡은 기준으로 거리 재기
            {
                Enemy = found;
                shortDis = Distance;
            }
        }
        //Manager.Instance.Modules.Sort((Module A, Module B) => Vector3.Distance(transform.position, A.transform.position).CompareTo(Vector3.Distance(transform.position, B.transform.position)));
        //STarget = Manager.Instance.Modules[0].gameObject;
        Target = Enemy.gameObject;
    }
}
