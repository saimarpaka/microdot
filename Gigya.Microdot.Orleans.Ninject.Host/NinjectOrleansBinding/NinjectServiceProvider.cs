﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Extensions.DependencyInjection;
using Ninject;
using Ninject.Activation;
using Ninject.Activation.Caching;
using Ninject.Activation.Strategies;
using Ninject.Injection;
using Ninject.Modules;
using Ninject.Parameters;
using Ninject.Planning;
using Ninject.Planning.Bindings;
using Ninject.Planning.Bindings.Resolvers;
using Ninject.Planning.Strategies;
using Ninject.Planning.Targets;
using Ninject.Selection;
using Ninject.Selection.Heuristics;
using Ninject.Syntax;

namespace Gigya.Microdot.Orleans.Ninject.Host.NinjectOrleansBinding
{
    internal class NinjectServiceProvider : IServiceProvider, IServiceScope
    {
        private readonly IResolutionRoot _resolver;
        private readonly MicrodotNinectScopParameter _microdotNinectScopParameter;
        private readonly CacheItem _cacheItem;


        public NinjectServiceProvider(IResolutionRoot resolver, IRequestScopedType requestScopedType)
        {
            _cacheItem = new CacheItem(requestScopedType);
            _microdotNinectScopParameter = new MicrodotNinectScopParameter(_cacheItem);
            _resolver = resolver;
        }

        public IServiceProvider ServiceProvider => this;

        public void Dispose()
        {
            _cacheItem.Dispose();
        }

        public object GetService(Type type)
        {
            return _resolver.Get(type, _microdotNinectScopParameter);

        }

    }


}
