using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.InnerNetComponents
{
    public class MeetingHud : InnerNetObject
    {
		[Flags]
		public enum PlayerVoteAreaFlags
        {
			VOTEDFOR_MASK = 0xF,
			DID_REPORT = 0x20,
			DID_VOTE = 0x40,
			IS_DEAD = 0x80,
		}

		public class PlayerVoteArea
		{
			public PlayerVoteAreaFlags value;
		}

		public List<PlayerVoteArea> playerStates = new List<PlayerVoteArea>();

		public override void Deserialize(HazelBinaryReader reader, bool onSpawn)
        {
			if (onSpawn)
			{
				Console.WriteLine("Meetinghud spawn");
                for (int i = 0; i < 10; i++)
                {
					playerStates.Add(new PlayerVoteArea()
					{
						value = (PlayerVoteAreaFlags)reader.ReadByte(),
					});
                }
				return;
			}
			Console.WriteLine("Meetinghud data");
			uint updateMask = reader.ReadPackedUInt32();
            for (int j = 0; j < 10; j++)
            {
                if ((updateMask & (1u << j)) != 0u)
                {
                    playerStates[j].value = (PlayerVoteAreaFlags)reader.ReadByte();
                }
            }
        }
    }
}
