﻿namespace Thorium.Data
{
    public enum KeyType
    {
        Primary,
        Key,
        Unique,
    }

    public class KeyDefinition
    {
        public ColumnDefinition[] Columns { get; set; }
    }
}
