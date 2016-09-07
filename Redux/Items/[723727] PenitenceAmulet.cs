using System;
using Redux.Game_Server;
using Redux.Structures;
using Redux.Enum;
using Redux.Packets.Game;
namespace Redux.Items
{
    /// <summary>
    /// Handles item usage for [723727] PenitenceAmulet
    /// </summary>
    public class Item_723727 : IItem
    {
        public override void Run(Player _client, ConquerItem _item)
        {
            if (_client.PK < 30)
            {
                _client.SendMessage("You must have 30 Pk Points or more to use it.");
            }
            else
            {
                _client.PK -= 30;
                _client.Send(UpdatePacket.Create(_client.UID, UpdateType.Pk, (ulong)_client.PK));
                _client.Save();
                _client.DeleteItem(_item);
                //If Between 30 and 99 and does not have Red Name.....then Add redname
                if (_client.PK >= 30 && _client.PK < 100 && !_client.HasEffect(ClientEffect.Red))
                {
                    _client.RemoveEffect(ClientEffect.Black);
                    _client.AddEffect(ClientEffect.Red, ((_client.PK - 29) * 6) * 60000, true);//Adds red name
                }

                //If under 30 PK, remove redname
                if (_client.PK < 30 && _client.HasEffect(ClientEffect.Red))
                    _client.RemoveEffect(ClientEffect.Red);
            }
        }
    }
}
