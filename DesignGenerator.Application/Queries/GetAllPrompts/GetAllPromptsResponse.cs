using DesignGenerator.Application.Interfaces;
using DesignGenerator.Domain;

namespace DesignGenerator.Application.Queries.GetAllPrompts
{
    public class GetAllPromptsResponse : IResult
    {
        public IEnumerable<Prompt> Prompts { get; set; }
    }
}
