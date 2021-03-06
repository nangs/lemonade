﻿namespace Lemonade
{
    public abstract class FeatureWrapper
    {
        public bool IsEnabled => Feature.Switches[Key];

        public void Execute()
        {
            if (IsEnabled) OnExecute();
        }

        protected abstract void OnExecute();
        protected abstract string Key { get; }
    }
}