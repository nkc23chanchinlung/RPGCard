using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class UiManager : MonoBehaviour
{
    [SerializeField] Text _debug_Text;
    [SerializeField] GameObject _dmg_Text_Prefab;
    [SerializeField] GameObject _gameCanvas;
    [SerializeField] Text _chance_Text;
    
    [SerializeField] GameObject _bag;
    Animator _bag_Anim;

    bool _isTouchingBag = false;

    public static UiManager Instance;
    void Awake()
    {
        CheckUIManagerExist();
        _bag_Anim = _bag.GetComponent<Animator>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       // CheckUIManagerExist();
    }
    private void FixedUpdate()
    {
        _isTouchingBag = UITouchCollider(_bag, 1f);
        _bag_Anim.SetBool("IsTouching", _isTouchingBag);
    }
    // Update is called once per frame
    void Update()
    {
       

        if (GameManager.Instance._isDebugMode)
        {
            _debug_Text.gameObject.SetActive(true);
        }
        else
        {
            _debug_Text.gameObject.SetActive(false);
        }

       
    }
    
    /// <summary>
    /// ダメージ値のテキストを生成する
    /// </summary>
    /// <param name="targer">対象</param>
    /// <param name="dmg">ダメージ値</param>
    async public UniTask CreateDmg_Text(Transform targer,int dmg)
    {
        float uiPosZ = 10f;//UIのZ座標を指定
        Vector3 screenPos = Camera.main.WorldToScreenPoint( targer.transform.position + new Vector3(0, 1, uiPosZ));
       Text _dmg_Text= Instantiate(_dmg_Text_Prefab, _gameCanvas.transform).GetComponent<Text>();
        _dmg_Text.transform.position = screenPos;    
        _dmg_Text.text = dmg.ToString();
        _dmg_Text.transform.DOMove(
            screenPos + new Vector3(0f, 100f, 0f),
            1f
        ).SetEase(Ease.OutCubic).OnComplete(() =>
        {
            Destroy(_dmg_Text.gameObject);
        });

        await UniTask.Yield();

    }
    async public UniTask EditChanceText(int times)
    {
        _chance_Text.gameObject.transform.localScale = new Vector3(16, 16, 16);
        _chance_Text.gameObject.transform.DOScale(8, 0.5f).SetEase(Ease.OutBounce);
        if(times==1)_chance_Text.color = Color.red;
        else _chance_Text.color = Color.white;
        _chance_Text.text ="チャンス:"+ times.ToString();
    }


    void CheckUIManagerExist()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    /// <summary>
    /// Uiに当たり判定をつける関数
    /// </summary>
    /// <param name="obj">対象のUIオブジェクト</param>
    /// <param name="scale">当たり判定のスケール</param>
    public bool UITouchCollider(GameObject obj, float scale)
    {
        Vector3 worldPosObj=Camera.main.ScreenToWorldPoint(obj.transform.position);
        float sizeX = Mathf.Abs(obj.transform.localScale.x) / 2;
        float sizeY = Mathf.Abs(obj.transform.localScale.y) / 2;

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPos.z = 0;
        float x1 = worldPosObj.x - sizeX * scale;
        float x2 = worldPosObj.x + sizeX * scale;
        float y1 = worldPosObj.y - sizeY * scale;
        float y2 = worldPosObj.y + sizeY * scale;
        Debug.DrawLine(new Vector3(x1, y2, 0), new Vector3(x2, y2, 0), Color.red);
        Debug.DrawLine(new Vector3(x1, y1, 0), new Vector3(x2, y1, 0), Color.red);
        Debug.DrawLine(new Vector3(x1, y1, 0), new Vector3(x1, y2, 0), Color.red);
        Debug.DrawLine(new Vector3(x2, y1, 0), new Vector3(x2, y2, 0), Color.red);
     
        if (worldPos.x > x1 && worldPos.x < x2 && worldPos.y > y1 && worldPos.y < y2)
        {
            return true;
        }
        else         {
            return false;
        }


    }



}
