using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum ETeam
{
    Module, Enemy
}

public class Manager : MonoBehaviour
{
    public static Manager Instance;
    public List<Module> Modules;
    public List<Enemy> Enemies;
    public Canvas Canvas;
    public GameObject HealthBar;
    public Camera Camera;
    private bool WorldCamera;
    private Vector3 TargetCameraPosition;
    private Quaternion TargetCameraRotation;

    void Start()
    {
        Manager.Instance = this;

        Modules = FindObjectsOfType<Module>().ToList();
        Enemies = FindObjectsOfType<Enemy>().ToList();

        foreach (Module each in Modules)
        {
            GameObject HPBar = Instantiate(HealthBar, Canvas.transform);
            each.Team = ETeam.Module;
            HPBar.GetComponent<HealthBar>().Setup(each);
        }

        foreach (Enemy each in Enemies)
        {
            GameObject HPBar = Instantiate(HealthBar, Canvas.transform);
            each.Team = ETeam.Enemy;
            HPBar.GetComponent<HealthBar>().Setup(each);
        }
    }

    // public ResetTarget(ETeam team, List<GameObject> Targets)
    // {
    //     StartCoroutine(AssignTarget());
    // }

    // IEnumerator AssignTarget()
    // {

    // }

    void Update()
    {
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
    }
}
