using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] Text _debug_Text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance._isDebugMode)
        {
            _debug_Text.gameObject.SetActive(true);
        }
        else
        {
            _debug_Text.gameObject.SetActive(false);
        }
    }
}
