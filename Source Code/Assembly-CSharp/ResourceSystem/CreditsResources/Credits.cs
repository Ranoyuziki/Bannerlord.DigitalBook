using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace TaleWorlds.CompanionBook.ResourceSystem.CreditsResources
{
	// Token: 0x02000033 RID: 51
	public class Credits : IResource
	{
		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x000068FB File Offset: 0x00004AFB
		public bool IsLoaded
		{
			get
			{
				if (this._isLoadedItself)
				{
					return Enumerable.All<CompanyStoryItem>(this.CompanyStoryItems, (CompanyStoryItem x) => x.IsLoaded);
				}
				return false;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x00006931 File Offset: 0x00004B31
		// (set) Token: 0x060001B8 RID: 440 RVA: 0x00006939 File Offset: 0x00004B39
		public CreditsNodeList CreditsItems { get; private set; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x00006942 File Offset: 0x00004B42
		// (set) Token: 0x060001BA RID: 442 RVA: 0x0000694A File Offset: 0x00004B4A
		public IReadOnlyList<CompanyStoryItem> CompanyStoryItems { get; private set; } = new List<CompanyStoryItem>();

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001BB RID: 443 RVA: 0x00006953 File Offset: 0x00004B53
		// (set) Token: 0x060001BC RID: 444 RVA: 0x0000695B File Offset: 0x00004B5B
		public Sprite SmallMask { get; private set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001BD RID: 445 RVA: 0x00006964 File Offset: 0x00004B64
		// (set) Token: 0x060001BE RID: 446 RVA: 0x0000696C File Offset: 0x00004B6C
		public Sprite MediumMask { get; private set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001BF RID: 447 RVA: 0x00006975 File Offset: 0x00004B75
		// (set) Token: 0x060001C0 RID: 448 RVA: 0x0000697D File Offset: 0x00004B7D
		public Sprite LargeMask { get; private set; }

		// Token: 0x060001C1 RID: 449 RVA: 0x00006986 File Offset: 0x00004B86
		public Credits()
		{
			this.Initialize();
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000699F File Offset: 0x00004B9F
		public float GetLoadingProgress()
		{
			return ResourceProvider.GetDependentResourceLoadingProgress(this._isLoadedItself, this.CompanyStoryItems, this.CompanyStoryItems.Count, 0.1f);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x000069C4 File Offset: 0x00004BC4
		private void Initialize()
		{
			Credits.<Initialize>d__30 <Initialize>d__;
			<Initialize>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<Initialize>d__.<>4__this = this;
			<Initialize>d__.<>1__state = -1;
			<Initialize>d__.<>t__builder.Start<Credits.<Initialize>d__30>(ref <Initialize>d__);
		}

		// Token: 0x0400014F RID: 335
		private AsyncOperationHandle<TextAsset> _creditsDataHandle;

		// Token: 0x04000150 RID: 336
		private AsyncOperationHandle<TextAsset> _companyStoryDataHandle;

		// Token: 0x04000151 RID: 337
		private AsyncOperationHandle<Sprite> _smallMaskHandle;

		// Token: 0x04000152 RID: 338
		private AsyncOperationHandle<Sprite> _mediumMaskHandle;

		// Token: 0x04000153 RID: 339
		private AsyncOperationHandle<Sprite> _largeMaskHandle;

		// Token: 0x04000154 RID: 340
		private bool _isLoadedItself;
	}
}
