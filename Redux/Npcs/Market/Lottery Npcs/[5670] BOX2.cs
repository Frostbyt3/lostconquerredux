﻿using System;
using System.Collections.Generic;
using Redux.Enum;
using Redux.Managers;
using Redux.Packets.Game;
using Task = Redux.Structures.Task;

namespace Redux.Npcs
{
    public class NPC_5670 : INpc
    {

        public NPC_5670(Game_Server.Player _client)
            : base(_client)
        {
            ID = 5670;
            Face = 6;
        }

        public override void Run(Game_Server.Player _client, ushort _linkback)
        {
            Responses = new List<NpcDialogPacket>();
            AddAvatar();
            switch (_linkback)
            {
                case 0:
                    {
                        if (!_client.Tasks.ContainsKey(TaskType.Lottery))
                            _client.Tasks.TryAdd(TaskType.Lottery, new Task(_client.UID, TaskType.Lottery,
                           DateTime.Now.AddHours(24)));
                        if (_client.Tasks[TaskType.Lottery].Count >= 10)
                        {
                            AddText("I'm afraid you already played the lottery 10 times today");
                            AddOption("i was just getting started!", 255);
                            break;
                        }
                        else if (!_client.HasItem(710212))
                        {
                            AddText("Sorry i can't help you unless you buy a ticket!");
                            AddOption("No thanks", 255);
                        }
                        else
                        {
                            _client.Tasks[TaskType.Lottery].Count++;
                            _client.DeleteItem(710212);
                            var ItemInfo = Common.QurryLotteryItem();
                            var item = ItemInfo.Item1;
                            item.SetOwner(_client);
                            if (_client.AddItem(item))
                                _client.Send(ItemInformationPacket.Create(item));
                            else
                                _client.SendMessage("Error adding item"); 
                            string pre = "";
                            switch (ItemInfo.Item3)
                            {
                                case LotteryItemType.Elite1Socket:
                                    pre = " Elite 1 socket ";
                                    break;
                                case LotteryItemType.Elite2Socket:
                                    pre = " Elite 2 socket ";
                                    break;
                                case LotteryItemType.ElitePlus8:
                                    pre = " Elite +8 ";
                                    break;
                                case LotteryItemType.Super1Socket:
                                    pre = " Super 1 socket ";
                                    break;
                                case LotteryItemType.SuperNoSocket:
                                    pre = " Super ";
                                    break;
                            }
                            PlayerManager.SendToServer(new TalkPacket(ChatType.GM, _client.Name + " was so lucky and won a/an " + pre + ItemInfo.Item2 + " in the lottery"));
                        }
                        break;
                    }
            }
            AddFinish();
            Send();

        }
    }
}
