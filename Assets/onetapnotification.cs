using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onetapnotification : MonoBehaviour
{
void Start()
{
    // Enable line below to enable logging if you are having issues setting up OneSignal. (logLevel, visualLogLevel)
    // OneSignal.SetLogLevel(OneSignal.LOG_LEVEL.INFO, OneSignal.LOG_LEVEL.INFO);

    OneSignal.StartInit("3cbbf30e-5e96-4c6d-a428-3f12c701cd4f")
      .HandleNotificationOpened(HandleNotificationOpened)
      .EndInit();

    OneSignal.inFocusDisplayType = OneSignal.OSInFocusDisplayOption.Notification;
}

// Gets called when the player opens the notification.
private static void HandleNotificationOpened(OSNotificationOpenedResult result)
{
       
}
}
