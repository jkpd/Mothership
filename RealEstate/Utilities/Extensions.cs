using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using MongoDB.Driver;


namespace RealEstate.Utilities
{
    public static class Extensions
    {
        public static IEnumerable<T> ToList<T>(this IAsyncCursor<T> cursor)
        {
            var list = new List<T>();
            cursor.ForEachAsync(d => list.Add(d));
            return list;
        }

        public static async Task<IEnumerable<T>> ToListAsync<T>(this Task<IAsyncCursor<T>> task)
        {
            var list = new List<T>();
            var cursor = await task;
            await cursor.ForEachAsync(d => list.Add(d));
            return list;
        }
    }
}