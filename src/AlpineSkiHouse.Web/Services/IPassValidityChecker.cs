namespace AlpineSkiHouse.Web.Services
{
    public interface IPassValidityChecker
    {
        bool IsValid(int passId);
    }
}