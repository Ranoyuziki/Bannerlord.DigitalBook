using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TaleWorlds.CompanionBook.ResourceSystem.BookResources;
using TaleWorlds.CompanionBook.ResourceSystem.ConceptArtResources;
using TaleWorlds.CompanionBook.ResourceSystem.CreditsResources;
using TaleWorlds.CompanionBook.ResourceSystem.Localization;
using TaleWorlds.CompanionBook.ResourceSystem.MapResources;
using TaleWorlds.CompanionBook.ResourceSystem.SoundtrackResources;
using UnityEngine;
using UnityEngine.U2D;

namespace TaleWorlds.CompanionBook.ResourceSystem
{
	// Token: 0x0200002A RID: 42
	public class ResourceProvider : MonoBehaviour
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000167 RID: 359 RVA: 0x0000618C File Offset: 0x0000438C
		// (set) Token: 0x06000168 RID: 360 RVA: 0x00006194 File Offset: 0x00004394
		public bool IsLoading { get; private set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000169 RID: 361 RVA: 0x0000619D File Offset: 0x0000439D
		// (set) Token: 0x0600016A RID: 362 RVA: 0x000061A5 File Offset: 0x000043A5
		public float LoadingProgress { get; private set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600016B RID: 363 RVA: 0x000061AE File Offset: 0x000043AE
		// (set) Token: 0x0600016C RID: 364 RVA: 0x000061B6 File Offset: 0x000043B6
		public Book BookResources { get; private set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600016D RID: 365 RVA: 0x000061BF File Offset: 0x000043BF
		// (set) Token: 0x0600016E RID: 366 RVA: 0x000061C7 File Offset: 0x000043C7
		public SoundtrackClips SoundtrackResources { get; private set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600016F RID: 367 RVA: 0x000061D0 File Offset: 0x000043D0
		// (set) Token: 0x06000170 RID: 368 RVA: 0x000061D8 File Offset: 0x000043D8
		public ConceptArts ConceptArtsResources { get; private set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000171 RID: 369 RVA: 0x000061E1 File Offset: 0x000043E1
		// (set) Token: 0x06000172 RID: 370 RVA: 0x000061E9 File Offset: 0x000043E9
		public Credits CreditsResources { get; private set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000173 RID: 371 RVA: 0x000061F2 File Offset: 0x000043F2
		// (set) Token: 0x06000174 RID: 372 RVA: 0x000061FA File Offset: 0x000043FA
		public Map MapResources { get; private set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000175 RID: 373 RVA: 0x00006203 File Offset: 0x00004403
		public SpriteAtlas BindingsAtlas
		{
			get
			{
				return this._bindingsAtlas;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000176 RID: 374 RVA: 0x0000620B File Offset: 0x0000440B
		public SpriteAtlas CreditsLogos
		{
			get
			{
				return this._creditsLogos;
			}
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00006214 File Offset: 0x00004414
		private void Awake()
		{
			List<ValueTuple<IResource, float>> list = new List<ValueTuple<IResource, float>>();
			this.BookResources = new Book();
			this.SoundtrackResources = new SoundtrackClips();
			this.ConceptArtsResources = new ConceptArts();
			this.CreditsResources = new Credits();
			this.MapResources = new Map();
			this._localizationLoader = new LocalizationLoader();
			list.Add(new ValueTuple<IResource, float>(this.BookResources, 213f));
			list.Add(new ValueTuple<IResource, float>(this.SoundtrackResources, 38f));
			list.Add(new ValueTuple<IResource, float>(this.ConceptArtsResources, 175f));
			list.Add(new ValueTuple<IResource, float>(this.CreditsResources, 94f));
			list.Add(new ValueTuple<IResource, float>(this.MapResources, 100f));
			list.Add(new ValueTuple<IResource, float>(this._localizationLoader, 1f));
			this._coefficientSum = Enumerable.Sum(Enumerable.Select<ValueTuple<IResource, float>, float>(list, ([TupleElementNames(new string[]
			{
				"resource",
				"coefficient"
			})] ValueTuple<IResource, float> x) => x.Item2));
			this._resources = list;
			this.IsLoading = true;
		}

		// Token: 0x06000178 RID: 376 RVA: 0x0000632C File Offset: 0x0000452C
		private void Update()
		{
			if (this.IsLoading)
			{
				this.LoadingProgress = Enumerable.Sum<ValueTuple<IResource, float>>(this._resources, ([TupleElementNames(new string[]
				{
					"resource",
					"coefficient"
				})] ValueTuple<IResource, float> x) => x.Item1.GetLoadingProgress() * x.Item2) / this._coefficientSum;
				this.IsLoading = Enumerable.Any<ValueTuple<IResource, float>>(this._resources, ([TupleElementNames(new string[]
				{
					"resource",
					"coefficient"
				})] ValueTuple<IResource, float> x) => !x.Item1.IsLoaded);
				if (!this.IsLoading)
				{
					Action onPersistentResourcesLoadingFinished = this.OnPersistentResourcesLoadingFinished;
					if (onPersistentResourcesLoadingFinished == null)
					{
						return;
					}
					onPersistentResourcesLoadingFinished.Invoke();
				}
			}
		}

		// Token: 0x06000179 RID: 377 RVA: 0x000063C0 File Offset: 0x000045C0
		public static float GetDependentResourceLoadingProgress(bool isLoadedItself, IEnumerable<IResource> resourceDependencyList, int resourceCount, float selfCoefficient)
		{
			float num = isLoadedItself ? 1f : 0f;
			float num2 = 0f;
			if (resourceCount > 0)
			{
				num2 = Enumerable.Sum<IResource>(resourceDependencyList, (IResource x) => x.GetLoadingProgress()) / (float)resourceCount;
			}
			else if (isLoadedItself)
			{
				num2 = 1f;
			}
			return num * selfCoefficient + num2 * (1f - selfCoefficient);
		}

		// Token: 0x04000115 RID: 277
		public Action OnPersistentResourcesLoadingFinished;

		// Token: 0x04000116 RID: 278
		[SerializeField]
		private SpriteAtlas _bindingsAtlas;

		// Token: 0x04000117 RID: 279
		[SerializeField]
		private SpriteAtlas _creditsLogos;

		// Token: 0x04000118 RID: 280
		private LocalizationLoader _localizationLoader;

		// Token: 0x04000119 RID: 281
		[TupleElementNames(new string[]
		{
			"resource",
			"coefficient"
		})]
		private IReadOnlyList<ValueTuple<IResource, float>> _resources = new List<ValueTuple<IResource, float>>();

		// Token: 0x0400011A RID: 282
		private float _coefficientSum;
	}
}
