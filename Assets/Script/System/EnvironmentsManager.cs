using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class EnvironmentsManager : MonoBehaviour
{
    [SerializeField] GameObject[] _cloud;
    [SerializeField] GameObject[] _farawayCloud;
    [SerializeField] GameObject _bird;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AsyncEnvironments().Forget();
    }

    // Update is called once per frame
    void Update()
    {
        MoveEnvironments();
    }

    void MoveEnvironments()
    {
        for (int i = 0; i < _cloud.Length; i++)
        {
            _cloud[i].transform.Translate(Vector3.left * Time.deltaTime*0.5f);
            if (_cloud[i].transform.position.x <= -19f)
            {
                _cloud[i].transform.position = new Vector3(19f, _cloud[i].transform.position.y, _cloud[i].transform.position.z);
            }
        }
        for (int i = 0; i < _farawayCloud.Length; i++)
        {
            _farawayCloud[i].transform.Translate(Vector3.left * Time.deltaTime * 0.2f);
            if (_farawayCloud[i].transform.position.x <= -19f)
            {
                _farawayCloud[i].transform.position = new Vector3(19f, _farawayCloud[i].transform.position.y, _farawayCloud[i].transform.position.z);
            }
        }
    }
    /// <summary>
    /// 非同期処理環境オブジェクト処理関数
    /// </summary>
    /// <returns></returns>
    async UniTask AsyncEnvironments()
    {
        _bird.transform.DOMoveX(20f, 10f).SetLoops(-1, LoopType.Restart);
        await UniTask.Delay(1000);
    }
}
