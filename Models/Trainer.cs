using FitnessSimulation.Models.Common;

namespace FitnessSimulation.Models
{
    public class Trainer:BaseEntity
    {   
        public string FullName { get; set; }=string.Empty;
        public int Experience { get; set; }

        public string ImagePath { get; set; } = string.Empty;

        public Category Category { get; set; } = null!;

        public int CategoryId { get; set; } 
    }
}
