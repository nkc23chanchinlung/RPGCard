using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class UiManager : MonoBehaviour
{
    [SerializeField] Text _debug_Text;
    [SerializeField] GameObject _dmg_Text_Prefab;
    [SerializeField] Transform _dmg_Text_Parent;
    [SerializeField] GameObject _gameCanvas;

    public static UiManager Instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CheckUIManagerExist();
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

        if (Input.GetKeyDown(KeyCode.T))
        {
            CreateDmg_Text(_dmg_Text_Parent, 100).Forget();
        }
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
}
