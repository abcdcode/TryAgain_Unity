using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
/// <summary>
/// 게임 업데이트 총괄 부서
/// </summary>
public class GameManager : SingletonBehavior<GameManager>
{
    void Start()
    {
        Application.targetFrameRate = 120;
        ReplayHamburger.Instance.Reset();
        m_ContainerList = GetComponents<IReplayable>().ToList();
        CurPlayer = Instantiate(m_playerPrefab).GetComponent<Player>();
        CurPlayer.Position = new Vector2(0, 0);
        /*
        for(int i = 0 ; i < 1000; i++)
        {
            var e = EnemyContainer.Instance.Create("TestEnemy",true);
            e.Position = new Vector2(600,0);
            e.SetSize(new Vector2(100,100));
        }
        */
        SeedManager.Instance.InitSeed(Random.Range(0, 10000000));
        WaveManager.Instance.SetTestWave();
        CurFrame = 0;
        State = GameManagerState.Playing;
        pDeadTime = 2;
        Inventory.Instance.Create("SpeedUp",true);
    }
    /// <summary>
    /// 인풋 받아서 플레이 상태 결정하는 곳
    /// </summary>
    void StateCheck()
    {
        if(pDeadTime <= 0)
        {
            State = GameManagerState.Pause;
            return;
        }
        if(CurPlayer.IsDead &&  State == GameManagerState.Playing)
        {
            pDeadTime -= Time.deltaTime;
            if(pDeadTime <= 0)
            {
                MainUIManager.Instance.OpenGameOver();
                State = GameManagerState.Pause;
                return;
            }
        }
        else
        {
            pDeadTime = 2;
        }
        var inputinfo = InputManager.Instance.InputInfo;
        if (inputinfo.OnESCDown)
        {
            State = State == GameManagerState.Pause ? GameManagerState.Playing : GameManagerState.Pause;
        }
        if (State == GameManagerState.Pause) return;
        if (inputinfo.OnReplay)
        {
            State = GameManagerState.Replay;
        }
        else
        {
            State = GameManagerState.Playing;
        }
    }
    private float pDeadTime;
    void Update()
    {
        var v = System.DateTime.Now.Millisecond;
        StateCheck();
        if (State == GameManagerState.Playing)
        {
            GameUpdate();
        }
        if (State == GameManagerState.Pause)
        {
            
        }
        if (State == GameManagerState.Replay)
        {
            Load();
        }
        var v2 = System.DateTime.Now.Millisecond;
        if (IsDebug)
        {
            m_DebugText.text = $"Time : {CurFrame} , GameManager Frame Time : {v2 - v} ms , Tick1 {tick1}, Tick2 {tick2}";
        } else
        {
            m_DebugText.text = "";
        }
    }
    private long tick1;
    private long tick2;
    void GameUpdate()
    {
        Stopwatch sw = Stopwatch.StartNew();
        CurPlayer.GameUpdate();

        foreach (var c in m_ContainerList)
        {
            c.GameUpdate();
        }
        sw.Stop();
        tick1 = sw.ElapsedTicks;
        sw.Reset();
        sw.Start();
        Save();
        sw.Stop();
        tick2 = sw.ElapsedTicks;
        CurPlayer.LateGameUpdate();
        foreach (var c in m_ContainerList)
        {
            c.LateGameUpdate();
        }
        CurFrame += 1;
    }
    void Save()
    {

        SaveData data = new SaveData();
        CurPlayer.Save(data);
        foreach (var c in m_ContainerList)
        {
            c.Save(data);
        }
        data.Save();
        ReplayHamburger.Instance.Save(CurFrame, data);

    }
    void Load()
    {
        if (CurFrame > 0)
        {
            CurFrame -= 1;
        }
        else
        {
            return;
        }
        var data = ReplayHamburger.Instance.Load(CurFrame);
        CurPlayer.Load(data);
        foreach (var c in m_ContainerList)
        {
            c.Load(data);
        }
        data.Dispose();
    }
    public GameManagerState State { get; set; }
    public Player CurPlayer { get; private set; }
    public int CurFrame { get; private set; }
    private List<IReplayable> m_ContainerList;
    [SerializeField] private GameObject m_playerPrefab;
    [SerializeField] private TextMeshProUGUI m_DebugText;
    [SerializeField] public bool IsDebug;
    public const int ScreenX = 1920;
    public const int ScreenY = 1080;
}
public enum GameManagerState
{
    Playing,
    Pause,
    Replay
}
