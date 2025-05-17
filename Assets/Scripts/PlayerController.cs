using System.Collections;
using UnityEngine;
using static UnityEngine.LightAnchor;

public class PlayerController : MonoBehaviour
{
    Animator _anim;
    [SerializeField] Vector3 _warfPosition; // 워프 위치
    [SerializeField] Vector2 _upDirection;
    NewArrow _arrow; // 화살

    void Awake()
    {
        _anim = GetComponent<Animator>();
        _anim.SetTrigger("SpawnTrigger");
        _arrow = transform.Find("Bow").GetComponentInChildren<NewArrow>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            Spawn();
        else if (Input.GetKeyDown(KeyCode.R))
            Dissolve();
    }

    public void Spawn()
    {
        _anim.SetTrigger("SpawnTrigger");
    }

    public void Dissolve()
    {
        _anim.SetTrigger("DissolveTrigger");
    }

    public void Warf(Vector3 warfPosition, Vector2 upDirection)
    {
        _warfPosition = warfPosition;
        _upDirection = upDirection;
        StartCoroutine(WarfSequence());
        Debug.Log("워프1");
    }

    IEnumerator WarfSequence()
    {

        // 애니메이션 트리거 설정
        _anim.SetTrigger("DissolveTrigger");

        // 현재 애니메이션 상태를 가져옴
        AnimatorStateInfo stateInfo = _anim.GetCurrentAnimatorStateInfo(0);

        // 애니메이션이 재생될 때까지 대기
        yield return new WaitForSeconds(stateInfo.length);

        //// 애니메이션이 끝날 때까지 대기
        //yield return new WaitUntil(() => _anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);

        transform.position = _warfPosition;
        transform.up = _upDirection;
        Debug.Log("워프2");
        _arrow.Init();
    }
}