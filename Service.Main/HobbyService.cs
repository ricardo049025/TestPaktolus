using System;
using Domain.Domain.Dtos;
using Domain.Domain.Interfaces;
using Domain.Domain.Interfaces.Service;
using Domain.Domain.Models;

namespace Service.Main
{
	public class HobbyService : BaseService<Hobby>, IHobbyService
	{
		protected IHobbyRepository hobbyRepository;

		public HobbyService(IHobbyRepository hobbyRepository):base(hobbyRepository)
		{
			this.hobbyRepository = hobbyRepository;
		}

		public HobbyResponseDto AddNewHobby(HobbyRequestDto request)
		{
			if(this.hobbyRepository.getAll().Count(x=> x.Name.ToUpper() == request.name.ToUpper()) != 0)
				throw new Exception("Already exists a hobby with the name: " + request.name);

			Hobby hobby = new Hobby();
			hobby.Name = request.name;
			hobby.Active = true;

			this.hobbyRepository.Add(hobby);

			return new HobbyResponseDto { id = hobby.Id, name = hobby.Name, success = true, xerror = "" };
        }
    }
}

