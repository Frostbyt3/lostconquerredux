using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Redux.Packets.Game;

namespace Redux.Npcs
{

    /// <summary>
    /// Handles NPC usage for [923] LadyLuck
    /// </summary>
    public class NPC_923 : INpc
    {

        public NPC_923(Game_Server.Player _client)
            : base(_client)
        {
            ID = 923;
            Face = 3;
        }

        public override void Run(Game_Server.Player _client, ushort _linkback)
        {
            Responses = new List<NpcDialogPacket>();
            AddAvatar();
            switch (_linkback)
            {
                /*case 0:
                    AddText("The lottery system is currently under development. Please be patient!");
                    AddOption("Okay, I'll wait.", 255);
                    break;*/
                case 0:
                    AddText("Hello! Have you heard about the lottery? We have amazing");
                    AddText(" rewards waiting for you! All you need is 1 lottery ticket.");
                    AddText(" Each lottery ticket only costs 215 CPs.");
                    AddOption("Let me try my luck!", 2);
                    AddOption("Buy 1 Lottery Ticket (215 CPs)", 1);
                    AddOption("Buy 10 Lottery Tickets (2150 CPs)", 10);
                    AddOption("No thanks.", 255);
                    break;
                case 2:
                    _client.ChangeMap(700, 40, 49);
                    break;
                case 1:
                case 10:
                    if (_client.CP >= _linkback * 215)
                    {
                        _client.CP -= (uint)(_linkback * 215);
                        for (int i = 0; i < _linkback; i++)
                            _client.CreateItem(710212);
                    }
                    break;
            }
            AddFinish();
            Send();

        }
    }
}
