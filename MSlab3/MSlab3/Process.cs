using System;
using System.Collections.Generic;
using System.Linq;

namespace MSlab3
{
	public class Process : Element
	{
		private int queue, maxqueue, failure, maxDevices;
		private double meanQueue, load;

		private bool processBusy;
				
		public Process(double delay, int devices, int id) : base(delay)
		{
			queue = 0;
			maxqueue = int.MaxValue;
			meanQueue = 0.0;
			maxDevices = devices;
			load = 0.0;
			failure = 0;
			processBusy = false;
			Id = id;
		}

		public override void inAct(int tasksNumber)
		{
			int freeDevices = maxDevices - base.State;
			if (tasksNumber<freeDevices)
            {
				base.State += tasksNumber;
				tasksNumber = 0;
				processBusy = true;
            }
			else
            {
				tasksNumber -= freeDevices;
				base.State = maxDevices;
            }
			Tnext = base.Tcurr + base.Delay;
			if (tasksNumber>0)
            {
				freeDevices = maxqueue - queue;
				if (tasksNumber< freeDevices)
                {
					queue += tasksNumber;
					tasksNumber = 0;
                }
				else
                {
					tasksNumber -= freeDevices;
					queue = maxqueue;
                }
            }
			if (tasksNumber>0)
				failure += tasksNumber;
		}

		public override void outAct(List<Element> followingElements, int iterator)
		{
			base.outAct(followingElements, iterator);
			base.Tnext = double.MaxValue;
            if (base.State > 0)
            {
                base.State -= 1;
            }
			processBusy = false;

            if (Queue > 0)
            {
                Queue -= 1;
                base.State += 1;
                base.Tnext = base.Tcurr + base.Delay;
				
			}
            this.SetNextElement(Id, followingElements);
			if (iterator==0)  Console.WriteLine(this.Name + " goes to " + this.NextElement.Name + " time: " + Tcurr);
            this.NextElement.inAct(1);          
        }

		public virtual int Failure
		{
			get
			{
				return failure;
			}
            set
            {
				this.failure = value;
            }
		}

		public virtual int MaxDeviaces
		{
			get
			{
				return maxDevices;
			}
		}

		public virtual bool ProcessBusy
		{
			get
			{
				return processBusy;
			}
		}

		public virtual int Queue
		{
			get
			{
				return queue;
			}
			set
			{
				this.queue = value;
			}
		}

		public virtual int Maxqueue
		{
			get
			{
				return maxqueue;
			}
			set
			{
				this.maxqueue = value;
			}
		}

		public override void printInfo()
		{
			base.printInfo();
			Console.WriteLine("failure = " + this.Failure);
		}

        public override void doStatistics(double delta)
        {
            MeanQueue = MeanQueue + Queue * delta;
            Load = Load + base.State * delta;
        }

        public virtual double MeanQueue
		{
			get
			{
				return meanQueue;
			}
            set
            {
				this.meanQueue = value;
            }
		}

		public virtual double Load
		{
			get
			{
				return load;
			}
			set
            {
				this.load = value;
            }
		}

	}

}
