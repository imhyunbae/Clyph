using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleBundle
{
    public ModuleKind Kind;
    public int Count;
}
public class Combination
{
    public List<ModuleBundle> Condition = new List<ModuleBundle>();


    bool CanCombination(ref BattleInventory inventory)
    {
        foreach (var Bundle in Condition)
        {
            if (inventory.DictionaryModule[Bundle.Kind] < Bundle.Count)
                return false;
        }
        return true;
    }
}
public class CombinationSystem : MonoBehaviour
{
    public BattleInventory battleInventory;

    public List<Combination> ListComnination;





}
