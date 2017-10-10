﻿using KiraNet.GutsMvc.Implement;
using System;

namespace KiraNet.GutsMvc.Infrastructure
{
    public sealed class QueryStringValueProviderFactory : IValueProviderFactory
    {
        public IValueProvider CreateValueProvider(ControllerContext controllerContext)
        {
            if(controllerContext == null)
            {
                throw new ArgumentNullException(nameof(controllerContext));
            }

            try
            {
                return new QueryStringValueProvider(controllerContext);
            }
            catch
            {
                return null;
            }
        }
    }
}