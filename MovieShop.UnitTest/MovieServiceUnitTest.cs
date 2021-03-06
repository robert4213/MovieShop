using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
// using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using MovieShop.Core.Entities;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Infrastructure.Services;
using Moq;
using MovieShop.Core.MappingProfiles;
using MovieShop.Core.Models.Responses;

namespace MovieShop.UnitTest
{
    [TestFixture]
    public class MovieServiceUnitTest
    {
        //SUT: system under test
        private MovieService _sut;
        private static List<Movie> _movies;
        private Mock<IMovieRepository> _mockMovieRepository;
        private Mock<IReviewRepository> _mockReviewRepository;
        private Mock<IAsyncRepository<MovieGenre>> _mockMovieGenreRepository;
        private IMapper _mockMapper;

        [OneTimeSetUp]
        //[OneTimeSetup] in nunit
        public void OneTimeSetup()
        {
            _movies = new List<Movie>
            {
                new Movie {Id = 1, Title = "Avengers: Infinity War", Budget = 1200000, ReleaseDate = DateTime.Now},
                new Movie {Id = 2, Title = "Avatar", Budget = 1200000, ReleaseDate = DateTime.Now},
                new Movie {Id = 3, Title = "Star Wars: The Force Awakens", Budget = 1200000, ReleaseDate = DateTime.Now},
                new Movie {Id = 4, Title = "Titanic", Budget = 1200000, ReleaseDate = DateTime.Now},
                new Movie {Id = 5, Title = "Inception", Budget = 1200000, ReleaseDate = DateTime.Now},
                new Movie {Id = 6, Title = "Avengers: Age of Ultron", Budget = 1200000, ReleaseDate = DateTime.Now},
                new Movie {Id = 7, Title = "Interstellar", Budget = 1200000, ReleaseDate = DateTime.Now},
                new Movie {Id = 8, Title = "Fight Club", Budget = 1200000, ReleaseDate = DateTime.Now},
                new Movie {Id = 9, Title = "The Lord of the Rings: The Fellowship of the Ring", Budget = 1200000, ReleaseDate = DateTime.Now},
                new Movie {Id = 10, Title = "The Dark Knight", Budget = 1200000, ReleaseDate = DateTime.Now},
                new Movie {Id = 11, Title = "The Hunger Games", Budget = 1200000, ReleaseDate = DateTime.Now},
                new Movie {Id = 12, Title = "Django Unchained", Budget = 1200000, ReleaseDate = DateTime.Now},
                new Movie {Id = 13, Title = "The Lord of the Rings: The Return of the King", Budget = 1200000, ReleaseDate = DateTime.Now},
                new Movie {Id = 14, Title = "Harry Potter and the Philosopher's Stone", Budget = 1200000, ReleaseDate = DateTime.Now},
                new Movie {Id = 15, Title = "Iron Man", Budget = 1200000, ReleaseDate = DateTime.Now},
                new Movie {Id = 16, Title = "Furious 7", Budget = 1200000, ReleaseDate = DateTime.Now}
            };
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MoviesMappingProfile());
            });

            _mockMapper = mapperConfig.CreateMapper();
        }

        [SetUp]
        public void Setup()
        {
            _mockMovieRepository = new Mock<IMovieRepository>();
            _mockReviewRepository = new Mock<IReviewRepository>();
            _mockMovieGenreRepository = new Mock<IAsyncRepository<MovieGenre>>();
            
            _mockMovieRepository.Setup(m => m.GetHighestRevenueMovies()).ReturnsAsync(_movies);

            _sut = new MovieService(_mockMovieRepository.Object, _mockReviewRepository.Object,
                _mockMovieGenreRepository.Object,_mockMapper);
        }

        /*
         * Arrange: Initializes objects, creates mocks with arguments that are passed to the method under test and adds expectations.
         * Act: Invokes the method or property under test with the arranged parameters.
         * Assert: Verifies that the action of the method under test behaves as expected.
         */
        [Test]
        public async Task TestListOfHighestGrossingMoviesFromFakeData()
        {
            //Arrange, act and act
            //Mock objects data, methods.

            //MovieService=>Get top revenues movie
            var movies = await _sut.GetTopRevenueMovies();//act

            // check output
            //Assert
            Assert.IsNotNull(movies);
            CollectionAssert.AllItemsAreInstancesOfType(movies, typeof(MovieResponseModel));
            Assert.AreEqual(16, movies.Count());
        }
    }
}