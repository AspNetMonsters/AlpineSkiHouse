using MediatR;

namespace AlpineSkiHouse.Web.Command
{
    public class ActivatePass : IRequest<int>
    {
        public int PassId { get; set; }
        public int ScanId { get; set; }
    }
}
