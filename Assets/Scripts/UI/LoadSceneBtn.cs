using UnityEngine;
using UnityEngine.UI;
using static Define;

public class LoadSceneBtn : MonoBehaviour
{
    //[SerializeField] Define.SceneType _sceneType;
    [SerializeField] string _sceneName; // 나중에 Enum으로 바꾸기
    Button _btn;
    void Start()
    {
        _btn = GetComponent<Button>();
        _btn.onClick.AddListener(() =>
        {
            //Managers.Scene.LoadScene(_sceneType);
            Managers.Scene.LoadScene(_sceneName);
        });
    }
}