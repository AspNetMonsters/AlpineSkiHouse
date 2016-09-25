using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace AlpineSkiHouse.Configuration
{
    public class CsrInformationConfigurationProvider : FileConfigurationProvider
    {
        public CsrInformationConfigurationProvider(FileConfigurationSource source) : base(source) { }

        public override void Load(Stream stream)
        {
            var parser = new CsrInformationParser();
            Data = parser.Parse(stream);
        }
    }
}
