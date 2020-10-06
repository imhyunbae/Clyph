using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
//[CustomEditor(typeof(ModuleData))]
public class ModuleEditor : EditorWindow
{
    public ModuleData selected;
    int select;
    int Phase;

    ModuleKind SelectKind;
    // Start is called before the first frame update
    [MenuItem("Window/Example")]
    static void Open()
    {
        
        
        ModuleEditor moduleEditor = CreateInstance<ModuleEditor>();
        moduleEditor.Show();
        moduleEditor.selected = Resources.Load<ModuleData>("Module Data 1");
        moduleEditor.Phase = 0;
    
    }
    
    public void GUI_BaseData()
    { 
        selected.DataCode = EditorGUILayout.TextField("식별 코드",selected.DataCode);
        selected.Kind = (ModuleKind)EditorGUILayout.EnumPopup("모듈 종류", selected.Kind);
        selected.Name = EditorGUILayout.TextField("모듈 이름", selected.Name);
        selected.Icon = (Sprite)EditorGUILayout.ObjectField("아이콘", selected.Icon, typeof(Sprite));
    }

    public void GUI_BattleData()
    {
        selected.MaxHp = EditorGUILayout.FloatField("모듈 체력", selected.MaxHp);
        if (selected.MaxHp < 0)
            selected.MaxHp = 0;

        selected.Attack = EditorGUILayout.FloatField("모듈 공격력", selected.Attack);
    }
    public void GUI_TooltipData()
    {
        EditorGUILayout.LabelField("툴팁");
        selected.Tooltip = EditorGUILayout.TextArea(selected.Tooltip, GUILayout.Height(80));

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("개발자 코멘트");
        selected.Tooltip = EditorGUILayout.TextArea(selected.Tooltip, GUILayout.Height(80));

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
        selected.Cost);
    }
   
    public  void OnGUI()
    {
            
        if (Phase == 0)
        {
            SelectKind = (ModuleKind)EditorGUILayout.EnumPopup("모듈 종류를 선택 하세요", SelectKind);

            if (GUILayout.Button("새로 만들기"))
            {
            }


            if (GUILayout.Button("불러 오기"))
            {
            }

        }
        else    if (Phase == 1)
        {
            if (selected == null)
                return;

            EditorGUILayout.Space();
            if (GUILayout.Button("이전으로"))
            {
            }
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

            EditorGUILayout.Space();
            EditorGUILayout.Space();
            if (GUILayout.Button("메타 데이터 갱신"))
            {
            }
        }
    }
}
