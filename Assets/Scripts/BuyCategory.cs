using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class BuyCategory : MonoBehaviour
{
    public GameObject DirectorPrefab;
    public GameObject ElementParent;

    public Transform CreatureStart;
    public Transform RandomStart;
    public Transform SpiritStart;

    public Transform CreatureEnd;
    public Transform SpiritEnd;

    public void OnClickCreature()
    {
        GameObject Element = GameObject.Instantiate(DirectorPrefab, ElementParent.transform);
        int RandomModule = UnityEngine.Random.Range(0, Enum.GetNames(typeof(ModuleKind)).Length);
        Element.GetComponent<BuyElementBegin>().Init(
            CreatureStart.position,
            CreatureEnd.position,
           () => BattleUIManager.Instance.BattleInventory.AddModule((ModuleKind)RandomModule, 1)
            );

        Element.GetComponent<Image>().sprite = Resources.Load<GameObject>("Prefabs/Creature/" + ((ModuleKind)RandomModule).ToString()).GetComponent<SpriteRenderer>().sprite;

    }

    public void OnClickRandom()
    {
        GameObject Element = GameObject.Instantiate(DirectorPrefab, ElementParent.transform);
        Element.GetComponent<BuyElementBegin>().Init(
            RandomStart.position, 
            CreatureEnd.position,
            () => BattleUIManager.Instance.BattleInventory.AddModule(ModuleKind.Creature_0001, 1)
            );

    }

    public void OnClickSpirit()
    {
        GameObject Element = GameObject.Instantiate(DirectorPrefab, ElementParent.transform);
        Element.GetComponent<BuyElementBegin>().Init(
            SpiritStart.position, 
            SpiritEnd.position,
                        () => BattleUIManager.Instance.BattleInventory.AddModule(ModuleKind.Creature_0001, 1)
        );

    }
}
