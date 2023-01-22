using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Transform spawn;
    List<CarSpawner> carSpawners = new List<CarSpawner>();

    internal static int level = 1;
    internal static int score = 0;
    internal static int highscore = 0;

    [SerializeField] GameObject pieceOfRoad;
    [SerializeField] GameObject pieceSizeCube;
    float pieceSize;
    [SerializeField] GameObject endOfRoad;

    internal static Dictionary<int, int> lvlSizes = new Dictionary<int, int>() { [1] = 1, [2] = 1, [3] = 2, [4] = 2, [5] = 3, [6] = 3, [7] = 4, [8] = 4, [9] = 5, [10]= 5 };
    internal static Dictionary<int, int> orderBook = new Dictionary<int, int>() { [1] = 3, [2] = 5, [3] = 7, [4] = 10, [5] = 12, [6] = 16, [7] = 18, [8] = 20, [9] = 24, [10] = 28 };

    List<GameObject> allLevel = new List<GameObject>();
    internal List<GameObject> allHouses = new List<GameObject>();
    internal List<GameObject> housesToDeliver = new List<GameObject>();

    void Start()
    {
        pieceSize = pieceSizeCube.transform.localScale.x;
        for (int i = 0; i < lvlSizes[level]; i++)
        {
            GameObject nRoad = Instantiate(pieceOfRoad);
            nRoad.transform.position = new Vector3 (pieceSize*i, nRoad.transform.position.y, nRoad.transform.position.z);
            nRoad.GetComponent<HousesManager>().lvlmanager = this;
            allLevel.Add(nRoad);
            carSpawners.Add(nRoad.transform.Find("carSpawner").GetComponent<CarSpawner>());
        }
        GameObject fRoad = Instantiate(endOfRoad);
        fRoad.transform.position = new Vector3(pieceSize*lvlSizes[level], fRoad.transform.position.y, fRoad.transform.position.z);
        allLevel.Add(fRoad);
    }

    internal void buildGame()
    {
        for (int i = 0; i < orderBook[level];  i++)
        {
            var dice = Random.Range(0, allHouses.Count);
            var house = allHouses[dice];
            house.tag = "house";
            house.GetComponent<Renderer>().material.color = new Color(0.2f, 1.0f, 0.2f, 1.0f);
            house.transform.Find("mailbox").GetComponent<Renderer>().material.color = new Color(0.2f, 1.0f, 0.2f, 1.0f);
            house.GetComponent<MeshCollider>().isTrigger = true;
            house.GetComponent<Outline>().enabled = true;
            housesToDeliver.Add(house);
            allHouses.RemoveAt(dice);
        }
    }

    void Update()
    {
        for (int i= 0; i< carSpawners.Count; i++)
        {
            carSpawners[i].spawn = spawn;
        }
    }
}
