using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impostor.Shared.Innersloth.Enums
{
	public enum JoinFailureReasons
	{
		TooManyPlayers = 1,
		GameStarted = 2,
		GameNotFound = 3,
		HostNotReady = 4,
		IncorrectVersion = 5,
		Banned = 6,
		Kicked = 7,
	}
}
