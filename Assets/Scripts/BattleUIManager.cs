using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUIManager : MonoBehaviour
{
    //싱글톤 변수
    public static BattleUIManager Instance;

    [SerializeField] BattleInventory BattleInventory;

    //SP
    [SerializeField] Text SP_Text;


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
    }

    private void LateUpdate()
    {
        SP_Text.text = "SP : " + BattleInventory.SP.ToString();
    }
}
