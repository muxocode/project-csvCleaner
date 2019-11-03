using entities;
using Microsoft.Extensions.DependencyInjection;
using model;
using model.rules.csv;
using model.rules.line;
using mxcd.core.rules;
using System;

namespace App
{
    internal static class HelperExtension
    {
        internal static IServiceCollection AddHelper(this IServiceCollection services)
        {
            services.AddTransient<IAppHelper, Helper>();
            return services;
        }

        internal static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient(x => config);
            foreach (var item in CsvLineRuleCreator.Create(config.output.conditions))
            {
                services.AddTransient(x => item);
            }
            foreach (var item in CsvActionCreator.Create(config.output.replaces))
            {
                services.AddTransient(x => item);
            }
            foreach (var item in CsvActionCreator.Create(config.output.types))
            {
                services.AddTransient(x => item);
            }
            return services;
        }

        internal static IServiceCollection AddStaticRules(this IServiceCollection services)
        {
            services
                .AddTransient<IRule<ICsvData>, FieldsCountRule>()
                .AddTransient<IRule<ICsvData>, FieldsNameRule>()
                .AddTransient<IRule<ICsvLine>, LineEmptyRule>();
                

            return services;
        }
    }

    public class CsvHelper
    {


        public CsvHelper()
        {

        }
        public IConfigHelper CreateConfig()
        {
            return new Helper(null, null);
        }

        public IAppHelper CreateHelper(IConfiguration config)
        {
            var oServ = new ServiceCollection()
                .AddHelper()
                .AddStaticRules()
                .AddConfiguration(config)
                .AddTransient(x=>config)
                .BuildServiceProvider();

            return oServ.GetService<IAppHelper>();
        }
    }
}
