using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ƒŠƒUƒ‹ƒg‰æ–Ê
/// </summary>
public class ResultController : MonoBehaviour
{
    [SerializeField] Text HP;
    int hp;
    [SerializeField] Text KillScore;
    int killScore;
    [SerializeField] Text HPBonus;
    int hpBonus;
    [SerializeField] Text KillBonus;
    int killBonus;
    [SerializeField] Text Score;
    int score;

    [SerializeField] int hpBonusNum = 100;
    [SerializeField] int killBonusNum = 200;

    void Start()
    {
        hp = GameManager.hp;
        killScore = GameManager.killScore;
        hpBonus = hp * hpBonusNum;
        killBonus = killScore * killBonusNum;
        score = hpBonus + killBonus;
    }

    void Update()
    {
        HP.text = hp + "";
        KillScore.text = killScore + "";
        HPBonus.text = hpBonus + "";
        KillBonus.text = killBonus + "";
        Score.text = score + "";
    }
}
