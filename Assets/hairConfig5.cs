using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hairConfig5 : MonoBehaviour
{
    public bool apply = false;

    // Start is called before the first frame update

    [SerializeField]
    [Range(1, 1000)]
    int positionSpring = 30;

    [SerializeField]
    [Range(1, 100)]
    int positionDamper = 5;

    [SerializeField]
    float decayfactor = 0.6f;

    [SerializeField]
    float minimumSpring = 2;
    //[SerializeField]
    //bool angularX = true;
    //[SerializeField]
    //bool angularY = false;
    //[SerializeField]
    //bool angularZ = false;



    void Start()
    {


        float decay = 1;
        for (int i = transform.childCount - 1; i > 0; i--)
        {
            JointDrive drive = new JointDrive();

            if (positionSpring * decay > minimumSpring)
            {
                drive.positionSpring = positionSpring * decay;
                drive.positionDamper = positionDamper * decay;
                drive.maximumForce = float.MaxValue;
            } else
            {
                drive.positionSpring = minimumSpring;
                drive.positionDamper = positionDamper;
                drive.maximumForce = float.MaxValue;
            }
            Transform child = transform.GetChild(i);
            ConfigurableJoint config = child.GetComponent<ConfigurableJoint>();
            config.xDrive = drive;
            config.yDrive = drive;
            config.zDrive = drive;
            decay *= decayfactor;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (apply)
        {
            Start();
            apply = false;
        }
    }
}
