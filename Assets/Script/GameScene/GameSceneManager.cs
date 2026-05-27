using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

//GameScene必要な関数クラス
public class GameSceneManager : MonoBehaviour
{
    static public GameSceneManager Instance;
    [SerializeField] public GameObject[] _charactorPreList;

    [SerializeField]List<GameObject> _enemyPreList;

    public List<GameObject> InstantedEnemyPreList;


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
        Instantiate(_charactorPreList[charID],new Vector3(-10,3,0), Quaternion.identity);
    }

}
