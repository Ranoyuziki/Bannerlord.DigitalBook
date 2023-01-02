using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

namespace TaleWorlds.CompanionBook
{
	// Token: 0x02000010 RID: 16
	public static class Helper
	{
		// Token: 0x06000078 RID: 120 RVA: 0x00003208 File Offset: 0x00001408
		public static string ToRoman(int number)
		{
			if (number >= 0)
			{
			}
			if (number < 1)
			{
				return string.Empty;
			}
			if (number >= 1000)
			{
				return "M" + Helper.ToRoman(number - 1000);
			}
			if (number >= 900)
			{
				return "CM" + Helper.ToRoman(number - 900);
			}
			if (number >= 500)
			{
				return "D" + Helper.ToRoman(number - 500);
			}
			if (number >= 400)
			{
				return "CD" + Helper.ToRoman(number - 400);
			}
			if (number >= 100)
			{
				return "C" + Helper.ToRoman(number - 100);
			}
			if (number >= 90)
			{
				return "XC" + Helper.ToRoman(number - 90);
			}
			if (number >= 50)
			{
				return "L" + Helper.ToRoman(number - 50);
			}
			if (number >= 40)
			{
				return "XL" + Helper.ToRoman(number - 40);
			}
			if (number >= 10)
			{
				return "X" + Helper.ToRoman(number - 10);
			}
			if (number >= 9)
			{
				return "IX" + Helper.ToRoman(number - 9);
			}
			if (number >= 5)
			{
				return "V" + Helper.ToRoman(number - 5);
			}
			if (number >= 4)
			{
				return "IV" + Helper.ToRoman(number - 4);
			}
			if (number >= 1)
			{
				return "I" + Helper.ToRoman(number - 1);
			}
			return "";
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003388 File Offset: 0x00001588
		public static int FindIndex<T>(this IReadOnlyList<T> list, Predicate<T> match)
		{
			for (int i = 0; i < list.Count; i++)
			{
				if (match.Invoke(list[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000033B8 File Offset: 0x000015B8
		public static T GetEnumValueByIndex<T>(int index) where T : Enum
		{
			T[] array = (T[])Enum.GetValues(typeof(T));
			T result = default(T);
			if (index >= 0 && index < array.Length)
			{
				result = array[index];
			}
			return result;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000033F8 File Offset: 0x000015F8
		public static string GetTextEntryPlatformExtension()
		{
			string result;
			if (SystemInfo.deviceType == 3)
			{
				result = "pc";
			}
			else if (Application.platform == 25 || Application.platform == 38)
			{
				result = "ps";
			}
			else
			{
				result = "xbox";
			}
			return result;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000343C File Offset: 0x0000163C
		public static string GetXmlNodeAttribute(this XmlNode node, string attributeName)
		{
			string text;
			if (node == null)
			{
				text = null;
			}
			else
			{
				XmlAttribute xmlAttribute = node.Attributes.get_ItemOf(attributeName);
				text = ((xmlAttribute != null) ? xmlAttribute.Value : null);
			}
			return text ?? string.Empty;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003468 File Offset: 0x00001668
		public static void ForEach(this XmlNodeList nodeList, string expectedChildNodeName, Action<XmlNode> action)
		{
			foreach (object obj in nodeList)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.Name == expectedChildNodeName && action != null)
				{
					action.Invoke(xmlNode);
				}
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000034D0 File Offset: 0x000016D0
		public static List<T> GetListFromXmlDocument<T>(TextAsset textAsset, string parentNodeName, string itemNodeName, Func<XmlNode, T> createItem)
		{
			List<T> list = new List<T>();
			XmlDocument xmlDocument = Helper.LoadXmlFile(textAsset);
			if (xmlDocument != null && createItem != null && xmlDocument.ChildNodes.Count == 2 && xmlDocument.ChildNodes.get_ItemOf(1).Name == parentNodeName)
			{
				xmlDocument.ChildNodes.get_ItemOf(1).ChildNodes.ForEach(itemNodeName, delegate(XmlNode node)
				{
					list.Add(createItem.Invoke(node));
				});
			}
			return list;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003558 File Offset: 0x00001758
		public static XmlDocument LoadXmlFile(TextAsset textAsset)
		{
			try
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.LoadXml(textAsset.text);
				return xmlDocument;
			}
			catch
			{
			}
			return null;
		}
	}
}
