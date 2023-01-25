using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Paymentsense.Coding.Challenge.Api.Constants;
using PaymentSense.BusinessLayer;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Paymentsense.Coding.Challenge.Api.Controllers
{
    [ApiController]
    [Route("api/PaymentSense")]
    public class PaymentSenseCodingChallengeController : ControllerBase
    {
        private readonly ILogger<PaymentSenseCodingChallengeController> _logger;
        private readonly IMapper _mapper;
        private readonly ICountryManager _countryManager;

        public PaymentSenseCodingChallengeController(
            ILogger<PaymentSenseCodingChallengeController> logger,
            IMapper mapper,
            ICountryManager countryManager)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _countryManager = countryManager ?? throw new ArgumentNullException(nameof(countryManager));
        }


        [HttpGet]
        public async Task<IActionResult> Get(string callingCode)
        {
            try
            {

                _logger.LogInformation(LogEventNames.PaymentSenseCodingChallengeController.Get.Invoked);

                var allCounties = await _countryManager.GetAllCountriesAsync();

                _logger.LogInformation(LogEventNames.PaymentSenseCodingChallengeController.Get.Complete);

                return Ok(allCounties);
            }
            catch (HttpRequestException e)
            {
                _logger.LogInformation(LogEventNames.PaymentSenseCodingChallengeController.Get.HttpRequestException);
                return StatusCode((int)562);
            }
            catch (Exception e)
            {
                _logger.LogInformation($"{LogEventNames.PaymentSenseCodingChallengeController.Get.Exception} - {e.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

        }
    }
}
