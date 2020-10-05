using System;

namespace Impostor.Server.Net.Messages
{
    public enum MessageType : byte 
    {
        HostGame = 0,
        JoinGame = 1,
        StartGame = 2,
        RemoveGame = 3,
        RemovePlayer = 4,
        GameData = 5,
        GameDataTo = 6,
        JoinedGame = 7,
        EndGame = 8,
        AlterGame = 10,
        KickPlayer = 11,
        WaitForHost = 12,
        Redirect = 13,
        ReselectServer = 14,
        GetGameList = 9,
        GetGameListV2 = 16,
    }
}