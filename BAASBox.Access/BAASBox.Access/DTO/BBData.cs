using System;
using System.Runtime.Serialization;

namespace BAASBox.Access.DTO
{
	[DataContract]
	public abstract class BBData
	{
		[DataMember] public string id {get;set;}
		[DataMember] public string _creation_date {get;set;}
		[DataMember] public string _author {get;set;}

		[IgnoreDataMember] public abstract bool MayDelete { get; }

		public override bool Equals (object obj)
		{
			if (obj is BBData) {
				var bbObj = obj as BBData;

				if (id != null) {
					return id.Equals (bbObj.id);
				}
			}

			return base.Equals(obj);
		}

		public override int GetHashCode ()
		{
			return base.GetHashCode ();
		}

	}
}

