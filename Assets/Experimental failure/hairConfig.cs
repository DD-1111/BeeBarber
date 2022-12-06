using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hairConfig : MonoBehaviour
{
    public bool apply = false;

    // Start is called before the first frame update
    [SerializeField]
    bool springJoint = true;

    [SerializeField]
    int spring = 150;

    [SerializeField]
    int damper = 35;

    [SerializeField]
    bool ConfigurableJoint = true;

    [SerializeField]
    [Range(1, 1000)]
    int positionSpring = 30;

    [SerializeField]
    [Range(1, 100)]
    int positionDamper = 5;

    [SerializeField]
    [Range(1, 1000)]
    int angularSpring = 100;

    [SerializeField]
    [Range(1, 1000)]
    int angularDamper = 30;

    //[SerializeField]
    //bool angularX = true;
    //[SerializeField]
    //bool angularY = false;
    //[SerializeField]
    //bool angularZ = false;



    void Start()
    {
        JointDrive drive = new JointDrive();

        drive.positionSpring = positionSpring;
        drive.positionDamper = positionDamper;

        JointDrive angularDrive = new JointDrive();
        angularDrive.positionSpring = angularSpring;
        angularDrive.positionDamper = angularDamper;

        for (int i = 1; i < transform.childCount; i++)
        {

            Transform child = transform.GetChild(i);
            SpringJoint springJ = child.GetComponent<SpringJoint>();
            ConfigurableJoint config = child.GetComponent<ConfigurableJoint>();

            if (!springJoint)
            {
                Destroy(springJ);
            } else
            {
                springJ.spring = spring;
                springJ.damper = damper;
            }
            if (!ConfigurableJoint)
            {
                Destroy(config);
            } else
            {
                config.angularXDrive = angularDrive;
                config.angularYZDrive = angularDrive;
    
                config.xDrive = drive;
                config.yDrive = drive;
                config.zDrive = drive;
            }

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
