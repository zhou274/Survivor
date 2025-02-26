using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public GameObject BuffPanel;
    public BuffItem[] item;
    List<int> UniqueNumber = new List<int>();
    private void OnEnable()
    {
        while(UniqueNumber.Count<3)
        {
            int index = Random.Range(0, item.Length);
            int level = 0;
            if(!UniqueNumber.Contains(index))
            {
                GameObject buff = Instantiate(BuffPanel, transform);
                switch (index)
                {
                    case 0:
                        level=WeaponManager.Instance.BoomerangLevel;
                        buff.GetComponent<BuffShow>().button.onClick.AddListener(() => { WeaponManager.Instance.BoomerangLevel += 1; 
                            transform.parent.gameObject.SetActive(false);
                            LevelManager.Instance.CompleteSelect();
                        });
                        if(level>=4)
                        {
                            Destroy(buff);
                            continue;
                        }
                        break;
                    case 1:
                        level = WeaponManager.Instance.DartLevel;
                        buff.GetComponent<BuffShow>().button.onClick.AddListener(() => { WeaponManager.Instance.DartLevel += 1;
                            transform.parent.gameObject.SetActive(false);
                            LevelManager.Instance.CompleteSelect();
                        });
                        if (level >= 4)
                        {
                            Destroy(buff);
                            continue;
                        }
                        break;
                    case 2:
                        level=WeaponManager.Instance.MisslileLevel;
                        buff.GetComponent<BuffShow>().button.onClick.AddListener(() => { WeaponManager.Instance.MisslileLevel += 1;
                            transform.parent.gameObject.SetActive(false);
                            LevelManager.Instance.CompleteSelect();
                        });
                        if (level >= 4)
                        {
                            Destroy(buff);
                            continue;
                        }
                        break;
                    case 3:
                        level = WeaponManager.Instance.FootBallLevel;
                        buff.GetComponent<BuffShow>().button.onClick.AddListener(() => { WeaponManager.Instance.FootBallLevel += 1;
                            transform.parent.gameObject.SetActive(false);
                            LevelManager.Instance.CompleteSelect();
                        });
                        if (level >= 4)
                        {
                            Destroy(buff);
                            continue;
                        }
                        break;
                    case 4:
                        level=WeaponManager.Instance.RockLevel;
                        buff.GetComponent<BuffShow>().button.onClick.AddListener(() => { level = WeaponManager.Instance.RockLevel += 1;
                            transform.parent.gameObject.SetActive(false);
                            LevelManager.Instance.CompleteSelect();
                        });
                        if (level >= 4)
                        {
                            Destroy(buff);
                            continue;
                        }
                        break;
                    case 5:
                        level = WeaponManager.Instance.RocketLevel;
                        buff.GetComponent<BuffShow>().button.onClick.AddListener(() => { level = WeaponManager.Instance.RocketLevel += 1 ;
                            transform.parent.gameObject.SetActive(false);
                            LevelManager.Instance.CompleteSelect();
                        });
                        if (level >= 4)
                        {
                            Destroy(buff);
                            continue;
                        }
                        break;
                    case 6:
                        level = WeaponManager.Instance.TopLevel;
                        buff.GetComponent<BuffShow>().button.onClick.AddListener(() =>
                        {
                            level = WeaponManager.Instance.TopLevel += 1;
                            transform.parent.gameObject.SetActive(false);
                            LevelManager.Instance.CompleteSelect();
                        });
                        if (level >= 0)
                        {
                            Destroy(buff);
                            continue;
                        }
                        continue;
                        //break;
                    case 7:
                        //加血
                        level = 0;
                        buff.GetComponent<BuffShow>().button.onClick.AddListener(() => {
                            FindObjectOfType<Player>().hp = FindObjectOfType<Player>().maxhp;
                            transform.parent.gameObject.SetActive(false);
                            LevelManager.Instance.CompleteSelect();
                        });
                        break;
                    case 8:
                        //增加持续时间
                        level = WeaponManager.Instance.HoldLevel;
                        buff.GetComponent<BuffShow>().button.onClick.AddListener(() => {
                            WeaponManager.Instance.HoldTimePersent += 0.1f;
                            transform.parent.gameObject.SetActive(false);
                            LevelManager.Instance.CompleteSelect();
                        });
                        break;
                    case 9:
                        level =WeaponManager.Instance.RangeLevel;
                        buff.GetComponent<BuffShow>().button.onClick.AddListener(() => {
                            FindObjectOfType<Player>().GetComponent<PhysicsDetector>().detectRadius += 0.1f;
                            transform.parent.gameObject.SetActive(false);
                            LevelManager.Instance.CompleteSelect();
                        });
                        //增加攻击范围

                        break;
                    case 10:
                        //减少冷却时间
                        level = WeaponManager.Instance.CooldownLevel;
                        buff.GetComponent<BuffShow>().button.onClick.AddListener(() => {
                            WeaponManager.Instance.CooldownTimePersent -= 0.1f;
                            transform.parent.gameObject.SetActive(false);
                            LevelManager.Instance.CompleteSelect();
                        });
                        if (level >= 4)
                        {
                            Destroy(buff);
                            continue;
                        }
                        break;
                }
                UniqueNumber.Add(index);
                buff.GetComponent<BuffShow>().Init(item[index].BuffImage, item[index].Title, level);
            }
        }
        UniqueNumber.Clear();
    }
    private void OnDisable()
    {
        for(int i=0;i<transform.childCount;i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

}
