using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TaleWorlds.CompanionBook.MapSection
{
	// Token: 0x02000057 RID: 87
	public class MapInputEventProvider : MonoBehaviour, IDragHandler, IEventSystemHandler, IBeginDragHandler, IEndDragHandler, IScrollHandler, IPointerDownHandler
	{
		// Token: 0x060002B5 RID: 693 RVA: 0x00009B20 File Offset: 0x00007D20
		void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
		{
			Action onBeginDrag = this.OnBeginDrag;
			if (onBeginDrag == null)
			{
				return;
			}
			onBeginDrag.Invoke();
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x00009B32 File Offset: 0x00007D32
		void IDragHandler.OnDrag(PointerEventData eventData)
		{
			Action<Vector2> onDrag = this.OnDrag;
			if (onDrag == null)
			{
				return;
			}
			onDrag.Invoke(eventData.delta);
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x00009B4A File Offset: 0x00007D4A
		void IEndDragHandler.OnEndDrag(PointerEventData eventData)
		{
			Action onEndDrag = this.OnEndDrag;
			if (onEndDrag == null)
			{
				return;
			}
			onEndDrag.Invoke();
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x00009B5C File Offset: 0x00007D5C
		void IScrollHandler.OnScroll(PointerEventData eventData)
		{
			if (eventData.IsScrolling())
			{
				Action<float> onScroll = this.OnScroll;
				if (onScroll == null)
				{
					return;
				}
				onScroll.Invoke(eventData.scrollDelta.y);
			}
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x00009B81 File Offset: 0x00007D81
		void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
		{
			Action onPointerDown = this.OnPointerDown;
			if (onPointerDown == null)
			{
				return;
			}
			onPointerDown.Invoke();
		}

		// Token: 0x04000210 RID: 528
		public Action OnBeginDrag;

		// Token: 0x04000211 RID: 529
		public Action<Vector2> OnDrag;

		// Token: 0x04000212 RID: 530
		public Action OnEndDrag;

		// Token: 0x04000213 RID: 531
		public Action<float> OnScroll;

		// Token: 0x04000214 RID: 532
		public Action OnPointerDown;
	}
}
