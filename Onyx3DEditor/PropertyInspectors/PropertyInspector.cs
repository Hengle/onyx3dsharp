﻿
using System.ComponentModel;

namespace Onyx3DEditor
{

	public interface IPropertyInspector
	{
		void Apply();

		int GetFieldCount();
	}

	public class PropertyInspector<T> : IPropertyInspector
	{
		protected T mObject;

		[Browsable(false)]
		public T Object
		{
			get { return mObject; }
		}

		public PropertyInspector(T obj)
		{
			mObject = obj;
		}

		public virtual void Apply() {}

		public virtual int GetFieldCount() { return 5; }
	}

}