using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class SheedUnitEditor : EditorWindow
{
    int UnitCategory;

    int SelectedUnitKind;


    [MenuItem("Sheed/유닛 데이터 편집기")]
    static void Open()
    {
        SheedUnitEditor Editor = CreateInstance<SheedUnitEditor>();
        Editor.Show();

        Editor.position = new Rect(100, 100, 300, 80);
    }

    private void OnGUI()
    {
        UnitCategory = GUILayout.Toolbar(UnitCategory, new string[] { "모듈", "적", "주인공" }, GUILayout.Height(30));

        EditorGUILayout.Space();

        if(UnitCategory == 0)
        {
            SelectedUnitKind = (int)(ModuleKind)EditorGUILayout.EnumPopup("모듈 종류를 선택 하세요",(ModuleKind)SelectedUnitKind);

            EditorGUILayout.Space();

            if (GUILayout.Button("Load", GUILayout.Height(25)))
            {
                Debug.Log(((ModuleKind)SelectedUnitKind).ToString() + " 유닛의 데이터 정보를 불러옵니다..");
                ModuleEditor.Open((ModuleKind)SelectedUnitKind);
            }

        }
    }
}
