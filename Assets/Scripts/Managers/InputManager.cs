using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager
{
    public InputSystemActions InputSystemActions => _inputSystemActions;
    InputSystemActions _inputSystemActions;

    #region 입력 변수
    public Vector2 MoveInput { get; private set; }
    public Vector2 MouseWorldPos { get; private set; }  // 마우스 입력(월드 좌표)
    public bool IsPressAttack { get; private set; }     // 공격 입력 여부
    public bool IsPressParry { get; private set; }      // 패링 입력 여부
    public bool IsPressDash { get; private set; }       // 대시 입력 여부
    #endregion

    #region 액션
    public Action attackAction;                   // 공격
    public Action parryAction;                    // 패링
    public Action<Vector2> dashAction;            // 대시
    public Action<bool> rotateMagicAction;        // 마법 보따리 회전
    public Action reloadMagicAction;              // 장전
    #endregion

    public void Init()
    {
        _inputSystemActions = new InputSystemActions();
        SetInGame();
    }

    public void SetInGame()
    {
        _inputSystemActions.Player.Enable();

        _inputSystemActions.Player.Move.performed += OnMove;
        _inputSystemActions.Player.Move.canceled += OnMove;

        _inputSystemActions.Player.MousePos.performed += OnMousePos;

        _inputSystemActions.Player.Dash.performed += OnDash;

        _inputSystemActions.Player.Attack.performed += OnAttack;
        _inputSystemActions.Player.Attack.canceled += OnAttack;
        _inputSystemActions.Player.Parry.performed += OnParry;
        _inputSystemActions.Player.Parry.canceled += OnParry;
        _inputSystemActions.Player.Release.performed += OnRelease;

        _inputSystemActions.Player.Reload.performed += OnReload;

        _inputSystemActions.Player.RotateMagicBundle.performed += OnRotateMagicBundle;
    }

    void OnMove(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();
        //Debug.Log(MoveInput);
        //Debug.Log(MoveInput);
    }

    void OnMousePos(InputAction.CallbackContext context)
    {
        Vector2 mouseInput = context.ReadValue<Vector2>();
        MouseWorldPos = Camera.main.ScreenToWorldPoint(mouseInput);
        //Debug.Log(MouseWorldPos);
    }

    void OnDash(InputAction.CallbackContext context)
    {
        dashAction?.Invoke(MoveInput);
        IsPressDash = context.ReadValueAsButton();
        //Debug.LogWarning("IsPressDash: " + IsPressDash);
    }

    void OnAttack(InputAction.CallbackContext context)
    {
        //Debug.Log("Attack");
        if(context.performed)
            attackAction?.Invoke();
        IsPressAttack = context.ReadValueAsButton();
    }

    void OnParry(InputAction.CallbackContext context)
    {
        IsPressParry = context.ReadValueAsButton();
        if (context.performed)
        {
            parryAction?.Invoke();
        }
        //Debug.Log("IsPressParry: " + IsPressParry);
    }

    void OnRelease(InputAction.CallbackContext context)
    {
        Debug.Log("마법 방출");
    }

    void OnReload(InputAction.CallbackContext context)
    {
        Debug.Log("마법 장전");
        reloadMagicAction?.Invoke();
    }

    void OnRotateMagicBundle(InputAction.CallbackContext context)
    {
        float x = context.ReadValue<Vector2>().x;
        bool isCCW = x < 0;
        rotateMagicAction?.Invoke(isCCW);
    }

    public void CancelAction()
    {
        attackAction = null;
        parryAction = null;
        dashAction = null;
        rotateMagicAction = null;
        reloadMagicAction = null;
    }

    public void Clear()
    {
        if (_inputSystemActions != null)
        {
            CancelAction();
            _inputSystemActions.Player.Disable();
            _inputSystemActions = null;
        }
    }
}