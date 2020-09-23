using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Grid : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool PointerOver = false;
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
        if(Input.GetMouseButtonDown(0) && PointerOver)
        {
            if (BattleUIManager.Instance.HandleTimer > BattleUIManager.Instance.HandleDelay)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    BattleUIManager.Instance.HandleTimer = 0f;
                    BattleUIManager.Instance.HandleCreature.transform.position = Manager.Instance.Camera.ScreenToWorldPoint(this.GetComponent<RectTransform>().anchoredPosition);
                    Ray ray = Manager.Instance.Camera.ScreenPointToRay((this.GetComponent<RectTransform>().position));
                    ray.origin = BattleUIManager.Instance.HandleCreature.transform.position;
                   
                    RaycastHit hit = new RaycastHit();
                    if (Physics.Raycast(ray, out hit, 5000000, LayerMask.GetMask("Map")))
                    {
                        print(hit.transform.gameObject.tag);
                        BattleUIManager.Instance.HandleCreature.transform.position = hit.point + new Vector3(0, 0.5f, 0);
                    }


                    BattleUIManager.Instance.HandleCreature.GetComponent<Unit>().enabled = false;
                    BattleUIManager.Instance.HandleCreature = null;

                }
                if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Escape))
                {
                    BattleUIManager.Instance.HandleTimer = 0f;
                    GameObject.Destroy(BattleUIManager.Instance.HandleCreature.gameObject);
                    BattleUIManager.Instance.HandleCreature = null;
                }
            }
        }

        
    }
}
