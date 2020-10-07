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

		}
		public override void Deserialize(HazelBinaryReader reader, bool onSpawn)
        {
			if (onSpawn)
			{
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
				//return;
			}
			//uint num = reader.ReadPackedUInt32();
			//for (int j = 0; j < this.playerStates.Length; j++)
			//{
			//	if ((num & 1u << j) != 0u)
			//	{
			//		this.playerStates[j].Deserialize(reader);
			//	}
			//}
		}

        void PopulateButtons(int v)
        {
        }
    }
}
