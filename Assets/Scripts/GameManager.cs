using System.Collections.Generic;
using TMPro;
using UnityEngine;
/// <summary>
/// 게임 업데이트 총괄 부서
/// </summary>
public class GameManager : SingletonBehavior<GameManager>
{
    void Start()
    {
        ReplayHamburger.Instance.Reset();
        m_ContainerList = new List<IReplayable>();
        foreach(var c in Containers)
        {
            if(c is IReplayable i)
            {
                m_ContainerList.Add(i);
            }
        }
        CurPlayer = Instantiate(m_playerPrefab).GetComponent<Player>();
        CurPlayer.Position = new Vector2(0,0);
        CurFrame = 0;
        State = GameManagerState.Playing;
    }
    /// <summary>
    /// 인풋 받아서 플레이 상태 결정하는 곳
    /// </summary>
    void StateCheck()
    {
        var inputinfo = InputManager.Instance.InputInfo;
        if(inputinfo.OnESCDown)
        {
            State = State == GameManagerState.Pause ? GameManagerState.Playing : GameManagerState.Pause;
        }
        if(State == GameManagerState.Pause) return;
        if(inputinfo.OnReplay)
        {
            State = GameManagerState.Replay;
        }
        else
        {
            State = GameManagerState.Playing;
        }
    }
    void Update()
    {
        StateCheck();
        if(State == GameManagerState.Playing)
        {
            GameUpdate();
        }
        if(State == GameManagerState.Pause)
        {
            
        }
        if(State == GameManagerState.Replay)
        {
            Load();
        }
        m_DebugText.text = $"Time : {CurFrame}";
    }
    void GameUpdate()
    {
        CurPlayer.GameUpdate();
        foreach(var c in m_ContainerList)
        {
            c.GameUpdate();
        }
        Save();
        CurFrame += 1;
    }
    void Save()
    {
        SaveData data = new SaveData();
        CurPlayer.Save(data);
        foreach(var c in m_ContainerList)
        {
            c.Save(data);
        }
        data.Save();
        ReplayHamburger.Instance.Save(CurFrame,data);
    }
    void Load()
    {
        if(CurFrame > 0) 
        {
            CurFrame -= 1;
        } else
        {
            return;
        }
        var data = ReplayHamburger.Instance.Load(CurFrame);
        CurPlayer.Load(data);
        foreach(var c in m_ContainerList)
        {
            c.Load(data);
        }
    }
    public GameManagerState State{get;set;}
    public Player CurPlayer{get;private set;}
    public int CurFrame{get;private set;}
    private List<IReplayable> m_ContainerList;
    [SerializeField]private GameObject m_playerPrefab;
    [SerializeField]private List<MonoBehaviour> Containers;
    [SerializeField]private TextMeshProUGUI m_DebugText;
}
public enum GameManagerState
{
    Playing,
    Pause,
    Replay
}
