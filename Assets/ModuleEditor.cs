using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
public class ModuleEditor : EditorWindow
{
    public ModuleData Data;
    int select;
    int Phase;

    ModuleKind SelectKind;
    // Start is called before the first frame update
    static public void Open(ModuleKind _Kind)
    {
        ModuleEditor Editor = CreateInstance<ModuleEditor>();
        Editor.Show();
        Editor.Data = ModuleData.GetData(_Kind);
        Editor.position = new Rect(400, 400, 350, 300);
    }

    public void GUI_BaseData()
    {
        Data.DataCode = EditorGUILayout.TextField("식별 코드", Data.DataCode);
        Data.Kind = (ModuleKind)EditorGUILayout.EnumPopup("모듈 종류", Data.Kind);
        Data.Name = EditorGUILayout.TextField("모듈 이름", Data.Name);
        Data.Icon = (Sprite)EditorGUILayout.ObjectField("아이콘", Data.Icon, typeof(Sprite));
    }

    public void GUI_BattleData()
    {
        Data.MaxHp = EditorGUILayout.FloatField("모듈 체력", Data.MaxHp);
        if (Data.MaxHp < 0)
            Data.MaxHp = 0;

        Data.Attack = EditorGUILayout.FloatField("모듈 공격력", Data.Attack);

        Data.AttackPrefab = (GameObject)EditorGUILayout.ObjectField("모듈 공격", Data.AttackPrefab, typeof(BaseAttack));
    }
    public void GUI_TooltipData()
    {
        EditorGUILayout.LabelField("툴팁");
        Data.Tooltip = EditorGUILayout.TextArea(Data.Tooltip, GUILayout.Height(80));

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("개발자 코멘트");
        Data.Tooltip = EditorGUILayout.TextArea(Data.Tooltip, GUILayout.Height(80));

    }
    public void GUI_CombinationData()
    {
        GUIContent[] contents = new GUIContent[]
        {
            new GUIContent("      물"),
            new GUIContent("      불"),
            new GUIContent("      땅"),
            new GUIContent("  바람")
        };
        GUILayout.Space(20);

        EditorGUI.MultiIntField(
        new Rect(15, 90, EditorGUIUtility.currentViewWidth - 45, EditorGUIUtility.singleLineHeight),
        contents,
        Data.Cost);
    }

    public void OnGUI()
    {
        if (Data == null)
            return;

        EditorGUILayout.Space();

        select = GUILayout.Toolbar(select, new string[] { "기본 정보", "전투 정보", "툴팁 정보", "조합 정보" }, GUILayout.Height(40)); ;

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        if (select == 0)
            GUI_BaseData();
        else if (select == 1)
            GUI_BattleData();
        else if (select == 2)
            GUI_TooltipData();
        else if (select == 3)
            GUI_CombinationData();

       
    }
}