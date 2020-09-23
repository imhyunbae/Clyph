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
        if (BattleUIManager.Instance.HandleCreature == null)
        {
            GameObject Unit = GameObject.Instantiate(BattleUIManager.Instance.CreaturePrefeb);
            Unit.transform.position = Manager.Instance.Camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
            Unit.GetComponent<Unit>().enabled = false;
            BattleUIManager.Instance.HandleCreature = Unit.GetComponent<Unit>();
        }
    }
}
