﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.Enums
{
	public enum GameOverReason
	{
		HumansByVote = 0,
		HumansByTask = 1,
		ImpostorByVote = 2,
		ImpostorByKill = 3,
		ImpostorBySabotage = 4,
		ImpostorDisconnect = 5,
		HumansDisconnect = 6,
	}
}
