﻿using System;

namespace XamarinEnterpriseApp.Xamarin.Core.Exceptions
{
    public class ServiceAuthenticationException : Exception
    {
        public string Content { get; }

        public ServiceAuthenticationException()
        {
        }

        public ServiceAuthenticationException(string content):base(content)
        {
            Content = content;
        }
    }
}
