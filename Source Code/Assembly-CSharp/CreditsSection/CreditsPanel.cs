using System;
using TaleWorlds.CompanionBook.ResourceSystem.CreditsResources;
using UnityEngine;

namespace TaleWorlds.CompanionBook.CreditsSection
{
	// Token: 0x02000064 RID: 100
	[RequireComponent(typeof(RectTransform))]
	public class CreditsPanel : MonoBehaviour
	{
		// Token: 0x06000316 RID: 790 RVA: 0x0000B477 File Offset: 0x00009677
		private void Awake()
		{
			this._rectTransform = base.GetComponent<RectTransform>();
			this._creditsNodeList = Game.Instance.ResourceProvider.CreditsResources.CreditsItems;
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000B4A0 File Offset: 0x000096A0
		private void Update()
		{
			bool flag = this._itemToAddIndex < this._creditsNodeList.Nodes.Count;
			float num = this._rectTransform.rect.height - this._rectTransform.localPosition.y;
			if (flag && num < 2500f)
			{
				int num2 = Mathf.Min(this._itemToAddIndex + 5, this._creditsNodeList.Nodes.Count);
				while (this._itemToAddIndex < num2)
				{
					this.AddNode(this._creditsNodeList.Nodes[this._itemToAddIndex]);
					this._itemToAddIndex++;
				}
			}
			RectTransform rectTransform;
			if (base.transform.childCount > 0 && base.transform.GetChild(0).TryGetComponent<RectTransform>(ref rectTransform) && this._rectTransform.localPosition.y > rectTransform.rect.height + 100f)
			{
				this.AddToScroll(-rectTransform.rect.height);
				Object.Destroy(base.transform.GetChild(0).gameObject);
			}
			this.HandleScroll();
			if (base.transform.childCount == 0 && !flag && this._creditsNodeList.Nodes.Count > 0)
			{
				this.Restart();
			}
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000B5F0 File Offset: 0x000097F0
		private void Restart()
		{
			this._itemToAddIndex = 0;
			this._scrollSpeed = 0f;
			this._scrollAnimationBeginTimer = 0f;
			this._scrollAnimationState = CreditsPanel.ScrollAnimationState.BeforeStart;
			this._scrollAnimation = null;
			this.AddToScroll(-this._rectTransform.localPosition.y - 2500f);
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000B648 File Offset: 0x00009848
		private void HandleScroll()
		{
			if (this._scrollAnimationState == CreditsPanel.ScrollAnimationState.BeforeStart)
			{
				this._scrollAnimationBeginTimer += Time.deltaTime;
				if (this._scrollAnimationBeginTimer > 6f)
				{
					this._scrollAnimationState = CreditsPanel.ScrollAnimationState.Transitioning;
					this._scrollAnimation = new AnimationProgress(4f, AnimationProgressType.EaseOutCirc);
				}
			}
			else if (this._scrollAnimationState == CreditsPanel.ScrollAnimationState.Transitioning)
			{
				this._scrollAnimation.Progress(Time.deltaTime);
				this._scrollSpeed = this._scrollAnimation.Progression * 150f;
				if (this._scrollAnimation.IsFinished)
				{
					this._scrollAnimationState = CreditsPanel.ScrollAnimationState.Finished;
				}
			}
			this.AddToScroll(this._scrollSpeed * Time.deltaTime);
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000B6EC File Offset: 0x000098EC
		private void AddToScroll(float scrollValue)
		{
			Vector3 localPosition = this._rectTransform.localPosition;
			localPosition.y += scrollValue;
			this._rectTransform.localPosition = localPosition;
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000B720 File Offset: 0x00009920
		private void AddNode(CreditsNode node)
		{
			GameObject gameObject = null;
			switch (node.Type)
			{
			case CreditsNodeType.Product:
				gameObject = this._productPrefab;
				break;
			case CreditsNodeType.Category:
				gameObject = this._categoryPrefab;
				break;
			case CreditsNodeType.Section:
				gameObject = this._sectionPrefab;
				break;
			case CreditsNodeType.Entry:
				gameObject = this._entryPrefab;
				break;
			case CreditsNodeType.Image:
				gameObject = this._imagePrefab;
				break;
			case CreditsNodeType.EmptyLine:
				gameObject = this._emptyLinePrefab;
				break;
			}
			CreditsNodeItem creditsNodeItem;
			if (gameObject != null && Object.Instantiate<GameObject>(gameObject, base.transform).TryGetComponent<CreditsNodeItem>(ref creditsNodeItem))
			{
				creditsNodeItem.Initialize(node.Value);
			}
		}

		// Token: 0x0400026B RID: 619
		[SerializeField]
		private GameObject _productPrefab;

		// Token: 0x0400026C RID: 620
		[SerializeField]
		private GameObject _categoryPrefab;

		// Token: 0x0400026D RID: 621
		[SerializeField]
		private GameObject _sectionPrefab;

		// Token: 0x0400026E RID: 622
		[SerializeField]
		private GameObject _entryPrefab;

		// Token: 0x0400026F RID: 623
		[SerializeField]
		private GameObject _emptyLinePrefab;

		// Token: 0x04000270 RID: 624
		[SerializeField]
		private GameObject _imagePrefab;

		// Token: 0x04000271 RID: 625
		private AnimationProgress _scrollAnimation;

		// Token: 0x04000272 RID: 626
		private CreditsPanel.ScrollAnimationState _scrollAnimationState;

		// Token: 0x04000273 RID: 627
		private float _scrollAnimationBeginTimer;

		// Token: 0x04000274 RID: 628
		private const float _scrollAnimationBeginDelay = 6f;

		// Token: 0x04000275 RID: 629
		private const float _scrollAnimationMaxSpeed = 150f;

		// Token: 0x04000276 RID: 630
		private const float _scrollAnimationDuration = 4f;

		// Token: 0x04000277 RID: 631
		private RectTransform _rectTransform;

		// Token: 0x04000278 RID: 632
		private float _scrollSpeed;

		// Token: 0x04000279 RID: 633
		private CreditsNodeList _creditsNodeList;

		// Token: 0x0400027A RID: 634
		private int _itemToAddIndex;

		// Token: 0x0400027B RID: 635
		private const int _itemCountToAddInOneFrame = 5;

		// Token: 0x0400027C RID: 636
		private const float _minRectTransformBottomToAddItems = 2500f;

		// Token: 0x0400027D RID: 637
		private const float _destroyItemOffset = 100f;

		// Token: 0x020000BB RID: 187
		private enum ScrollAnimationState
		{
			// Token: 0x04000402 RID: 1026
			BeforeStart,
			// Token: 0x04000403 RID: 1027
			Transitioning,
			// Token: 0x04000404 RID: 1028
			Finished
		}
	}
}
