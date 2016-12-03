using System;
using System.Linq;
using System.Collections.Generic;

namespace AlpineSkiHouse.Services
{
    public interface IDateService
    {
        DateTime Today();
        DateTime Now();
    }
}
