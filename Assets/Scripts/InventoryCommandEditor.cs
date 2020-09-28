using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(InventoryCommand))]
public class InventoryCommandEditor : Editor
{
    public int a;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("크리쳐 데이터 추가"))
        {
            ((InventoryCommand)target).Add();
        }
        if (GUILayout.Button("스피릿 데이터 추가"))
        {
            ((InventoryCommand)target).SpiritAdd();
        }

    }
}
