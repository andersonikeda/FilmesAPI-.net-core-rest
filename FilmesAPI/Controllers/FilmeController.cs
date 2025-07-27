using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace FilmesAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class FilmeController : ControllerBase
{

    private FilmeContext _context;
    private IMapper _mapper;

    public FilmeController(FilmeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    //Adiciona novo registro a lista estatica
    [HttpPost]
    public ActionResult AdicionaFile([FromBody] CreateFilmeDto filmeDto)
    {
        Filme filme = _mapper.Map<Filme>(filmeDto);

        _context.Filmes.Add(filme);
        _context.SaveChanges();

        //após criarem, execute a consulta por ID para retornar com o id
        return CreatedAtAction(nameof(RecuperaFilmePorId), new { id = filme.ID }, filme);
    }

    //Traz toda a lista
    [HttpGet]
    public IEnumerable<Filme> RecuperaFilmes()
    {
        return _context.Filmes;
    }
    
    //Traz um registro em especifico
    [HttpGet("Id")]
    public ActionResult<Filme> RecuperaFilmePorId(int id)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.ID == id);

        if (filme == null) return NotFound();

        return filme;
    }

    //Chamada por paginação, retornando 5 por consulta
    [HttpGet("paginado")]
    public ActionResult<List<Filme>> RecuperaFilmesPaginacao([FromQuery] int pagina)
    {
        int skip;

        if (pagina <= 0)
        {
            return BadRequest("O parametro pagina não pode ser menor ou igual a 0");
        }

        pagina = pagina - 1;

        skip = pagina * 5;

        return _context.Filmes.Skip(skip).Take(5).ToList();
    }

    //Atualiza um registro passando o id
    [HttpPut("{id}")]
    public IActionResult AtualizaFilme(int id, [FromBody] UpdateFilmeDto filmeDto)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.ID == id);
        if (filme == null) return NotFound();

        _mapper.Map(filmeDto, filme);
        _context.SaveChanges();
        return NoContent();

    }

    //Atualizando em partes
    [HttpPatch("{id}")]
    public IActionResult AtualizaFilmeParcial(int id, [FromBody] JsonPatchDocument<UpdateFilmeDto> patch)
    {

        var filme = _context.Filmes.FirstOrDefault(filme => filme.ID == id);
        if (filme == null) return NotFound();

        var filmeParaAtualizar = _mapper.Map<UpdateFilmeDto>(filme);

        patch.ApplyTo(filmeParaAtualizar, ModelState);

        if (!TryValidateModel(filmeParaAtualizar))
        {
            return ValidationProblem(ModelState);
        }

        _mapper.Map(filmeParaAtualizar, filme);
        _context.SaveChanges();
        return NoContent();

    }

    //Excluindo
    [HttpDelete("{id}")]
    public IActionResult DeletaFilme(int id)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.ID == id);
        if (filme == null) return NotFound();

        _context.Remove(filme);
        _context.SaveChanges();
        return NoContent();
    }

}
