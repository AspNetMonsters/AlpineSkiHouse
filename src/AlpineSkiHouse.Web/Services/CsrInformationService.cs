using AlpineSkiHouse.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlpineSkiHouse.Services
{
    public class CsrInformationService : ICsrInformationService
    {
        private CsrInformationOptions _options;

        public CsrInformationService(IOptions<CsrInformationOptions> options)
        {
            _options = options.Value;
        }

        public bool CallCenterOnline
        {
            get
            {
                return _options.OnlineRepresentatives > 0;
            }
        }

        public string CallCenterPhoneNumber
        {
            get
            {
                return _options.PhoneNumber;
            }
        }

        public int OnlineRepresentatives
        {
            get
            {
                return _options.OnlineRepresentatives;
            }
        }
    }
}
