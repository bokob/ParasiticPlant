using TMPro;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers _instance;
    public static Managers Instance { get { Init(); return _instance; } }

    SoundManager _sound = new SoundManager();
    ResourceManager _resource = new ResourceManager();
    SceneManagerEx _scene = new SceneManagerEx();
    InputManager _input = new InputManager();

    public static SoundManager Sound => Instance._sound;
    public static ResourceManager Resource => Instance._resource;
    public static SceneManagerEx Scene => Instance._scene;
    public static InputManager Input => Instance._input;

    void Start()
    {
        Init();
    }

    // 매니저 인스턴스 없어도 생성
    static void Init()
    {
        if (_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }
            DontDestroyOnLoad(go);
            _instance = go.GetComponent<Managers>();

            // 필요한 매니저 초기화
            _instance._sound.Init();
            _instance._input.Init();
        }
    }

    // 게임 종료 시 매니저 정리
    public static void Clear()
    {
        Scene.Clear();
        Sound.Clear();
        Input.Clear();
    }

    void OnDisable()
    {
        Debug.LogWarning("Managers 파괴");
        Clear();
        _instance = null;
    }
}