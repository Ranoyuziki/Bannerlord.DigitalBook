using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace TaleWorlds.CompanionBook.ResourceSystem.MapResources
{
	// Token: 0x0200002D RID: 45
	public class Map : IResource
	{
		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000191 RID: 401 RVA: 0x0000660B File Offset: 0x0000480B
		public bool IsLoaded
		{
			get
			{
				if (this._isLoadedItself)
				{
					return Enumerable.All<MapFaction>(this.Factions.Values, (MapFaction x) => x.IsLoaded);
				}
				return false;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000192 RID: 402 RVA: 0x00006646 File Offset: 0x00004846
		// (set) Token: 0x06000193 RID: 403 RVA: 0x0000664E File Offset: 0x0000484E
		public Sprite MapImage { get; private set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000194 RID: 404 RVA: 0x00006657 File Offset: 0x00004857
		// (set) Token: 0x06000195 RID: 405 RVA: 0x0000665F File Offset: 0x0000485F
		public Sprite MapFrame { get; private set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00006668 File Offset: 0x00004868
		// (set) Token: 0x06000197 RID: 407 RVA: 0x00006670 File Offset: 0x00004870
		public IReadOnlyDictionary<MapFactionType, MapFaction> Factions { get; private set; } = new Dictionary<MapFactionType, MapFaction>();

		// Token: 0x06000198 RID: 408 RVA: 0x00006679 File Offset: 0x00004879
		public Map()
		{
			this.Initialize();
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00006692 File Offset: 0x00004892
		public float GetLoadingProgress()
		{
			return ResourceProvider.GetDependentResourceLoadingProgress(this._isLoadedItself, this.Factions.Values, this.Factions.Count, 0.5f);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x000066BC File Offset: 0x000048BC
		private void Initialize()
		{
			Map.<Initialize>d__19 <Initialize>d__;
			<Initialize>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<Initialize>d__.<>4__this = this;
			<Initialize>d__.<>1__state = -1;
			<Initialize>d__.<>t__builder.Start<Map.<Initialize>d__19>(ref <Initialize>d__);
		}

		// Token: 0x04000129 RID: 297
		private AsyncOperationHandle<Sprite> _mapImageHandle;

		// Token: 0x0400012A RID: 298
		private AsyncOperationHandle<Sprite> _mapFrameHandle;

		// Token: 0x0400012B RID: 299
		private bool _isLoadedItself;
	}
}
