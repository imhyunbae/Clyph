using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public enum ElementalType { Water , Fire , Ground , Wind  }

[Serializable]
public enum SpiritType { Joy, Anger, Sorrow, Pleasure }

[Serializable]
public enum ModuleKind {  }

public class BattleInventory : MonoBehaviour
{
    public int[] SpiritCount;
    public Dictionary<ModuleKind, int> DictionaryModule;
    public int SP;

    void Awake()
    {
        SpiritCount = new int[4] { 0, 0, 0, 0 };
        DictionaryModule = new Dictionary<ModuleKind, int>();
        SP = 0;
    }
    void Start()
    {
        //플레이어 정보대로 값을 취하기

        SP = 100;
    }

    void Update()
    {
    }
}
