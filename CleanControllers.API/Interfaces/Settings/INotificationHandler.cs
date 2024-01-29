﻿using CleanControllers.API.Settings.NotificationSettings;

namespace CleanControllers.API.Interfaces.Settings;

public interface INotificationHandler
{
    List<Notification> GetNotifications();
    bool HasNotifications();
    void AddNotification(string key, string message);
}
