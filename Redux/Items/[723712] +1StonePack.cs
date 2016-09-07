using System;
using Redux.Game_Server;
using Redux.Structures;

namespace Redux.Items
{
    /// <summary>
    /// Handles item usage for [723712] +1StonePack
    /// </summary>
    public class Item_723712 : IItem
    {
        public override void Run(Player _client, ConquerItem _Item)
        {
            if (_client.Inventory.Count > 35)
                return;
            _client.DeleteItem(_Item);
            for (var i = 0; i < 5; i++)
                _client.CreateItem(Constants.STONE_ID);
        }
    }
}
