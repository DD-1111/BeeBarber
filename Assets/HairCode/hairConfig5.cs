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

    [SerializeField]
    float minimusDamper = 1f;

    [SerializeField]
    [Range(0.5f, 5)]
    float massCoefficient = 1f;

    //[SerializeField]
    //bool angularX = true;
    //[SerializeField]
    //bool angularY = false;
    //[SerializeField]
    //bool angularZ = false;



    void Start()
    {


        float decay = 1;
        float damperDecay = 1;
        Transform child = transform.GetChild(0);
        child.GetComponent<Rigidbody>().mass = 0.43f * massCoefficient;
        for (int i = transform.childCount - 1; i > 0; i--)
        {
            JointDrive drive = new JointDrive();
            SoftJointLimitSpring linear = new SoftJointLimitSpring();
     
            drive.positionDamper = Mathf.Max(positionDamper * damperDecay, minimusDamper);
            drive.positionSpring = Mathf.Max(positionSpring * decay, minimumSpring);
            drive.maximumForce = float.MaxValue;
            child = transform.GetChild(i);
            ConfigurableJoint config = child.GetComponent<ConfigurableJoint>();
            config.xDrive = drive;
            config.yDrive = drive;
            config.zDrive = drive;
            decay *= decayfactor;
            damperDecay *= (decayfactor + 1f) / 2f;

            linear.spring = positionSpring* (0.5f - Mathf.Abs(transform.rotation.x));
            linear.damper = 1;
            config.linearLimitSpring = linear;

            child.GetComponent<Rigidbody>().mass = ((1.3f + Random.value * 2) * 0.1f) * massCoefficient;
        }
    }

    public void rearrangeSpring()
    {
        apply = true;
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
