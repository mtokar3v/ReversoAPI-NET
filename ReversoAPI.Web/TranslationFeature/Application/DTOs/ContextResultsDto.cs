using System.Collections.Generic;

namespace ReversoAPI.Web.TranslationFeature.Application.DTOs
{
    public class ContextResultsDto
    {
        public bool RudeWords { get; set; }
        public bool Colloquialisms { get; set; }
        public bool RiskyWords { get; set; }
        public IEnumerable<ConcreteContextResultDto> Results { get; set; }
        public int TotalContextCallsMade { get; set; }
        public int TimeTakenContext { get; set; }
    }
}
