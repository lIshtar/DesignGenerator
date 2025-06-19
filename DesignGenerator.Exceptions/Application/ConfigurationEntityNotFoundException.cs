using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGenerator.Exceptions.Application
{
    public class ConfigurationEntityNotFoundException<TEntity> : Exception
    {
        public string Key { get; }

        public ConfigurationEntityNotFoundException(string key)
            : base($"Entity of type '{typeof(TEntity).Name}' with key '{key}' was not found in configuration.")
        {
            Key = key;
        }
    }
}
