using Bogus;
using Microsoft.AspNetCore.Mvc;
using SlidoSapm.Http.Clients.Slido;
using SlidoSapm.Http.ServiceExtensions;
using SlidoSpam.Api.V1;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSlidoHttp();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapGet("api/Events/{eventGuid}",
    async (string eventGuid, SlidoClient slidoClient, CancellationToken cancellationToken) =>
    {
        var auth = await slidoClient.Authenticate(eventGuid);
        return await slidoClient.GetEvent(auth, eventGuid, cancellationToken);
    });

app.MapPost("api/Events/{eventGuid}/SpamQuestions",
    async (string eventGuid, [FromBody] SpamQuestionsParameters spamQuestionsParameters, SlidoClient slidoClient,
        CancellationToken cancellationToken) =>
    {
        var faker = new Faker();
        for (var count = 0; count < spamQuestionsParameters.QuestionCount; count++)
        {
            var question = new QuestionPayload
            {
                Text = faker.Rant.Review(),
                EventId = spamQuestionsParameters.EventId,
                IsAnonymous = spamQuestionsParameters.PostAnonymously,
                EventSectionId = spamQuestionsParameters.EventSectionId
            };

            var auth = await slidoClient.Authenticate(eventGuid);
            var createQuestionTask = slidoClient.CreateQuestion(auth, eventGuid, question, cancellationToken);

            if (spamQuestionsParameters.PostAnonymously)
            {
                await createQuestionTask;
                continue;
            }

            var userPayload = new UserPayload
            {
                Name = faker.Name.FullName()
            };
            
            var updateUsernameTask = slidoClient.UpdateUsername(auth, eventGuid, userPayload, cancellationToken);

            await Task.WhenAll(createQuestionTask, updateUsernameTask);
        }
    });

app.MapControllers();

app.Run();