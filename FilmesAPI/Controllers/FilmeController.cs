using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.DTOs;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

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

    /// <summary>
    /// Adiciona um filme ao banco de dados
    /// </summary>
    /// <param name="filmeDto">Objeto com os campos necessários para criação de um filme</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso a inserção seja feita com sucesso</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult AdicionaFilme([FromBody] FilmeDto filmeDto)
    {
        Filme filme = _mapper.Map<Filme>(filmeDto);

        _context.Filmes.Add(filme);
        _context.SaveChanges();
        return CreatedAtAction(nameof(RecuperaFilmePorId), new { id = filme.Id }, filme);
    }

    /// <summary>
    /// Consulta todos os filmes do banco de dados
    /// </summary>
    /// <param name="ReadFilmeDto">Nenhum</param>
    /// <returns>Lista de filmes</returns>
    /// <response code="200">Caso a consulta seja feita com sucesso</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IEnumerable<ReadFilmeDto> RecuperaFilmes([FromQuery] int skip = 0, [FromQuery] int take = 10)
    {
        return _mapper.Map<List<ReadFilmeDto>>(_context.Filmes.Skip(skip).Take(take));
    }
    /// <summary>
    /// Consulta um filme do banco de dados
    /// </summary>
    /// <param name="ReadFilmeDto">Id do filme</param>
    /// <returns>Dados do filme</returns>
    /// <response code="200">Caso a consulta seja feita com sucesso</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult RecuperaFilmePorId(int Id)
    {
        var filme = _context.Filmes.FirstOrDefault(filmes => filmes.Id == Id);

        if (filme == null) return NotFound();

        var filmeDto = _mapper.Map<ReadFilmeDto>(filme);
        return Ok(filmeDto);
    }

    /// <summary>
    /// Atualiza um filme do banco de dados
    /// </summary>
    /// <param name="filmeDto">Id do filme e dados do filme atualizado</param>
    /// <returns>Nada</returns>
    /// <response code="204">Caso a atualização seja feita com sucesso</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult AtualizaFilmePorId(int Id, [FromBody]FilmeDto novoFilmeDto)
    {
        var filme = _context.Filmes.FirstOrDefault(filmes => filmes.Id == Id);
        if (filme == null) return NotFound();

        _mapper.Map(novoFilmeDto, filme);
        _context.SaveChanges();

        return NoContent();
    }

    /// <summary>
    /// Atualiza parcialmente um filme do banco de dados
    /// </summary>
    /// <param name="filmeDto">Id do filme e dados do filme que serão atualizados</param>
    /// <returns>Nada</returns>
    /// <response code="204">Caso a atualização seja feita com sucesso</response>
    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult AtualizaFilmePorIdParcial(int Id, JsonPatchDocument<FilmeDto> patch)
    {
        var filme = _context.Filmes.FirstOrDefault(filmes => filmes.Id == Id);
        if (filme == null) return NotFound();

        //converto o filme para filmeDto
        var filmeParaAtualizar = _mapper.Map<FilmeDto>(filme);
        //aplica as mudanças
        patch.ApplyTo(filmeParaAtualizar, ModelState);

        //testa se as informações alteradas são validas pelas DataAnotations do Dto
        if (!TryValidateModel(ModelState))
        {
            return ValidationProblem(ModelState);
        }

        _mapper.Map(filmeParaAtualizar, filme);
        _context.SaveChanges();
        return NoContent();
    }

    /// <summary>
    /// Delte um filme do banco de dados
    /// </summary>
    /// <param name="filmeDto">Id do filme</param>
    /// <returns>Nada</returns>
    /// <response code="204">Caso a remoção seja feita com sucesso</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult DeletaFilmePorId(int Id)
    {
        var filme = _context.Filmes.FirstOrDefault(filmes => filmes.Id == Id);
        if (filme == null) return NotFound();

        _context.Filmes.Remove(filme);
        _context.SaveChanges();
        return NoContent();
    }
}
