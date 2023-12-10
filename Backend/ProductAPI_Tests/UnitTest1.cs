using System.Diagnostics;
using System.Runtime.InteropServices;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Controllers;
using ProductAPI.DTOs;
using ProductAPI.DTOs.Mappings;
using ProductAPI.Services;
using ProductAPI.Services.impl;

namespace ProductAPI_Tests;

public class UnitTest1
{
    private readonly CategoriesController controller;

    public UnitTest1()
    {
        var mapper = new MapperConfiguration(opt => {
            opt.AddProfile<MappingProfile>();
        });

        ICategoryService service = new CategoryService(mapper.CreateMapper(), new CategoryRepository());
    
        controller = new CategoriesController(service);
    }

    [Fact]
    public async void GetCategory()
    {
        var actionResult = await controller.GetById(1);

        if (actionResult.Result is OkObjectResult obj && obj.Value is CategoryDTO dto)
        {
            dto.Name.Should().Be("ELETRONICOS", because: "The category name is eletronicos");
        }
        else 
        {
            Assert.Fail("No Result");
        }
    }

    [Fact]
    public async void GetAll() 
    {
        var actionResult = await controller.Get();

        if (actionResult.Result is OkObjectResult obj && obj.Value is IEnumerable<CategoryDTO?> categories)
        {
            categories.Count().Should().BeGreaterThan(0, because: "The categories is not empty.");
        }
        else 
        {
            Assert.Fail("No result");
        }
    }
}