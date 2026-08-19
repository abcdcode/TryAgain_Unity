using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


/*///////////////////////////////////////////
                InputManager
기능 : 연결된 액션의 값을 읽어 tInputInfo에 반영
 *///////////////////////////////////////////
public struct tInputInfo
{
    public Vector2 MoveDir;    // 이동 방향 (아날로그 크기 유지, 최대 1)
    public bool OnAttack; // 공격키 눌림 상태
    public bool OnReplay; // 리플레이 키 눌림 상태
    public bool OnESC; // ESC 키 눌림 상태
    public bool OnESCDown; // 리플레이 키 눌림 상태 - 1프레임
}

public class InputManager : SingletonBehavior<InputManager>
{

    [SerializeField] private List<InputActionReference> m_MoveAction;
    [SerializeField] private List<InputActionReference> m_AttackAction;
    [SerializeField] private List<InputActionReference> m_ReplayAction;
    [SerializeField] private List<InputActionReference> m_ESCAction;

    private tInputInfo m_tInputInfo;
    public tInputInfo InputInfo => m_tInputInfo;


    private void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);

        EnableAll(m_MoveAction);
        EnableAll(m_AttackAction);
        EnableAll(m_ReplayAction);
        EnableAll(m_ESCAction);
    }

    private void OnDestroy()
    {
        DisableAll(m_MoveAction);
        DisableAll(m_AttackAction);
        DisableAll(m_ReplayAction);
        DisableAll(m_ESCAction);
    }

    private void Update()
    {
        m_tInputInfo.MoveDir = ReadMove();
        m_tInputInfo.OnAttack = ReadIsPressed(m_AttackAction);
        m_tInputInfo.OnReplay = ReadIsPressed(m_ReplayAction);
        m_tInputInfo.OnESC = ReadIsPressed(m_ESCAction);
        m_tInputInfo.OnESCDown = ReadIsDown(m_ESCAction);
    }

    // 여러 이동 소스 중 크기가 가장 큰 입력을 선택 (덮어쓰기 버그 방지)
    private Vector2 ReadMove()
    {
        Vector2 Best = Vector2.zero;
        float BestSqr = 0f;

        for (int i = 0; i < m_MoveAction.Count; ++i)
        {
            Vector2 CurVector = m_MoveAction[i].action.ReadValue<Vector2>();
            float Sqr = CurVector.sqrMagnitude;
            if (Sqr > BestSqr)
            {
                BestSqr = Sqr;
                Best = CurVector;
            }
        }
        return Vector2.ClampMagnitude(Best, 1f);
    }

    private Vector2 ReadVector2(List<InputActionReference> list)
    {
        Vector2 Value = Vector2.zero;
        for (int i = 0; i < list.Count; ++i)
            Value = list[i].action.ReadValue<Vector2>();
        return Value;
    }

    private bool ReadIsPressed(List<InputActionReference> list)
    {
        for (int i = 0; i < list.Count; ++i)
            if (list[i].action.IsPressed())
                return true;
        return false;
    }
    private bool ReadIsDown(List<InputActionReference> list)
    {
        for (int i = 0; i < list.Count; ++i)
            if (list[i].action.WasPressedThisFrame())
                return true;
        return false;
    }
    private static void EnableAll(List<InputActionReference> list)
    {
        for (int i = 0; i < list.Count; ++i)
            list[i].action.Enable();
    }

    private static void DisableAll(List<InputActionReference> list)
    {
        for (int i = 0; i < list.Count; ++i)
            list[i].action.Disable();
    }

    private static void Subscribe(List<InputActionReference> list, Action<InputAction.CallbackContext> CallBack)
    {
        for (int i = 0; i < list.Count; ++i)
            list[i].action.performed += CallBack;
    }
}
