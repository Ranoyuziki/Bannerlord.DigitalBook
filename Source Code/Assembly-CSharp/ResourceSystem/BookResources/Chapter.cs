using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace TaleWorlds.CompanionBook.ResourceSystem.BookResources
{
	// Token: 0x0200003B RID: 59
	public class Chapter : IResource
	{
		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x00006EEB File Offset: 0x000050EB
		public bool IsLoaded
		{
			get
			{
				return this._isLoaded;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x00006EF3 File Offset: 0x000050F3
		// (set) Token: 0x060001E5 RID: 485 RVA: 0x00006EFB File Offset: 0x000050FB
		public int Index { get; private set; } = -1;

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x00006F04 File Offset: 0x00005104
		// (set) Token: 0x060001E7 RID: 487 RVA: 0x00006F0C File Offset: 0x0000510C
		public int FirstPageNumber { get; private set; } = -1;

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x00006F15 File Offset: 0x00005115
		// (set) Token: 0x060001E9 RID: 489 RVA: 0x00006F1D File Offset: 0x0000511D
		public AudioClip VoiceOverClip { get; private set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001EA RID: 490 RVA: 0x00006F26 File Offset: 0x00005126
		// (set) Token: 0x060001EB RID: 491 RVA: 0x00006F2E File Offset: 0x0000512E
		public Sprite Image { get; private set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001EC RID: 492 RVA: 0x00006F37 File Offset: 0x00005137
		// (set) Token: 0x060001ED RID: 493 RVA: 0x00006F3F File Offset: 0x0000513F
		public IReadOnlyList<Page> Pages { get; private set; } = new List<Page>();

		// Token: 0x060001EE RID: 494 RVA: 0x00006F48 File Offset: 0x00005148
		public Chapter(XmlNode node)
		{
			this.Initialize(node);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00006F70 File Offset: 0x00005170
		public float GetLoadingProgress()
		{
			if (!this._isLoaded)
			{
				return 0f;
			}
			return 1f;
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00006F88 File Offset: 0x00005188
		private void Initialize(XmlNode node)
		{
			Chapter.<Initialize>d__27 <Initialize>d__;
			<Initialize>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<Initialize>d__.<>4__this = this;
			<Initialize>d__.node = node;
			<Initialize>d__.<>1__state = -1;
			<Initialize>d__.<>t__builder.Start<Chapter.<Initialize>d__27>(ref <Initialize>d__);
		}

		// Token: 0x0400017B RID: 379
		private AsyncOperationHandle<AudioClip> _clipHandle;

		// Token: 0x0400017C RID: 380
		private AsyncOperationHandle<Sprite> _spriteHandle;

		// Token: 0x0400017D RID: 381
		private bool _isLoaded;
	}
}
