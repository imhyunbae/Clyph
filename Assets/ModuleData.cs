using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CreateAssetMenu(fileName = "Module Data", menuName = "모듈 데이터", order = int.MaxValue)]
public class ModuleData : ScriptableObject
{ 
    //기본 정보
    public string DataCode;
    public ModuleKind Kind ;
    public string Name;
    public Sprite Icon;
    
    //전투 정보
    public float Attack;
    public float MaxHp;

    //툴팁 정보
    public string GD_Comment;
    public string Tooltip;

    //조합 정보
    public int[] Cost;

    public   ModuleData()
    {
        Cost = new int[] { 0, 0, 0, 0 };
    }
}
