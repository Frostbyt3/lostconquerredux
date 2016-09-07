using System;
using System.Collections.Generic;
using Redux.Enum;
using Redux.Managers;
using Redux.Packets.Game;
using Task = Redux.Structures.Task;

namespace Redux.Npcs
{
    public class NPC_1338 : INpc
    {
        public NPC_1338(Game_Server.Player _client)
            : base(_client)
        {
            ID = 1338;
            Face = 6;
        }

        public override void Run(Game_Server.Player _client, ushort _linkback)
        {
            Responses = new List<NpcDialogPacket>();
            AddAvatar();
            switch (_linkback)
            {
                case 0:
                    AddText("Hello, I'm the lottery controller. I can teleport you out of this place if you wish.");
                    AddOption("Yes, please.", 1);
                    AddOption("I want to stay.", 255);
                    break;
                case 1:
                    _client.ChangeMap(1036, 215, 188);
                    break;
            }
            AddFinish();
            Send();
        }
    }
}
