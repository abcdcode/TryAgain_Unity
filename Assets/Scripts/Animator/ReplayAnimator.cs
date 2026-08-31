using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(ReplayMono))]
public class ReplayAnimator :GameMono,IReplayable
{
    public void Awake()
    {
        m_parent = GetComponent<ReplayMono>();
        var spr = GetComponent<SpriteRenderer>();
        if(spr == null) return;
        foreach(var c in m_clips)
        {
            foreach(var a in c.doAction)
            {
                if(a.target == null)
                {
                    a.target = GetComponent<SpriteRenderer>();
                }
            }
        }
    }
    public override void GameUpdate()
    {
        base.GameUpdate();
        if(m_curClip == null) return;
        m_time += Time.deltaTime;
        for(int i = curAction+1; i < m_curClip.doAction.Count; i++)
        {
            var a = m_curClip.doAction[i];
            if(m_time >= a.time)
            {
                a.target.sprite = a.sprite;
                curAction += 1;
            }
            else
            {
                break;
            }
        }
        if(m_time >= m_curClip.m_endTime)
        {
            if(m_isLoop)
            {
                m_time = 0;
                curAction = -1;
            }
            else
            {
                m_curClip = null;
            }
        }
    }
    public void Load(SaveData data)
    {
        bool isclip = data;
        if(!isclip) return;
        m_time = data;
        m_isLoop = data;
        int curclip = data;
        if(m_curClip == null || m_curClip.m_clipId != curclip)
        {
            m_curClip = m_clips.Find(x => x.m_clipId == curclip);
        }
        int curA = data;
        if(curA != curAction && curA != -1)
        {
            curAction = curA;
            var a = m_curClip.doAction[curA];
            a.target.sprite = a.sprite;
        }
    }

    public void Save(SaveData data)
    {
        data.Write(m_curClip != null);
        if(m_curClip == null) return;
        data.Write(m_time);
        data.Write(m_isLoop);
        data.Write(m_curClip.m_clipId);
        data.Write(curAction);
    }

    public void SetAnim(int id, bool isLoop)
    {
        m_curClip = m_clips.Find(x => x.m_clipId == id);
        m_isLoop = isLoop;
        m_time = 0;
        curAction = -1;
    }
    protected int curAction = -1;
    protected bool m_isLoop = true;
    protected ReplayAnimClip m_curClip;
    [SerializeField]protected List<ReplayAnimClip> m_clips;
    [SerializeField]protected float m_time;
    [SerializeField]protected ReplayMono m_parent;
}
[Serializable]
public class ReplayAnimClip
{
    public int m_clipId;
    public float m_endTime;
    public List<AnimAction> doAction;
}
[Serializable]
public class AnimAction
{
    public float time;
    public Sprite sprite;
    public SpriteRenderer target;
}