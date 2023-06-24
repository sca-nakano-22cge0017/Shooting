using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    void Start()
    {
        hp = GameManager.hp;
        killScore = GameManager.killScore;
        hpBonus = hp * 100;
        killBonus = killScore * 200;
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
