using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MinimalAPI_Udemy;
using MinimalAPI_Udemy.Data;
using MinimalAPI_Udemy.Models;
using MinimalAPI_Udemy.Models.DTOs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingConfig));
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/api/coupon", () =>
{
    var apiResponse = new APIResponse();

    apiResponse.Results = CouponStore.Coupons;
    apiResponse.IsSuccess = true;
    apiResponse.StatusCode = System.Net.HttpStatusCode.OK;

    return Results.Ok(apiResponse);
}).WithName("GetCoupons").Produces<Coupon>(200).Produces(400);

app.MapGet("/api/coupon/{id:int}", (int id) =>
{
    var apiResponse = new APIResponse();

    apiResponse.Results = CouponStore.Coupons.FirstOrDefault(x => x.Id == id);
    apiResponse.IsSuccess = true;
    apiResponse.StatusCode = System.Net.HttpStatusCode.OK;

    return Results.Ok(apiResponse);
}).WithName("GetCoupon").Produces<Coupon>(200).Produces(400);

app.MapPost("/api/coupon", async (IMapper _mapper, IValidator<CouponCreateDTO> _validator,[FromBody] CouponCreateDTO couponCreateDTO) =>
{

    var validationResult = await _validator.ValidateAsync(couponCreateDTO);
    var apiResponse = new APIResponse
    {
        IsSuccess = false,
        StatusCode = System.Net.HttpStatusCode.BadRequest
    };

    
    if (!validationResult.IsValid)
    {
        apiResponse.ErrorMessages.Add(validationResult.Errors.First().ToString());
        return Results.BadRequest(apiResponse);
    }

    if (CouponStore.Coupons.FirstOrDefault(b => b.Name.ToLower() == couponCreateDTO.Name.ToLower()) != null)
    {
        apiResponse.ErrorMessages.Add("coupon already exist!!");
        Results.BadRequest(apiResponse);
    }

    var coupnon = _mapper.Map<Coupon>(couponCreateDTO);

    coupnon.Id = CouponStore.Coupons.OrderByDescending(x => x.Id).FirstOrDefault().Id + 1;

    CouponStore.Coupons.Add(coupnon);

    var couponDto = _mapper.Map<CouponDTO>(coupnon);

    apiResponse.IsSuccess = true;
    apiResponse.StatusCode = System.Net.HttpStatusCode.Created;
    apiResponse.Results = couponDto;
    return Results.Ok(apiResponse);

}).WithName("CreateCoupon").Accepts<CouponCreateDTO>("application/json").Produces<CouponDTO>(400);

//app.MapPut("/api/coupon", () =>
//{

//});

//app.MapDelete("/api/coupon", () =>
//{

//});

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
