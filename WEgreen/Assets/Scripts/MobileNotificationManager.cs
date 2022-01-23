using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Notifications.Android;
using UnityEngine;
using UnityEngine.UI;

public class MobileNotificationManager : MonoBehaviour
{
    // define two dates
    //DateTime date1 = new DateTime(2021, 1, 1, 9, 0, 0); 
    //DateTime date2 = new DateTime(2021, 1, 2, 9, 0, 0); // 02.01.2021 - 09:00:00 Uhr
    // Calculate the interval between the two dates.
    TimeSpan interval;

    AndroidNotificationChannel defaultNotificationChannel;
    int id;
    AndroidNotification notification;
    [SerializeField]
    private Text wateringPlantIntervall;
    // Start is called before the first frame update
    void Start()
    {
        //interval = date2 - date1;
        // remove notifications that have been displayed
        AndroidNotificationCenter.CancelAllDisplayedNotifications();

        // create android notificaiton channel to send messages thorugh
        var channel = new AndroidNotificationChannel()
        {
            Id = "channel_id",
            Name = "Notification Channel",
            Importance = Importance.Default,
            Description = "Reminder notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);

        CreateNotification();

        // send notification
        id = AndroidNotificationCenter.SendNotification(notification, "channel_id");

        //defaultNotificationChannel = new AndroidNotificationChannel()
        //{
        //    Id = "default channel",
        //    Name = "Default Channel",
        //    Description = "For Generic notifications",
        //    Importance = Importance.Default
        //};
        //AndroidNotificationCenter.RegisterNotificationChannel(defaultNotificationChannel);

        //AndroidNotification notification = new AndroidNotification()
        //{
        //    Title = "Test Notification",
        //    Text = "This is a test Notificaiton",
        //    SmallIcon = "default",
        //    LargeIcon = "default",
        //    FireTime = System.DateTime.Now.AddSeconds(3),
        //};

        //identifier = AndroidNotificationCenter.SendNotification(notification, "default_channel");
    }

    // Update is called once per frame
    void Update()
    {
        RescheduleNotification();
    }

    private void CreateNotification()
    {
        // create notification that is going to be sent
        notification = new AndroidNotification();
        notification.Title = "ERINNERUNG";
        notification.Text = "Deine Pflanze hat durst: 'Gieﬂ mich bitte!'";
        notification.FireTime = System.DateTime.Now.AddSeconds(3);
        //notification.CustomTimestamp = Convert.ToDateTime(interval);
        notification.RepeatInterval = new TimeSpan(int.Parse(wateringPlantIntervall.text), 0, 0, 0);
    }

    public void SendNotification()
    {
        AndroidNotificationCenter.SendNotification(notification, "channel_id"); 
    }

    private void RescheduleNotification()
    {
        // if the script is running and a notification is already scheduled, cancel it and re-schedule another message
        if (AndroidNotificationCenter.CheckScheduledNotificationStatus(id) == NotificationStatus.Scheduled)
        {
            AndroidNotificationCenter.CancelAllNotifications();
            AndroidNotificationCenter.SendNotification(notification, "channel_id");
        }
    }
}
