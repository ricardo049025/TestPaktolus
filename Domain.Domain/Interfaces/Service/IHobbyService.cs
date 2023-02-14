using System;
using Domain.Domain.Dtos;
using Domain.Domain.Models;

namespace Domain.Domain.Interfaces.Service
{
	public interface IHobbyService : IBaseService<Hobby>
	{
        HobbyResponseDto AddNewHobby(HobbyRequestDto request);

    }
}

