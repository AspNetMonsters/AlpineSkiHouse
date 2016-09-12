using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlpineSkiHouse.Services
{
    public interface ICsrInformationService
    {
        int OnlineRepresentatives { get; }
        string CallCenterPhoneNumber { get; }
        bool CallCenterOnline { get; }
    }
}
