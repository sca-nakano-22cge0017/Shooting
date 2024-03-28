using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �G�X�e�[�^�X
/// </summary>
[CreateAssetMenu(menuName = "Create CharacterStatus")]
public class StatuaData : ScriptableObject
{
    [SerializeField] float m_speed; //�ړ����x
    [SerializeField] int m_shots; //��x�Ɍ����̐�
    [SerializeField] float m_angle; //���˂���鋅�̊p�x
    [SerializeField] float m_coolTime; //���ɔ��˂����܂ł̎���
    [SerializeField] float m_correction; //���ˊp�x�␳

    public float Speed { get { return m_speed;} }
    public int Shots { get { return m_shots;} }
    public float Angle { get { return m_angle;} }
    public float CoolTime { get { return m_coolTime;} }
    public float Correction { get { return m_correction; } }
}
