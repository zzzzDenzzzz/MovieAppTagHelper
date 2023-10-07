using MovieAppTagHelper.Models;

namespace MovieAppTagHelper.Services
{
    public interface IMovieApiService
    {
        Task<MovieApiResponse> SearchByTitleAsync(string title);
        Task<Models.Cinema> SearchByIdAsync(string id);
    }
}