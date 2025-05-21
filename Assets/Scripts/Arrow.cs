using System.Collections;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody2D _rb;
    PlayerController _playerController;
    Bow _bow;

    [Header("화살 상태")]
    [field: SerializeField] public bool IsFly { get; private set; } = false;    // 화살이 날아가고 있는지 여부
    [field: SerializeField] public bool IsStop { get; private set; } = false;   // 화살이 멈췄는지 여부
    [field: SerializeField] public bool IsReturn { get; set; } = true;          // 활에 회수 되었는지 여부
    [SerializeField] bool _isHit = false;                                       // 화살이 꽂을 수 있는 곳에 닿았는지 여부

    [Header("적중")]
    [SerializeField] bool _isStick = false;
    [SerializeField] bool _isStickWindow = false;    // 판정 시간에 들어왔는지 여부
    [SerializeField] float _stickWindow = 0.2f;      // 윈도우 시간 (판정 시간)
    Coroutine _stickCoroutine;
    LayerMask _platformLayerMask;

    Vector2 _stickPosition;     // 꽂힌 위치 (화살 촉 끝과 닿은 지점)
    Vector2 _stickDirection;    // 꽂힌 방향 (화살 촉 -> 벽 방향)
    Transform _stickTransform;  // 꽂힌 Transform (화살 촉 끝과 닿은 지점)

    float _stickCheckOffset = 0.475f; // 꽂는 것을 검사할 위치 (화살 중심으로부터)

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerController = transform.root.GetComponent<PlayerController>();
        _bow = transform.GetComponentInParent<Bow>();

        _platformLayerMask = LayerMask.GetMask("Platform");
    }

    void Start()
    {
        Managers.Input.parryAction += StartStickCoroutine; // 패링 입력 시 패링 시도
        Init();
    }

    void Update()
    {
        Debug.DrawLine(transform.position + transform.right * _stickCheckOffset, transform.position + transform.right * _stickCheckOffset + transform.right * 0.1f, Color.blue);

        if (IsFly)
            CheckCollision();

        if (_rb.linearVelocity == Vector2.zero && IsStop == false)
        {
            IsStop = true;
            Debug.Log("화살 멈춤");

            if (_isStick)
            {
                _playerController.Warf(_stickPosition, _stickDirection, _stickTransform);
            }
            else
            {
                _playerController.Warf(transform.position, Vector2.up);
            }

            return;
        }
        
        if (IsFly == true && _isHit == false && IsStop == false)
            TrackMovement();
    }

    public void Init()
    {
        _isHit = false;
        IsFly = false;
        IsStop = true;
        IsReturn = true;
        _isStick = false;

        _rb.bodyType = RigidbodyType2D.Kinematic;

        // 로컬 Transform 초기화
        transform.SetParent(_bow.transform);
        transform.localPosition = Vector2.zero;
        transform.localRotation = Quaternion.identity;
    }

    public void Fly(Vector2 flyVector)
    {
        _isHit = false;
        IsFly = true;
        IsStop = false;
        IsReturn = false;

        transform.SetParent(null, true);

        _rb.bodyType = RigidbodyType2D.Dynamic;
        _rb.constraints &= ~RigidbodyConstraints2D.FreezeRotation;
        _rb.linearVelocity = Vector2.zero;      // 속도 초기화
        _rb.angularVelocity = 0f;               // 각속도 초기화
        _rb.AddForce(flyVector, ForceMode2D.Impulse);
    }

    // 화살이 날아가는 방향을 따라 회전
    void TrackMovement()
    {
        Vector2 direction = _rb.linearVelocity;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    #region 화살 박기
    public void StartStickCoroutine()
    {
        if (_isStick && !gameObject.activeSelf)
            return;

        if (_stickCoroutine != null)
            StopCoroutine(_stickCoroutine);
        _stickCoroutine = StartCoroutine(StickCoroutine());
    }

    // 판정
    IEnumerator StickCoroutine()
    {
        _isStickWindow = true;
        yield return new WaitForSeconds(_stickWindow);  // 판정 시간동안 대기
        _isStickWindow = false;
        _stickCoroutine = null;
    }

    public void TryStuck()
    {
        if (_isStickWindow)
        {
            Stick();
        }
    }

    // 꽂기
    public void Stick()
    {
        _isStick = true;
        _rb.bodyType = RigidbodyType2D.Kinematic;
        _rb.constraints |= RigidbodyConstraints2D.FreezeRotation;
        _rb.linearVelocity = Vector2.zero;
        _rb.angularVelocity = 0f;
    }
    #endregion

    void OnCollisionEnter2D(Collision2D collision)
    {
        _isHit = true;
        _rb.constraints &= ~RigidbodyConstraints2D.FreezeRotation;
    }

    void CheckCollision()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position + transform.right * 0.475f, 0.1f, _platformLayerMask);
        if (hit != null)
        {
            // Raycast로 노멀 벡터 추출
            RaycastHit2D rayHit = Physics2D.Raycast(transform.position + transform.right * 0.475f, transform.right, 0.1f, _platformLayerMask);
            if (rayHit.collider != null)
            {
                Vector2 collisionPoint = rayHit.point;
                Vector2 normal = rayHit.normal;

                Debug.Log("플랫폼에 닿음");
                Debug.Log("충돌 지점: " + collisionPoint);
                Debug.Log("노멀 벡터: " + normal);
                Debug.Log("꽂힌 Transform: " + rayHit.collider.transform);

                _stickPosition = collisionPoint;
                _stickDirection = -((Vector3)_stickPosition - transform.position + transform.right * _stickCheckOffset).normalized;
                _stickTransform = rayHit.collider.transform;

                // 충돌 지점을 가시화
                Debug.DrawLine(transform.position + transform.right, collisionPoint, Color.red, 1f);    // 화살 촉 너머 -> 충돌 지점(화살 촉 끝)
                Debug.DrawRay(collisionPoint, normal, Color.green, 1f);

                TryStuck();
            }
        }
    }

    public void OnDestroy()
    {
        Managers.Input.parryAction -= StartStickCoroutine;
        _stickCoroutine = null;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.right * 0.475f, 0.1f);
    }
}