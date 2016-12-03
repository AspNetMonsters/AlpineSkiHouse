using MediatR;

namespace AlpineSkiHouse.Web.Command
{
    public class CreateScan : IRequest<int>
    {
        public int CardId { get; set; }

        public int LocationId { get; set; }
    }
}
