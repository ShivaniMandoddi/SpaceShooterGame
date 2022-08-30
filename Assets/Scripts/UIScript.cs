using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using JsonData;
using System;

public class UIScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Image selectedShip;
    public Text shipName;
    public Text shipDescription;
    public Text scoreText;
    public GameObject[] ships;
    //public UI ship;
    ShipDataList shipdataList;
    private void Awake()
    {
        string name = HandlingJsonData.Instance.GetTrueShip();
        GameObject temp = GameObject.Find(name);
        selectedShip.sprite = temp.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite;
        shipName.text = temp.transform.GetChild(1).gameObject.name;
        ShipData item = GetShipData(temp.name);
        DisplayData(item);
        
        temp.GetComponent<Toggle>().isOn = true;
        shipdataList = HandlingJsonData.Instance.ReadFile();
        ChangeStatus(name, shipdataList);
        scoreText.text ="Points - "+shipdataList.score.ToString();
    }

   
    public void Enter()
    {
        GameObject temp = EventSystem.current.currentSelectedGameObject;
        string shipname= EventSystem.current.currentSelectedGameObject.name;
        ChangingTicks();
        temp.GetComponent<Toggle>().isOn = true;
        //PlayerPrefs.SetString("ShipName", shipname);
        shipdataList=HandlingJsonData.Instance.ReadFile();
        ChangeStatus(shipname, shipdataList);
        selectedShip.sprite = temp.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite;
        shipName.text = temp.transform.GetChild(1).gameObject.name;
        ShipData item = GetShipData(shipname);
        DisplayData(item);

    }

    private void ChangeStatus(string shipname, ShipDataList shipdataList)
    {

        for (int i = 0; i < shipdataList.ships.Length; i++)
        {
            shipdataList.ships[i].status = false;
        }
        for (int i = 0; i < shipdataList.ships.Length; i++)
        {
            if (shipname == shipdataList.ships[i].name)
            {
                shipdataList.ships[i].status = true;
                HandlingJsonData.Instance.UpdateFile(shipdataList);
                break;
            }
        }
    }

    public ShipData GetShipData(string value)
    {
     
        shipdataList = HandlingJsonData.Instance.ReadFile();
        for (int i = 0; i < shipdataList.ships.Length; i++)
        {
            if (value == shipdataList.ships[i].name)
                return (shipdataList.ships[i]);
        }
        return null;
    }
    public void GameScene()
    {
        SceneManager.LoadScene(1);
    }
    public void UpgradingShip()
    {
        string name = HandlingJsonData.Instance.GetTrueShip();
        //GameObject temp = GameObject.Find(name);
        ShipData shipdata = GetShipData(name);
        if(shipdataList.score>=shipdata.shipupgrades && shipdata.upgrades>0)
        {
            shipdata.upgrades--;
            shipdataList.score=shipdataList.score-shipdata.shipupgrades;
            shipdata.shipupgrades += 5500;
            shipdata.shipSpeed++;
            shipdata.shipPoints++;
            scoreText.text= "Points - " + shipdataList.score.ToString();
        }
        HandlingJsonData.Instance.UpdateFile(shipdataList);
        DisplayData(shipdata);
    }
    public void ChangingTicks()
    {
        foreach (GameObject item in ships)
        {
            item.GetComponent<Toggle>().isOn = false;
        }
    }
    public void DisplayData(ShipData item)
    {
        shipDescription.text = "Points - " + item.shipPoints.ToString() + "\nSpeed - " + item.shipSpeed.ToString() + "\nUpgradePoints - " + item.shipupgrades +"\n Upgrades - "+item.upgrades+"/5";
    }
   
}
