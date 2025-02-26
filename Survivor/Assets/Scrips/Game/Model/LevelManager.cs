using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TTSDK.UNBridgeLib.LitJson;
using TTSDK;
using StarkSDKSpace;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    private int m_Level=1;
    public int KillCount;
    public Action LevelImprove;
    public Action CompleteSelect;
    public GameObject ShopPanel;
    public Image ExperienceBar;
    public bool IsFirstChanged=true;
    public float Timer;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI LevelText;
    private Player m_Player;
    public GameObject PausePanel;
    public GameObject PropPanel;
    public string clickid;
    private StarkAdManager starkAdManager;

    public int Level
    {
        get
        {
            return m_Level;
        }
        set
        {
            m_Level = value;
            if(IsFirstChanged==false)
            {
                LevelImprove();
                EnemyManager.ChangeSpawn();
            }
            else
            {
                IsFirstChanged = false;
            }
        }
    }
    public float CurrentExperience;
    public float MaxExperience;
    public GameObject GameOverUI;
    public bool isOver=false;
    // Start is called before the first frame update
    private void Awake()
    {
        Level = 1;
        Instance = this;
    }
    
    private void OnDestroy()
    {
        LevelImprove -= LevelChanged;
        CompleteSelect -= ResumeGame;
    }
    public void SetPropPanel()
    {
        PropPanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        PausePanel.SetActive(true);
    }
    void Start()
    {
        m_Player = FindObjectOfType<Player>();
        if(m_Player==null)
        {
            Debug.Log("没找到");
        }
        LevelImprove += LevelChanged;
        CompleteSelect += ResumeGame;
        Init();
    }
    public void Revive()
    {
        ShowVideoAd("192if3b93qo6991ed0",
            (bol) => {
                if (bol)
                {

                    WeaponManager.Instance.StartWeapon();
                    m_Player.Revive();
                    HideGameOver();


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
    public void ShowGameOver()
    {
        ShowInterstitialAd("1lcaf5895d5l1293dc",
            () => {
                Debug.LogError("--插屏广告完成--");

            },
            (it, str) => {
                Debug.LogError("Error->" + str);
            });
        isOver = true;
        FindObjectOfType<EnemyManager>().StopCreate();
        GameOverUI.SetActive(true);
    }
    /// <summary>
    /// 播放插屏广告
    /// </summary>
    /// <param name="adId"></param>
    /// <param name="errorCallBack"></param>
    /// <param name="closeCallBack"></param>
    public void ShowInterstitialAd(string adId, System.Action closeCallBack, System.Action<int, string> errorCallBack)
    {
        starkAdManager = StarkSDK.API.GetStarkAdManager();
        if (starkAdManager != null)
        {
            var mInterstitialAd = starkAdManager.CreateInterstitialAd(adId, errorCallBack, closeCallBack);
            mInterstitialAd.Load();
            mInterstitialAd.Show();
        }
    }
    public void HideGameOver() 
    {
        isOver = false;
        if (FindObjectOfType<EnemyManager>().isBossAlive==false)
        {
            FindObjectOfType<EnemyManager>().StartCreate();
        }
        GameOverUI?.SetActive(false);
    }
    void Init()
    {
        if(Level<=15)
        {
            MaxExperience = 10+(Level-1)*10;
            //Debug.Log(MaxExperience);
        }
        else if(Level<=30)
        {
            MaxExperience = 150 + (Level - 15) * 20;
        }
        else
        {
            MaxExperience = 450 * 3 * (Level - 30);
        }
    }
    public void LevelChanged()
    {
        ShopPanel.SetActive(true);
        CurrentExperience = 0;
        Init();
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        ShopPanel.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (isOver==false)
        {
            Timer += Time.deltaTime;
            ExperienceBar.fillAmount = CurrentExperience / MaxExperience;
            if (CurrentExperience >= MaxExperience)
            {
                Level += 1;
            }
            UpdateTimerDisplay();
            LevelText.text="等级："+Level.ToString();
        }
    }
    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(Timer / 60); // 计算分钟
        int seconds = Mathf.FloorToInt(Timer % 60); // 计算秒数

        // 更新UI Text显示
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
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
