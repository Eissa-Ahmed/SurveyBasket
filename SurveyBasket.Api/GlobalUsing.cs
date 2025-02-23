﻿global using FluentValidation;
global using FluentValidation.AspNetCore;
global using Mapster;
global using MapsterMapper;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Microsoft.Extensions.Options;
global using Microsoft.IdentityModel.Tokens;
global using Scalar.AspNetCore;
global using SurveyBasket.Api;
global using SurveyBasket.Api.Context;
global using SurveyBasket.Api.Contracts.Authentication;
global using SurveyBasket.Api.Contracts.Poll.Requests;
global using SurveyBasket.Api.Contracts.Poll.Responses;
global using SurveyBasket.Api.Entities;
global using SurveyBasket.Api.Errors;
global using SurveyBasket.Api.Models;
global using SurveyBasket.Api.Provider;
global using SurveyBasket.Api.ResponseManager;
global using SurveyBasket.Api.Services;
global using System.IdentityModel.Tokens.Jwt;
global using System.Reflection;
global using System.Security.Claims;
global using System.Security.Cryptography;
global using System.Text;
