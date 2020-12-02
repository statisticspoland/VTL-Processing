﻿namespace Target.TSQL.Infrastructure
{
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Interfaces;
    using Target.TSQL.Infrastructure.Interfaces;

    internal class TargetConfiguration : ITargetConfiguration
    {
        public TargetConfiguration(IEnvironmentMapper envMapper = null, bool useComments = false)
        {
            this.EnvMapper = envMapper;
            this.UseComments = useComments;
        }

        public IEnvironmentMapper EnvMapper { get; set; }

        public bool UseComments { get; set; }
    }
}