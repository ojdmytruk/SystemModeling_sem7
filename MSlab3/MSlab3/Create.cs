using System;
using System.Collections.Generic;
using System.Text;

namespace MSlab3
{
	public class Create : Element
	{

		public Create(double delay, int id) : base(delay)
		{
			this.Id = id;
		}

		public override void outAct(List<Element> followingElements, int iterator)
		{
			base.outAct(followingElements, iterator);
			base.Tnext = base.Tcurr + base.Delay;
			base.NextElement.inAct(1);
		}
	}

}
