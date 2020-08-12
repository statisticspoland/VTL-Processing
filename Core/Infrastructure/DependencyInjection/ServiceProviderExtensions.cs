﻿namespace StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection
{
    using Microsoft.Extensions.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.BackEnd;
    using StatisticsPoland.VtlProcessing.Core.FrontEnd.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.MiddleEnd.Modifiers.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using System;
    using System.Linq;

    /// <summary>
    /// The <see cref="IServiceProvider"/> extensions.
    /// </summary>
    public static class ServiceProviderExtensions
    {
        /// <summary>
        /// Gets the font-end VTL 2.0 transformation schema generator from service provider.
        /// </summary>
        /// <param name="provider">The service provider.</param>
        /// <returns>The front-end VTL 2.0 transformation schema generator.</returns>
        public static ITreeGenerator GetFrontEnd(this IServiceProvider provider)
        {
            return provider.GetService<ITreeGenerator>();
        }

        /// <summary>
        /// Gets the middle-end transformation schema generator from service provider.
        /// </summary>
        /// <param name="provider">The service provider.</param>
        /// <returns>The middle-end VTL 2.0 transformation schema generator.</returns>
        public static ISchemaModifiersApplier GetMiddleEnd(this IServiceProvider provider)
        {
            return provider.GetService<ISchemaModifiersApplier>();
        }

        /// <summary>
        /// Gets the VTL 2.0 data model from service provider.
        /// </summary>
        /// <param name="provider">The service provider.</param>
        /// <returns>The VTL 2.0 data model.</returns>
        public static IDataModel GetDataModel(this IServiceProvider provider)
        {
            return provider.GetService<IDataModel>();
        }

        /// <summary>
        /// Gets the expression factory from service provider.
        /// </summary>
        /// <param name="provider">The service provider.</param>
        /// <returns>The expression factory.</returns>
        public static IExpressionFactory GetExpressionFactory(this IServiceProvider provider)
        {
            return provider.GetService<IExpressionFactory>();
        }

        /// <summary>
        /// Gets the target renderer for the VTL 2.0 code from service provider.
        /// </summary>
        /// <param name="provider">The service provider.</param>
        /// <param name="name">The name of the target renderer.</param>
        /// <returns>The target renderer.</returns>
        public static ITargetRenderer GetTargetRenderer(this IServiceProvider provider, string name)
        {
            return provider.GetServices<ITargetRenderer>().FirstOrDefault(tr => tr.Name == name);
        }
    }
}
