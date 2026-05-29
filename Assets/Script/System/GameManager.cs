using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
//ゲーム管理クラス
public class GameManager : MonoBehaviour
{
    public static GameManager Instance; //シングルトンインスタンス
    public static event Action OnGameStart; //ゲーム開始時のイベント
    public  bool IsGameInit = false;
  


    [SerializeField] Toggle _debug_Mode_Toggle; //デバッグモードのトグル
    public bool _isDebugMode = false; //デバッグモードかどうか
    bool _isBattle = false; //戦闘中かどうか
    bool _isGameOver = false; //ゲームオーバーかどうか
    int _round = 0; //現在のラウンド数
    public int PlayingCharactorId { get; set; }//プレイヤー選択したキャラID 1:魔法使い　2:剣士
    public bool GameStart { get; set; }
    GameSceneManager _gameSceneManager;
    [SerializeField]GameObject[] _charactorlist;
   public bool IsInit = false;//初期化フラグ
 // public  bool testInit = false;

   
    
    
    
    //ゲーム開始するとき初期化
    public void GameInit(int charnum)
    {
        //_charactorlist = GameSceneManager.Instance.InstancecharactorList;
        //InstanceCharactor(charnum);
        GameSceneManager.Instance.InstandCharactor(charnum);
        //OnGameStart?.Invoke();
        IsInit = true;


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

        //if (!GameStart) _isInit=false;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            GameSceneManager.Instance.InstandEnemy(0);
        }
      //  Debug.Log("IsInit:" + _isInit);
        if (IsInit == false && SceneManager.GetActiveScene().name == "GameScene")
        {
            GameInit(PlayingCharactorId);
            //_isInit=true;
        }

        
    }
    void InstanceCharactor(int num)
    {
         
        _charactorlist[num].SetActive(true);
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
