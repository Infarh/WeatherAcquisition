using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeatherAcquisition.Interfaces.Base.Entities;
using WeatherAcquisition.Interfaces.Base.Repositories;

namespace WeatherAcquisition.API.Controllers.Base
{
    [ApiController, Route("api/[controller]")]
    public abstract class MappedEntityController<T, TBase> : ControllerBase 
        where T : IEntity
        where TBase : IEntity
    {
        private readonly IRepository<TBase> _Repository;
        private readonly IMapper _Mapper;

        protected MappedEntityController(IRepository<TBase> Repository, IMapper Mapper)
        {
            _Repository = Repository;
            _Mapper = Mapper;
        }

        protected virtual TBase GetBase(T item) => _Mapper.Map<TBase>(item);
        protected virtual T GetItem(TBase item) => _Mapper.Map<T>(item);
        protected virtual IEnumerable<TBase> GetBase(IEnumerable<T> items) => _Mapper.Map<IEnumerable<TBase>>(items);
        protected virtual IEnumerable<T> GetItem(IEnumerable<TBase> item) => _Mapper.Map<IEnumerable<T>>(item);

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        public async Task<IActionResult> GetItemsCount() => Ok(await _Repository.GetCount());

        [HttpGet("exist/id/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(bool))]
        public async Task<IActionResult> ExistId(int id) => await _Repository.ExistId(id) ? Ok(true) : NotFound(false);

        [HttpPost("exist")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(bool))]
        public async Task<IActionResult> Exist(T item) => await _Repository.Exist(GetBase(item)) ? Ok(true) : NotFound(false);

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll() => Ok(GetItem(await _Repository.GetAll()));

        [HttpGet("items[[{Skip:int}:{Count:int}]]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<T>>> Get(int Skip, int Count) =>
            Ok(GetItem(await _Repository.Get(Skip, Count)));

        protected record Page(IEnumerable<T> Items, int TotalCount, int PageIndex, int PageSize) : IPage<T>
        {
            public int TotalPagesCount => (int)Math.Ceiling((double)TotalCount / PageSize);
        }

        protected IPage<T> GetItems(IPage<TBase> page) => new Page(GetItem(page.Items), page.TotalCount, page.PageIndex, page.PageSize);

        [HttpGet("page/{PageIndex:int}/{PageSize:int}")]
        [HttpGet("page[[{PageIndex:int}/{PageSize:int}]]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IPage<T>>> GetPage(int PageIndex, int PageSize)
        {
            var result = await _Repository.GetPage(PageIndex, PageSize);
            return result.Items.Any()
                ? Ok(GetItems(result))
                : NotFound();
        }

        [HttpGet("{id:int}")]
        [ActionName("Get")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id) =>
            await _Repository.GetById(id) is { } item
                ? Ok(item)
                : NotFound();

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Add(T item)
        {
            var result = await _Repository.Add(GetBase(item));
            return CreatedAtAction(nameof(Get), new { id = result.Id }, GetItem(result));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(T item)
        {
            if (await _Repository.Update(GetBase(item)) is not { } result)
                return NotFound(item);
            return AcceptedAtAction(nameof(Get), new { id = result.Id }, GetItem(result));
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(T item)
        {
            if (await _Repository.Delete(GetBase(item)) is not { } result)
                return NotFound(item);
            return Ok(GetItem(result));
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteById(int id)
        {
            if (await _Repository.DeleteById(id) is not { } result)
                return NotFound(id);
            return Ok(GetItem(result));
        }
    }
}
