using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public enum ElementalType { Water , Fire , Ground , Wind  }

[Serializable]
public enum SpiritType { Joy, Anger, Sorrow, Pleasure }

[Serializable]
public enum ModuleKind { M_01, M_02, M_03 }

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
            DictionaryModule.Add((ModuleKind)i, 0);

        SP = 100;
    }

    void Update()
    {
    }

  
}
