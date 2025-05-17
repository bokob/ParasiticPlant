using UnityEngine;
using UnityEngine.SceneManagement;
using static Define;

public class SceneManagerEx
{
    // 현재 씬
    public BaseScene CurrentScene { get { return GameObject.FindAnyObjectByType<BaseScene>(); } }

    // 씬 로드(개발 마무리 단계에서 사용할 예정)
    public void LoadScene(SceneType sceneType)
    {
        //Managers.Clear(); // 매니저 정리
        //SceneManager.LoadScene((int)sceneType);

        //나중에 씬 정해지면 이 코드로 사용 예정
        string sceneName = sceneType.ToString();
        SceneManager.LoadScene(sceneName);
    }

    // 개발 과정에서 사용할 로드 씬
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // 씬 정리
    public void Clear()
    {
        CurrentScene.Clear();
    }
}