using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUIManager : MonoBehaviour
{
    //싱글톤 변수
    public static BattleUIManager Instance;

    public BattleInventory BattleInventory;
    
    [Header("Category")]
    public GameObject ManageCatergory;
    public GameObject BuyCategory;

    [Header("SP")]
    public Text SP_Text;

    [Header("Grid")]
    public GameObject GridParent;

    [Header("Wave")]
    public Text Wave_Text;

    [Header("LeftTime")]
    public Text LeftTime_Text;

    [Header("Pannel")]
    public GameObject Hero_Pannel;
    public GameObject Creature_Pannel;

    [Header("Creature")]
    public RectTransform CreatureList;
    public GameObject CreatureListElementPrefeb;
    public GameObject CreaturePrefeb;

    [HideInInspector]public Unit HandleCreature = null;
    [HideInInspector]public float HandleTimer = 0f;
    [HideInInspector]public float HandleDelay = 0f;

    [Header("Spirit")]
    [SerializeField] Text[] Spirit_Text = new Text[4];

    private void Awake()
    {
        //싱글톤 변수 초기화
        Instance = this;
        HandleDelay = 0f;
    }

    void Start()
    {

    }

    public void ManageMode()
    {
        if (BattleManager.Instance.CurrentPhase == Phase.Break)
        {
            ManageCatergory.SetActive(true);
            GridParent.SetActive(true);
            BuyCategory.SetActive(false);
            ClosePannel();
        }
    }

    public void BuyMode()
    {
        if (BattleManager.Instance.CurrentPhase == Phase.Break)
        {
            Hero_Pannel.SetActive(false);
            Creature_Pannel.SetActive(false);

            ManageCatergory.SetActive(false);
            GridParent.SetActive(false);
            BuyCategory.SetActive(true);
            ClosePannel();
        }
    }

    public void OffModeUI()
    {
        ManageCatergory.SetActive(false);
        GridParent.SetActive(false);
        BuyCategory.SetActive(false);
        Hero_Pannel.SetActive(false);
        Creature_Pannel.SetActive(false);
        ClosePannel();
    }

    public void ClosePannel()
    {
        if (Creature_Pannel.active == true)
            Creature_Pannel.GetComponent<Animator>().Play("Out");

        if (Hero_Pannel.active == true)
            Hero_Pannel.GetComponent<Animator>().Play("Out");
    }

    public void OpenCreaturePannel()
    {
        if (Hero_Pannel.active == true)
            Hero_Pannel.GetComponent<Animator>().Play("Out");

        if (Creature_Pannel.active == false)
            Creature_Pannel.SetActive(true);

       Instance.Creature_Pannel.GetComponent<Animator>().Play("In");
    }

    public void OpenHeroPannel()
    {
        if (Creature_Pannel.active == true)
            Creature_Pannel.GetComponent<Animator>().Play("Out");

        if (Hero_Pannel.active == false)
            Hero_Pannel.SetActive(true);

       Hero_Pannel.GetComponent<Animator>().Play("In");
    }

    public void SelectUnit(ModuleKind _Kind)
    {
        if (HandleCreature != null)
            return;
    
        GameObject Unit = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Creature/" + _Kind.ToString()));
        HandleCreature = Unit.GetComponent<Unit>();
        HandleCreature.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
        HandleCreature.Battle = false;

        ((Module)HandleCreature).Kind = _Kind;
    }

    public void OnGridDropSuccess(Vector3 _Position)
    {
        if (HandleCreature == null || HandleTimer < HandleDelay)
            return;

        HandleTimer = 0f;

        HandleCreature.transform.position = _Position + new Vector3(0,0.5f,0);
        HandleCreature.StartPos = HandleCreature.transform.position;
        BattleManager.Instance.RegisterUnit(BattleUIManager.Instance.HandleCreature.gameObject);
        BattleInventory.DictionaryModule[((Module)HandleCreature).Kind]--;

        HandleCreature = null;
    }

    public void OnGridDropFailed()
    {
        if (HandleCreature == null)
            return;

        HandleTimer = 0f;
        GameObject.Destroy(HandleCreature.gameObject);
        HandleCreature = null;
    }

    void Update()
    {

        if (HandleCreature != null)
        {
            HandleTimer += Time.deltaTime;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit, 5000000, LayerMask.GetMask("Map")))
            {
                HandleCreature.transform.position = new Vector3(hit.point.x, HandleCreature.transform.position.y, hit.point.z); 
            }
        }
    }

    private void LateUpdate()
    {
        if(BattleManager.Instance.CurrentPhase == Phase.Break)
        {
            LeftTime_Text.transform.parent.gameObject.SetActive(true);

            int LeftTime = (int)BattleManager.Instance.LeftBreakTime;
            LeftTime_Text.text = (LeftTime / 60).ToString() + " : " + (LeftTime % 60).ToString();

        }
        else LeftTime_Text.transform.parent.gameObject.SetActive(false);


        SP_Text.text = "SP : " + BattleInventory.SP.ToString();
        int MaxWave = BattleManager.Instance.MaxWave;
        int CurrentWave = BattleManager.Instance.CurrentWave;

        Wave_Text.text = "Wave " + CurrentWave.ToString() + "/" + MaxWave.ToString();

        for (int i = 0; i < 4; i++)
            Spirit_Text[i].text = "X" + BattleInventory.SpiritCount[i].ToString();


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
                    Element.GetComponent<CreatureListElement>().IconSprite = Resources.Load<GameObject>("Prefabs/Creature/" + iter.Key.ToString()).GetComponent<SpriteRenderer>().sprite;
                    Element.name = iter.Key.ToString();
                    SortCreatureList();
                }
            }


        }
    }
    public void OnBattleStartButtonDown()
    {

        BattleManager.Instance.SetBattlePhase(Phase.Battle);
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
