﻿namespace EasyMeets.Notifier.Hubs.Interfaces
{
    public interface IBroadcastHubClient
    {
        Task BroadcastMessage(string msg);
    }
}
