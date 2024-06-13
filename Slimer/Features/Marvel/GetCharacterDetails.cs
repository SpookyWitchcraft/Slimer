using FluentValidation;
using Slimer.Common.Interfaces;
using Slimer.Features.Marvel.Contracts;
using Slimer.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace Slimer.Features.Marvel
{
    public static class GetCharacterDetails
    {
        public record Request(string CharacterName);
        public record Response(int Id, string Description, string Name);

        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(x => x.CharacterName).NotNull().NotEmpty().Length(1, 255);
            }
        }

        public class Endpoint : IEndpoint
        {
            private static string GetUrl(string name, string ts, string key, string hash) => $"https://gateway.marvel.com/v1/public/characters?name={name}&ts={ts}&apikey={key}&hash={hash}";

            public void MapEndpoint(IEndpointRouteBuilder app)
            {
                app.MapGet("api/marvel/{characterName}", Handler);
            }

            public static async Task<IResult> Handler(
                string characterName,
                IValidator<Request> validator,
                IHttpClientService clientService,
                ISecretsService secretsService)
            {
                var validationResult = await validator.ValidateAsync(new Request(characterName));

                if (!validationResult.IsValid)
                    throw new BadHttpRequestException(validationResult.ToString());

                var timeStamp = Guid.NewGuid().ToString();
                Task<string>[] keys = [secretsService.GetValueAsync("MarvelPublicKey"), secretsService.GetValueAsync("MarvelPrivateKey")];

                var publicKey = await keys[0];
                var privateKey = await keys[1];

                var url = GetUrl(characterName, timeStamp, publicKey, CreateHash(timeStamp, privateKey, publicKey));

                var response = (await clientService.GetAsync<MarvelCharacterResponse>(url)).Data.Results.FirstOrDefault();

                return response == null
                    ? Results.NotFound()
                    : Results.Ok(new Response(response.Id, response.Description, response.Name));
            }

            private static string CreateHash(string timeStamp, string privateKey, string publicKey)
            {
                using var md5 = MD5.Create();
                var input = Encoding.ASCII.GetBytes(timeStamp + privateKey + publicKey);
                var hash = md5.ComputeHash(input);

                return Convert.ToHexString(hash).ToLower();
            }
        }
    }
}
