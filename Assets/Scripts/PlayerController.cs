using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator _anim;
    Arrow _arrow;   // 화살
    [SerializeField] Vector3 _warfPosition;       // 워프 위치
    [SerializeField] Vector2 _upDirectionOfWarf;  // 워프 후 방향

    void Awake()
    {
        _anim = GetComponent<Animator>();
        _arrow = transform.Find("Bow").GetComponentInChildren<Arrow>();
    }

    void Start()
    {
        PlaySpawnAnimation();
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.E))
        //    PlaySpawnAnimation();
        //else if (Input.GetKeyDown(KeyCode.R))
        //    PlayDissolveAnimation();
    }

    public void PlaySpawnAnimation()
    {
        _anim.SetTrigger("SpawnTrigger");
    }

    public void PlayDissolveAnimation()
    {
        _anim.SetTrigger("DissolveTrigger");
    }

    public void Warf(Vector3 warfPosition, Vector2 upDirectionOfWarf, Transform warfTargetTransform = null)
    {
        //transform.SetParent(warfTargetTransform, true);
        _warfPosition = warfPosition;
        _upDirectionOfWarf = upDirectionOfWarf;
        StartCoroutine(WarfSequence());
        Debug.Log("워프1");
    }

    IEnumerator WarfSequence()
    {
        PlayDissolveAnimation();

        // 현재 애니메이션 재생하는 동안 대기
        AnimatorStateInfo stateInfo = _anim.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(stateInfo.length);

        //// 애니메이션이 끝날 때까지 대기
        //yield return new WaitUntil(() => _anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);

        transform.position = _warfPosition;
        transform.up = _upDirectionOfWarf;
        Debug.Log("워프2");
        _arrow.Init();
    }
}