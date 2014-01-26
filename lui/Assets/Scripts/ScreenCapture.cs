using UnityEngine;
using System.Collections;
using System;

public class ScreenCapture : MonoBehaviour
{

    void LateUpdate()
    {

        if (Input.GetKeyDown("k"))
        {
            Application.CaptureScreenshot(ScreenShotName()) ;
            GameObject lui = GameObject.Find("lui") ;
            Pawn p = lui.GetComponent("Pawn") as Pawn;
            if (p.actualAnim == "Kite" || p.actualAnim == "BackFlying" || p.actualAnim == "Umbrella" || p.actualAnim == "Balloon" || p.actualAnim == "BackJump")
            {
                print(p.actualAnim);
            }
        }

    }

    public static string ScreenShotName()
    {
        return string.Format("{0}/screenshots/screen_{1}.png",
                             Application.dataPath,
                             System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    }
}