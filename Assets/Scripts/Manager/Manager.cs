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

public class Manager : MonoBehaviour
{
    public static Manager Instance;
    public List<Module> Modules;
    public List<Enemy> Enemies;
    public GameObject HpBarParnet;
    public GameObject BattleStartButton;
    public Canvas Canvas;
    public GameObject HealthBar;
    public GameObject WaveEnemies;
    public GameObject WaveParent;
    public Camera Camera;
    // private bool WorldCamera;
    // private Vector3 TargetCameraPosition;
    // private Quaternion TargetCameraRotation;
    public List<Stage> Stages;
    public int CurrentStageIndex = 0;
    public Stage CurrentStage { get { return Stages[CurrentStageIndex]; } }
    public (int, int) nthWave
    {
        get
        {
            int nth = 0, total = 0;
            for (int i = 0; i < Stages.Count; i++)
            {
                if (Stages[i].phase != Phase.Battle)
                    continue;
                if (i <= CurrentStageIndex)
                    nth += 1;
                total += 1;
            }
            return (nth, total);
        }
    }
    private void Awake()
    {
        Manager.Instance = this;
    }
    public void RegisterUnit(GameObject _Unit)
    {
        Unit unit = _Unit.GetComponent<Unit>();
        GameObject HPBar = Instantiate(HealthBar, HpBarParnet.transform);
        HPBar.GetComponent<HealthBar>().Setup(unit);

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

        Modules = FindObjectsOfType<Module>().ToList();

        foreach (Module each in Modules)
        {
            GameObject HPBar = Instantiate(HealthBar, HpBarParnet.transform);
            each.Team = ETeam.Module;
            HPBar.GetComponent<HealthBar>().Setup(each);
        }

        StageWillBegin();
    }

    void Update()
    {
        if (Stages.Count == 0)
            return; // game end

        if (CurrentStage.phase == Phase.Break)
        {
            CurrentStage.Duration -= Time.deltaTime;

            if (CurrentStage.Duration <= 0.0f)
                NextStage();
        }
    }

    public void NextStage()
    {
        if (Stages.Count == CurrentStageIndex)
            return;

        StageWillFinished();
        CurrentStageIndex += 1;
        StageWillBegin();
    }

    void StageWillFinished()
    {
        if (CurrentStage.phase == Phase.Break)
        {

            BattleUIManager.Instance.OnGridDropFailed();
            BattleUIManager.Instance.ClosePannel();
            BattleUIManager.Instance.GridParent.SetActive(false);
            BattleStartButton.SetActive(false);
            BattleUIManager.Instance.OffModeUI();

        }
    }

    void StageWillBegin()
    {
        if (CurrentStage.phase == Phase.Battle)
        {


            foreach (var each in Enemies)
                each.Battle = true;

            foreach (var each in Modules)
                each.Battle = true;
        }

        if (CurrentStage.phase == Phase.Break)
        {
            BattleUIManager.Instance.ManageMode();
            BattleStartButton.SetActive(true);
            BattleUIManager.Instance.GridParent.SetActive(true);

            foreach (var each in Modules)
            {
                each.Battle = false;
                each.transform.position = each.StartPos;
            }

            Enemies.Clear();
            GameObject Wave = GameObject.Instantiate(WaveEnemies, WaveParent.transform);
            Enemies = FindObjectsOfType<Enemy>().ToList();
            foreach (var each in Enemies)
            {
                if (each.HP <= 0)
                {
                    Enemies.Remove(each);
                    break;
                }
            }
            foreach (var each in Enemies)
            {
                if (each.HP <= 0)
                {
                    Enemies.Remove(each);
                    continue;
                }
                else
                {
                    each.Battle = false;
                    GameObject HPBar = Instantiate(HealthBar, HpBarParnet.transform);
                    each.Team = ETeam.Enemy;
                    HPBar.GetComponent<HealthBar>().Setup(each);
                }
            }
        }
    }
    public void OnModuleDie(Module DeadModule)
    {
        Modules.Remove(DeadModule);
        if (Modules.Count == 0)
            return;//game over;
    }

    public void OnEnemyDie(Enemy DeadEnemy)
    {
        Enemies.Remove(DeadEnemy);
        if (Enemies.Count == 0)
        {
       
            Enemies.Clear();
            GameObject.Destroy(DeadEnemy.transform.parent.gameObject);
            this.Invoke( "NextStage", 1.25f);

        }
    }

    // public ResetTarget(ETeam team, List<GameObject> Targets)
    // {
    //     StartCoroutine(AssignTarget());
    // }

    // IEnumerator AssignTarget()
    // {

    // }

    // void Update()
    // {
    // if (Input.GetKeyDown(KeyCode.C))
    // {
    //     WorldCamera = !WorldCamera;
    // }

    // if (WorldCamera)
    // {
    //     TargetCameraPosition = Modules[0].transform.position + new Vector3(7.5f, 7.5f, 0.0f);
    //     TargetCameraRotation = Quaternion.Euler(45, -90, 0);
    // }
    // else
    // {
    //     TargetCameraPosition = new Vector3(0.0f, 15.0f, -15.0f);
    //     TargetCameraRotation = Quaternion.Euler(45, 0, 0);
    // }
    // float Multiplier = 10;
    // Camera.transform.position = Vector3.Lerp(Camera.transform.position, TargetCameraPosition, Time.deltaTime * Multiplier);
    // Camera.transform.rotation = Quaternion.Lerp(Camera.transform.rotation, TargetCameraRotation, Time.deltaTime * Multiplier);
    // }
}
