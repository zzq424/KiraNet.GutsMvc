﻿using KiraNet.GutsMvc.Metadata;
using KiraNet.GutsMvc.View;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text;

namespace KiraNet.GutsMvc
{
    public class WebHost : IWebHost
    {
        private IServiceProvider _serviceProvider;
        private IConfiguration _config;

        public WebHost(IServiceCollection services, IConfiguration config)
        {
            _serviceProvider = services
                //.AddScoped<IHttpContextCache, HttpContextCache>()
                //.AddMemoryCache()
                .BuildServiceProvider();
            _config = config;

            //new DefaultModelMetadataProvider(_serviceProvider.GetRequiredService<IMemoryCache>());
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance); // 支持中文编码
        }

        public void Start()
        {
            // 真正完成中间件注册
            IApplicationBuilder applicationBuilder = new ApplicationBuilder();// _serviceProvider.GetRequiredService<IApplicationBuilder>();
            _serviceProvider.GetRequiredService<IApplicationStartup>().Configure(applicationBuilder);

            IServer server = _serviceProvider.GetRequiredService<IServer>();
            IServerAddressesFeature addressFeatures = server.Features.Get<IServerAddressesFeature>();

            string addresses = _config["ServerAddresses"] ?? "http://localhost:17758";
            foreach (string address in addresses.Split(';'))
            {
                addressFeatures.Addresses.Add(address);
            }

            server.Run(new HostingApplication(applicationBuilder.Build(), _serviceProvider));
        }
    }
}
