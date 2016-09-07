using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Redux.Packets.Game;

namespace Redux.Npcs
{
    /// <summary>
    /// Handles NPC usage for [300500] Eternity
    /// </summary>
    class NPC_300500 : INpc
    {
        public NPC_300500(Game_Server.Player _client)
            : base(_client)
        {
            ID = 300500;
            Face = 35;
        }

        public override void Run(Game_Server.Player _client, ushort _linkback)
        {
            Responses = new List<NpcDialogPacket>();
            AddAvatar();
            switch (_linkback)
            {
                case 0:
                    AddText("I don't do anything yet! Relyks is working on switching the Rebirth NPCs around to be the correct way.");
                    AddOption("I see.", 255);
                    break;
            }
            AddFinish();
            Send();

        }
    }
}
