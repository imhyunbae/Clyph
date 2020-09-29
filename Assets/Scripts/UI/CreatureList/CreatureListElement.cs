using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[ExecuteInEditMode]
public class CreatureListElement : MonoBehaviour
{
    [SerializeField] Text TestText;
    [SerializeField] Text CountText;
    [SerializeField] Image IconImage;
   
    public Sprite IconSprite;
    public int Count;
    public string Name;
    public ModuleKind Kind;
    private void Awake()
    {
       // CountText = GetComponentInChildren<Text>();
       // IconImage = GetComponentInChildren<Image>();
    }

    void LateUpdate()
    {
        TestText.text = Name;
        IconImage.sprite = IconSprite;
        CountText.text = "X" + Count.ToString();
    }

    public void OnButtonClick()
    {
        if(Manager.Instance.CurrentStage.phase == Phase.Break)
        BattleUIManager.Instance.SelectUnit(Kind);
    }
}
