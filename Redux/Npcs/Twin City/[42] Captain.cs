using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Redux.Packets.Game;

namespace Redux.Npcs
{
    /// <summary>
    /// Handles NPC usage for [42] Captain
    /// </summary>
    public class NPC_42 : INpc
    {

        public NPC_42(Game_Server.Player _client)
            :base (_client)
    	{
    		ID = 42;	
			Face = 37;    
    	}

        public override void Run(Game_Server.Player _client, ushort _linkback)
        {
            Responses = new List<NpcDialogPacket>();
            AddAvatar();
            switch (_linkback)
            {
                case 0:
                    {
                        AddText("If you aren't an evildoer, you can leave here.");
                        AddOption("Let me out of here!", 1);
                        AddOption("I see.", 255);
                        break;
                    }

                case 1:
                    if (_client.PK >= 100)
                    {
                        AddText("No, you will stay here until you have regretted your sins or you can do some work and mine some gold ores.");
                        AddText("Mine 5 Gold Ores with a rate of 4 or higher and I will let you leave.");
                        AddOption("Lend me a hoe please.", 3);
                        AddOption("I have the ores.", 4);
                        AddOption("I see.", 255);
                        break;
                    }
                    else
                    {
                        _client.ChangeMap(1002, 512, 355);
                        break;
                    }

                case 3:
                    {
                        if (_client.HasItem(562001, 1)) // Check if the client has a Hoe in his/her inventory.
                        {
                            AddText("You already have a Hoe. I cannot give you another one.");
                            AddOption("I see.", 255);
                            break;
                        }
                        else
                        {
                            if (!_client.HasItem(562001, 1)) // Second check. If a player does not have a Hoe, give them one. :)
                                _client.CreateItem(562001);
                            break;
                        }
                    }

                case 4:
                    var GoldOres =                                          // "GoldOres" Cursor.
                        from Item                                           // "Item" Secondary cursor refers to _client.Inventory.Values
                        in _client.Inventory.Values                         // That's where you need the Secondary cursor to point.
                        where Item.StaticID / 10 == 107205                  // Secondary cursor.StaticID for the item you have to put in the item ID without the quality 10 == refers to the quality.
                        where Item.StaticID % 10 >= 3                       // "% 10 >= 3" refers to the quality.
                        select Item;                                        // Every Query Link should end up with the "select" then secondary cursor.

                    if (GoldOres.Count() >= 5)
                    {
                        for (var i = 0; i < 5; i++)                         // Start our loop for removing the Ores 
                            _client.DeleteItem(GoldOres.ElementAt(i));      // Now actually remove the ores.

                        _client.ChangeMap(1002, 512, 355);
                        break;
                    }
                    else
                    {
                        AddText("Don't try to fool me! You do not have 5 Gold Ores of rate 4 or higher!");
                        AddOption("Sorry! I'll get back to work.", 255);
                        break;
                    }
            }
            AddFinish();
            Send();
        }
    }
}
