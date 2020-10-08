using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class ModuleData : ScriptableObject
{ 
    //기본 정보
    public string DataCode;
    public ModuleKind Kind ;
    public string Name;
    public Sprite Icon;
    
    //전투 정보
    public float DamageMultipler;
    public float MoveSpeedMultipier;
    public float AttackSpeedMultiplier;

    public float MaxHp;

    public float Scale;
    public BaseAttack AttackPrefab;

    //툴팁 정보
    public string GD_Comment;
    public string Tooltip;

    //조합 정보
    public int[] Cost;


    public ModuleData()
    {
        Cost = new int[] { 0, 0, 0, 0 };
    }

    public static ModuleData GetData(ModuleKind _Kind)
    {
        ModuleData data = Resources.Load<ModuleData>("Data/Unit/Module/" + _Kind.ToString());

        if (data == null)
        {
            Debug.Log(_Kind.ToString() + " 유닛의 데이터 정보가 없어 새로 생성합니다");
            data = CreateInstance<ModuleData>();

            AssetDatabase.CreateAsset(data, "Assets/Resources/Data/Unit/Module/" + _Kind.ToString() + ".asset");

            AssetDatabase.SaveAssets();
        }
        return data;
    }
}
