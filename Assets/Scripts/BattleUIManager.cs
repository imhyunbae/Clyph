using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUIManager : MonoBehaviour
{
    //싱글톤 변수
    public static BattleUIManager Instance;

    public BattleInventory BattleInventory;

    [Header ("SP")]
    [SerializeField] Text SP_Text;

    [Header ("Wave")]
    [SerializeField] Text Wave_Text;

    [Header("Creature")]
    [SerializeField] RectTransform CreatureList;
    [SerializeField] GameObject CreatureListElementPrefeb;
    [SerializeField] public GameObject CreaturePrefeb;
    public Unit HandleCreature = null;
    public float HandleTimer = 0f;
    public float HandleDelay = 1.5f;

   [Header("Spirit")]
    [SerializeField] Text[] Spirit_Text = new Text[4];

    private void Awake()
    {     
        //싱글톤 변수 초기화
        Instance = this;

    }

    void Start()
    {
        
    }

    void Update()
    {
        if(HandleCreature != null)
        {
            HandleTimer += Time.deltaTime;
            Ray ray=  Manager.Instance.Camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit,5000000,LayerMask.GetMask("Map")))
            {
                print(hit.transform.gameObject.tag);
                HandleCreature.transform.position = hit.point + new Vector3(0,0.5f,0);
            }

           
        }
    }

    private void LateUpdate()
    {
        SP_Text.text = "SP : " + BattleInventory.SP.ToString();
        var waveCount = Manager.Instance.nthWave;
        Wave_Text.text = "Wave " + waveCount.Item1.ToString() + "/" + waveCount.Item2.ToString();

        for (int i = 0; i < 4; i++)
            Spirit_Text[i].text = "X" +  BattleInventory.SpiritCount[i].ToString();


        foreach (var iter in BattleInventory.DictionaryModule)
        {
            Transform child = CreatureList.Find(iter.Key.ToString());
            if (child != null)
            {
                    if (BattleInventory.DictionaryModule[iter.Key] == 0)
                    {
                        GameObject.Destroy(child.gameObject);
                        SortCreatureList();
                    }
                    else child.GetComponent<CreatureListElement>().Count = iter.Value;
            }
            else
            {
                if (BattleInventory.DictionaryModule[iter.Key] > 0)
                {
                    GameObject Element = GameObject.Instantiate(CreatureListElementPrefeb, CreatureList);
                    Element.GetComponent<CreatureListElement>().Name = iter.Key.ToString();
                    Element.GetComponent<CreatureListElement>().Count = iter.Value;
                    Element.GetComponent<CreatureListElement>().Kind = iter.Key;
                    Element.name = iter.Key.ToString();
                    SortCreatureList();
                }
            }


        }
    }
    public void SortCreatureList()
    {
        List<Transform> children = new List<Transform>();
        for (int i = CreatureList.transform.childCount - 1; i >= 0; i--)
        {
            Transform child = CreatureList.transform.GetChild(i);
            children.Add(child);
            child.parent = null;
        }
        children.Sort((Transform t1, Transform t2) => { return t1.name.CompareTo(t2.name); });
        foreach (Transform child in children)
        {
            child.parent = CreatureList.transform;
        }
    }


}
