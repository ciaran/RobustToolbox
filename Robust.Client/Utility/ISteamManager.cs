using System;
using Steamworks;

namespace Robust.Client.Utility
{
    public interface ISteamManager : IDisposable
    {
        void Initialize();
        void Update(string serverName, string username, string maxUsers, string users);

        void SetTimelineTooltip(string description, float timeOffsetSeconds);

        void AddInstantaneousTimelineEvent(
            string title,
            string description,
            string icon,
            uint priority,
            float startOffsetSeconds);
    }
}
