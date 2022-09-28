namespace Facehook.Business.Base;
public interface IBaseServiceForUsers<TGet>
{
    Task<TGet> Get(int id);
    Task<List<TGet>> GetAll();
}
