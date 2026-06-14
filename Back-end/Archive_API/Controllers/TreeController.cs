using Application.Services;
using Archive_API.Contracts.ChapterContracts;
using Archive_API.Contracts.FileContracts;
using Archive_API.Contracts.PersonContracts;
using Archive_API.Contracts.TreeContracts;
using Archive_API.Contracts.UserContracts;
using Domain.Classes;
using Microsoft.AspNetCore.Mvc;

namespace Archive_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TreeController : ControllerBase
    {
        private readonly IFamilyTreeService _treeService;
        public TreeController(IFamilyTreeService familyTreeService)
        {
            _treeService = familyTreeService;
        }

        [HttpGet]
        public async Task<ActionResult<List<TreeShortResponse>>> GetAllTrees()
        {
            var trees = await _treeService.GetAllTrees();
            var response = trees.Select(t => new TreeShortResponse(t.Id, t.Title, t.MainUserId)).ToList();
            return Ok(response);
        }

        [HttpGet("by-id")]
        public async Task<ActionResult<TreeFullResponse>> GetTreeById(Guid id)
        {
            var tree = await _treeService.GetTreeById(id);
            if (tree == null) { return BadRequest("Семейного архива с данным id не существует"); }

            var users = tree.Users.Select(u => new UserResponse(u.Id, u.Login, u.Password, u.Email)).ToList();
            var persons = tree.Persons.Select(p =>
            {
                var chapters = p.Chapters.Select(c =>
                {
                    var files = c.Files.Select(f => new FileResponse(f.Id, f.Title, f.ResourceUrl)).ToList();
                    var chapter = new ChapterResponse(c.Id, c.SerialNumber, c.Title, c.Description, c.StartDate, c.EndDate, files);
                    return chapter;
                }).ToList();
                var person = new PersonFullResponse(p.Id, p.FirstName, p.LastName, p.Surname, p.ShortBiography, p.Birthday, p.DayOfDeath, chapters);
                return person;
            }).ToList();

            var response = new TreeFullResponse(tree.Id, tree.Title, tree.MainUserId, users, persons);

            return Ok(response);
        }

        [HttpGet("by-user-id")]
        public async Task<ActionResult<List<TreeShortResponse>>> GerTreesByUserId(Guid id)
        {
            var trees = await _treeService.GetTreesByUserId(id);
            var response = trees.Select(t => new TreeShortResponse(t.Id, t.Title, t.MainUserId)).ToList();
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateNewTree([FromBody] TreeRequest request)
        {
            var (tree, error) = FamilyTree.Create(Guid.NewGuid(), request.title, request.mainUserId);
            if (error != string.Empty) { return BadRequest(error); }

            var resp = await _treeService.CreateNewTree(tree);
            await _treeService.AddUserToTreeUserList(resp, request.mainUserId);

            return Ok(resp);
        }

        [HttpPut]
        public async Task<ActionResult<Guid>> UpdateTitle(Guid id, string title)
        {
            Guid resp = await _treeService.UpdateTreeTitle(id, title);
            if (resp == Guid.Empty) { return BadRequest("Дерева с данным id не существует"); }
            return Ok(resp);
        }

        [HttpPost("add-user")]
        public async Task<ActionResult<Guid>> AddUserToTree(Guid treeId, Guid userId)
        {
            Guid id = await _treeService.AddUserToTreeUserList(treeId, userId);
            if (id == Guid.Empty) { return BadRequest("Пользователя или архива с данным id не существует"); }
            return Ok(id);
        }

        [HttpDelete]
        public async Task<ActionResult<Guid>> DeleteTree(Guid id)
        {
            return Ok(await _treeService.DeleteTree(id));
        }
    }
}
