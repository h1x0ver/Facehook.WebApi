namespace Facehook.Business.Base;

public interface IBaseServiceForStory<TGet, TCreate>
{
    Task<TGet> Get(int id);
    Task<List<TGet>> GetAll();
    Task Create(TCreate entity);
    Task Delete(int id);
}