using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveRagdoll : MonoBehaviour
{
  

    public Transform[] animatedRigs;
    public ConfigurableJoint[] ragdollRigs;

    public Quaternion[] targetInitialRotations;

    public ConfigurableJoint mainJoint;

    public float spring;
    public float force;

    public float characterSprings;
    void Start()
    {
        for (int a = 0; a < animatedRigs.Length; a++)
        {
            targetInitialRotations[a] = animatedRigs[a].localRotation;
        }
    }

    void FixedUpdate()
    {
        JointDrive drive = new JointDrive();
        drive.maximumForce = force;
        drive.positionSpring = spring;
        for (int a = 0; a < animatedRigs.Length; a++)
        {
            if (ragdollRigs[a] != null)
            {
                ragdollRigs[a].targetRotation = Quaternion.Inverse(animatedRigs[a].localRotation) * targetInitialRotations[a];
                ragdollRigs[a].angularYZDrive = drive;
                ragdollRigs[a].angularXDrive = drive;
            }
        }


        foreach (CharacterJoint characterJoint in GetComponentsInChildren<CharacterJoint>())
        {
            if (characterJoint != null)
            {
                SoftJointLimitSpring softJointLimitSpring = characterJoint.twistLimitSpring;
                softJointLimitSpring.spring = characterSprings;
                characterJoint.twistLimitSpring = softJointLimitSpring;
                characterJoint.swingLimitSpring = softJointLimitSpring;
            }
        }
    }
}

