using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public enum ElementalType { Water , Fire , Ground , Wind  }

[Serializable]
public enum SpiritType { Joy, Anger, Sorrow, Pleasure }

[Serializable]
public enum ModuleKind { 
    Creature_0001,
    Creature_0002,
    Creature_0003,
    Creature_0004,
    Creature_0005,
    Creature_0006,
    Creature_0007,
    Creature_0008,
}

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
        for (int i = 0; i < Enum.GetNames(typeof(ModuleKind)).Length; i++)
            DictionaryModule.Add((ModuleKind)i, 1);

        SP = 100;
    }

    public void AddModule(ModuleKind _Kind, int _Count)
    {
        DictionaryModule[_Kind] += _Count;
    }
}
