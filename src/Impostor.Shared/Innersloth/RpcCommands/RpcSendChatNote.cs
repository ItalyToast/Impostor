﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.RpcCommands
{
    public class RpcSendChatNote
    {
        public byte b1;
        public byte b2;
        public static RpcSendChatNote Deserialize(HazelBinaryReader reader)
        {
            var msg = new RpcSendChatNote();

            msg.b1 = reader.ReadByte();
            msg.b2 = reader.ReadByte();

            return msg;
        }
    }
}
