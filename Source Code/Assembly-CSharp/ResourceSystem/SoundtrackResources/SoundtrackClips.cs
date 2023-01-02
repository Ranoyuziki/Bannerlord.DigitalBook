using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace TaleWorlds.CompanionBook.ResourceSystem.SoundtrackResources
{
	// Token: 0x0200002C RID: 44
	public class SoundtrackClips : IResource
	{
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600018B RID: 395 RVA: 0x0000654F File Offset: 0x0000474F
		public bool IsLoaded
		{
			get
			{
				if (this._isLoadedItself)
				{
					return Enumerable.All<SoundtrackClip>(this.Clips, (SoundtrackClip x) => x.IsLoaded);
				}
				return false;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00006585 File Offset: 0x00004785
		// (set) Token: 0x0600018D RID: 397 RVA: 0x0000658D File Offset: 0x0000478D
		public IReadOnlyList<SoundtrackClip> Clips { get; private set; } = new List<SoundtrackClip>();

		// Token: 0x0600018E RID: 398 RVA: 0x00006596 File Offset: 0x00004796
		public SoundtrackClips()
		{
			this.Initialize();
		}

		// Token: 0x0600018F RID: 399 RVA: 0x000065AF File Offset: 0x000047AF
		public float GetLoadingProgress()
		{
			return ResourceProvider.GetDependentResourceLoadingProgress(this._isLoadedItself, this.Clips, this.Clips.Count, 0.1f);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x000065D4 File Offset: 0x000047D4
		private void Initialize()
		{
			SoundtrackClips.<Initialize>d__10 <Initialize>d__;
			<Initialize>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<Initialize>d__.<>4__this = this;
			<Initialize>d__.<>1__state = -1;
			<Initialize>d__.<>t__builder.Start<SoundtrackClips.<Initialize>d__10>(ref <Initialize>d__);
		}

		// Token: 0x04000124 RID: 292
		private AsyncOperationHandle<TextAsset> _soundtrackDataHandle;

		// Token: 0x04000125 RID: 293
		private bool _isLoadedItself;
	}
}
