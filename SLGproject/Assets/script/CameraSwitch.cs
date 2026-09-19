using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraSwitch : MonoBehaviour
{
    [SerializeField] private Transform cameraTs;
    [SerializeField] private Vector3 camera1_1;
    [SerializeField] private Vector3 camera1_2;
    [SerializeField] private Vector3 camera2_1;
    [SerializeField] private Vector3 camera2_2;
    [SerializeField] private float transTime = 1f;

    public bool capsLock = false;
    private bool isTransitioning = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.CapsLock) && !isTransitioning)
        {
            Quaternion fromRotation = Quaternion.Euler(capsLock ? camera1_1 : camera1_2);
            Quaternion toRotation = Quaternion.Euler(capsLock ? camera1_2 : camera1_1);
            Vector3 fromPotion = (capsLock ? camera2_1 : camera2_2);
            Vector3 toPotion = (capsLock ? camera2_2 : camera2_1);


            StartCoroutine(TransitionRotation(fromRotation, toRotation,fromPotion,toPotion));
            capsLock = !capsLock;
        }
    }

    IEnumerator TransitionRotation(Quaternion from01, Quaternion to01,Vector3 from02,Vector3 to02)
    {
        isTransitioning = true;
        float elapsed = 0f;

        while (elapsed < transTime)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / transTime);

            cameraTs.rotation = Quaternion.Slerp(from01, to01, t);
            cameraTs.position = Vector3.Slerp(from02, to02, t);
            yield return null;   // 关键
        }

        cameraTs.rotation = to01;  //防止t没到达1 结果不准确
        cameraTs.position = to02;  //防止t没到达1 
        isTransitioning = false; //不要在改变的过程中重复按
    }
}
