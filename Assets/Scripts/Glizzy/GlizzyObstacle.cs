using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlizzyObstacle : MonoBehaviour
{
    public enum RewardType
    {
        CrewMember,
        BulletSpeed
    }

    public int Score;
    public TMPro.TMP_Text ScoreText;
    public RewardType Reward;
    bool GotReward = false;

    void Update()
    {
        ScoreText.text = Score.ToString();
        if (Score <= 0 && !GotReward)
        {
            GetReward();
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Pawn")
        {
            collision.gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag == "Player")
        {
            //Reset
            GlizzyGameManager.Instance.StartGame();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Score--;
    }

    void GetReward()
    {
        GotReward = true;
        switch (Reward)
        {
            case RewardType.CrewMember:
                GlizzyGameManager.Instance.Player.AddCrewMember();
                break;
            case RewardType.BulletSpeed:
                GlizzyGameManager.Instance.Player.IncrementGunSpeed();
                break;
        }
    }
}
