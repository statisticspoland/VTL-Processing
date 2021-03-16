namespace StatisticsPoland.VtlProcessing.Core.UserInterface
{
    using Microsoft.Extensions.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.BackEnd;
    using System;
    using System.Linq;

    public class TargetCollection
    {
        private readonly IServiceCollection _targets;
        private readonly IServiceCollection _services;
        private readonly Action _providerCreationMethod;

        public TargetCollection(IServiceCollection services, Action providerCreationMethod)
        {
            this._targets = new ServiceCollection();
            this._services = services;
            this._providerCreationMethod = providerCreationMethod;

            foreach (ServiceDescriptor service in services.Where(s => s.ServiceType == typeof(ITargetRenderer)))
            {
                this._targets.AddSingleton(typeof(ITargetRenderer), service);
            }
        }

        public ITargetRenderer this[int index] => (ITargetRenderer)this._targets[index];

        public void Add(ITargetRenderer target)
        {
            this._targets.AddSingleton(typeof(ITargetRenderer), target);
            this._services.AddSingleton(typeof(ITargetRenderer), target);
            this._providerCreationMethod();
        }

        public void AddTarget(Type targetType, IServiceCollection services = null)
        {
            if (!typeof(ITargetRenderer).IsAssignableFrom(targetType)) throw new Exception("Wrong type of target instance.");

            this._targets.AddSingleton(typeof(ITargetRenderer), targetType);
            this._services.AddSingleton(typeof(ITargetRenderer), targetType);

            if (services != null)
            {
                foreach (ServiceDescriptor service in services.Where(s => s.ServiceType != typeof(ITargetRenderer)))
                {
                    this._services.Add(service);
                }
            }

            this._providerCreationMethod();
        }

        public void RemoveTarget(ITargetRenderer target)
        {
            this._targets.Remove(this._targets.FirstOrDefault(s => s.ImplementationInstance == target));
            this._services.Remove(this._services.FirstOrDefault(s => s.ImplementationInstance == target));
            this._providerCreationMethod();
        }

        public void Clear()
        {
            this._targets.Clear();

            ServiceDescriptor[] services = this._services.Where(s => s.ServiceType == typeof(ITargetRenderer)).ToArray();
            foreach (ServiceDescriptor service in services)
            {
                this._services.Remove(service);
            }

            this._providerCreationMethod();
        }
    }
}
