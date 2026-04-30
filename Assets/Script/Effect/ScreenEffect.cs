using Cysharp.Threading.Tasks;
using UnityEngine;

public class ScreenEffect : MonoBehaviour
{
    [SerializeField] GameObject _flash;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) Flash(100).Forget();
    }

    public async UniTask Flash(int Delay)
    {
        await UniTask.Delay(Delay);
        _flash.SetActive(true);
        await UniTask.Delay(50);
        _flash.SetActive(false);
        await UniTask.Delay(50);
        _flash.SetActive(true);
        await UniTask.Delay(50);
        _flash.SetActive(false);


    }
}
