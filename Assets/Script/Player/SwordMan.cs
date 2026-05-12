using System.Collections.Generic;
using UnityEngine;

//職業:剣士クラス
public class SwordMan :PlayerBase
{
    PlayerController _controller;
    [SerializeField] GameObject _enemy;
    //剣士の攻撃ルールを管理するリスト(書き込み専用)
    [SerializeField] private List<JobAttackArray> _SwordManAttackArrays = new List<JobAttackArray>();


    private void OnEnable()
    {
        _jobAttackArrays = _SwordManAttackArrays;
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
