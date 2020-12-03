using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using sisapWebApi.Context.Interfaces;
using sisapWebApi.DTO;
using sisapWebApi.Models.SQLServer;
using sisapWebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace sisapWebApi.Controllers
{
    [Route("api/refeitorio")]
    [ApiController]
    public class RefeitorioController : ControllerBase
    {
        private readonly IRefeitorioRepo _repository;
        private readonly IMapper _mapper;

        public RefeitorioController(IRefeitorioRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<RefeitorioDTO>> GetAllApontamentos()
        {
            var apontamentoRefeitorio = _repository.GetAllApontamentos();
            return Ok(_mapper.Map<IEnumerable<RefeitorioDTO>>(apontamentoRefeitorio));
        }

    }
}
