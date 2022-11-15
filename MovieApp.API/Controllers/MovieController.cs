using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Core.DTOs;
using MovieApp.Core.Entities;
using MovieApp.Core.Services;

namespace MovieApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IService<Movie> _service;

        public MovieController(IService<Movie> movieService, IMapper mapper) 
        {
            _service = movieService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> All() 
        {
            var movies = await _service.GetAllAsync();
            var moviesDtos = _mapper.Map<List<MovieDto>>(movies.ToList());
            return CreateActionResult<List<MovieDto>>(CustomResponseDto<List<MovieDto>>.Success(200, moviesDtos));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var movie = await _service.GetByIdAsync(id);
            var movieDto = _mapper.Map<MovieDto>(movie);
            return CreateActionResult<MovieDto>(CustomResponseDto<MovieDto>.Success(200, movieDto));
        }

        [HttpPost]
        public async Task<IActionResult> Save(MovieDto movieDto)
        {
            var movie = await _service.AddAsync(_mapper.Map<Movie>(movieDto));
            return CreateActionResult<MovieDto>(CustomResponseDto<MovieDto>.Success(201, movieDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(MovieUpdateDto movieUpdateDto)
        {
            await _service.UpdateAsync(_mapper.Map<Movie>(movieUpdateDto));
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var movie = await _service.GetByIdAsync(id);
            var response = movie == null ? CustomResponseDto<NoContentDto>.Fail(404,"Cannot found given movie") : CustomResponseDto<NoContentDto>.Success(204);
            await _service.RemoveAsync(movie);
            return CreateActionResult(response);
        }
    }
}
