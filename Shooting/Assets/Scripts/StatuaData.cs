using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create CharacterStatus")]
public class StatuaData : ScriptableObject
{
    [SerializeField] float m_speed; //�ړ����x
    [SerializeField] int m_shots; //��x�Ɍ����̐�
    [SerializeField] float m_angle; //���˂���鋅�̊p�x
    [SerializeField] float m_coolTime; //���ɔ��˂����܂ł̎���

    public float Speed { get { return m_speed;} }
    public int Shots { get { return m_shots;} }
    public float Angle { get { return m_angle;} }
    public float CoolTime { get { return m_coolTime;} }
}
