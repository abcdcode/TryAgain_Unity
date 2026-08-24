using UnityEngine;

public class FixedAspectCamera : MonoBehaviour
{
    public float targetAspect = 16f / 9f;

    void Awake()
    {
        Camera cam = GetComponent<Camera>();

        float currentAspect =
            (float)Screen.width / Screen.height;

        if (currentAspect < targetAspect)
        {
            // 화면이 더 세로로 긴 경우
            // 좌우 기준을 유지하도록 카메라 크기 조정
            float scale = targetAspect / currentAspect;

            cam.orthographicSize *= scale;
        }
    }
}