using System;
using UnityEngine;
public class EnemyAI_StateOnly : EnemyAIDataSO
{
    [SerializeField]private string m_script;
    public override EnemyAIState BuildAIState(Enemy enemy)
    {
        var t = Type.GetType(m_script);
        return (EnemyAIState)Activator.CreateInstance(t);
    }
}