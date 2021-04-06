namespace StatisticsPoland.VtlProcessing.Core.UserInterface
{
    using Microsoft.Extensions.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.BackEnd;
    using System;
    using System.Linq;

    public class TargetsCollection
    {
        private readonly IServiceCollection _targets;
        private readonly IServiceCollection _services;
        private readonly Action _confirmTargets;

        public TargetsCollection(IServiceCollection services, Action confirmTargets)
        {
            this.Confirmed = false;
            this._targets = new ServiceCollection();
            this._services = services;
            this._confirmTargets = confirmTargets;

            foreach (ServiceDescriptor service in services.Where(s => s.ServiceType == typeof(ITargetRenderer)))
            {
                this._targets.AddSingleton(typeof(ITargetRenderer), service);
            }
        }

        public ITargetRenderer this[int index] => (ITargetRenderer)this._targets[index];

        public bool Confirmed { get; private set; }

        /// <summary>
        /// Adds a target to the target collection and injects its depentent services.
        /// </summary>
        /// <param name="targetType">The target type.</param>
        /// <param name="services">The services to inject.</param>
        public void AddTarget(Type targetType, IServiceCollection services = null)
        {
            if (this.Confirmed) throw new Exception("The collection of targets has been confirmed already.");
            if (!typeof(ITargetRenderer).IsAssignableFrom(targetType)) throw new Exception("Wrong type of a target instance.");

            this._targets.AddSingleton(typeof(ITargetRenderer), targetType);
            this._services.AddSingleton(typeof(ITargetRenderer), targetType);

            if (services != null)
            {
                foreach (ServiceDescriptor service in services.Where(s => s.ServiceType != typeof(ITargetRenderer)))
                {
                    this._services.Add(service);
                }
            }
        }

        /// <summary>
        /// Removes a target from the collection but not removes its dependent sevices.
        /// </summary>
        ///<param name="target">The target to remove.</param>
        public void RemoveTarget(ITargetRenderer target)
        {
            if (this.Confirmed) throw new Exception("The collection of targets has been confirmed already.");

            this._targets.Remove(this._targets.FirstOrDefault(s => s.ImplementationInstance == target));
            this._services.Remove(this._services.FirstOrDefault(s => s.ImplementationInstance == target));
        }

        /// <summary>
        /// Clears the target collection but not removes dependent sevices of these tagrgets.
        /// </summary>
        public void Clear()
        {
            if (this.Confirmed) throw new Exception("The collection of targets has been confirmed already.");

            this._targets.Clear();

            ServiceDescriptor[] services = this._services.Where(s => s.ServiceType == typeof(ITargetRenderer)).ToArray();
            foreach (ServiceDescriptor service in services)
            {
                this._services.Remove(service);
            }
        }

        public void ConfirmTargets()
        {
            if (!this.Confirmed)
            {
                this._confirmTargets();
                this.Confirmed = true;
            }
        }
    }
}
