using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace TaleWorlds.CompanionBook.ResourceSystem.SoundtrackResources
{
	// Token: 0x0200002B RID: 43
	public class SoundtrackClip : IResource
	{
		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600017B RID: 379 RVA: 0x00006439 File Offset: 0x00004639
		public bool IsLoaded
		{
			get
			{
				return this._isLoaded;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600017C RID: 380 RVA: 0x00006441 File Offset: 0x00004641
		// (set) Token: 0x0600017D RID: 381 RVA: 0x00006449 File Offset: 0x00004649
		public AudioClip Clip { get; private set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600017E RID: 382 RVA: 0x00006452 File Offset: 0x00004652
		// (set) Token: 0x0600017F RID: 383 RVA: 0x0000645A File Offset: 0x0000465A
		public string Name { get; private set; } = string.Empty;

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000180 RID: 384 RVA: 0x00006463 File Offset: 0x00004663
		// (set) Token: 0x06000181 RID: 385 RVA: 0x0000646B File Offset: 0x0000466B
		public string ID { get; private set; } = string.Empty;

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000182 RID: 386 RVA: 0x00006474 File Offset: 0x00004674
		// (set) Token: 0x06000183 RID: 387 RVA: 0x0000647C File Offset: 0x0000467C
		public string DurationText { get; private set; } = string.Empty;

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000184 RID: 388 RVA: 0x00006485 File Offset: 0x00004685
		// (set) Token: 0x06000185 RID: 389 RVA: 0x0000648D File Offset: 0x0000468D
		public string InfoTextID { get; private set; } = string.Empty;

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000186 RID: 390 RVA: 0x00006496 File Offset: 0x00004696
		// (set) Token: 0x06000187 RID: 391 RVA: 0x0000649E File Offset: 0x0000469E
		public IReadOnlyDictionary<string, string> InfoTextVariables { get; private set; } = new Dictionary<string, string>();

		// Token: 0x06000188 RID: 392 RVA: 0x000064A8 File Offset: 0x000046A8
		public SoundtrackClip(XmlNode node)
		{
			this.Initialize(node);
		}

		// Token: 0x06000189 RID: 393 RVA: 0x000064F9 File Offset: 0x000046F9
		public float GetLoadingProgress()
		{
			if (!this._isLoaded)
			{
				return 0f;
			}
			return 1f;
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00006510 File Offset: 0x00004710
		private void Initialize(XmlNode xmlNode)
		{
			SoundtrackClip.<Initialize>d__30 <Initialize>d__;
			<Initialize>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<Initialize>d__.<>4__this = this;
			<Initialize>d__.xmlNode = xmlNode;
			<Initialize>d__.<>1__state = -1;
			<Initialize>d__.<>t__builder.Start<SoundtrackClip.<Initialize>d__30>(ref <Initialize>d__);
		}

		// Token: 0x04000121 RID: 289
		private AsyncOperationHandle<AudioClip> _clipHandle;

		// Token: 0x04000122 RID: 290
		private bool _isLoaded;
	}
}
