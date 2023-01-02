using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TaleWorlds.CompanionBook
{
	// Token: 0x02000014 RID: 20
	[RequireComponent(typeof(RectTransform))]
	[RequireComponent(typeof(ScrollRect))]
	public class NavigationAutoScroll : MonoBehaviour
	{
		// Token: 0x0600009C RID: 156 RVA: 0x00003BD4 File Offset: 0x00001DD4
		private void Awake()
		{
			this._rectTransform = base.GetComponent<RectTransform>();
			this._scrollRect = base.GetComponent<ScrollRect>();
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003BF0 File Offset: 0x00001DF0
		private void Update()
		{
			GameObject currentSelectedGameObject = EventSystem.current.currentSelectedGameObject;
			if (currentSelectedGameObject != this._selected)
			{
				this._selected = currentSelectedGameObject;
				this.OnEventSystemSelectedObjectChanged(currentSelectedGameObject);
			}
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003C24 File Offset: 0x00001E24
		private void OnEventSystemSelectedObjectChanged(GameObject selected)
		{
			if (Game.Instance.IsCurrentSchemeGamepad && this._scrollRect.content != null && selected != null)
			{
				using (IEnumerator enumerator = this._scrollRect.content.transform.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						NavigationAutoScrollItem navigationAutoScrollItem;
						if (((Transform)enumerator.Current).TryGetComponent<NavigationAutoScrollItem>(ref navigationAutoScrollItem) && navigationAutoScrollItem.NavigationTarget == selected)
						{
							float height = this._rectTransform.rect.height;
							Vector3 localPosition = this._scrollRect.content.localPosition;
							RectTransform rectTransform = navigationAutoScrollItem.RectTransform;
							float num = rectTransform.rect.height * 0.5f;
							float y = rectTransform.localPosition.y;
							float num2 = y + num;
							float num3 = y - num;
							float num4 = num2 + localPosition.y;
							float num5 = num3 + localPosition.y;
							float num6 = 0f;
							if (num4 > 0f)
							{
								num6 = -num4;
							}
							else if (-num5 > height)
							{
								num6 = -num5 - height;
							}
							localPosition.y += num6;
							this._scrollRect.content.localPosition = localPosition;
						}
					}
				}
			}
		}

		// Token: 0x04000065 RID: 101
		private RectTransform _rectTransform;

		// Token: 0x04000066 RID: 102
		private ScrollRect _scrollRect;

		// Token: 0x04000067 RID: 103
		private GameObject _selected;
	}
}
