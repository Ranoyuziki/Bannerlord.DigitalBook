using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace TaleWorlds.CompanionBook.ResourceSystem.BookResources
{
	// Token: 0x0200003A RID: 58
	public class Book : IResource
	{
		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001DD RID: 477 RVA: 0x00006E2F File Offset: 0x0000502F
		public bool IsLoaded
		{
			get
			{
				if (this._isLoadedItself)
				{
					return Enumerable.All<Chapter>(this.Chapters, (Chapter x) => x.IsLoaded);
				}
				return false;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001DE RID: 478 RVA: 0x00006E65 File Offset: 0x00005065
		// (set) Token: 0x060001DF RID: 479 RVA: 0x00006E6D File Offset: 0x0000506D
		public IReadOnlyList<Chapter> Chapters { get; private set; } = new List<Chapter>();

		// Token: 0x060001E0 RID: 480 RVA: 0x00006E76 File Offset: 0x00005076
		public Book()
		{
			this.Initialize();
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00006E8F File Offset: 0x0000508F
		public float GetLoadingProgress()
		{
			return ResourceProvider.GetDependentResourceLoadingProgress(this._isLoadedItself, this.Chapters, this.Chapters.Count, 0.1f);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00006EB4 File Offset: 0x000050B4
		private void Initialize()
		{
			Book.<Initialize>d__10 <Initialize>d__;
			<Initialize>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<Initialize>d__.<>4__this = this;
			<Initialize>d__.<>1__state = -1;
			<Initialize>d__.<>t__builder.Start<Book.<Initialize>d__10>(ref <Initialize>d__);
		}

		// Token: 0x04000174 RID: 372
		private AsyncOperationHandle<TextAsset> _bookDataHandle;

		// Token: 0x04000175 RID: 373
		private bool _isLoadedItself;
	}
}
