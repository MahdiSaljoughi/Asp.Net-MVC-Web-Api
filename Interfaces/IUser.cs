using Microsoft.AspNetCore.Mvc;
using MvcApi.Models;

namespace MvcApi.Interfaces;

public interface IUser
{
    Task<IActionResult> Create(User user);
    
    IActionResult GetAll();
    
    Task<IActionResult> GetOne(Guid id);
    
    Task<IActionResult> Update(Guid id, User user);
    
    Task<IActionResult> Delete(Guid id);
}