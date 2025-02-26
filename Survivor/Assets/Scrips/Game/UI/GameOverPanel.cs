using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverPanel : MonoBehaviour
{
    public TextMeshProUGUI bestTime;
    public TextMeshProUGUI lastTime;
    public TextMeshProUGUI bestKill;
    public TextMeshProUGUI lastKill;
    public TextMeshProUGUI reward;
    // Start is called before the first frame update
    public void OnEnable()
    {
        SetBestTimeText();
        SetLastTimeText();
        SetBestKillText();
        UpdateRewardText();
        lastKill.text = LevelManager.Instance.KillCount.ToString();
    }
    public void SetBestTimeText()
    {
        if (LevelManager.Instance.Timer >= PlayerPrefs.GetFloat("BestTime"))
        {
            PlayerPrefs.SetFloat("BestTime", LevelManager.Instance.Timer);
        }
        int minutes = Mathf.FloorToInt(PlayerPrefs.GetFloat("BestTime") / 60); // 计算分钟
        int seconds = Mathf.FloorToInt(PlayerPrefs.GetFloat("BestTime") % 60); // 计算秒数
        // 更新UI Text显示
        bestTime.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    public void SetLastTimeText()
    {
        int minutes = Mathf.FloorToInt(LevelManager.Instance.Timer / 60); // 计算分钟
        int seconds = Mathf.FloorToInt(LevelManager.Instance.Timer % 60); // 计算秒数
        lastTime.text= string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    public void SetBestKillText()
    {
        if(LevelManager.Instance.KillCount>=PlayerPrefs.GetInt("BestKill"))
        {
            PlayerPrefs.SetInt("BestKill", LevelManager.Instance.KillCount);
        }
        bestKill.text= PlayerPrefs.GetInt("BestKill").ToString();
    }
    void UpdateRewardText()
    {
        int rewardCoins;
        rewardCoins = 10 + LevelManager.Instance.KillCount / 100;
        CoinManager.AddCoins(rewardCoins);
        reward.text = "获得金币×" + rewardCoins.ToString();

    }
}
