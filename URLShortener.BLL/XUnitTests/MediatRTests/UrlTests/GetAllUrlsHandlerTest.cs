using Xunit;
using Moq;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using URLShortener.DAL.Repositories.Interfaces.Base;
using URLShortener.BLL.DTO;
using URLShortener.BLL.MediatR.Url.GetAll;

namespace URLShortener.BLL.XUnitTests.MediatRTests.UrlTests
{
    public class GetAllTagsRequestHandlerTests
    {
        private readonly Mock<IRepositoryWrapper> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;

        public GetAllTagsRequestHandlerTests()
        {
            _mockRepo = new Mock<IRepositoryWrapper>();
            _mockMapper = new Mock<IMapper>();
        }

        private readonly List<DAL.Entities.Url> urls = new List<DAL.Entities.Url>()
        {
            new DAL.Entities.Url
            {
                Id = 1,
                OriginalUrl = "OriginalUrl 1"
            },
            new DAL.Entities.Url
            {
                Id = 2,
                OriginalUrl = "OriginalUrl 2"
            }
        };
        private readonly List<UrlDTO> urlDTOs = new List<UrlDTO>()
        {
            new UrlDTO
            {
                Id = 1,
                OriginalUrl = "OriginalUrl 1"
            },
            new UrlDTO
            {
                Id = 2,
                OriginalUrl = "OriginalUrl 2"
            }
        };

        async Task SetupRepository(List<DAL.Entities.Url> returnList)
        {
            _mockRepo.Setup(repo => repo.UrlRepository.GetAllAsync(
                It.IsAny<Expression<Func<DAL.Entities.Url, bool>>>(),
                It.IsAny<Func<IQueryable<DAL.Entities.Url>,
                IIncludableQueryable<DAL.Entities.Url, object>>>()))
                .ReturnsAsync(returnList);
        }
        async Task SetupMapper(List<UrlDTO> returnList)
        {
            _mockMapper.Setup(x => x.Map<IEnumerable<UrlDTO>>(It.IsAny<IEnumerable<object>>()))
                .Returns(returnList);
        }

        [Fact]
        public async Task Handler_Returns_NotEmpty_List()
        {
            //Arrange
            await SetupRepository(urls);
            await SetupMapper(urlDTOs);

            var handler = new GetAllUrlsHandler(_mockRepo.Object, _mockMapper.Object);

            //Act
            var result = await handler.Handle(new GetAllUrlsQuery(), CancellationToken.None);

            //Assert
            Assert.Multiple(
                () => Assert.IsType<List<UrlDTO>>(result.Value),
                () => Assert.True(result.Value.Count() == urls.Count));
        }

        [Fact]
        public async Task Handler_Returns_Empty_List()
        {
            //Arrange
            await SetupRepository(new List<DAL.Entities.Url>());
            await SetupMapper(new List<UrlDTO>());

            var handler = new GetAllUrlsHandler(_mockRepo.Object, _mockMapper.Object);

            //Act
            var result = await handler.Handle(new GetAllUrlsQuery(), CancellationToken.None);

            //Assert
            Assert.Multiple(
                () => Assert.IsType<List<UrlDTO>>(result.Value),
                () => Assert.Empty(result.Value));
        }
    }
}