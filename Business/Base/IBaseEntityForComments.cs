namespace Facehook.Business.Base;

public interface IBaseEntityForComments<TCreate, TGet>
{
    Task CreateCommentAsync(TCreate entity);
    Task<List<TGet>> GetPostCommentsAsync(int id);
    Task DeleteCommentAsync(int id);
}
