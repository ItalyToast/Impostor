using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.InnerNetComponents
{
    public class MeetingHud : InnerNetObject
    {
		public class PlayerVoteArea
		{
			public byte value;
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
						value = reader.ReadByte(),
					});
                }
				//this.PopulateButtons(0);
				//for (int i = 0; i < this.playerStates.Length; i++)
				//{
				//	PlayerVoteArea playerVoteArea = this.playerStates[i];
				//	playerVoteArea.Deserialize(reader);
				//	if (playerVoteArea.didReport)
				//	{
				//		this.reporterId = (byte)playerVoteArea.TargetPlayerId;
				//	}
				//}
				return;
			}
			Console.WriteLine("Meetinghud data");
			uint updateMask = reader.ReadPackedUInt32();
            for (int j = 0; j < 10; j++)
            {
                if ((updateMask & (1u << j)) != 0u)
                {
                    playerStates[j].value = reader.ReadByte();
                }
            }
        }
    }
}
