using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopManager : MonoBehaviour
{
    public Transform Player;
    public GameObject Top;
    public float HoldTime=4;
    public float Fireinterval=2;
    public int CurrentLevel;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartTop());
        Player = FindObjectOfType<Player>().transform;
        transform.position = Player.position;
        Init();
        CurrentLevel = 0;
    }
    private void OnDestroy()
    {

    }
    public void Init()
    {
        if (transform.position != Player.position)
        {
            transform.position = Player.position;
        }
        if (WeaponManager.Instance.TopLevel==1)
        {
            GameObject Top1 = Instantiate(Top, transform);
            Top1.GetComponent<Top>().damage = 10;
            Top1.transform.localPosition = new Vector3(transform.position.x + 8, transform.position.y - 8, transform.position.z);
            GameObject Top2 = Instantiate(Top, transform);
            Top2.GetComponent<Top>().damage = 10;
            Top2.transform.localPosition = new Vector3(transform.position.x - 8, transform.position.y - 8, transform.position.z);
            GameObject Top3 = Instantiate(Top, transform);
            Top3.GetComponent<Top>().damage = 10;
            Top3.transform.localPosition = new Vector3(transform.position.x + 8, transform.position.y + 8, transform.position.z);
            GameObject Top4 = Instantiate(Top, transform);
            Top4.GetComponent<Top>().damage = 10;
            Top4.transform.localPosition = new Vector3(transform.position.x - 8, transform.position.y + 8, transform.position.z);

        }
        else if (WeaponManager.Instance.TopLevel == 2)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<Top>().damage = 40;
            }
        }
        else if (WeaponManager.Instance.TopLevel == 3)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<Top>().damage = 60;
            }

        }
        else if (WeaponManager.Instance.TopLevel == 4)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<Top>().damage = 100;
            }
        }
    }
    private void FixedUpdate()
    {
        transform.position = Player.position;
    }
    // Update is called once per frame
    void Update()
    {
        if(CurrentLevel!=WeaponManager.Instance.TopLevel)
        {
            
            Init();
            CurrentLevel = WeaponManager.Instance.TopLevel;
        }
    }
    IEnumerator StartTop()
    {
        while (true) 
        {
            HoldTime = 4;Fireinterval = 2;
            Fireinterval *= WeaponManager.Instance.CooldownTimePersent;
            HoldTime*=WeaponManager.Instance.HoldTimePersent;
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            yield return new WaitForSeconds(HoldTime);
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
            yield return new WaitForSeconds(Fireinterval);
        }
    }
}
