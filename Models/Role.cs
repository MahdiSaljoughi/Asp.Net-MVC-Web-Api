using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MvcApi.Models;

public class Role
{
    [Key] public int Id { get; set; }

    [Required, StringLength(30)] public string Name { get; set; }

    // [BindNever]
    // public virtual ICollection<User> Users { get; set; }
}