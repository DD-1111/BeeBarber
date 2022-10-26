using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HairManager : MonoBehaviour
{

    public GameObject hairPrefab;
    // Start is called before the first frame update
    private int hairPartCount = 0;
 
    public float arrangeLength = 1.0f;
    public float arrangeWidth = 1.0f;
    private GameObject[] lastList;

    private int n;
    private int row;
    private int col;
    void Start()
    {
        GameObject[] temp = GameObject.FindGameObjectsWithTag("part");
        hairPartCount = hairPrefab.transform.childCount;
        List<GameObject> lastPartList = new List<GameObject>();
        foreach (GameObject part in temp)
        {
            if (part.name == hairPartCount.ToString())
            {
                lastPartList.Add(part);
            }
        }
        n = lastPartList.Count;
        if (n > 0)
        {
            row = (int)Mathf.Ceil(Mathf.Sqrt(n));
            col = (int)Mathf.Ceil((float)n / row);
            lastList = lastPartList.ToArray();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 curPos = transform.position;
        int count = 0;
        float rowInterv = arrangeLength / row;
        float colInterv = arrangeWidth / col;
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                if (count < n)
                {
                    lastList[count].transform.position = curPos + new Vector3(rowInterv * i, 0, colInterv * j);
                    count++;
                }
            }
        }
    }
}
