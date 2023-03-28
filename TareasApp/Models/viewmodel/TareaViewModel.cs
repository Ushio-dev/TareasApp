using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TareasApp.Models.viewmodel
{
	public class TareaViewModel
	{
		[Required]
		[Display(Name = "Nombre")]
		public string TareaName { get; set; }
	}
}
