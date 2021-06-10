using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrationsV2 : MonoBehaviour
{
    private static AndroidJavaObject vibrator;
    private static AndroidJavaClass vibrationEffectClass;


    private static void Initialize() {
        using(AndroidJavaObject unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        using ( AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity")) {
            if (currentActivity != null) {
                vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
                vibrationEffectClass = new AndroidJavaClass("android.os.VibrationEffect");
            }
        }
    }

    public static void Vibrate(long milliseconds, int amplitude) {
        if (vibrationEffectClass == null || vibrator == null) Initialize();
        using (AndroidJavaObject vibrationEffect =
            vibrationEffectClass.CallStatic<AndroidJavaObject>("createOneShot", milliseconds, amplitude)) {
            vibrator.Call("vibrate", vibrationEffect);
        }

    }
}