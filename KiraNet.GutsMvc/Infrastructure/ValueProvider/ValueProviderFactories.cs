﻿namespace KiraNet.GutsMvc.Infrastructure
{
    public static class ValueProviderFactories
    {
        private static readonly ValueProviderFactoryCollection _factories = new ValueProviderFactoryCollection()
        {
            //new ChildActionValueProviderFactory(),
            new RedirectQueryStringValueProviderFactory(),
            new QueryStringValueProviderFactory(),
            new FormValueProviderFactory(),
            new JsonValueProviderFactory(),
            //new RouteDataValueProviderFactory(),
            new FileCollectionValueProviderFactory(),
        };

        public static ValueProviderFactoryCollection Factories { get => _factories; }
    }
}
