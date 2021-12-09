using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YotorAspNetCoreApiResources.Contracts;

namespace YotorAspNetCoreApiResources.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatabaseController : ControllerBase
    {
        private readonly IDatabaseRepository _databaseRepository;
        private readonly IHelpRepository _helpRepository;
        private int UserId => int.Parse(User.Claims.Single(c => c.Type == "user_id").Value);
        public DatabaseController(IDatabaseRepository databaseRepository, IHelpRepository helpRepository)
        {
            _databaseRepository = databaseRepository;
            _helpRepository = helpRepository;
        }

        [HttpGet]
        

        public async Task<IActionResult> CreateBackupAsync()
        {
            try
            {
                Random rnd = new Random();
                int value = rnd.Next(0, 100000);

                string path = $"C:\\Program Files\\Microsoft SQL Server\\MSSQL14.SQLEXPRESS\\MSSQL\\Backup\\YotorDb{value}.bak";
                //bool isAdmin = await _helpRepository.IsAdminAsync(UserId);
                //if(isAdmin == true)
                //{
                    await _databaseRepository.CreateBackupAsync(path);
                    await _databaseRepository.InsertBackupToDbAsync(path);
                    return Ok("Ok");
                //}
                return Unauthorized("Недостаточно прав");
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("RestoreByLastBackup")]
        public async Task<IActionResult> RestoreDatabaseByLastBackupAsync()
        {
            try
            {
                var lastBackup = await _databaseRepository.GetLastBackupAsync();
                if (lastBackup != null)
                {
                    await _databaseRepository.RestoreDatabaseBySomeBackupAsync(lastBackup);
                    return Ok("Ok");
                }
                return NotFound("Что-то пошло не так"); 
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> RestoreDatabaseBySomeBackupAsync(int id)
        {
            try
            {
                var backup = await _databaseRepository.GetBackupByIdAsync(id);
                if(backup!= null)
                {
                    await _databaseRepository.RestoreDatabaseBySomeBackupAsync(backup);
                    return Ok("Ok");
                }
                return NotFound("Что-то пошло не так");
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


    }
}
