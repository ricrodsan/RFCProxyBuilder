using System;
using System.ComponentModel;


namespace RFCProxyBuilder
{
	/// <summary>
	/// RFCFilter에 대한 요약 설명입니다.
	/// </summary>
	public class RFCFilter
	{
		private string m_strNameFilter = "";
		private string m_strGroupFilter = "";
		private string m_strLanguage = "EN";

		public RFCFilter()
		{
		}

		[Category("RFC Filter")]
		[Description("Name of function module. ex: RFC*")]
		public string NameFilter
		{
			get
			{
				return m_strNameFilter;
			}
			set 
			{
				m_strNameFilter = value;
			}
		}

		[Category("RFC Filter")]
		[Description("Name of group, default: \"\"")]
		public string GroupFilter
		{
			get
			{
				return m_strGroupFilter;
			}
			set 
			{
				m_strGroupFilter = value;
			}
		}

		[Category("RFC Filter")]
		[Description("Language for parameter texts")]
		public string Language
		{
			get
			{
				return m_strLanguage;
			}
			set 
			{
				m_strLanguage = value;
			}
		}
	}
}
