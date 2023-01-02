using System;
using System.Collections.Generic;
using UnityEngine;

namespace TaleWorlds.CompanionBook.Soundtracks
{
	// Token: 0x02000022 RID: 34
	public interface ISoundtrack
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600011C RID: 284
		AudioClip Clip { get; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600011D RID: 285
		string Name { get; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600011E RID: 286
		string DurationText { get; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600011F RID: 287
		string InfoTextID { get; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000120 RID: 288
		IReadOnlyDictionary<string, string> InfoTextVariables { get; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000121 RID: 289
		string ID { get; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000122 RID: 290
		bool IsPlaying { get; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000123 RID: 291
		bool IsSelected { get; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000124 RID: 292
		// (set) Token: 0x06000125 RID: 293
		Action<bool> OnIsPlayingChanged { get; set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000126 RID: 294
		// (set) Token: 0x06000127 RID: 295
		Action<bool> OnIsSelectedChanged { get; set; }
	}
}
