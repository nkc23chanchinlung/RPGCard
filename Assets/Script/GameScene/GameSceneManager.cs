using UnityEngine;

//GameScene必要な関数クラス
public class GameSceneManager : MonoBehaviour
{
    static public GameSceneManager Instance;
    [SerializeField] GameObject[] _charactorPre;


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
        Instantiate(_charactorPre[charID],new Vector3(-10,3,0), Quaternion.identity);
    }

}
