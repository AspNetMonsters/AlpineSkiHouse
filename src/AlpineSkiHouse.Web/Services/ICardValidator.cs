using System;
using System.Linq;
using System.Collections.Generic;

namespace AlpineSkiHouse.Services
{
    public interface ICardValidator
    {
        bool IsValid(int passId, int locationId);
    }
}
