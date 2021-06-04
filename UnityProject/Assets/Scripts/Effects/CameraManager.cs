
using UnityEngine;
using UnityEngine.U2D;

class CameraManager : MonoBehaviour
{
    PixelPerfectCamera ppcamera;

    private void Awake()
    {
        ppcamera = this.gameObject.GetComponent<PixelPerfectCamera>();
    }

    private void Update()
    {
        ppcamera.refResolutionX = Screen.width;
        ppcamera.refResolutionY = Screen.height;
        ppcamera.assetsPPU = PPU(Screen.width);
    }

    int PPU(int width)
    {
        if (Screen.width >= 1900) return 64;
        if (Screen.width >= 1380) return 48;
        if (Screen.width >= 900) return 32;
        return 16;
    }
}