using System;

namespace TaleWorlds.CompanionBook.ResourceSystem
{
	// Token: 0x02000029 RID: 41
	public interface IResource
	{
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000165 RID: 357
		bool IsLoaded { get; }

		// Token: 0x06000166 RID: 358
		float GetLoadingProgress();
	}
}
