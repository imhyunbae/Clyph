using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Grid : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool PointerOver = false;
    public Unit AttachedUnit = null;

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 0.25f);
        PointerOver = true;
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.25f);
        PointerOver = false;
    }

    private void Start()
    {
        GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.25f);

    }

    public void Update()
    {
        if (PointerOver)
        {
            if (Input.GetMouseButtonDown(0))
                BattleUIManager.Instance.OnGridDropSuccess(GetComponent<RectTransform>().position);

            if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Escape))
                BattleUIManager.Instance.OnGridDropFailed();
        }
    }

}
