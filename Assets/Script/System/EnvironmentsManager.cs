using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class EnvironmentsManager : MonoBehaviour
{
    [SerializeField] GameObject[] _cloud;
    [SerializeField] GameObject[] _farawayCloud;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //void Start()
    //{
    //    Moveenvir().Forget();
    //}

    // Update is called once per frame
    void Update()
    {
        MoveEnvironments();
    }
    //async UniTask Moveenvir()
    //{
    //    foreach (var cloud in _cloud)
    //    {
    //        cloud.transform.DOMoveX(-10f, 5f).SetLoops(-1, LoopType.Restart);
    //    }
    //    foreach (var _farawayCloud in _farawayCloud)
    //    {
    //        _farawayCloud.transform.DOMoveX(-10f, 5f).SetLoops(-1, LoopType.Restart);
    //    }
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

        //}
    }
}
