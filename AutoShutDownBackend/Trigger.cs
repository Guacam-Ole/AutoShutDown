﻿namespace AutoShutDown.Backend
{
    public abstract class Trigger
    {
        public abstract bool ConditionsMet { get;  }
        public abstract void ShutDown();
    }
}