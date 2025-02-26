using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using TTSDK.UNBridgeLib.LitJson;
using TTSDK;
using StarkSDKSpace;

public class MainMenu : MonoBehaviour
{
    //玩家名字
    public TextMeshProUGUI PlayerName;
    //金币
    public TextMeshProUGUI CoinText;
    //体力
    public int MaxStamina;
    public int CurrentStamina;
    public TextMeshProUGUI StaminaNumberText;
    public TextMeshProUGUI timerText;
    public int staminaIncreaseInterval = 300; // 体力增加的间隔时间（秒），默认5分钟
    private float timeRemaining; // 剩余的倒计时时间

    public GameObject RewardPanel;
    public GameObject ShopPanel;
    public string clickid;
    private StarkAdManager starkAdManager;
    public void Start()
    {
        if(PlayerPrefs.HasKey("NameNumber"))
        {
            PlayerName.text = "玩家 " + PlayerPrefs.GetInt("NameNumber");
            StaminaNumberText.text=PlayerPrefs.GetInt("Stamina").ToString()+"/"+MaxStamina.ToString();
        }
        else
        {
            int index = Random.Range(57866, 69880);
            PlayerPrefs.SetInt("NameNumber", index);
            PlayerName.text = "玩家 " + index;
            CurrentStamina = MaxStamina;
            PlayerPrefs.SetInt("Stamina", CurrentStamina);
            StaminaNumberText.text = PlayerPrefs.GetInt("Stamina").ToString() + "/" + MaxStamina.ToString();
        }
        UpdateCoinText();
        UpdateStaminaText();
        timeRemaining = staminaIncreaseInterval;
    }
    public void UpdateCoinText()
    {
        CoinText.text=PlayerPrefs.GetInt("Coin").ToString();
    }
    public void UpdateStaminaText()
    {
        StaminaNumberText.text = PlayerPrefs.GetInt("Stamina").ToString() + "/" + MaxStamina.ToString();
    }
    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60); // 计算分钟
        int seconds = Mathf.FloorToInt(timeRemaining % 60); // 计算秒数

        // 更新UI Text显示
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    public void ShowShop()
    {
        ShopPanel.SetActive(true);
    }
    public void LoadGame()
    {
        if(StaminaManager.GetStamina()>=5)
        {
            StaminaManager.ReduceStamina(5);
            UpdateStaminaText();
            SceneManager.LoadScene("City");
        }
        else
        {
            RewardPanel.SetActive(true);
        }
    }
    public void ShowRewardPanel()
    {
        RewardPanel.SetActive(true);
    }
    public void HideRewardPanel()
    {
        RewardPanel.SetActive(false);
    }
    public void Reward()
    {
        ShowVideoAd("192if3b93qo6991ed0",
            (bol) => {
                if (bol)
                {

                    CoinManager.AddCoins(100);
                    UpdateCoinText();
                    StaminaManager.AddStamina(10);
                    UpdateStaminaText();
                    RewardPanel.SetActive(false);


                    clickid = "";
                    getClickid();
                    apiSend("game_addiction", clickid);
                    apiSend("lt_roi", clickid);


                }
                else
                {
                    StarkSDKSpace.AndroidUIManager.ShowToast("观看完整视频才能获取奖励哦！");
                }
            },
            (it, str) => {
                Debug.LogError("Error->" + str);
                //AndroidUIManager.ShowToast("广告加载异常，请重新看广告！");
            });
    }
    private void Update()
    {
        UpdateCoinText();
        CurrentStamina=StaminaManager.GetStamina();
        if (CurrentStamina >= MaxStamina)
        {
            timerText.gameObject.SetActive(false);
            return;
        }
        else
        {
            timerText.gameObject.SetActive(true);
        }
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime; // 减少倒计时时间
            UpdateTimerDisplay(); // 更新UI显示
        }
        else
        {
            // 倒计时结束，增加体力
            StaminaManager.AddStamina(5);
            UpdateStaminaText();
            Debug.Log("体力增加！当前体力: " + CurrentStamina);

            // 重置倒计时
            timeRemaining = staminaIncreaseInterval;
        }
    }
    public void getClickid()
    {
        var launchOpt = StarkSDK.API.GetLaunchOptionsSync();
        if (launchOpt.Query != null)
        {
            foreach (KeyValuePair<string, string> kv in launchOpt.Query)
                if (kv.Value != null)
                {
                    Debug.Log(kv.Key + "<-参数-> " + kv.Value);
                    if (kv.Key.ToString() == "clickid")
                    {
                        clickid = kv.Value.ToString();
                    }
                }
                else
                {
                    Debug.Log(kv.Key + "<-参数-> " + "null ");
                }
        }
    }

    public void apiSend(string eventname, string clickid)
    {
        TTRequest.InnerOptions options = new TTRequest.InnerOptions();
        options.Header["content-type"] = "application/json";
        options.Method = "POST";

        JsonData data1 = new JsonData();

        data1["event_type"] = eventname;
        data1["context"] = new JsonData();
        data1["context"]["ad"] = new JsonData();
        data1["context"]["ad"]["callback"] = clickid;

        Debug.Log("<-data1-> " + data1.ToJson());

        options.Data = data1.ToJson();

        TT.Request("https://analytics.oceanengine.com/api/v2/conversion", options,
           response => { Debug.Log(response); },
           response => { Debug.Log(response); });
    }


    /// <summary>
    /// </summary>
    /// <param name="adId"></param>
    /// <param name="closeCallBack"></param>
    /// <param name="errorCallBack"></param>
    public void ShowVideoAd(string adId, System.Action<bool> closeCallBack, System.Action<int, string> errorCallBack)
    {
        starkAdManager = StarkSDK.API.GetStarkAdManager();
        if (starkAdManager != null)
        {
            starkAdManager.ShowVideoAdWithId(adId, closeCallBack, errorCallBack);
        }
    }
}
