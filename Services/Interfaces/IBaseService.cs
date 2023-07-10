using gps_app.Entities;
using gps_app.Entities.Dtos;

namespace gps_app.Services.Interfaces
{
    public interface IBaseService<T, E>
    {
        E ToDto(T entity, bool addPagination, int page);
        T FromDto(E dto);
    }
}
