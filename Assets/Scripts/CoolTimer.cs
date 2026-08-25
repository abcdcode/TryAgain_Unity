using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 쿨타임 관리자
/// </summary>
public class CoolTimer : IReplayable
{
    public CoolTimer(ICoolOwner o)
    {
        owner =o;
        coolDic = new Dictionary<int, CoolInfo>();
    }
    /// <summary>
    /// 쿨타임 추가. 이미 같은 id를 추가했었다면 덮어씀
    /// </summary>
    /// <param name="id">추가할 쿨타임의 id</param>
    /// <param name="cool">쿨타임. ms단위 ex) 1000 = 1초</param>
    /// <param name="startCool">시작 쿨타임</param>
    /// <param name="isOnce">한번 호출 후 사라질 지 여부. 단순 쿨타임 체크용이라면 false 권장</param>
    public void SetCool(int id, float cool, float startCool, bool isOnce)
    {
        CoolInfo info = new (startCool,cool,isOnce);
        coolDic[id] = info;
    }
    /// <summary>
    /// 쿨타임 삭제
    /// </summary>
    /// <param name="id"></param>
    public void DeleteCool(int id)
    {
        if(coolDic.ContainsKey(id))
        {
            coolDic.Remove(id);
        }
    }
    /// <summary>
    /// 지정 쿨타임 0으로 초기화
    /// </summary>
    /// <param name="id"></param>
    /// <param name="newCool">해당 쿨타임 정보 새로운 쿨타임 지정</param>
    public void RefreshCool(int id, float newCool = -1)
    {
        if(coolDic.ContainsKey(id))
        {
            coolDic[id].cur = 0;
            if(newCool > 0)
            {
                coolDic[id].cool = newCool;
            }
        }
    }
    /// <summary>
    /// 쿨타임 정보 가져오기
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public CoolInfo GetCool(int id)
    {
        if(coolDic.ContainsKey(id)) return coolDic[id];
        return null;
    }
    /// <summary>
    /// 쿨타임이 다 되었는지 체크하기
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool IsCoolComp(int id)
    {
        if(!coolDic.ContainsKey(id)) return true;
        return coolDic[id].cur >= coolDic[id].cool;
    }

    public void Save(SaveData data)
    {
        data.Write(coolDic.Count);
        if(coolDic.Count > 0)
        {
            foreach(var pair in coolDic)
            {
                data.Write(pair.Key);
                pair.Value.Save(data);
            }
        }
    }
    private List<int> lkeys= new List<int>();
    public void Load(SaveData data)
    {
        int count = data;
        lkeys.Clear();
        for(int i = 0 ; i < count; i++)
        {
            int key = data;
            lkeys.Add(key);
            if(!coolDic.ContainsKey(key))
            {
                CoolInfo info = new CoolInfo(0,0,false);
                info.Load(data);
                coolDic[key] = info;
            }
            else
            {
                coolDic[key].Load(data);
            }
        }
        foreach(var key in coolDic.Keys)
        {
            if(!lkeys.Contains(key))
            {
                coolDic.Remove(key);
            }
        }
    }
    public void Clear()
    {
        coolDic.Clear();
    }
    public void GameUpdate()
    {
        foreach(var pair in coolDic.ToArray())
        {
            var ci = pair.Value;
            ci.cur += Time.deltaTime;
            if(ci.cur >= ci.cool)
            {
                if(ci.IsOnce) DeleteCool(pair.Key);
                owner.ExecuteCool(pair.Key);
            }
        }
    }
    public void LateGameUpdate()
    {
        
    }
    public ICoolOwner owner;
    public Dictionary<int,CoolInfo> coolDic;
    public class CoolInfo : IReplayable
    {
        public void Save(SaveData data)
        {
            data.Write(cur);
            data.Write(cool);
            data.Write(IsOnce);
        }

        public void Load(SaveData data)
        {
            cur = data;
            cool = data;
            IsOnce = data;
        }
        public CoolInfo(float c, float co, bool io)
        {
            cur = c;
            cool = co;
            IsOnce = io;
        }
        public float cur;
        public float cool;
        public bool IsOnce;



        public void GameUpdate()
        {
        }
        public void LateGameUpdate()
        {
            
        }
    }
}