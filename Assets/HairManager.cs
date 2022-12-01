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
        /*
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
        }*/
        UpdateInSphere();
    }

    private static float MAX_RADIUS = 3f;
    private static float MAX_HEIGHT = 2.5f;
    private static int NUM_LAYERS = 9;
    private static int INIT_NUN_HAIRS = 10;

    private void UpdateInSphere()
    {
        Vector3 center = transform.position;
        int count = 0;
        int currentNumHairs = INIT_NUN_HAIRS;

        for (int layer = 1; layer <= NUM_LAYERS; layer++)
        {
            float height = MAX_HEIGHT * layer / NUM_LAYERS;
            float radius = height * MAX_RADIUS / MAX_HEIGHT;
            float yPos = center.y + MAX_HEIGHT - height;
            float angleDiff = 360.0f / currentNumHairs;

            for (int i = 0; i < currentNumHairs; i++)
            {
                float angle = i * angleDiff;
                float angleInRadian = angle / 180 * Mathf.PI;
                float xPos = radius * Mathf.Cos(angleInRadian);
                float zPos = radius * Mathf.Sin(angleInRadian);
                lastList[count].transform.position = new Vector3(center.x + xPos, yPos, center.z + zPos);
                count++;
                if (count >= n)
                {
                    return;
                }

            }
            currentNumHairs += INIT_NUN_HAIRS;
        }
    }
}
