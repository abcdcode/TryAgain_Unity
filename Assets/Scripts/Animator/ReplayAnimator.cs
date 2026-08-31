using System;
using System.Collections.Generic;
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
        m_time += Time.deltaTime;

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
    }

    public void Save(SaveData data)
    {
        data.Write(m_curClip != null);
        if(m_curClip == null) return;
        data.Write(m_time);
        data.Write(m_isLoop);
        data.Write(m_curClip.m_clipId);
    }

    public void SetAnim(int id, bool isLoop)
    {
        m_curClip = m_clips.Find(x => x.m_clipId == id);
        m_isLoop = isLoop;
        m_time = 0;
    }

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