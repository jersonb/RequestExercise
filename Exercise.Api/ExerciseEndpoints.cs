using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Exercise.Api;

public static class ExerciseEndpoints
{
    public static WebApplication AddExerciseEnpoints(this WebApplication app)
    {
        app.MapGet("/exercise", async Task<Results<NotFound<string>, Ok<IEnumerable<Response>>>> ([FromServices] FakeCache cache) =>
        {
            try
            {
                var result = await cache.ToListAsync();

                var response = result.Select(x => (Response)x);
                return TypedResults.Ok(response);
            }
            catch (Exception ex)
            {
                return TypedResults.NotFound(ex.Message);
            }
        })
       .WithName("Exercises")
       .WithOpenApi();

        app.MapGet("/exercise/{id:guid}", async Task<Results<NotFound<string>, Ok<Response>>> (Guid id, [FromServices] FakeCache cache) =>
        {
            try
            {
                var result = await cache.SingleAsync(x => x.Uuid == id);

                return TypedResults.Ok((Response)result);
            }
            catch (Exception ex)
            {
                return TypedResults.NotFound(ex.Message);
            }
        })
        .WithName("ExerciseById")
        .WithOpenApi();

        app.MapPost("/exercise", async Task<Results<ProblemHttpResult, CreatedAtRoute>> ([FromBody] RequestPost request, [FromServices] FakeCache cache) =>
        {
            try
            {
                await cache.AddAsync(request);
                return TypedResults.CreatedAtRoute("ExerciseById", new { id = request.Uuid });
            }
            catch (Exception ex)
            {
                return TypedResults.Problem(ex.Message);
            }
        })
        .WithName("ExercisePost")
        .WithOpenApi();

        app.MapPut("/exercise/{id:guid}", async Task<Results<ProblemHttpResult, AcceptedAtRoute>> (Guid id,[FromBody] RequestPut request, [FromServices] FakeCache cache) =>
        {
            try
            {
                request.Uuid = id;
                await cache.UpdateAsync(request);
                return TypedResults.AcceptedAtRoute("ExerciseById", new { id = request.Uuid });
            }
            catch (Exception ex)
            {
                return TypedResults.Problem(ex.Message);
            }
        })
        .WithName("ExercisePut")
        .WithOpenApi();

        app.MapDelete("/exercise{id:guid}", async Task<Results<ProblemHttpResult, NoContent>> (Guid id, [FromBody] RequestDelete request, [FromServices] FakeCache cache) =>
        {
            try
            {
                request.Uuid = id;
                await cache.DeleteAsync(request);
                return TypedResults.NoContent();
            }
            catch (Exception ex)
            {
                return TypedResults.Problem(ex.Message);
            }
        })
        .WithName("ExerciseDelete")
        .WithOpenApi();

        return app;
    }
}