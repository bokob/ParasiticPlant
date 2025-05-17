using UnityEngine;
using UnityEngine.UI;

public class GameQuitBtn : MonoBehaviour
{
    Button _quitBtn;
    void Start()
    {
        _quitBtn = GetComponent<Button>();
        _quitBtn.onClick.AddListener(OnQuitGame);
    }

    void OnQuitGame()
    {
        // 빌드할 때는 Editor 부분 지우기
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
                    Application.Quit();
#endif
    }
}