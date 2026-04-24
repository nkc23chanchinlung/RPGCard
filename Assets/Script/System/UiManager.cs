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
    [SerializeField] GameObject bag;

    public static UiManager Instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CheckUIManagerExist();
    }

    // Update is called once per frame
    void Update()
    {
        ColliderMouseOver(bag);

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
    void ColliderMouseOver(GameObject obj) /*******いま書いてる********/
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            GameObject hitobj= EventSystem.current.IsPointerOverGameObject() ? this.gameObject : null;

       
            Debug.Log(hitobj.name);
        }
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //// Raycastを作成
        //RaycastHit2D hit2d = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);
        //Debug.Log(hit2d.transform.gameObject);

        //if (hit2d.transform.gameObject== obj)
        //{
        //    Debug.Log(obj);
        //}
    }
        
           
    
}
