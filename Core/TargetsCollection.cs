﻿namespace StatisticsPoland.VtlProcessing.Core
{
    using Microsoft.Extensions.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.BackEnd;
    using System;
    using System.Linq;

    /// <summary>
    /// Collection of translation targets.
    /// </summary>
    public class TargetsCollection
    {
        private readonly IServiceCollection _targets;
        private readonly IServiceCollection _services;

        /// <summary>
        /// Initializes a new instance of the <see cref="TargetsCollection"/> class.
        /// </summary>
        /// <param name="services">The service collection.</param>
        public TargetsCollection(IServiceCollection services)
        {
            this._targets = new ServiceCollection();
            this._services = services;

            foreach (ServiceDescriptor service in services.Where(s => s.ServiceType == typeof(ITargetRenderer)))
            {
                this._targets.AddScoped(typeof(ITargetRenderer), p => service);
            }
        }

        /// <summary>
        /// Adds a target to the target collection and injects its depentent services.
        /// </summary>
        /// <param name="targetType">The target type.</param>
        /// <param name="services">The services to inject.</param>
        internal void AddTarget(Type targetType, IServiceCollection services = null)
        {
            if (!typeof(ITargetRenderer).IsAssignableFrom(targetType)) throw new ArgumentException("Wrong type of a target instance.", "targetType");

            this._targets.AddScoped(typeof(ITargetRenderer), targetType);
            this._services.AddScoped(typeof(ITargetRenderer), targetType);

            if (services != null)
            {
                foreach (ServiceDescriptor service in services.Where(s => s.ServiceType != typeof(ITargetRenderer)))
                {
                    this._services.Add(service);
                }
            }
        }

        /// <summary>
        /// Removes a target from the collection but doesn't remove its dependent sevices.
        /// </summary>
        /// <param name="target">The target to remove.</param>
        internal void RemoveTarget(ITargetRenderer target)
        {
            this._targets.Remove(this._targets.FirstOrDefault(s => s.ImplementationInstance == target));
            this._services.Remove(this._services.FirstOrDefault(s => s.ImplementationInstance == target));
        }

        /// <summary>
        /// Clears the target collection but doesn't remove dependent sevices of these targets.
        /// </summary>
        internal void Clear()
        {
            this._targets.Clear();

            ServiceDescriptor[] services = this._services.Where(s => s.ServiceType == typeof(ITargetRenderer)).ToArray();
            foreach (ServiceDescriptor service in services)
            {
                this._services.Remove(service);
            }
        }
    }
}
