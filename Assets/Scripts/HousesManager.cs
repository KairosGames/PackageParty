using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HousesManager : MonoBehaviour
{
    [SerializeField] GameObject[] housesPrefabs;
    [SerializeField] GameObject[] elementsPrefabs;
    internal LevelManager lvlmanager;
    float[] xPos = new float[] { 1.2f, 4.0f, 6.8f, 9.6f, 12.4f, 15.2f, 18.0f };
    float[] xPos2 = new float[] { 4.5f, 9.0f, 13.5f, 18.0f };

    void Start()
    {
        for (int i = 0; i < 7; i++)
        {
            int rndm = Random.Range(0, housesPrefabs.Length);
            GameObject house = Instantiate(housesPrefabs[rndm]);
            house.GetComponent<HouseBehaviour>().houseType = rndm;
            house.transform.parent = transform;
            house.layer = 4;
            house.transform.localPosition = new Vector3(xPos[i], 0.5f, Random.Range(370, 473)/10); ;
            lvlmanager.allHouses.Add(house);
        }
        for (int i = 0; i < 4; i++)
        {
            float p1 = (i == 0) ? 0f : xPos2[i-1]+1.0f;
            float p2 = xPos2[i]-1.0f;
            var elem = Instantiate(elementsPrefabs[Random.Range(0, elementsPrefabs.Length)]);
            elem.transform.parent = transform;
            elem.transform.localPosition = new Vector3(Random.Range(p1*100, p2*100)/100, 0.5f, 21.5f);
        }
        if (lvlmanager.allHouses.Count == 7 * LevelManager.lvlSizes[LevelManager.level]){ lvlmanager.buildGame(); }
    }
}
