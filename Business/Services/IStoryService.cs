using Facehook.Business.Base;
using Facehook.Business.DTO_s;
using Facehook.Business.DTO_s.Story;

namespace Facehook.Business.Services;

public interface IStoryService : IBaseServiceForStory<StoryGetDTO, StoryCreateDTO>
{
}
