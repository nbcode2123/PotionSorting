using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ServiceLocator
{
    // Dictionary dùng để lưu trữ các service, theo kiểu dữ liệu là key
    private static Dictionary<Type, object> services = new();

    // Đăng ký một service
    public static void Register<T>(T service)
    {
        services[typeof(T)] = service;
    }

    // Lấy service ra theo kiểu T
    public static T Get<T>()
    {
        return (T)services[typeof(T)];
    }

    // Reset toàn bộ services
    public static void Reset()
    {
        services.Clear();
    }
}
