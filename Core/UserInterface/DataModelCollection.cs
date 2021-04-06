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
        private readonly IServiceCollection _services;
        private readonly Action _providerCreationMethod;

        public DataModelCollection(IDataModelAggregator aggregator, IServiceCollection services, Action providerCreationMethod)
        {
            this._Aggregator = aggregator;
            this._services = services;
            this._providerCreationMethod = providerCreationMethod;
        }

        internal readonly IDataModelAggregator _Aggregator;

        public IDataModel this[int index] => this._Aggregator.DataModels.ElementAt(index);

        public string DefaultNamespace => this._Aggregator.DefaultNamespace;

        public void AddModel(IDataModel model)
        {
            if (this._Aggregator.DataModels != null) this._Aggregator.DataModels.Add(model);
            else this._Aggregator.DataModels = new List<IDataModel>() { model };

            //this._services.AddSingleton(typeof(IDataModel), model);
        }

        public void RemoveModel(IDataModel model)
        {
            this._Aggregator.DataModels.Remove(model);
            //this._services.Remove(this._services.FirstOrDefault(s => s.ImplementationInstance == model));
            //this._providerCreationMethod();
        }

        public void Clear()
        {
            this._Aggregator.DataModels.Clear();

            //ServiceDescriptor[] services = this._services.Where(s => s.ServiceType == typeof(IDataModel)).ToArray();
            //foreach (ServiceDescriptor service in services)
            //{
            //    this._services.Remove(service);
            //}

            //this._providerCreationMethod();
        }
    }
}
