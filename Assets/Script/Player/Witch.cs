using UnityEngine;
using System.Collections.Generic;


//職業:魔女クラス
public class Witch : PlayerBase
{
    PlayerController _controller;
    //魔女の攻撃ルールを管理するリスト(書き込み専用)
    [SerializeField]private List<JobAttackArray> _WitchAttackArrays  = new List<JobAttackArray>();
    


    private void OnEnable()
    {
        _jobAttackArrays=_WitchAttackArrays;
    }
    private void Awake()
    {
        _controller = GetComponent<PlayerController>();
   
        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Hp = MaxHP;
       _controller.SelectCard();
     
    }

}
