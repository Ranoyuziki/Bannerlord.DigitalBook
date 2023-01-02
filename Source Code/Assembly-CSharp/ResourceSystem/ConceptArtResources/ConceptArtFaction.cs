using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace TaleWorlds.CompanionBook.ResourceSystem.ConceptArtResources
{
	// Token: 0x02000037 RID: 55
	public class ConceptArtFaction : IResource
	{
		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001CB RID: 459 RVA: 0x00006CB0 File Offset: 0x00004EB0
		public bool IsLoaded
		{
			get
			{
				return this._isLoaded;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001CC RID: 460 RVA: 0x00006CB8 File Offset: 0x00004EB8
		// (set) Token: 0x060001CD RID: 461 RVA: 0x00006CC0 File Offset: 0x00004EC0
		public ConceptArtFactionType Faction { get; private set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001CE RID: 462 RVA: 0x00006CC9 File Offset: 0x00004EC9
		// (set) Token: 0x060001CF RID: 463 RVA: 0x00006CD1 File Offset: 0x00004ED1
		public Sprite DefaultCard { get; private set; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x00006CDA File Offset: 0x00004EDA
		// (set) Token: 0x060001D1 RID: 465 RVA: 0x00006CE2 File Offset: 0x00004EE2
		public Sprite HoveredCard { get; private set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x00006CEB File Offset: 0x00004EEB
		// (set) Token: 0x060001D3 RID: 467 RVA: 0x00006CF3 File Offset: 0x00004EF3
		public IReadOnlyList<string> ImageNames { get; private set; } = new List<string>();

		// Token: 0x060001D4 RID: 468 RVA: 0x00006CFC File Offset: 0x00004EFC
		public ConceptArtFaction(XmlNode node)
		{
			this.Initialize(node);
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00006D16 File Offset: 0x00004F16
		public float GetLoadingProgress()
		{
			if (!this._isLoaded)
			{
				return 0f;
			}
			return 1f;
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00006D2C File Offset: 0x00004F2C
		private void Initialize(XmlNode node)
		{
			ConceptArtFaction.<Initialize>d__23 <Initialize>d__;
			<Initialize>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<Initialize>d__.<>4__this = this;
			<Initialize>d__.node = node;
			<Initialize>d__.<>1__state = -1;
			<Initialize>d__.<>t__builder.Start<ConceptArtFaction.<Initialize>d__23>(ref <Initialize>d__);
		}

		// Token: 0x04000166 RID: 358
		private AsyncOperationHandle<Sprite> _defaultCardHandle;

		// Token: 0x04000167 RID: 359
		private AsyncOperationHandle<Sprite> _hoveredCardHandle;

		// Token: 0x04000168 RID: 360
		private bool _isLoaded;
	}
}
