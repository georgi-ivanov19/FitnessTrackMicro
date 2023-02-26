using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using FitnessTrackMicro;
using Radzen;
using Blazored.LocalStorage;
using FitnessTrackMicro.Services.ExerciseService;
using FitnessTrackMicro.Services.ExerciseSetService;
using FitnessTrackMicro.Services.MealService;
using FitnessTrackMicro.Services.MeasurementsService;
using FitnessTrackMicro.Services.TrackedWorkoutService;
using FitnessTrackMicro.Services.WorkoutService;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddMsalAuthentication(options =>
{
    builder.Configuration.Bind("AzureAdB2C", options.ProviderOptions.Authentication);
    options.ProviderOptions.DefaultAccessTokenScopes.Add("openid");
    options.ProviderOptions.DefaultAccessTokenScopes.Add("offline_access");
});
builder.Services.AddScoped<IMeasurementsService, MeasurementsService>();
builder.Services.AddScoped<IMealService, MealService>();
builder.Services.AddScoped<IWorkoutService, WorkoutService>();
builder.Services.AddScoped<ITrackedWorkoutService, TrackedWorkoutService>();
builder.Services.AddScoped<IExerciseService, ExerciseService>();
builder.Services.AddScoped<IExerciseSetService, ExerciseSetService>();
builder.Services.AddScoped<DialogService>();
builder.Services.AddBlazoredLocalStorage();
await builder.Build().RunAsync();
