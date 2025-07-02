using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ProcGenScript : MonoBehaviour
{
    public GameObject lPlacer;
    private List<GameObject> tileList = new List<GameObject>();
    public GameObject[] tileTypes = new GameObject[3];
    private GameObject[] currentSides = new GameObject[4];
    private GameObject lastSpawn;
    public int tileMaxCount;
    public int tileCount;
    public int tilesChecked;
    public bool overTop;
    // Start is called before the first frame update
    void Start()
    {
        lastSpawn = Instantiate(lPlacer, new Vector3(0,0,0), Quaternion.identity);
        tileList.Add(lastSpawn);
        tileCount = tileList.Count;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < tileList.Count; i++) //Do for every tile
        {
            if (tileList[i].GetComponent<tileScript>().done != true)
            {
                currentSides = tileList[i].GetComponent<tileScript>().sides;
                for (int j = 0; j < currentSides.Length; j++) //Do for every side of the current i tile
                {
                    overTop = false;
                    tilesChecked = 0;
                    for (int k = 0; k < tileList.Count; k++) //Look at every tile and see if it shares a position with the current side
                    {
                        if (currentSides[j].transform.position != tileList[k].transform.position)
                        {
                            tilesChecked += 1;
                        }
                        if (currentSides[j].transform.position - new Vector3(0, 1, 0) == tileList[k].transform.position && tileList[k].GetComponent<tileScript>().top == true)
                        {
                            overTop = true;
                        }
                    }
                    if (tilesChecked == tileCount && tileCount < tileMaxCount && currentSides[j].transform.position.y >= -5)
                    {
                        if (j == 0 && Random.Range(0, 10) == 0 && overTop != true)
                        {
                            lastSpawn = Instantiate(lPlacer, currentSides[j].transform.position, Quaternion.identity);
                            tileList.Add(lastSpawn);
                            tileCount = tileList.Count;
                        }
                        else if ((j == 1 || j == 3) && Random.Range(0, 10) != 0 && overTop != true)
                        {
                            lastSpawn = Instantiate(lPlacer, currentSides[j].transform.position, Quaternion.identity);
                            tileList.Add(lastSpawn);
                            tileCount = tileList.Count;
                        }
                        else if (j == 2)
                        {
                            lastSpawn = Instantiate(lPlacer, currentSides[j].transform.position, Quaternion.identity);
                            tileList.Add(lastSpawn);
                            tileCount = tileList.Count;
                        }
                    }
                }
                //tileList[i].GetComponent<tileScript>().done = true;
            }
        }
        for (int i = 0; i < tileList.Count; i++)
        {
            tilesChecked = 0;
            for (int j = 0; j < tileList.Count; j++)
            {
                if(tileList[i].gameObject.transform.position + new Vector3(0, 1, 0) != tileList[j].transform.position)
                {
                    tilesChecked += 1;
                }
            }
            if (tilesChecked == tileList.Count)
            {
                tileList[i].GetComponent<tileScript>().top = true;
            }
        }
        if (tileCount == tileMaxCount)
        {
            for (int i = 0;i < tileList.Count;i++)
            {
                if(tileList[i].GetComponent<tileScript>().top == true)
                {
                    Instantiate(tileTypes[0], tileList[i].transform.position, Quaternion.identity);
                    Destroy(tileList[i]);
                }
                else if(tileList[i].GetComponent<tileScript>().top == false)
                {
                    Instantiate(tileTypes[1], tileList[i].transform.position, Quaternion.identity);
                    Destroy(tileList[i]);
                }
            }
            tileList.Clear();
        }
    }
}
