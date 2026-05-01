using UnityEngine;

//職業:剣士クラス
public class SwordMan :PlayerBase
{
    PlayerController _controller;
    [SerializeField] GameObject _enemy;



    private void Awake()
    {
        _controller = GetComponent<PlayerController>();

        Attack = 10;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Hp = MaxHP;
        _controller.SelectCard();

    }
}
