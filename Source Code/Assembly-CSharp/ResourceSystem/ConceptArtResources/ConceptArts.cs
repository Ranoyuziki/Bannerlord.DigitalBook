using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace TaleWorlds.CompanionBook.ResourceSystem.ConceptArtResources
{
	// Token: 0x02000039 RID: 57
	public class ConceptArts : IResource
	{
		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x00006D6B File Offset: 0x00004F6B
		public bool IsLoaded
		{
			get
			{
				if (this._isLoadedItself)
				{
					return Enumerable.All<ConceptArtFaction>(this.Factions.Values, (ConceptArtFaction x) => x.IsLoaded);
				}
				return false;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x00006DA6 File Offset: 0x00004FA6
		// (set) Token: 0x060001D9 RID: 473 RVA: 0x00006DAE File Offset: 0x00004FAE
		public IReadOnlyDictionary<ConceptArtFactionType, ConceptArtFaction> Factions { get; private set; } = new Dictionary<ConceptArtFactionType, ConceptArtFaction>();

		// Token: 0x060001DA RID: 474 RVA: 0x00006DB7 File Offset: 0x00004FB7
		public ConceptArts()
		{
			this.Initialize();
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00006DD0 File Offset: 0x00004FD0
		public float GetLoadingProgress()
		{
			return ResourceProvider.GetDependentResourceLoadingProgress(this._isLoadedItself, this.Factions.Values, this.Factions.Count, 0.1f);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00006DF8 File Offset: 0x00004FF8
		private void Initialize()
		{
			ConceptArts.<Initialize>d__10 <Initialize>d__;
			<Initialize>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<Initialize>d__.<>4__this = this;
			<Initialize>d__.<>1__state = -1;
			<Initialize>d__.<>t__builder.Start<ConceptArts.<Initialize>d__10>(ref <Initialize>d__);
		}

		// Token: 0x04000171 RID: 369
		private AsyncOperationHandle<TextAsset> _conceptArtDataHandle;

		// Token: 0x04000172 RID: 370
		private bool _isLoadedItself;
	}
}
