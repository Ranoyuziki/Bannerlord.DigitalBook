using System;
using System.Runtime.CompilerServices;
using System.Xml;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace TaleWorlds.CompanionBook.ResourceSystem.CreditsResources
{
	// Token: 0x02000031 RID: 49
	public class CompanyStoryItem : IResource
	{
		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00006841 File Offset: 0x00004A41
		public bool IsLoaded
		{
			get
			{
				return this._isLoaded;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001AB RID: 427 RVA: 0x00006849 File Offset: 0x00004A49
		// (set) Token: 0x060001AC RID: 428 RVA: 0x00006851 File Offset: 0x00004A51
		public Sprite MainImage { get; private set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001AD RID: 429 RVA: 0x0000685A File Offset: 0x00004A5A
		// (set) Token: 0x060001AE RID: 430 RVA: 0x00006862 File Offset: 0x00004A62
		public Sprite BottomImage { get; private set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001AF RID: 431 RVA: 0x0000686B File Offset: 0x00004A6B
		// (set) Token: 0x060001B0 RID: 432 RVA: 0x00006873 File Offset: 0x00004A73
		public CompanyStoryItemSize Size { get; private set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x0000687C File Offset: 0x00004A7C
		// (set) Token: 0x060001B2 RID: 434 RVA: 0x00006884 File Offset: 0x00004A84
		public string Text { get; private set; } = string.Empty;

		// Token: 0x060001B3 RID: 435 RVA: 0x0000688D File Offset: 0x00004A8D
		public CompanyStoryItem(XmlNode node)
		{
			this.Initialize(node);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x000068A7 File Offset: 0x00004AA7
		public float GetLoadingProgress()
		{
			if (!this._isLoaded)
			{
				return 0f;
			}
			return 1f;
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x000068BC File Offset: 0x00004ABC
		private void Initialize(XmlNode node)
		{
			CompanyStoryItem.<Initialize>d__23 <Initialize>d__;
			<Initialize>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<Initialize>d__.<>4__this = this;
			<Initialize>d__.node = node;
			<Initialize>d__.<>1__state = -1;
			<Initialize>d__.<>t__builder.Start<CompanyStoryItem.<Initialize>d__23>(ref <Initialize>d__);
		}

		// Token: 0x04000143 RID: 323
		private bool _isLoaded;

		// Token: 0x04000144 RID: 324
		private AsyncOperationHandle<Sprite> _mainImageHandle;

		// Token: 0x04000145 RID: 325
		private AsyncOperationHandle<Sprite> _bottomImageHandle;
	}
}
