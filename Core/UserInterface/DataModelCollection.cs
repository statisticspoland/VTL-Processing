namespace StatisticsPoland.VtlProcessing.Core.UserInterface
{
    using Microsoft.Extensions.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.BackEnd;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class DataModelCollection
    {
        private readonly IDataModelAggregator _aggregator;
        private readonly IServiceCollection _services;
        private readonly Action _providerCreationMethod;

        public DataModelCollection(IDataModelAggregator aggregator, IServiceCollection services, Action providerCreationMethod)
        {
            this._aggregator = aggregator;
            this._services = services;
            this._providerCreationMethod = providerCreationMethod;
        }

        public IDataModel this[int index] => this._aggregator.DataModels.ElementAt(index);

        public string DefaultNamespace => this._aggregator.DefaultNamespace;

        public void AddModel(IDataModel model)
        {
            if (this._aggregator.DataModels != null) _aggregator.DataModels.Add(model);
            else this._aggregator.DataModels = new List<IDataModel>() { model };

            this._services.AddSingleton(typeof(IDataModel), model);
        }

        public void RemoveModel(IDataModel model)
        {
            this._aggregator.DataModels.Remove(model);
            this._services.Remove(this._services.FirstOrDefault(s => s.ImplementationInstance == model));
            this._providerCreationMethod();
        }

        public void Clear()
        {
            this._aggregator.DataModels.Clear();

            ServiceDescriptor[] services = this._services.Where(s => s.ServiceType == typeof(IDataModel)).ToArray();
            foreach (ServiceDescriptor service in services)
            {
                this._services.Remove(service);
            }

            this._providerCreationMethod();
        }
    }
}
