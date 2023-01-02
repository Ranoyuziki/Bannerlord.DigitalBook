using System;
using System.Collections.Generic;

namespace TaleWorlds.CompanionBook.Soundtracks
{
	// Token: 0x02000021 RID: 33
	public interface IPlaylist
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600011B RID: 283
		IReadOnlyList<ISoundtrack> Soundtracks { get; }
	}
}
