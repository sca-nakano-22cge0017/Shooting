using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create CharacterStatus")]
public class StatuaData : ScriptableObject
{
    [SerializeField] float m_speed; //ˆÚ“®‘¬“x
    [SerializeField] int m_shots; //ˆê“x‚ÉŒ‚‚Â‹…‚Ì”
    [SerializeField] float m_angle; //”­Ë‚³‚ê‚é‹…‚ÌŠp“x
    [SerializeField] float m_coolTime; //Ÿ‚É”­Ë‚³‚ê‚é‚Ü‚Å‚ÌŠÔ

    public float Speed { get { return m_speed;} }
    public int Shots { get { return m_shots;} }
    public float Angle { get { return m_angle;} }
    public float CoolTime { get { return m_coolTime;} }
}
