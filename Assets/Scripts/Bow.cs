using UnityEngine;

public class Bow : MonoBehaviour
{
    [Header("회전")]
    float _rotationRadius = 1f;
    Vector2 _prevMouseWorldPos;
    [SerializeField] Transform _pivot;

    [Header("발사")]
    [SerializeField] float _launchForce;
    [SerializeField] float _maxLaunchForce;
    [SerializeField] Vector2 _direction;
    [SerializeField] Arrow _arrow;

    void Start()
    {
        _pivot = transform.root.Find("Visual");
        _arrow = GetComponentInChildren<Arrow>();
    }

    void Update()
    {
        // 회전
        if (_prevMouseWorldPos != Managers.Input.MouseWorldPos)
        {
            RotateAroundPivot();
        }
        FaceMouse();

        if(_arrow.IsReturn && Input.GetKey(KeyCode.Space))
        {
            _launchForce += Time.deltaTime * 10f;
        }

        if(_arrow.IsReturn && Input.GetKeyUp(KeyCode.Space))
        {
            Shoot();
        }
    }

    #region 활
    void RotateAroundPivot()
    {
        // 피봇 중심으로 회전
        _prevMouseWorldPos = Managers.Input.MouseWorldPos;
        Vector2 mouseWorldPos = Managers.Input.MouseWorldPos;
        _direction = (mouseWorldPos - (Vector2)_pivot.position).normalized;
        transform.position = (Vector2)_pivot.position + _direction * _rotationRadius;

        // 마우스 방향으로 회전
        float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    // 마우스 방향 바라보기
    void FaceMouse()
    {
        transform.right = _direction;
    }
    #endregion

    void Shoot()
    {
        _launchForce = Mathf.Clamp(_launchForce, 0, _maxLaunchForce);
        _arrow.IsReturn = false;
        _arrow.Fly(transform.right * _launchForce);
        _launchForce = 0;
    }
}