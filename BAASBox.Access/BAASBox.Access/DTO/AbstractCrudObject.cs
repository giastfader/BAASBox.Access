using System;

namespace BAASBox.Access.DTO
{
	public abstract class AbstractCrudObject : BBData
	{
		public bool MayEdit(string candidate)
		{
			// default behaviour - only the author can edit
			return candidate == _author;
		}
	}
}

