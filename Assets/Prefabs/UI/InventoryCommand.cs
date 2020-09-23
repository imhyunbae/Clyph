using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryCommand : MonoBehaviour
{
    [Header ("Creature")]
    public ModuleKind CreatureKind;
    public int CreatureCount;

    public void Add()
    {
       BattleUIManager.Instance.BattleInventory.DictionaryModule[CreatureKind] = CreatureCount;
    }

    [Header("Spirit")]
    public SpiritType SpiritType;
    public int SpiritCount;

    public void SpiritAdd()
    {
        BattleUIManager.Instance.BattleInventory.SpiritCount[(int)SpiritType] = SpiritCount;
    }

}
