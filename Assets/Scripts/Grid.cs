using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Grid : MonoBehaviour
{
    public bool PointerOver = false;
    public Unit AttachedUnit = null;

   

    private void Start()
    {
        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);

    }

    public void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit, float.MaxValue, LayerMask.GetMask("Grid")) == true )
        {
            if (hit.transform.gameObject == this.gameObject)
            {
                transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, 0.15f);
                PointerOver = true;
            }

        }
        else
        {
            if (Input.GetMouseButtonDown(0))
                BattleUIManager.Instance.ClosePannel();

            transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.15f);
            PointerOver = false;
        }


        if (PointerOver)
        {
            if (Input.GetMouseButtonDown(0))
            {

                if(AttachedUnit != null)
                {
        
                    BattleUIManager.Instance.OpenCreaturePannel();

                }
                else
                {
                    if (BattleUIManager.Instance.HandleCreature != null)
                    {
                        AttachedUnit = BattleUIManager.Instance.HandleCreature;
                        BattleUIManager.Instance.OnGridDropSuccess(transform.position);

                    }
                    else BattleUIManager.Instance.OpenHeroPannel();
                }
            }



            if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Escape))
                BattleUIManager.Instance.OnGridDropFailed();
        }
    }
   
}
