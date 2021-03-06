﻿using System;

namespace RandomUser.Business.Insfrastructure.Base
{
    public abstract class SingletonBase<T> where T : class
    {
        private static readonly Lazy<T> instance = new Lazy<T>(() =>
        {
            return Activator.CreateInstance(typeof(T), true) as T;
        });

        public static T Instance => instance.Value;
    }
}
