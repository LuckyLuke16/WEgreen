using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Notifications.Android;
using UnityEngine;
using UnityEngine.UI;

/**
* @brief Creates, manages and sends notification to your andorid phone
*/
public class MobileNotificationManager : MonoBehaviour
{
    // declaring variables
    int id;
    AndroidNotification notification;
    [SerializeField]
    private Text wateringPlantIntervall;
    // Start is called before the first frame update
    void Start()
    {
        // remove notifications that have been displayed
        AndroidNotificationCenter.CancelAllDisplayedNotifications();

        // create android notificaiton channel to send messages through
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
    }

    // Update is called once per frame
    void Update()
    {
        RescheduleNotification();
    }

    /**
     * @brief Create notification and assign values to specific attributes e.g. notification title
     * @return void
     */
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

    /**
     * @brief Sends notification through a specific channel
     * @return void
     */
    public void SendNotification()
    {
        // first arg: the notification you want to send; second arg: the channel id and therefore the channel you want the notification to be sent through
        AndroidNotificationCenter.SendNotification(notification, "channel_id"); 
    }

    /**
     * @brief Reschedule the notifications by basically removing all already existing notifications when script is running and send new ones
     * @return void
     */
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
