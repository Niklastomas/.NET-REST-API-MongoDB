using System.ComponentModel.DataAnnotations;

namespace _NET_REST_API_MongoDB.Dtos
{
    public record CreateItemDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Range(1, 1000)]
        public float Price { get; set; }
    }

}