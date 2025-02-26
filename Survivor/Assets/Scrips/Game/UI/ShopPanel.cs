using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using TTSDK.UNBridgeLib.LitJson;
using TTSDK;
using StarkSDKSpace;

public class ShopPanel : MonoBehaviour
{
    public TextMeshProUGUI BombNumberText;
    public TextMeshProUGUI DrumstickNumberText;
    public TextMeshProUGUI MagnetNumberText;
    public string clickid;
    private StarkAdManager starkAdManager;
    private void Awake()
    {
        UpdateText();
    }
    public void BuyBomb()
    {
        if(CoinManager.GetCoins()>=200)
        {
            GamePropManager.instance.AddBomb(1);
            UpdateText();
            CoinManager.ReduceCoins(200);
        }
        else
        {
            Debug.Log("金币不足");
        }
    }
    public void BuyDrumstick()
    {
        if (CoinManager.GetCoins() >= 200)
        {
            GamePropManager.instance.AddDrumstick(1);
            UpdateText();
            CoinManager.ReduceCoins(200);
        }
        else
        {
            Debug.Log("金币不足");
        }
    }
    public void BuytMagnet()
    {
        if (CoinManager.GetCoins() >= 200)
        {
            GamePropManager.instance.AddMagnet(1);
            UpdateText();
            CoinManager.ReduceCoins(200);
        }
        else
        {
            Debug.Log("金币不足");
        }
    }
    public void AddBomb()
    {
        ShowVideoAd("192if3b93qo6991ed0",
            (bol) => {
                if (bol)
                {

                    GamePropManager.instance.AddBomb(1);
                    UpdateText();


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
    public void AddDrumstick()
    {
        
        ShowVideoAd("192if3b93qo6991ed0",
            (bol) => {
                if (bol)
                {
                    GamePropManager.instance.AddDrumstick(1);
                    UpdateText();


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
    public void AddMagnet()
    {
        ShowVideoAd("192if3b93qo6991ed0",
            (bol) => {
                if (bol)
                {

                    GamePropManager.instance.AddMagnet(1);
                    UpdateText();


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
    public void HidePanel()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
    public void UpdateText()
    {
        BombNumberText.text = "剩余数量：" + GamePropManager.instance.GetBombNumber().ToString();
        DrumstickNumberText.text = "剩余数量：" + GamePropManager.instance.GetDrumstickNumber().ToString();
        MagnetNumberText.text = "剩余数量：" + GamePropManager.instance.GetMagnetNumber().ToString();
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
