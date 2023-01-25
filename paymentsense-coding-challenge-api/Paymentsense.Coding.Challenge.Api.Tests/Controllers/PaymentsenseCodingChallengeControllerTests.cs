using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Paymentsense.Coding.Challenge.Api.Constants;
using PaymentSense.BusinessLayer;
using PaymentSense.Models.Internal;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using PaymentSenseCodingChallengeController = Paymentsense.Coding.Challenge.Api.Controllers.PaymentSenseCodingChallengeController;

namespace Paymentsense.Coding.Challenge.Api.Tests.Controllers
{
    public class PaymentsenseCodingChallengeControllerTests
    {
        private readonly ILogger<PaymentSenseCodingChallengeController> _logger;
        private readonly IMapper _mapper;
        private readonly ICountryManager _countryManager;
        private readonly PaymentSenseCodingChallengeController _controller;

        public PaymentsenseCodingChallengeControllerTests()
        {
            _logger = Substitute.For<ILogger<PaymentSenseCodingChallengeController>>();
            _mapper = Substitute.For<IMapper>();
            _countryManager = Substitute.For<ICountryManager>();

            _controller = new PaymentSenseCodingChallengeController(_logger, _mapper, _countryManager);
        }

        public static IEnumerable<object[]> NullParameters()
        {
            yield return new object[]
            {
                null,
                Substitute.For<IMapper>(),
                Substitute.For<ICountryManager>(),
                "logger"
            };

            yield return new object[]
            {
                Substitute.For<ILogger<PaymentSenseCodingChallengeController>>(),
                null,
                Substitute.For<ICountryManager>(),
                "mapper"
            };

            yield return new object[]
            {
                Substitute.For<ILogger<PaymentSenseCodingChallengeController>>(),
                Substitute.For<IMapper>(),
                null,
                "countryManager"
            };
        }

        [Theory]
        [MemberData(nameof(NullParameters))]
        public void Constructor_RequiredParameterNull_ThrowsArgumentNullException(
            ILogger<PaymentSenseCodingChallengeController> logger,
            IMapper mapper,
            ICountryManager countryManager,
            string expectedErrorParameter)
        {
            var action = () =>
            {
                new PaymentSenseCodingChallengeController(logger, mapper, countryManager);
            };

            action.Should().Throw<ArgumentNullException>()
                .Where(x => x.ParamName == expectedErrorParameter);
        }

        public class GetAsyncTest : PaymentsenseCodingChallengeControllerTests
        {
            public static IEnumerable<object[]> LogInformationMessages()
            {
                yield return new object[]
                {
                    LogEventNames.PaymentSenseCodingChallengeController.Get.Invoked
                };

                yield return new object[]
                {
                    LogEventNames.PaymentSenseCodingChallengeController.Get.Complete
                };
            }

            [Theory]
            [MemberData(nameof(LogInformationMessages))]
            public async Task HappyPathInformationLogged(string information)
            {
                var mockCountries = new List<Country>
                {
                    new Country { CallingCodes = "abc", Capital = "CAP1", Name = "NAME1", Region = "REG" },
                    new Country { CallingCodes = "abc2", Capital = "CAP2", Name = "NAME2", Region = "REG2" }

                };

                _countryManager.GetAllCountriesAsync().Returns(mockCountries);

                var response = await _controller.Get("test") as ObjectResult;

                response.StatusCode.Should().Be(StatusCodes.Status200OK);
                response.Value.Should().Be(mockCountries);
                _logger.Received().LogInformation(information);
            }

            [Fact]
            public async Task ManagerThrowsException_LogException()
            {
                _countryManager.GetAllCountriesAsync().Throws(new Exception("Test"));

                var response = await _controller.Get("abc");

                _logger.Received().LogInformation(LogEventNames.PaymentSenseCodingChallengeController.Get.Invoked);
                _logger.DidNotReceive().LogInformation(LogEventNames.PaymentSenseCodingChallengeController.Get.Complete);
                _logger.Received().LogInformation($"{LogEventNames.PaymentSenseCodingChallengeController.Get.Exception} - Test");
            }
        }

    }
}
