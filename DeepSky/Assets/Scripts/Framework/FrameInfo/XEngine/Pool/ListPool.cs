//
//  ListPool.cs
//  List对象池
//
//  Copyright (c) 2023 thedream.cc.  All rights reserved.
//
using System;
using System.Collections.Generic;
using UnityEngine;

namespace XEngine.Pool
{
    public static class ListPool<T>
    {
        // Object pool to avoid allocations.
        private static readonly ObjectPool<List<T>> s_ListPool = new ObjectPool<List<T>>(null, l => l.Clear());

        public static List<T> Get()
        {
            return s_ListPool.Get();
        }

        public static void Release(List<T> toRelease)
        {
            s_ListPool.Release(toRelease);
        }
    }
}
