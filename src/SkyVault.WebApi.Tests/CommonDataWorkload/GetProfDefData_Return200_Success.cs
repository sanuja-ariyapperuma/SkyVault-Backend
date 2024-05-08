
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Configuration;
using Moq;
using SkyVault.Payloads.ResponsePayloads;
using SkyVault.WebApi.Backend.Models;

namespace SkyVault.WebApi.Tests.CommonDataWorkload;

public class GetProfDefData_Return200_Success
{
    private readonly Mock<SkyvaultContext> _mockDbContext;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<IConfiguration> _mockConfiguration;
    private readonly DefaultHttpContext _mockHttpContext;

    public GetProfDefData_Return200_Success()
    {
        _mockDbContext = new Mock<SkyvaultContext>();
        _mockMapper = new Mock<IMapper>();
        _mockConfiguration = new Mock<IConfiguration>();
        _mockHttpContext = new DefaultHttpContext();
        _mockHttpContext.Items["X-Correlation-ID"] = "test-correlation-id";
    }

    [Fact]
    public void GetProfilePageDefinitionData_ReturnsOkResult_WhenDataFetchIsSuccessful()
    {
        // No Arrange section needed for this test

        // Act
        var result = SkyVault
        .WebApi
        .Workloads
        .CustomWorkload
        .GetProfilePageDefinitionData(_mockDbContext.Object, _mockMapper.Object, _mockConfiguration.Object, _mockHttpContext);

        // Assert
        Assert.IsType<Ok<ProfileDefinitionResponse>>(result); 
    }
}