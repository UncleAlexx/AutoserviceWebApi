﻿global using Autoservice.Domain.Interfaces;
global using MediatR;
global using Autoservice.Domain.Bases;
global using Autoservice.Domain.Results.Common;
global using Autoservice.Domain.Results.ResultKinds;
global using FluentValidation;
global using System.Text.RegularExpressions;
global using CarEntity = Autoservice.Domain.Entities.Car;
global using Autoservice.Application.Interfaces;
global using ProviderEntity = Autoservice.Domain.Entities.Provider;
global using PartEntity = Autoservice.Domain.Entities.Part;
global using ClientEntity = Autoservice.Domain.Entities.Client;
global using EmployeeEntity = Autoservice.Domain.Entities.Employee;
global using ProductEntity = Autoservice.Domain.Bases.ProductBase;
global using ContragentEntity = Autoservice.Domain.Bases.ContragentBase;
global using Autoservice.Application.Car.Queries;
global using Autoservice.Application.Extensions;
global using Autoservice.Domain.Exceptions;
global using Autoservice.Application.Bases;
global using System.Runtime.CompilerServices;
global using System.Collections.ObjectModel;
global using System.ComponentModel.DataAnnotations;
global using System.Reflection;
global using System.ComponentModel.DataAnnotations.Schema;
global using Autoservice.Application.Abtractions.Requests;
global using Autoservice.Application.Abtractions.Handlers;
global using static Autoservice.Application.ValidationConstants.Constants;
global using static Autoservice.Application.ValidationMessagesFactory;
global using Autoservice.Infrastructure;
global using Autoservice.Domain.Entities;
global using Autoservice.Application.Validators.RemoveValidators;
global using ValidationException = FluentValidation.ValidationException;
global using Autoservice.Application.Helpers;
global using Autoservice.Application.ContragentBase.Commands.SetEmployee;
global using Autoservice.Application.Common.Commands.Add;
global using Autoservice.Application.Common.Commands.Clear;
global using Autoservice.Application.Common.Commands.Remove;
global using Autoservice.Application.Common.Commands.Update;
global using Autoservice.Application.Common.Queries.GetAll;
global using Autoservice.Application.Common.Queries.GetAllByIds;
global using Autoservice.Application.Common.Queries.GetById;
global using Autoservice.Application.ContragentBase.Queries.GetCars;
global using Autoservice.Application.ContragentBase.Queries.GetParts;
global using Autoservice.Application.ContragentBase.Queries.GetRevenue;
global using Autoservice.Application.ProductBase.Commands.SetClient;
global using Autoservice.Application.ProductBase.Commands.SetProvider;
global using Autoservice.Application.ProductBase.Queries.GetClient;
global using Autoservice.Application.ProductBase.Queries.GetProvider;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.Extensions.DependencyInjection;
global using Autoservice.Infrastructure.Repositories;
global using Autoservice.Application.Validators.ClearValidators;
global using Autoservice.Application.Validators.ProviderIdUniqunessValidators;
global using Autoservice.Application.Validators.PropertiesValidators;
global using Autoservice.Application.Validators.UpdateValidators;
global using Autoservice.Application.Validators;
global using Autoservice.Application.Validators.AdditionValidators;