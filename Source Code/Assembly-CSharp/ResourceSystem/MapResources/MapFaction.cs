using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace TaleWorlds.CompanionBook.ResourceSystem.MapResources
{
	// Token: 0x0200002E RID: 46
	public class MapFaction : IResource
	{
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600019B RID: 411 RVA: 0x000066F3 File Offset: 0x000048F3
		public bool IsLoaded
		{
			get
			{
				return this._isLoaded;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600019C RID: 412 RVA: 0x000066FB File Offset: 0x000048FB
		// (set) Token: 0x0600019D RID: 413 RVA: 0x00006703 File Offset: 0x00004903
		public Sprite Border { get; private set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600019E RID: 414 RVA: 0x0000670C File Offset: 0x0000490C
		// (set) Token: 0x0600019F RID: 415 RVA: 0x00006714 File Offset: 0x00004914
		public Sprite Glow { get; private set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x0000671D File Offset: 0x0000491D
		// (set) Token: 0x060001A1 RID: 417 RVA: 0x00006725 File Offset: 0x00004925
		public MapFactionType Faction { get; private set; } = MapFactionType.None;

		// Token: 0x060001A2 RID: 418 RVA: 0x0000672E File Offset: 0x0000492E
		public MapFaction(MapFactionType faction)
		{
			this.Faction = faction;
			this.Initialize();
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0000674A File Offset: 0x0000494A
		public float GetLoadingProgress()
		{
			if (!this._isLoaded)
			{
				return 0f;
			}
			return 1f;
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00006760 File Offset: 0x00004960
		private void Initialize()
		{
			MapFaction.<Initialize>d__19 <Initialize>d__;
			<Initialize>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<Initialize>d__.<>4__this = this;
			<Initialize>d__.<>1__state = -1;
			<Initialize>d__.<>t__builder.Start<MapFaction.<Initialize>d__19>(ref <Initialize>d__);
		}

		// Token: 0x0400012F RID: 303
		private AsyncOperationHandle<Sprite> _borderHandle;

		// Token: 0x04000130 RID: 304
		private AsyncOperationHandle<Sprite> _glowHandle;

		// Token: 0x04000131 RID: 305
		private bool _isLoaded;
	}
}
