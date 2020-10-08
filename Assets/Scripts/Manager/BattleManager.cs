using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Phase
{
    Battle, Break
}

[System.Serializable]
public class Stage
{
    public Phase phase;
    public float Duration;
    public string Title;
}

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;

    [Header("Unit")]
    [SerializeField] public List<Module> Modules;
    [SerializeField] public List<Enemy> Enemies;

    [Header("UI")]
    [SerializeField] Canvas UICanvas;
    [SerializeField] Transform UnitHpBarParnet;
    [SerializeField] GameObject BattleStartButton; 

    [Header("Prefab")]
    [SerializeField]  public GameObject UnitHPBarPrefab;
    
    //Wave
    [Header("WaveSystem")]
    [SerializeField] Transform WaveSpawnParent;
    List<GameObject> WaveEnemies = new List<GameObject>();

    [SerializeField] float BreakDuraction;
    float BreakTime;

    int CurrentWaveIndex = 0;
    public Phase CurrentPhase;
    public int CurrentWave { get { return CurrentWaveIndex + 1; } }
    public float LeftBreakTime {  get { return  BreakDuraction - BreakTime; } }
    public int MaxWave {  get { return WaveEnemies.Count; } }


    private void Awake()
    {
        BattleManager.Instance = this;
    }
    public void RegisterUnit(GameObject _Unit)
    {
        Unit unit = _Unit.GetComponent<Unit>();

        //체력바 등록
        GameObject HPBar = Instantiate(UnitHPBarPrefab, UnitHpBarParnet);
        HPBar.GetComponent<HealthBar>().Setup(unit);

        //팀 등록
        if (_Unit.GetComponent<Module>() != null)
        {
            Modules.Add(_Unit.GetComponent<Module>());
            unit.Team = ETeam.Module;
        }
        if (_Unit.GetComponent<Enemy>() != null)
        {
            Enemies.Add(_Unit.GetComponent<Enemy>());
            unit.Team = ETeam.Enemy;
        }
    }

    void Start()
    {
        var FindModules = FindObjectsOfType<Module>().ToList();

        foreach (Module each in FindModules)
            RegisterUnit(each.gameObject);

        //스테이지 로드
        WaveEnemies = LoadStageWave(1);

        CurrentPhase = Phase.Break;
        BreakTime = 0f;
        OnPhaseBegin();
    }
    List<GameObject> LoadStageWave(int _Stage)
    {
        return Resources.LoadAll<GameObject>("Prefabs/EnemyWave/Stage" + _Stage.ToString("00")).ToList();
    }
    void Update()
    {
        if (CurrentPhase == Phase.Battle)
        {
            //모든 적을 처치 했다면
            if (Enemies.Count == 0)
            {
                //모든 스테이지를 클리어 했다면
                if (CurrentWave == MaxWave)
                {

                }
                //아니라면
                else
                {
                   
                    SetBattlePhase(Phase.Break);

                }
            }
        }
        else if (CurrentPhase == Phase.Break)
        {
            BreakTime += Time.deltaTime;

            if(BreakTime > BreakDuraction)
            {
                BreakTime = 0f;
                SetBattlePhase(Phase.Battle);
            }
        }
    }
    public void SetBattlePhase(Phase _Phase)
    {
        OnPhaseEnd();

        CurrentPhase = _Phase;

        OnPhaseBegin();
    }
    void OnPhaseBegin()
    {
        if (CurrentPhase == Phase.Battle)
        {
            foreach (var each in Enemies)
                each.Battle = true;

            foreach (var each in Modules)
                each.Battle = true;
        }
        else if (CurrentPhase == Phase.Break)
        {
            BattleUIManager.Instance.ManageMode();
            BattleStartButton.SetActive(true);
            BattleUIManager.Instance.GridParent.SetActive(true);

            foreach (var each in Modules)
            {
                each.Battle = false;
                each.transform.position = each.StartPos;
            }

            GameObject Wave = GameObject.Instantiate(WaveEnemies[CurrentWaveIndex], WaveSpawnParent);
     
            var FindEnemies = FindObjectsOfType<Enemy>().ToList();
            foreach (var each in FindEnemies)
                RegisterUnit(each.gameObject);
        }
    }
    void OnPhaseEnd()
    {
        if(CurrentPhase == Phase.Battle)
        {
            CurrentWaveIndex++;
        }
        else if (CurrentPhase == Phase.Break)
        {
            BattleUIManager.Instance.OnGridDropFailed();
            BattleUIManager.Instance.ClosePannel();
            BattleUIManager.Instance.GridParent.SetActive(false);
            BattleStartButton.SetActive(false);
            BattleUIManager.Instance.OffModeUI();


        }
    }
    void OnGameOver()
    {

    }

    public void OnModuleDie(Module DeadModule)
    {
        Modules.Remove(DeadModule);
        if (Modules.Count == 0)
            OnGameOver();//game over;
    }

    public void OnEnemyDie(Enemy DeadEnemy)
    {
        Enemies.Remove(DeadEnemy);
        if (Enemies.Count == 0)
            GameObject.Destroy(DeadEnemy.transform.parent.gameObject);
    }



}
