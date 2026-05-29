using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

//GameScene必要な関数クラス
public class GameSceneManager : MonoBehaviour
{
    [SerializeField] List<GameObject> _charactorPreList;
    static public GameSceneManager Instance;
    public GameObject PlayerPre;

    [SerializeField]List<GameObject> _enemyPreList;

    //public List<GameObject> InstantedEnemyPreList;
    public GameObject InstantedEnemyPreList;



    private void Awake()
    {
        Instance = this;
    }
    /// <summary>
    /// キャラ選択した生成する関数
    /// </summary>
    /// <param name="charID">キャラID</param>
    public void InstandCharactor(int charID)
    {
        PlayerPre = Instantiate(_charactorPreList[charID], new Vector3(-10, 3, 0), Quaternion.identity);
        
    }
    public void InstandEnemy(int enemyID)
    {
        GameObject enemy = Instantiate(_enemyPreList[enemyID], new Vector3(5, 2, 0), Quaternion.identity);
        InstantedEnemyPreList = enemy;
    }

}
