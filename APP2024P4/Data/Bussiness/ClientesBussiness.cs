﻿using APP2024P4.Data.Dtos;
using APP2024P4.Data.Entities;
using APP2024P4.Data.Repositories;
using Azure.Core;

namespace APP2024P4.Data.Bussiness;
public interface IClientesBussiness
{
    Task<ClienteDto> Buscar(int Id);
    Task<List<ClienteDto>> Consultar(string filtro);
    Task<ClienteDto> Crear(ClienteRequestDto request);
    Task<bool> Eliminar(int Id);
    Task<ClienteDto> Update(ClienteRequestDto request);
}
public class ClientesBussiness(IClientesRepository repository) : IClientesBussiness
{
    public async Task<ClienteDto> Crear(ClienteRequestDto request)
     => await repository.Crear(request).ConfigureAwait(false);

    public async Task<ClienteDto> Update(ClienteRequestDto request)
     => await repository.Update(request).ConfigureAwait(false);

    public async Task<List<ClienteDto>> Consultar(string filtro)
     => await repository.Consultar(filtro).ConfigureAwait(false);

    public async Task<ClienteDto> Buscar(int Id)
     => await repository.Buscar(Id).ConfigureAwait(false);
    public async Task<bool> Eliminar(int Id)
     => await repository.Eliminar(Id).ConfigureAwait(false);
}
