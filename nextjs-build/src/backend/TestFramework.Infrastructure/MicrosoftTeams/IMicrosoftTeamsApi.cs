using OneOf.Types;
using System.Threading.Tasks;
using TestFramework.Infrastructure.MicrosoftTeams.Dto;

namespace TestFramework.Infrastructure.MicrosoftTeams
{
    public interface IMicrosoftTeamsApi
    {
        Task<TrueOrFalse> PostMessage(Card card);
    }
}