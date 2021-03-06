﻿namespace Thorium.Data
{
    public interface ISerializer<TKey, TValue>
    {
        ARawDatabaseFactory DatabaseFactory { get; }
        string Table { get; }
        string KeyColumn { get; }

        void CreateTable();
        void CreateConstraints();
        void Save(TKey key, TValue value);
        TValue Load(TKey key);
        void Delete(TKey key);
    }
}
