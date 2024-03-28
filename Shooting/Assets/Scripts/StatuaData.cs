using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵ステータス
/// </summary>
[CreateAssetMenu(menuName = "Create CharacterStatus")]
public class StatuaData : ScriptableObject
{
    [SerializeField] float m_speed; //移動速度
    [SerializeField] int m_shots; //一度に撃つ球の数
    [SerializeField] float m_angle; //発射される球の角度
    [SerializeField] float m_coolTime; //次に発射されるまでの時間
    [SerializeField] float m_correction; //発射角度補正

    public float Speed { get { return m_speed;} }
    public int Shots { get { return m_shots;} }
    public float Angle { get { return m_angle;} }
    public float CoolTime { get { return m_coolTime;} }
    public float Correction { get { return m_correction; } }
}
