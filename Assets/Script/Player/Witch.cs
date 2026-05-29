using UnityEngine;
using System.Collections.Generic;


//職業:魔女クラス
public sealed class Witch : PlayerBase
{
    PlayerController _controller;
    //魔女の攻撃ルールを管理するリスト(書き込み専用)
    [SerializeField]private List<JobAttackArray> _WitchAttackArrays  = new List<JobAttackArray>();



    Witch()
    {
        Debug.Log("Witch");
        _jobAttackArrays = _WitchAttackArrays;
        _hpBar = UiManager.Instance.Player_Hp_bar;
        _controller.SelectCard();
       
        _controller = GetComponent<PlayerController>();
        
        Hp = MaxHP;
    }

    private void OnEnable()
    {
       
    }
    private void Awake()
    {
       
       


    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
     
       

    }

}
