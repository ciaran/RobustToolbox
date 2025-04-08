using System;
using System.Text;
using Robust.Shared;
using Robust.Shared.Configuration;
using Robust.Shared.IoC;
using Robust.Shared.Localization;
using Robust.Shared.Log;
using Steamworks;
using LogLevel = DiscordRPC.Logging.LogLevel;

namespace Robust.Client.Utility
{
    internal sealed class SteamManager : ISteamManager
    {
        [Dependency] private readonly ILogManager _logManager = default!;

        private ISawmill _logger = default!;

        private bool _initialized;

        public void Initialize()
        {
            _logger = _logManager.GetSawmill("steam");

            _logger.Debug("Steam Init");

            try
            {
                SteamClient.Init(1482520, true);
                _initialized = true;
            }
            catch (Exception _)
            {
                // This is not treated as an error as the user may just not be playing through Steam
                _initialized = false;
            }

            if (!_initialized)
                return;

            SteamFriends.SetRichPresence("steam_display", "#Status_MainMenu");
            // SteamTimeline.
        }

        public void Update(string serverName, string username, string maxUsers, string users)
        {
            if (!_initialized)
                return;

            SteamFriends.SetRichPresence("serverName", serverName);
            SteamFriends.SetRichPresence("players", users);
            SteamFriends.SetRichPresence("maxPlayers", maxUsers);
            SteamFriends.SetRichPresence("steam_display", "#Status_ServerConnected");
        }

        public void Dispose()
        {
        }

        public void SetTimelineTooltip(string description, float timeOffsetSeconds)
        {
            SteamTimeline.SetTimelineTooltip(description, timeOffsetSeconds);
        }
    }
}

