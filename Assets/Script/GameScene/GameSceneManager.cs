using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

//GameScene必要な関数クラス
public class GameSceneManager : MonoBehaviour
{
    GameObject Player;
    static public GameSceneManager Instance;
    [SerializeField] public GameObject[] _charactorPreList;

    [SerializeField]List<GameObject> _enemyPreList;

    public List<GameObject> InstantedEnemyPreList;

   
   PlayerController _playerController;

    private void Awake()
    {
        if (Player == null) return;
        Debug.Log(Player.gameObject.name);
       _playerController =Player. GetComponent<PlayerController>();
        _playerController.Init();
        Instance = this;
    }
    void Start()
    {

    }
    /// <summary>
    /// キャラ選択した生成する関数
    /// </summary>
    /// <param name="charID">キャラID</param>
    public void InstandCharactor(int charID)
    {
       Player= Instantiate(_charactorPreList[charID],new Vector3(-10,3,0), Quaternion.identity);
       
    }



   
}
