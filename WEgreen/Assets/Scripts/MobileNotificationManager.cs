using System.Collections;
using System.Collections.Generic;
using Unity.Notifications.Android;
using UnityEngine;

public class MobileNotificationManager : MonoBehaviour
{
    AndroidNotificationChannel defaultNotificationChannel;
    int id;
    AndroidNotification notification;
    // Start is called before the first frame update
    void Start()
    {
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
        notification.Title = "I AM NOTIFICATION";
        notification.Text = "COME BACK AND BE WATER!!!";
        notification.FireTime = System.DateTime.Now.AddSeconds(1);
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
