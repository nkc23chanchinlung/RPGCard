using UnityEngine;
using UnityEngine.UI;
//ゲーム管理クラス
public class GameManager : MonoBehaviour
{
    public static GameManager Instance; //シングルトンインスタンス


    [SerializeField] Toggle _debug_Mode_Toggle; //デバッグモードのトグル
    public bool _isDebugMode = false; //デバッグモードかどうか
    bool _isBattle = false; //戦闘中かどうか
    bool _isGameOver = false; //ゲームオーバーかどうか
    int _round = 0; //現在のラウンド数
    public int PlayingCharactorId { get; set; }//プレイヤー選択したキャラID 1:魔法使い　2:剣士
    public bool GameStart { get; set; }
    GameSceneManager _gameSceneManager;
    

    
    
    //ゲーム開始するとき初期化
    void GameInit()
    {
       
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      //  string SceneName =SceneManager.GetActiveScene().name;
        CheakGameManagerExist();
    }
    private void FixedUpdate()
    {
        if(_debug_Mode_Toggle!=null)
        _isDebugMode = _debug_Mode_Toggle.isOn;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    
    void CheakGameManagerExist()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }
    }

    
}
