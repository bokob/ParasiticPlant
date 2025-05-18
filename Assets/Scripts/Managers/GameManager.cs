using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager _instance;
    public static GameManager Instance;

    public PlayerController PlayerController { get; private set; }
    public Bow Bow { get; private set; }
    public Arrow Arrow { get; private set; }

    void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
    }

    void Start()
    {
        PlayerController = Managers.Resource.Instantiate("PlayerPrefab").GetComponent<PlayerController>();
        Bow = PlayerController.GetComponentInChildren<Bow>();
        Arrow = Bow.GetComponentInChildren<Arrow>();
    }
}