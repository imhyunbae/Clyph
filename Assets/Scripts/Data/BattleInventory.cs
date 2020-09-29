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
    M_Creature_0001,
    M_Creature_0002,
}

public class BattleInventory : MonoBehaviour
{
    public int[] SpiritCount;
    public Dictionary<ModuleKind, int> DictionaryModule;
    public int SP;
    public int Count_NormalCreature 
    { 
        get 
        {
            int Count = 0;
            foreach(var each in DictionaryModule)
            {
                if(each.Key.ToString()[0] != 'M')
                {
                    Count += each.Value;
                }
            }
            return Count;
        } 
    }
    public int Count_MaterialCreature 
    {
        get
        {
            int Count = 0;
            foreach (var each in DictionaryModule)
            {
                if (each.Key.ToString()[0] == 'M')
                {
                    Count += each.Value;
                }
            }
            return Count;
        }
    }

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
            DictionaryModule.Add((ModuleKind)i, 0);

        SP = 100;
    }

    public void AddModule(ModuleKind _Kind, int _Count)
    {
        DictionaryModule[_Kind] += _Count;
    }
}
