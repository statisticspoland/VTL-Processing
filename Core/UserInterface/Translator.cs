namespace StatisticsPoland.VtlProcessing.Core.UserInterface
{
    using Microsoft.Extensions.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.BackEnd;
    using StatisticsPoland.VtlProcessing.Core.FrontEnd.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.MiddleEnd.Modifiers.Interfaces;
    using System;

    public class Translator
    {
        private bool configured;
        private ServiceProvider provider;
        private IVtlProcessingConfig vtlConfig;
        private IServiceCollection services;

        public Translator()
        {
            this.configured = false;
            this.services = new ServiceCollection();
            this.vtlConfig = new VtlProcessingConfig(this.services);
        }

        public IVtlProcessingConfig VtlConfig
        {
            get => this.vtlConfig;
            set
            {
                this.services = value.Services;
                this.vtlConfig = value;
            }
        }

        public void Configure()
        {
            if (this.configured) throw new Exception("The translator instance has been configured already.");

            this.services.AddVtlProcessing((configure) => configure = this.VtlConfig);
            this.provider = this.services.BuildServiceProvider();
            this.configured = true;
        }

        public ITreeGenerator GetFrontEnd()
        {
            if (!this.configured) throw new Exception("The translator instance has been not configured yet.");

            return this.provider.GetFrontEnd();
        }

        public ISchemaModifiersApplier GetMiddleEnd()
        {
            if (!this.configured) throw new Exception("The translator instance has been not configured yet.");

            return this.provider.GetMiddleEnd();
        }

        public ITargetRenderer GetTargetRenderer(string name)
        {
            if (!this.configured) throw new Exception("The translator instance has been not configured yet.");

            return this.provider.GetTargetRenderer(name);
        }
    }
}
